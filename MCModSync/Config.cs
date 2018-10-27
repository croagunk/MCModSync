using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MCModSync {
    public class Config {
        private static readonly string path = @".\app.config";
        private static Config config = null;

        public string RepositoryUrl { get; set; }
        public string MinecraftProfile { get; set; }
        public HashSet<string> ExcludedMods { get; set; }

        private Config() {
            RepositoryUrl = null;
            MinecraftProfile = string.Empty;
            ExcludedMods = new HashSet<string>();
        }

        public void Save() {
            var xs = new XmlSerializer(typeof(Config));
            try {
                using (var sw = new StreamWriter(path, false, Encoding.UTF8)) {
                    xs.Serialize(sw, this);
                }
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public static Config Load() {
            if (!File.Exists(path)) {
                new Config().Save();
            }

            if (config == null) {
                var xs = new XmlSerializer(typeof(Config));
                try {
                    using (var sr = new StreamReader(path, Encoding.UTF8)) {
                        config = (Config)xs.Deserialize(sr);
                    }
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }

            return config;
        }
    }
}
