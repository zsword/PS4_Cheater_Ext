using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS4_Cheater
{    
    class CUCodeCheatPlugin : ICheatPlugin
    {
        public string process_name { get; set; }
        public string game_id { get; set; }
        public string game_ver { get; set; }

        public bool ParseGameInfo(string[] cheats)
        {
            if (cheats.Length < 2)
            {
                return false;
            }

            string header = cheats[0];
            header = header.Replace("# ", "");
            string[] header_items = header.Split(' ');

            if (header_items.Length < 4)
            {
                return false;
            }

            string[] version = (header_items[0]).Split('.');

            ulong major_version = 0;
            ulong secondary_version = 0;

            ulong.TryParse(version[0], out major_version);
            if (version.Length > 1)
            {
                ulong.TryParse(version[1], out secondary_version);
            }

            if (major_version > CONSTANT.MAJOR_VERSION || (major_version == CONSTANT.MAJOR_VERSION && secondary_version > CONSTANT.SECONDARY_VERSION))
            {
                return false;
            }

            this.process_name = header_items[1];

            this.game_id = "";
            this.game_ver = "";

            game_id = header_items[2];

            game_ver = header_items[3];
            return true;
        }

        public bool ParseCheats(ProcessManager processManager, string[] cheats)
        {
            List<Cheat> cheatList = new List<Cheat>();
            int i = 1;
            while (i < cheats.Length) { 
                string cheat_tuple = cheats[i++];
                if(cheat_tuple.StartsWith("_V"))
                {
                    List<string> lines = new List<string>();
                    lines.Add(cheat_tuple);
                    for(;i<cheats.Length;i++)
                    {
                        string line = cheats[i];
                        if (line.StartsWith("_V")) break;
                        lines.Add(line);
                    }
                    i--;
                    CUCodeCheat cheat = new CUCodeCheat(processManager);
                    cheat.Parse(lines.ToArray());
                }
            }
            return true;
        }

        public string GetCodeFileFilter()
        {
            return "PlayStation4CodeUnique|*.ps4";
        }
    }
}
