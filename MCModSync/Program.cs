using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCModSync {
    public static class Program {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        public static void Main() {
            SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var status = CheckRepositoryUrl();
            if (status == CheckStatus.Empty) {
                var dialog = new RepositoryConfigDialog();
                dialog.SetMessage("リポジトリ URL が設定されていません。");
                var result = dialog.ShowDialog();
                if (result == DialogResult.Cancel) {
                    return;
                }
            } else if (status != CheckStatus.OK) {
                var dialog = new RepositoryConfigDialog();
                dialog.SetMessage("リポジトリ URL が正しくありません。");
                dialog.SetDefaultUrl(Config.Load().RepositoryUrl);
                var result = dialog.ShowDialog();
                if (result == DialogResult.Cancel) {
                    return;
                }
            }

            Application.Run(new MainForm());
        }

        public enum CheckStatus {
            OK,
            Empty,
            Invalid,
            NotFound,
            MiscError,
        }

        public static CheckStatus CheckRepositoryUrl(string url = null) {
            if (url == null) {
                var config = Config.Load();
                url = config.RepositoryUrl;
                if (url != string.Empty && !url.EndsWith("/")) { url += "/"; }
                config.RepositoryUrl = url;
            }

            if (url == null || url == string.Empty) return CheckStatus.Empty;

            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out Uri uri);
            if (!isValidUrl) return CheckStatus.Invalid;

            CheckStatus result = CheckStatus.MiscError;
            try {
                var req = (HttpWebRequest)WebRequest.Create(url + "modlist.yml");
                using (var res = (HttpWebResponse)req.GetResponse()) {
                    if (res.StatusCode == HttpStatusCode.OK) {
                        result = CheckStatus.OK;
                    } else {
                        result = CheckStatus.NotFound;
                    }
                }
            } catch (WebException ex) {
                Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                if (ex.Status == WebExceptionStatus.ProtocolError) {
                    result = CheckStatus.NotFound;
                }
            }

            return result;
        }

        public static async Task<CheckStatus> CheckRepositoryUrlAsync(string url = null) {
            if (url == null) {
                var config = Config.Load();
                url = config.RepositoryUrl;
                if (url != string.Empty && !url.EndsWith("/")) { url += "/"; }
                config.RepositoryUrl = url;
            }

            if (url == null || url == string.Empty) return CheckStatus.Empty;

            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out Uri uri);
            if (!isValidUrl) return CheckStatus.Invalid;

            CheckStatus result = CheckStatus.MiscError;
            try {
                Debug.WriteLine("URL=" + url + "modlist.yml");
                var req = WebRequest.Create(url + "modlist.yml") as HttpWebRequest;
                using (var res = await req.GetResponseAsync() as HttpWebResponse) {
                    Debug.WriteLine("STATUS=" + res.StatusDescription);
                    if (res.StatusCode == HttpStatusCode.OK) {
                        result = CheckStatus.OK;
                    } else {
                        result = CheckStatus.NotFound;
                    }
                }
            } catch (WebException ex) {
                Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                if (ex.Status == WebExceptionStatus.ProtocolError) {
                    result = CheckStatus.NotFound;
                }
            }

            return result;
        }
    }
}
