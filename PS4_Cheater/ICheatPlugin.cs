using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS4_Cheater
{
    public interface ICheatPlugin
    {
        string process_name { get;}
        string game_id { get; }
        string game_ver { get; }
        bool ParseGameInfo(string[] lines);
        bool ParseCheats(ProcessManager processManager, string[] lines);
        string GetCodeFileFilter();
    }
}
