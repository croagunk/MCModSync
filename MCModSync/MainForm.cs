using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using YamlDotNet.RepresentationModel;

namespace MCModSync {
    public partial class MainForm : Form {
        private Config config = null;
        private List<Profile> profiles = null;
        private int selectedProfileIndex = -1;
        private Dictionary<string, string> localMods = null;
        private bool updating = false;

        public MainForm() {
            InitializeComponent();

            config = Config.Load();

            // profiles
            profiles = GetMinecraftProfiles();
            var i = profiles.FindIndex(p => p.ProfileId == config.MinecraftProfile);
            if (i == -1) i = profiles.FindIndex(p => p.ProfileId == "forge");
            if (i == -1) i = profiles.FindIndex(p => p.ProfileName == "latest-release");
            MinecraftProfileList.DataSource = profiles;
            MinecraftProfileList.DisplayMember = "ProfileName";
            MinecraftProfileList.ValueMember = "ProfileId";
            selectedProfileIndex = i;
        }

        private void SaveConfig() {
            config.MinecraftProfile = profiles[MinecraftProfileList.SelectedIndex].ProfileId;
            if (LocalModList.Items.Count > 0) {
                config.ExcludedMods.Clear();
                if (LocalModList.CheckedItems.Count > 0) {
                    foreach (var mod in LocalModList.CheckedItems.Cast<string>()) {
                        config.ExcludedMods.Add(mod);
                    }
                }
            }
            config.Save();
        }

