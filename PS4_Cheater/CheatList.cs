using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS4_Cheater
{
    public class DataCheat : Cheat
    {
        private const int CHEAT_CODE_DATA_TYPE_FLAG = 5;
        private const int CHEAT_CODE_DATA_TYPE_DESCRIPTION = 6;

        private const int CHEAT_CODE_DATA_TYPE_ELEMENT_COUNT = CHEAT_CODE_DATA_TYPE_DESCRIPTION + 1;

        public DataCheat(DataCheatOperator source, AddressCheatOperator dest, bool lock_, string description, ProcessManager processManager)
            : base(processManager)
        {
            CheatType = CheatType.DATA_TYPE;
            AllowLock = true;
            Source = source;
            Destination = dest;
            Lock = lock_;
            Description = description;
        }

        public DataCheat(ProcessManager ProcessManager) :
            base(ProcessManager)
        {
            Source = new DataCheatOperator(ProcessManager);
            Destination = new AddressCheatOperator(ProcessManager);
            CheatType = CheatType.DATA_TYPE;
            AllowLock = true;
        }

        public override bool Parse(string[] cheat_elements)
        {
            if (cheat_elements.Length < CHEAT_CODE_DATA_TYPE_ELEMENT_COUNT)
            {
                return false;
            }

            int start_idx = 1;
            AddressCheatOperator addressCheatOperator = (AddressCheatOperator)Destination;
            if (!(addressCheatOperator.ParseOldFormat(cheat_elements, ref start_idx)))
            {
                return false;
            }

            if (!Source.Parse(cheat_elements, ref start_idx, true))
            {
                return false;
            }

            ulong flag = ulong.Parse(cheat_elements[CHEAT_CODE_DATA_TYPE_FLAG], NumberStyles.HexNumber);

            Lock = flag == 1 ? true : false;

            Description = cheat_elements[CHEAT_CODE_DATA_TYPE_DESCRIPTION];

            Destination.ValueType = Source.ValueType;

            return true;
        }

        public override string ToString()
        {
            string save_buf = "";
            save_buf += "data|";
            save_buf += ((AddressCheatOperator)Destination).DumpOldFormat();
            save_buf += Source.Dump(true);
            save_buf += (Lock ? "1" : "0") + "|";
            save_buf += Description + "|";
            save_buf += Destination.ToString() + "\n";
            return save_buf;
        }
    }


    public class SimplePointerCheat : Cheat
    {
        public SimplePointerCheat(ProcessManager ProcessManager)
            : base(ProcessManager)
        {
            CheatType = CheatType.SIMPLE_POINTER_TYPE;
            AllowLock = true;
        }

        public SimplePointerCheat(CheatOperator source, CheatOperator dest, bool lock_, string description, ProcessManager processManager)
            : base(processManager)
        {
            CheatType = CheatType.DATA_TYPE;
            AllowLock = true;
            Source = source;
            Destination = dest;
            Lock = lock_;
            Description = description;
        }

        public override bool Parse(string[] cheat_elements)
        {
            int start_idx = 1;

            if (cheat_elements[start_idx] == "address")
            {
                Destination = new AddressCheatOperator(ProcessManager);
            }
            else if (cheat_elements[start_idx] == "pointer")
            {
                Destination = new SimplePointerCheatOperator(ProcessManager);
            }

            ++start_idx;
            Destination.Parse(cheat_elements, ref start_idx, true);

            if (cheat_elements[start_idx] == "data")
            {
                Source = new DataCheatOperator(ProcessManager);
            }
            else if (cheat_elements[start_idx] == "pointer")
            {
                Source = new SimplePointerCheatOperator(ProcessManager);
            }

            ++start_idx;
            Source.Parse(cheat_elements, ref start_idx, true);

            ulong flag = ulong.Parse(cheat_elements[start_idx], NumberStyles.HexNumber);

            Lock = flag == 1 ? true : false;

            Description = cheat_elements[start_idx + 1];

            return true;
        }

        public override string ToString()
        {
            string save_buf = "";
            save_buf += "simple pointer|";
            save_buf += "pointer|";
            save_buf += Destination.Dump(true) + "|";
            save_buf += "data|";
            save_buf += Source.Dump(true);
            save_buf += (Lock ? "1" : "0") + "|";
            save_buf += Description + "|";
            save_buf += "\n";
            return save_buf;
        }
    }

    class CheatList
    {
        public  const bool IS_DEV = false;
        private List<Cheat> cheat_list;

        private const int CHEAT_CODE_HEADER_VERSION = 0;
        private const int CHEAT_CODE_HEADER_PROCESS_NAME = 1;
        private const int CHEAT_CODE_HEADER_PROCESS_ID   = 2;
        private const int CHEAT_CODE_HEADER_PROCESS_VER  = 3;

        private const int CHEAT_CODE_HEADER_ELEMENT_COUNT = CHEAT_CODE_HEADER_PROCESS_NAME + 1;

        private const int CHEAT_CODE_TYPE = 0;
        public CheatList()
        {
            cheat_list = new List<Cheat>();
        }

        public void Add(Cheat cheat)
        {
            cheat_list.Add(cheat);
        }

        public void RemoveAt(int idx)
        {
            cheat_list.RemoveAt(idx);
        }

        public bool Exist(Cheat cheat)
        {
            return false;
        }

        public bool Exist(ulong destAddress)
        {
            return false;
        }

        public bool LoadFile(string path, ProcessManager processManager, ComboBox comboBox)
        {
            return this.LoadFile(path, processManager, comboBox, null);
        }

            public bool LoadFile(string path, ProcessManager processManager, ComboBox comboBox, ICheatPlugin cheatPlugin) {
            string[] cheats = File.ReadAllLines(path);

            string process_name = "";
            string game_id = "";
            string game_ver = "";

            if (cheatPlugin != null)
            {
                bool ret = cheatPlugin.ParseGameInfo(cheats);
                process_name = cheatPlugin.process_name;

                game_id = cheatPlugin.game_id;
                game_ver = cheatPlugin.game_ver;
            }
            else
            {

                if (cheats.Length < 2)
                {
                    return false;
                }

                string header = cheats[0];
                string[] header_items = header.Split('|');

                if (header_items.Length < CHEAT_CODE_HEADER_ELEMENT_COUNT)
                {
                    return false;
                }

                string[] version = (header_items[CHEAT_CODE_HEADER_VERSION]).Split('.');

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

                process_name = header_items[CHEAT_CODE_HEADER_PROCESS_NAME];

                if (header_items.Length > CHEAT_CODE_HEADER_PROCESS_ID)
                {
                    game_id = header_items[CHEAT_CODE_HEADER_PROCESS_ID];
                    game_id = game_id.Substring(3);
                }

                if (header_items.Length > CHEAT_CODE_HEADER_PROCESS_VER)
                {
                    game_ver = header_items[CHEAT_CODE_HEADER_PROCESS_VER];
                    game_ver = game_ver.Substring(4);
                }
            }

            if (process_name != (string)comboBox.SelectedItem)
            {
                comboBox.SelectedItem = process_name;
            }

            if (process_name != (string)comboBox.SelectedItem && !IS_DEV)
            {
                MessageBox.Show("Invalid process or refresh processes first.");
                return false;
            }

            if (game_id != "" && game_ver != "")
            {
                GameInfo gameInfo = new GameInfo();
                if (gameInfo.GameID != game_id && !IS_DEV)
                {
                    if (MessageBox.Show("Your Game ID(" + gameInfo.GameID + ") is different with cheat file(" + game_id + "), still load?",
                        "Invalid game ID", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                if (gameInfo.Version != game_ver && !IS_DEV)
                {
                    if (MessageBox.Show("Your game version(" + gameInfo.Version + ") is different with cheat file(" + game_ver + "), still load?",
                        "Invalid game version", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return false;
                    }
                }
            }

            if(cheatPlugin!=null)
            {
                bool ret = cheatPlugin.ParseCheats(processManager, cheats);
                return ret;
            }

            for (int i = 1; i < cheats.Length; ++i)
            {
                string cheat_tuple = cheats[i];
                string[] cheat_elements = cheat_tuple.Split(new string[] { "|" }, StringSplitOptions.None);

                if (cheat_elements.Length == 0)
                {
                    continue;
                }

                Cheat cheat = null;
                switch(cheat_elements[CHEAT_CODE_TYPE]) {
                    case "data":
                        cheat = new DataCheat(processManager);
                        if (!cheat.Parse(cheat_elements)) {
                            MessageBox.Show("Invaid cheat code:" + cheat_tuple);
                            continue;
                        }
                        break;
                    case "simple pointer":
                        cheat = new SimplePointerCheat(processManager);
                        if (!cheat.Parse(cheat_elements))
                            continue;
                        break;
                    case "@batchcode":
                        cheat = new BatchCodeCheat(processManager);
                        if (!cheat.Parse(cheat_elements))
                        {
                            MessageBox.Show("Invaid @batchcode:" + cheat_tuple);
                            continue;
                        }
                        break;
                    case "@cheatcode":
                        //cheat = new CheatCodeCheat(processManager);
                        //if (!cheat.Parse(cheat_elements))
                        //{
                        //    MessageBox.Show("Invaid @cheatcode:" + cheat_tuple);
                        //    continue;
                        //}
                        break;
                    default:
                        MessageBox.Show("Invaid cheat code:" + cheat_tuple);
                        continue;
                }
                cheat_list.Add(cheat);

                /*if (cheat_elements[CHEAT_CODE_TYPE] == "data")
                {
                    DataCheat cheat = new DataCheat(processManager);
                    if (!cheat.Parse(cheat_elements))
                    {
                        MessageBox.Show("Invaid cheat code:" + cheat_tuple);
                        continue;
                    }

                    cheat_list.Add(cheat);
                }
                else if (cheat_elements[CHEAT_CODE_TYPE] == "simple pointer")
                {

                    SimplePointerCheat cheat = new SimplePointerCheat(processManager);
                    if (!cheat.Parse(cheat_elements))
                        continue;
                    cheat_list.Add(cheat);
                }
                else
                {
                    MessageBox.Show("Invaid cheat code:" + cheat_tuple);
                    continue;
                }*/
            }
            return true;
        }

        public void SaveFile(string path, string prcessName, ProcessManager processManager)
        {
            GameInfo gameInfo = new GameInfo();
            string save_buf = CONSTANT.MAJOR_VERSION + "."
                + CONSTANT.SECONDARY_VERSION
                + "|" + prcessName
                + "|ID:" + gameInfo.GameID
                + "|VER:" + gameInfo.Version
                + "|FM:" + Util.Version
                + "\n";

            for (int i = 0; i < cheat_list.Count; ++i)
            {
                save_buf += cheat_list[i].ToString();
            }

            StreamWriter myStream = new StreamWriter(path);
            myStream.Write(save_buf);
            myStream.Close();
        }

        public Cheat this[int index]
        {
            get
            {
                return cheat_list[index];
            }
            set
            {
                cheat_list[index] = value;
            }
        }

        public void Clear()
        {
            cheat_list.Clear();
        }

        public int Count { get { return cheat_list.Count; } }
    }
}
