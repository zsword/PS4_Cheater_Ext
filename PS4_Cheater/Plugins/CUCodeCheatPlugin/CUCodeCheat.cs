using PS4_Cheater;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PS4_Cheater
{
    abstract class AbstractCUCodeOperator : CheatOperator
    {
        public AbstractCUCodeOperator(ValueType valueType, ProcessManager processManager) :base(valueType, processManager) {
            CheatOperatorType = CheatOperatorType.CHEAT_CODE_TYPE;
        }
    }

    class CUWriteOperator : AbstractCUCodeOperator
    {
        private const int DATA_TYPE = 0;
        private const int DATA = 1;

        private ulong address;
        private byte[] data;

        public CUWriteOperator(ValueType valueType, ProcessManager processManager) :base(valueType, processManager) {
        }

        public override byte[] Get(int idx = 0) { return data; }

        public override byte[] GetRuntime() { return data; }

        public override void Set(CheatOperator SourceCheatOperator, int idx = 0)
        {
            data = new byte[MemoryHelper.Length];
            Buffer.BlockCopy(SourceCheatOperator.Get(), 0, data, 0, MemoryHelper.Length);
        }

        public override void SetRuntime(CheatOperator SourceCheatOperator, int idx = 0)
        {
            data = new byte[MemoryHelper.Length];
            Buffer.BlockCopy(SourceCheatOperator.GetRuntime(), 0, data, 0, MemoryHelper.Length);
        }

        public void Set(string data)
        {
            this.data = MemoryHelper.StringToBytes(data);
        }

        public void Set(byte[] data)
        {
            this.data = data;
        }

        public override string ToString(bool simple)
        {
            return MemoryHelper.BytesToString(data);
        }

        public override string Display()
        {
            return MemoryHelper.BytesToString(data);
        }

        public override bool Parse(string[] cheat_elements, ref int start_idx, bool simple_format)
        {
            string[] strs = cheat_elements[start_idx].Split(' ');
            string str = strs[0];
            string addrStr = str.Substring(5);
            addrStr += strs[1];
            this.address = UInt32.Parse(addrStr, NumberStyles.HexNumber);
            address += CONSTANT.START_ADDRESS;
            data = MemoryHelper.StringToBytes(strs[2]);
            start_idx += 1;
            return true;
        }

        public override string Dump(bool simpleFormat)
        {
            string save_buf = "";
            save_buf += MemoryHelper.GetStringOfValueType(ValueType) + "|";
            save_buf += MemoryHelper.BytesToString(data) + "|";
            return save_buf;
        }
    }

    public class CUCodeCheat : Cheat
    {
        private List<CheatOperator> operators;

        public CUCodeCheat(ProcessManager ProcessManager)
            : base(ProcessManager)
        {
            this.operators = new List<CheatOperator>();
            CheatType = CheatType.CHEAT_CODE_TYPE;
            AllowLock = true;
        }

        public CUCodeCheat(CheatOperator source, CheatOperator dest, bool lock_, string description, ProcessManager processManager)
            : base(processManager)
        {
            CheatType = CheatType.DATA_TYPE;
            AllowLock = true;
            Source = source;
            Destination = dest;
            Lock = lock_;
            Description = description;
        }

        public override bool Parse(string[] lines)
        {
            int idx = 0;
            string line = lines[idx++];
            string str = line.Substring(0, 3);
            this.Lock = str.Replace("_V", "").Equals(1) ? true : false;
            str = line.Substring(4);
            this.Description = str.Trim();
            for(; idx < lines.Length; idx++)
            {
                line = lines[idx];
                if (!line.StartsWith("$")) continue;
                line = line.Substring(1);
                CheatOperator opt = null;
                string optType = line.Substring(0, 1);
                ValueType valueType = ValueType.NONE_TYPE;
                string valType = line.Substring(1, 2);
                switch(valType)
                {
                    case "0":
                        valueType = ValueType.BYTE_TYPE;break;
                    case "1":
                        valueType = ValueType.USHORT_TYPE;break;
                    case "2":
                        valueType = ValueType.UINT_TYPE;break;                        
                }
                switch (optType)
                {
                    case "0":
                        opt = new CUWriteOperator(valueType, ProcessManager);
                        break;
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                }
                opt.Parse(lines, ref idx, true);
                this.operators.Add(opt);
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("_V"+(this.Lock? "1" : "0")+" "+this.Description);
            foreach(CheatOperator opt in this.operators) {
                sb.AppendLine(opt.Dump(true));
            }
            return sb.ToString();
        }
    }
}
