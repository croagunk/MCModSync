using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace MCModSync {
    public partial class RepositoryConfigDialog : Form {
        private bool isSetMessage = false;

        public RepositoryConfigDialog() {
            InitializeComponent();
        }

        public void SetMessage(string message) {
            if (!isSetMessage) {
                DialogMessage.Text = message + DialogMessage.Text;
                isSetMessage = true;
            }
        }

        public void SetDefaultUrl(string url) {
            RepositoryUrl.Text = url;
        }

        private void OKButton_Click(object sender, EventArgs e) {
            var url = RepositoryUrl.Text;
            if (!url.EndsWith("/")) { url += "/"; }

            var dialog = new TaskDialog() {
                Caption = Application.ProductName,
                InstructionText = "入力された URL を確認しています...",
                StandardButtons = TaskDialogStandardButtons.Cancel,
                ProgressBar = new TaskDialogProgressBar() { State = TaskDialogProgressBarState.Marquee },
                OwnerWindowHandle = Handle,
            };
            dialog.Opened += async (_sender, _e) => {
                var wres = TaskDialogResult.None;
                var status = await Program.CheckRepositoryUrlAsync(url);

                if (status == Program.CheckStatus.OK) {
                    wres = TaskDialogResult.Ok;
                } else {
                    wres = TaskDialogResult.Cancel;
                }
                dialog.Close(wres);

                if (status == Program.CheckStatus.Empty || status == Program.CheckStatus.Invalid) {
                    new TaskDialog() {
                        Caption = Application.ProductName,
                        InstructionText = "URL が正しくありません",
                        Text = "入力された URL の形式が正しくありません。入力内容を確認してください。",
                        Icon = TaskDialogStandardIcon.Warning,
                        OwnerWindowHandle = dialog.OwnerWindowHandle,
                    }.Show();
                } else if (status == Program.CheckStatus.NotFound) {
                    new TaskDialog() {
                        Caption = Application.ProductName,
                        InstructionText = "リポジトリが見つかりませんでした",
                        Text = "入力された URL には Mod 定義ファイルが存在しません。",
                        Icon = TaskDialogStandardIcon.Warning,
                        OwnerWindowHandle = dialog.OwnerWindowHandle,
                    }.Show();
                } else if (status == Program.CheckStatus.MiscError) {
                    new TaskDialog() {
                        Caption = Application.ProductName,
                        InstructionText = "エラーが発生しました",
                        Text = "不明なエラーが発生しました。",
                        Icon = TaskDialogStandardIcon.Error,
                        OwnerWindowHandle = dialog.OwnerWindowHandle,
                    }.Show();
                    wres = TaskDialogResult.Cancel;
                }
            };
            var result = dialog.Show();

            if (result == TaskDialogResult.Ok) {
                var config = Config.Load();
                config.RepositoryUrl = url;
                config.Save();

                DialogResult = DialogResult.OK;
                Close();
            }

            RepositoryUrl.Focus();
            RepositoryUrl.SelectAll();
        }

        private void RepositoryConfigDialog_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult != DialogResult.OK) {
                var confirm = new TaskDialog() {
                    Caption = Application.ProductName,
                    InstructionText = "設定が完了していません",
                    Text = "設定が完了していませんが、このまま終了しますか?",
                    Icon = TaskDialogStandardIcon.Information,
                    StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No,
                }.Show();
                if (confirm == TaskDialogResult.Yes) {
                    DialogResult = DialogResult.Cancel;
                } else {
                    e.Cancel = true;
                }
            }
        }
    }
}
