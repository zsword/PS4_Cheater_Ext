using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS4_Cheater
{
    public enum BatchType
    {
        FOR, IF, VALUE
    }

    class NumberBytesHelper
    {
        public static decimal ParseString(string str)
        {
            if (str.StartsWith("0x"))
            {
                return Convert.ToInt64(str, 16);
            }
            return decimal.Parse(str);
        }

        public static byte[] HexToBytes(string hex)
        {
            int size = (hex.Length+1)/2*2;
            hex = hex.PadLeft(size, '0');
            byte[] data = MemoryHelper.string_to_hex_bytes(hex);
            Array.Reverse(data);
            return data;
        }

        public static byte[] Add(byte[] data, byte[] step)
        {
            int carry = 0;
            int val = 0;
            for(int i=0;i<data.Length;i++)
            {
                val = data[i];
                if (step.Length > i)
                {
                    val = val + step[i];
                }
                else if (carry == 0) break;
                if (carry > 0) val = val + carry;
                carry = 0;
                if(val>0xFF)
                {
                    carry = val / 0xFF;
                    val = (byte)(val % 0xFF);
                }
                data[i] = (byte)val;
            }
            return data;
        }
    }

     class NumberExcluder
    {
        List<decimal[]> ranges = new List<decimal[]>();

        public NumberExcluder() { }

        public void Parse(string data)
        {
            string[] strs = data.Split(new string[] { "," }, StringSplitOptions.None);
            for(int i=0;i<strs.Length;i++)
            {
                string str = strs[i];
                string[] tmp = str.Split(new string[] { "~" }, StringSplitOptions.None);
                decimal from = NumberBytesHelper.ParseString(tmp[0]);
                decimal to = from;
                if(tmp.Length>1)
                {
                    to = NumberBytesHelper.ParseString(tmp[1]);
                }
                this.ranges.Add(new decimal[]{ from, to});
            }
        }

        public bool Match(decimal target)
        {
            for(int i=0;i<this.ranges.Count;i++)
            {
                decimal[] range = ranges[i];
                if (target >= range[0] && target <= range[1]) return true;
            }
            return false;
        }
    }

     class BatchCode
    {
        public BatchType batchType { get; set; }
        public int begin {get;set;}
        public int target { get; set; }
        public int offset { get; set; }
        public int step { get; set; }
        public int skip { get; set; }
        public int size { get; set; }
        public string value { get; set; }
        public string reset { get; set; }
        public NumberExcluder excluder { get; set; }
        public ValueType vtype { get; set; }


        public int GetDataSize()
        {
            return target * (size+skip);
        }
    }

      class BatchCodeCheatOperator : CheatOperator
    {
        private CheatOperator Address { get; set;  }
        private List<BatchCode> Codes { get; set; }
        private string cheat_code;
        private const int DATA_TYPE = 0;
        private const int DATA = 1;

        private byte[] data;

        public BatchCodeCheatOperator(List<BatchCode> Codes, CheatOperator Address, ValueType valueType, ProcessManager processManager)
            : base(valueType, processManager)
        {
            this.Address = Address;
            this.Codes = Codes;
            CheatOperatorType = CheatOperatorType.BATCH_CODE_TYPE;
        }

        public BatchCodeCheatOperator(CheatOperator Address, ProcessManager processManager)
            : base(ValueType.HEX_TYPE, processManager)
        {
            this.Address = Address;
            this.Codes = new List<BatchCode>();
            CheatOperatorType = CheatOperatorType.BATCH_CODE_TYPE;
        }

        public override byte[] Get(int idx = 0) { return data; }

        private byte[] convertValue(decimal value, ValueType vtype)
        {
            object val = null;
            int size = 0;
            switch (vtype) {
                case ValueType.UINT_TYPE:
                    size = 4;
                    val = Convert.ToUInt32(value); break;
                case ValueType.ULONG_TYPE:
                    size = 8;
                    val = Convert.ToUInt64(value); break;
                case ValueType.USHORT_TYPE:
                    size = 2;
                    val = Convert.ToUInt16(value); break;
                case ValueType.BYTE_TYPE:
                    size = 1;
                    val = Convert.ToByte(value);break;
            }
            string str = String.Format("{0:X}", val);
            int len = size*2;
            str = str.PadLeft(len, '0');
            byte[] buff = MemoryHelper.string_to_hex_bytes(str);
            Array.Reverse(buff);
            return buff;
        }
        

        public bool WriteData()
        {
            for(int i=0;i<Codes.Count;i++)
            {
                this.WriteData(i, false);
            }
            return true;
        }

        public bool ResetData()
        {
            for (int i = 0; i < Codes.Count; i++)
            {
                this.WriteData(i, true);
            }
            return true;
        }

        public bool WriteData(int index, bool reset)
        {
            ulong address = this.getAddress();
            BatchCode code = Codes[index];
            if(reset && string.IsNullOrEmpty(code.reset))
            {
                return true;
            }
            byte[] data = this.GetData(index, reset);
            int offset = code.offset;
            address = address + (ulong)offset;
            if (CheatList.IS_DEV)
            {
            }
            else
            {
                MemoryHelper.WriteMemory(address, data);
            }
            return true;
        }

        private byte[] GetData(int index, bool reset) {
            ulong address = this.getAddress();
            BatchCode code = Codes[index];
            byte[] data = null;            
            string value = code.value;
            if(reset)
            {
                value = code.reset;
            }
            ValueType vtype = code.vtype;
            int size = code.size;
            int psize = size + code.skip;
            decimal baseVal = 0;
            byte[] stepData = null;
            if(code.step!=0)
            {
                string hexStr = String.Format("{0:X}", code.step);
                stepData = NumberBytesHelper.HexToBytes(hexStr);
            }
            if (value.StartsWith("0x"))
            {
                string str = value.Substring(2);
                data = MemoryHelper.string_to_hex_bytes(str);
                if (size < 9)
                {
                    string[] strs = new string[str.Length / 2];
                    for (int i = 0; i < strs.Length; i++)
                    {
                        strs[i] = str.Substring(i * 2, 2);
                    }
                    Array.Reverse(strs);
                    str = String.Join("", strs);
                    baseVal = ulong.Parse(str, NumberStyles.HexNumber);
                }
            } else {
                baseVal = Convert.ToDecimal(value);
                data = this.convertValue(baseVal, vtype);
            }

            if(BatchType.VALUE.Equals(code.batchType))
            {
                return data;
            }

            int dataSize = code.GetDataSize();
            int offset = code.offset;
            address = address + (ulong)offset;
            byte[] buff = null;
            if (CheatList.IS_DEV)
            {
                buff = new byte[dataSize];
            }
            else
            {
                buff = MemoryHelper.ReadMemory(address, dataSize);
            }
            decimal val = baseVal;
            int pos = 0;
            NumberExcluder excluder = code.excluder;
            bool ignore = false;
            int begin = code.begin + 1;
            int end = code.target + 1;
            switch(code.batchType)
            {
                case BatchType.FOR:
                    for(int i=begin;i<end;i++)
                    {
                        if(excluder!=null)
                        {
                            ignore = excluder.Match(i);
                        }
                        if (!ignore)
                        {
                            if (size<9 && val != baseVal)
                            {
                                data = this.convertValue(val, vtype);
                            }
                            Buffer.BlockCopy(data, 0, buff, pos, code.size);
                            pos += psize;
                        }
                        if (!reset)
                        {
                            if (size < 9)
                            {
                                val += code.step;
                            } else if(stepData!=null)
                            {
                                data = NumberBytesHelper.Add(data, stepData);
                            }
                        }
                    }
                    break;
            }
            return buff;
        }

        public override void Set(CheatOperator SourceCheatOperator, int idx = 0)
        {
            data = new byte[MemoryHelper.Length];
            Buffer.BlockCopy(SourceCheatOperator.Get(), 0, data, 0, MemoryHelper.Length);
        }

        private ulong getAddress()
        {
            switch(Address.CheatOperatorType)
            {
                case CheatOperatorType.DATA_TYPE:
                    return ((AddressCheatOperator)Address).Address;
                case CheatOperatorType.ADDRESS_TYPE:
                    return ((AddressCheatOperator)Address).Address;
                case CheatOperatorType.SIMPLE_POINTER_TYPE:
                    return ((SimplePointerCheatOperator)Address).GetAddress();
            }
            return 0;
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
            return this.cheat_code;
        }

        public override string Display()
        {
            return this.cheat_code;
        }

        private BatchCode ParseCode(string data)
        {
            string[] elements = data.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            BatchCode code = new BatchCode();
            BatchType batchType = BatchType.VALUE;
            int target = 0;
            int step = 0;
            int offset = 0;
            int skip = 0;
            int size = -1;
            string cheatVal = null;
            string resetVal = null;
            ValueType vtype = ValueType.NONE_TYPE;
            foreach(string elm in elements)
            {
                string[] buff = elm.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                string key = buff[0];
                string value = buff[1];
                switch(key)
                {
                    case "for":
                        batchType = BatchType.FOR;
                        target = (int)NumberBytesHelper.ParseString(value);
                        break;
                    case "if":
                        batchType = BatchType.IF;
                        target = int.Parse(value);
                        break;
                    case "step":
                        if(value.StartsWith("0x")) {
                            step = int.Parse(value.Substring(2), NumberStyles.HexNumber);break;
                        }
                        step = int.Parse(value);
                        break;
                    case "offset":
                        offset = decimal.ToInt32(NumberBytesHelper.ParseString(value));
                        break;
                    case "skip":
                        skip = (int)NumberBytesHelper.ParseString(value);
                        break;
                    case "value":
                        cheatVal = value;break;
                    case "reset":
                        resetVal = value;break;
                        break;
                    case "size":
                        size = (int)NumberBytesHelper.ParseString(value);
                        break;
                    case "vtype":
                        switch(value)
                        {
                            case "float":
                                vtype = ValueType.FLOAT_TYPE;break;
                            case "double":
                                vtype = ValueType.DOUBLE_TYPE;break;
                        }
                        break;
                    case "excludes":
                        NumberExcluder excluder = new NumberExcluder();
                        excluder.Parse(value);
                        code.excluder = excluder;
                        break;
                }
            }
            if (size < 1 && cheatVal.StartsWith("0x"))
            {
                string tmp = cheatVal.Substring(2);
                size = (tmp.Length + 1) / 2;
                size = size > 2 ? (size + 3) / 4 * 4 : size;
            }
            size = size<1 ? 4 : size;
            if(cheatVal.StartsWith("0x"))
            {
                cheatVal = "0x" + cheatVal.Substring(2).PadLeft(size * 2, '0');
            }
            code.value = cheatVal;
            if (!string.IsNullOrEmpty(resetVal) && resetVal.StartsWith("0x")) {
                resetVal = "0x" + resetVal.Substring(2).PadLeft(size * 2, '0');
            }
            code.reset = resetVal;
            if (ValueType.NONE_TYPE.Equals(vtype))
            {
                switch (size)
                {
                    case 1:
                        vtype = ValueType.BYTE_TYPE;
                        break;
                    case 2:
                        vtype = ValueType.USHORT_TYPE;
                        break;
                    case 4:
                        vtype = ValueType.UINT_TYPE;
                        break;
                    case 8:
                        vtype = ValueType.ULONG_TYPE;
                        break;
                    default:
                        vtype = ValueType.HEX_TYPE;break;
                }
            }
            code.batchType = batchType;
            code.target = target;
            code.step = step;
            code.skip = skip;
            code.size = size;
            code.offset = offset;
            code.vtype = vtype;
            return code;
        }

        public override bool Parse(string[] cheat_elements, ref int start_idx, bool simple_format)
        {
            string str = cheat_elements[start_idx];
            this.cheat_code = str;
            string[] datas = str.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < datas.Length; i++)
            {
                string cstr = datas[i];
                if (string.IsNullOrEmpty(cstr)) continue;
                BatchCode code = this.ParseCode(cstr);
                this.Codes.Add(code);
            }
            ++start_idx;
            if (CheatList.IS_DEV) { 
                this.GetData(0, false);
            }
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

    public class BatchCodeCheat : Cheat
    {
        private string AddressType;
        public BatchCodeCheat(ProcessManager ProcessManager)
            : base(ProcessManager)
        {
            CheatType = CheatType.BATCH_CODE_TYPE;
            AllowLock = true;
        }

        public BatchCodeCheat(CheatOperator source, CheatOperator dest, bool lock_, string description, ProcessManager processManager)
            : base(processManager)
        {
            CheatType = CheatType.DATA_TYPE;
            AllowLock = true;
            Source = source;
            Destination = dest;
            Lock = lock_;
            Description = description;
        }

        public void ParseCode(string cheat_code)
        {
            int index = 0;
            this.Source.Parse(new string[]{ cheat_code}, ref index, false);
        }

        public override bool Parse(string[] cheat_elements)
        {
            int start_idx = 1;
            string addressType = cheat_elements[start_idx];
            ++start_idx;
            switch (addressType)
            {
                case "data":
                    {
                        AddressCheatOperator addressCheatOperator = new AddressCheatOperator(ProcessManager);
                        Destination = addressCheatOperator;
                    }
                    break;
                case "address":
                    {
                        AddressCheatOperator addressCheatOperator = new AddressCheatOperator(ProcessManager);
                        Destination = addressCheatOperator;
                    }
                    break;
                case "pointer":
                    {
                        SimplePointerCheatOperator pointerCheatOperator = new SimplePointerCheatOperator(ProcessManager);
                        Destination = pointerCheatOperator;
                    }
                    break;
                default:
                    break;
            }
                switch (addressType)
                {
                    case "data":
                        ((AddressCheatOperator)Destination).ParseOldFormat(cheat_elements, ref start_idx);
                        break;
                    default:
                        Destination.Parse(cheat_elements, ref start_idx, true);
                        break;
                }
            this.AddressType = addressType;
            start_idx += 2;

            Source = new BatchCodeCheatOperator(Destination, this.ProcessManager);
            Source.Parse(cheat_elements, ref start_idx, false);

            ulong flag = ulong.Parse(cheat_elements[start_idx], NumberStyles.HexNumber);

            Lock = flag == 1 ? true : false;

            Description = cheat_elements[start_idx + 1];

            return true;
        }

        public bool Execute()
        {
            BatchCodeCheatOperator batchOperator = (BatchCodeCheatOperator)this.Source;
            return batchOperator.WriteData();
        }

        public bool Reset()
        {
            BatchCodeCheatOperator batchOperator = (BatchCodeCheatOperator)this.Source;
            return batchOperator.ResetData();
        }

        public override string ToString()
        {
            string save_buf = "";
            save_buf += "@batchcode|";
            save_buf += this.AddressType+"|";
            switch(this.AddressType)
            {
                case "data":
                    save_buf += ((AddressCheatOperator)Destination).DumpOldFormat();break;
                default:
                    save_buf += Destination.Dump(true) + "|";break;
            }
            save_buf += "code||";
            save_buf += Source.ToString(true)+"|";
            save_buf += (Lock ? "1" : "0") + "|";
            save_buf += Description + "|";
            save_buf += "\n";
            return save_buf;
        }
    }

    public class CheatCodeOperator : CheatOperator
    {
        private const int DATA_TYPE = 0;
        private const int DATA = 1;

        private byte[] data;

        public CheatCodeOperator(string data, ValueType valueType, ProcessManager processManager)
            : base(valueType, processManager)
        {
            this.data = MemoryHelper.StringToBytes(data);
            CheatOperatorType = CheatOperatorType.CHEAT_CODE_TYPE;
        }

        public CheatCodeOperator(byte[] data, ValueType valueType, ProcessManager processManager)
            : base(valueType, processManager)
        {
            this.data = data;
            CheatOperatorType = CheatOperatorType.CHEAT_CODE_TYPE;
        }

        public CheatCodeOperator(ProcessManager processManager)
            : base(ValueType.NONE_TYPE, processManager)
        {
            CheatOperatorType = CheatOperatorType.CHEAT_CODE_TYPE;
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
            ValueType = MemoryHelper.GetValueTypeByString(cheat_elements[start_idx + DATA_TYPE]);
            data = MemoryHelper.StringToBytes(cheat_elements[start_idx + DATA]);
            start_idx += 2;
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

    public class CheatCodeCheat : Cheat
    {
        public CheatCodeCheat(ProcessManager ProcessManager)
            : base(ProcessManager)
        {
            CheatType = CheatType.CHEAT_CODE_TYPE;
            AllowLock = true;
        }

        public CheatCodeCheat(CheatOperator source, CheatOperator dest, bool lock_, string description, ProcessManager processManager)
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
}