        private List<Profile> GetMinecraftProfiles() {
            var r = new List<Profile>();

            var basedir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft";
            var path = basedir + @"\launcher_profiles.json";
            var encoding = Encoding.UTF8;
            var json = File.ReadAllText(path, encoding);

            var profiles = JObject.Parse(json)["profiles"].ToObject<Dictionary<string, JObject>>();
            foreach (var profile in profiles) {
                var type = (string)(JValue)profile.Value["type"];
                var name = (string)(JValue)profile.Value["name"];

                var profileId = profile.Key;
                var profileName = name ?? type;
                var gameDir = ((string)(JValue)profile.Value["gameDir"] ?? basedir).Replace(@"/", @"\");

                r.Add(new Profile(profileId, profileName, gameDir));
            }

            return r;
        }

        private Dictionary<string, string> GetLocalMods() {
            var r = new Dictionary<string, string>();

            var gamedir = profiles[selectedProfileIndex].GameDir;
            var moddir = gamedir + @"\mods";
            if (Directory.Exists(moddir)) {
                var di = new DirectoryInfo(moddir);
                var files = di.EnumerateFiles("*", SearchOption.TopDirectoryOnly);
                foreach (var file in files) {
                    // filename
                    var name = file.Name;
                    // hash
                    string hash = GetFileSHA1Hash(file.FullName);
                    if (hash == null) { hash = string.Empty; }
                    // add result
                    r.Add(name, hash);
                }
            }

            return r;
        }

        private async void UpdateLocalMods() {
            updating = true;

            ModCount.Text = string.Empty;
            CheckModUpdate.Enabled = false;
            LocalModList.Items.Clear();
            LocalModList.BeginUpdate();
            ModListProgressBar.Visible = true;

            if (selectedProfileIndex != -1) {
                await Task.Run(() => {
                    localMods = GetLocalMods();
                });
                foreach (var mod in localMods) {
                    var name = mod.Key;
                    LocalModList.Items.Add(name, config.ExcludedMods.Contains(name));
                }
            }

            ModListProgressBar.Visible = false;
            LocalModList.EndUpdate();
            ModCount.Text = LocalModList.Items.Count.ToString();
            CheckModUpdate.Enabled = true;

            updating = false;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            // repository
            RepositoryUrl.Text = config.RepositoryUrl;

            // profiles, mods
            MinecraftProfileList.SelectedIndex = selectedProfileIndex;

            SaveConfig();
        }

        private void MinecraftProfileList_SelectedIndexChanged(object sender, EventArgs e) {
            var _i = selectedProfileIndex;
            selectedProfileIndex = MinecraftProfileList.SelectedIndex;

            var gamedir = profiles[MinecraftProfileList.SelectedIndex].GameDir;
            ModDirectoryPath.Text = gamedir.Replace(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"%appdata%");

            // validating
            var moddir = gamedir + @"\mods";
            if (!Directory.Exists(moddir)) {
                ErrorProvider.SetError(MinecraftProfileList, "このプロファイルには Mod ディレクトリが存在しません。");
                FileSystemWatcher.EnableRaisingEvents = false;
            } else {
                ErrorProvider.SetError(MinecraftProfileList, null);
                FileSystemWatcher.Path = moddir;
                FileSystemWatcher.EnableRaisingEvents = true;
            }
   
            // update mod list
            if (MinecraftProfileList.SelectedIndex == _i) return;
            UpdateLocalMods();
        }

        private void FocusToControl(object sender, EventArgs e) {
            ((Control)sender).Focus();
        }

        private void FocusToDummy(object sender, EventArgs e) {
            Dummy.Focus();
        }

        private string GetFileSHA1Hash(string path) {
            string hash = null;

            try {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var bs = new BufferedStream(fs))
                using (var sha1 = new SHA1Managed()) {
                    byte[] _hash = sha1.ComputeHash(bs);
                    var builder = new StringBuilder();
                    foreach (var _byte in _hash) {
                        builder.AppendFormat("{0:x2}", _byte);
                    }
                    hash = builder.ToString();
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

            return hash;
        }

        private void CheckModUpdate_Click(object sender, EventArgs e) {
            SaveConfig();

            var gamedir = profiles[MinecraftProfileList.SelectedIndex].GameDir;
            var moddir = gamedir + @"\mods";

            var remoteMods = new Dictionary<string, string>();
            string version = null;
            DateTime lastModified = DateTime.Now;
            Dictionary<string, string> addMods = null, delMods = null;

            // check update
            var dialog = new TaskDialog() {
                Caption = Application.ProductName,
                InstructionText = "Mod の更新を確認しています...",
                StandardButtons = TaskDialogStandardButtons.Cancel,
                ProgressBar = new TaskDialogProgressBar() { State = TaskDialogProgressBarState.Marquee },
                OwnerWindowHandle = Handle,
            };
            dialog.Opened += async (_sender, _e) => {
                dialog.Text = "Mod の一覧を取得しています...";
                try {
                    var req = WebRequest.Create(config.RepositoryUrl + "modlist.yml") as HttpWebRequest;
                    using (var res = await req.GetResponseAsync() as HttpWebResponse) {
                        // header
                        lastModified = DateTime.Parse(res.GetResponseHeader("Last-Modified"));
                        // body
                        using (var stream = res.GetResponseStream())
                        using (var sr = new StreamReader(stream, Encoding.UTF8)) {
                            var yml = new YamlStream();
                            yml.Load(sr);

                            var map = yml.Documents[0].RootNode as YamlMappingNode;
                            version = (map.Children[new YamlScalarNode("version")] as YamlScalarNode).Value;
                            var mods = map.Children[new YamlScalarNode("mods")] as YamlSequenceNode;
                            foreach (YamlMappingNode mod in mods) {
                                foreach (var info in mod) {
                                    remoteMods.Add((info.Key as YamlScalarNode).Value, (info.Value as YamlScalarNode).Value);
                                }
                            }
                        }
                    }
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                    dialog.Close(TaskDialogResult.No);
                    return;
                }

                dialog.Text = "Mod の更新を確認しています...";
                var excluded = config.ExcludedMods.ToDictionary(i => i, i => GetFileSHA1Hash(moddir + @"\" + i));
                addMods = remoteMods.Except(localMods).ToDictionary(i => i.Key, i => i.Value);
                delMods = localMods.Except(remoteMods).ToDictionary(i => i.Key, i => i.Value).Except(excluded).ToDictionary(i => i.Key, i => i.Value);
                if (addMods.Count == 0 && delMods.Count == 0) {
                    dialog.Close(TaskDialogResult.Ok);
                    return;
                }

                dialog.Close(TaskDialogResult.Yes);
            };
            var result = dialog.Show();

            if (result == TaskDialogResult.Ok) {
                new TaskDialog() {
                    Caption = Application.ProductName,
                    InstructionText = "Mod の更新はありません",
                    Icon = TaskDialogStandardIcon.Information,
                    OwnerWindowHandle = Handle,
                }.Show();
                return;
            } else if (result == TaskDialogResult.No) {
                new TaskDialog() {
                    Caption = Application.ProductName,
                    InstructionText = "エラーが発生しました",
                    Text = "更新の確認中にエラーが発生しました。",
                    Icon = TaskDialogStandardIcon.Error,
                    OwnerWindowHandle = Handle,
                }.Show();
                return;
            } else if (result != TaskDialogResult.Yes) {
                return;
            }

            var br = Environment.NewLine;

            var adds = string.Join(br, addMods.Keys.ToArray());
            var dels = string.Join(br, delMods.Keys.ToArray());
            if (adds == string.Empty) adds = "なし";
            if (dels == string.Empty) dels = "なし";

            string detail =
                "Forge バージョン: " + version + br
                + "変更日時: " + lastModified.ToLocalTime().ToString() + br
                + br
                + "追加される Mod:" + br
                + adds + br
                + br
                + "削除される Mod:" + br
                + dels;

            dialog = new TaskDialog() {
                Caption = Application.ProductName,
                InstructionText = "Mod の更新を適用しますか?",
                Text = "これにより、" + addMods.Count + " 個の Mod が追加、" + delMods.Count + " 個の Mod が削除されます。",
                Icon = TaskDialogStandardIcon.Warning,
                StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No,
                OwnerWindowHandle = Handle,
                DetailsCollapsedLabel = "詳細情報",
                DetailsExpandedLabel = "詳細情報を非表示",
                ExpansionMode = TaskDialogExpandedDetailsLocation.ExpandFooter,
                DetailsExpandedText = detail,
            };
            var confirmResult = dialog.Show();
            if (confirmResult != TaskDialogResult.Yes) return;

            // apply update
            dialog = new TaskDialog() {
                Caption = Application.ProductName,
                InstructionText = "Mod ファイルの変更を開始しています...",
                StandardButtons = TaskDialogStandardButtons.Cancel,
                ProgressBar = new TaskDialogProgressBar() { State = TaskDialogProgressBarState.Marquee },
                OwnerWindowHandle = Handle,
            };
            dialog.Opened += async (_sender, _e) => {
                bool flag;
                do {
                    flag = false;
                    string pid = null;
                    using (var win32proc = new System.Management.ManagementClass("Win32_Process"))
                    using (var ps = win32proc.GetInstances()) {
                        foreach (var p in ps) {
                            if (p.GetPropertyValue("Name").ToString().Contains("java") && p.GetPropertyValue("CommandLine").ToString().Contains("minecraft")) {
                                flag = true;
                                pid = p.GetPropertyValue("ProcessId").ToString();
                                break;
                            }
                        }
                    }
                    if (flag) {
                        var confirm = new TaskDialog() {
                            Caption = Application.ProductName,
                            InstructionText = "Minecraft が起動中です",
                            Text = "Minecraft が起動しているため Mod の更新ができません。" + br + "アプリケーションを終了後させ [再試行] を押してください。" + br + "中止する場合は [キャンセル] を押してください。",
                            Icon = TaskDialogStandardIcon.Warning,
                            StandardButtons = TaskDialogStandardButtons.Retry | TaskDialogStandardButtons.Cancel,
                            OwnerWindowHandle = Handle,
                        }.Show();
                        if (confirm == TaskDialogResult.Cancel) {
                            dialog.Close(TaskDialogResult.Cancel);
                            return;
                        }
                    }
                } while (flag);
                
                var i = 0;
                dialog.InstructionText = "Mod を削除しています...";
                foreach (var mod in delMods) {
                    dialog.Text = "(" + ++i + "/" + delMods.Count + ") " + mod.Key;
                    File.Delete(moddir + @"\" + mod.Key);
                    await Task.Run(() => System.Threading.Thread.Sleep(100));
                }

                i = 0;
                var failed = 0;
                dialog.InstructionText = "Mod をダウンロードしています...";
                using (var wc = new WebClient()) {
                    foreach (var mod in addMods) {
                        dialog.Text = "(" + ++i + "/" + addMods.Count + ") " + mod.Key;
                        try {
                            wc.DownloadFileAsync(new Uri(config.RepositoryUrl + @"/" + version + @"/" + mod.Key), moddir + @"\" + mod.Key);
                        } catch (Exception ex) {
                            Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                            failed++;
                        }
                        await Task.Run(() => System.Threading.Thread.Sleep(100));
                    }
                }

                dialog.Close();

                new TaskDialog() {
                    Caption = Application.ProductName,
                    InstructionText = "変更を適用しました",
                    Text = addMods.Count + "個の Mod を追加、" + delMods.Count + "個の Mod を削除しました。" + (failed > 0 ? br + failed + " 個の Mod のダウンロードが失敗しました。" : ""),
                    Icon = TaskDialogStandardIcon.Information,
                    StandardButtons = TaskDialogStandardButtons.Ok,
                    OwnerWindowHandle = Handle,
                }.Show();
            };
            result = dialog.Show();
            UpdateLocalMods();
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e) {
            if (!updating) UpdateLocalMods();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            SaveConfig();
        }
    }
}
