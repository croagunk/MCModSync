using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModSync {
    public class Profile {
        public string ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string GameDir { get; set; }
        public Profile(string profile_id, string profile_name, string game_dir) {
            ProfileId = profile_id;
            ProfileName = profile_name;
            GameDir = game_dir;
        }
    }
}
