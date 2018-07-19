using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PS4_Cheater
{
    public class DataCheatOperator : CheatOperator
    {
        private const int DATA_TYPE = 0;
        private const int DATA = 1;

        private byte[] data;

        public DataCheatOperator(string data, ValueType valueType, ProcessManager processManager)
            : base(valueType, processManager)
        {
            this.data = MemoryHelper.StringToBytes(data);
            CheatOperatorType = CheatOperatorType.DATA_TYPE;
        }

        public DataCheatOperator(byte[] data, ValueType valueType, ProcessManager processManager)
            : base(valueType, processManager)
        {
            this.data = data;
            CheatOperatorType = CheatOperatorType.DATA_TYPE;
        }

        public DataCheatOperator(ProcessManager processManager)
            : base(ValueType.NONE_TYPE, processManager)
        {
            CheatOperatorType = CheatOperatorType.DATA_TYPE;
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

    public class OffsetCheatOperator : CheatOperator
    {
        public long Offset { get; set; }

        public OffsetCheatOperator(long offset, ValueType valueType, ProcessManager processManager)
            : base(valueType, processManager)
        {
            this.Offset = offset;
            CheatOperatorType = CheatOperatorType.OFFSET_TYPE;
        }

        public OffsetCheatOperator(ProcessManager processManager)
            : base(ValueType.NONE_TYPE, processManager)
        {
            CheatOperatorType = CheatOperatorType.OFFSET_TYPE;
        }

        public override byte[] Get(int idx = 0) { return BitConverter.GetBytes(Offset); }

        public override byte[] GetRuntime() { return BitConverter.GetBytes(Offset); }

        public override void Set(CheatOperator SourceCheatOperator, int idx = 0)
        {
            Offset = BitConverter.ToInt64(SourceCheatOperator.Get(), 0);
        }

        public override void SetRuntime(CheatOperator SourceCheatOperator, int idx = 0)
        {
            Offset = BitConverter.ToInt64(SourceCheatOperator.Get(), 0);
        }

        public void Set(long offset)
        {
            this.Offset = offset;
        }

        public override string ToString(bool simple)
        {
            return Offset.ToString("X16");
        }

        public override string Display()
        {
            return Offset.ToString("X16");
        }

        public override bool Parse(string[] cheat_elements, ref int start_idx, bool simple_format)
        {
            Offset = Int64.Parse(cheat_elements[start_idx], NumberStyles.HexNumber);
            start_idx += 1;
            return true;
        }

        public override string Dump(bool simpleFormat)
        {
            string save_buf = "";
            save_buf += "+";
            save_buf += Offset.ToString("X");
            return save_buf;
        }
    }

    public class AddressCheatOperator : CheatOperator
    {
        private const int SECTION_ID = 0;
        private const int ADDRESS_OFFSET = 1;

        public ulong Address { get; set; }

        public AddressCheatOperator(ulong Address, ProcessManager processManager)
            : base(ValueType.ULONG_TYPE, processManager)
        {
            this.Address = Address;
            CheatOperatorType = CheatOperatorType.ADDRESS_TYPE;
        }

        public AddressCheatOperator(ProcessManager processManager)
            : base(ValueType.ULONG_TYPE, processManager)
        {
            CheatOperatorType = CheatOperatorType.ADDRESS_TYPE;
        }

        public override byte[] Get(int idx = 0)
        {
            return BitConverter.GetBytes(Address);
        }

        public override byte[] GetRuntime()
        {
            return MemoryHelper.ReadMemory(Address, MemoryHelper.Length);
        }

        public override int GetSectionID()
        {
            return ProcessManager.MappedSectionList.GetMappedSectionID(Address);
        }

        public override void Set(CheatOperator SourceCheatOperator, int idx = 0)
        {
            Address = BitConverter.ToUInt64(SourceCheatOperator.Get(), 0);
        }

        public override void SetRuntime(CheatOperator SourceCheatOperator, int idx = 0)
        {
            MemoryHelper.WriteMemory(Address, SourceCheatOperator.GetRuntime());
        }

        public string DumpOldFormat()
        {
            string save_buf = "";

            int sectionID = ProcessManager.MappedSectionList.GetMappedSectionID(Address);
            MappedSection mappedSection = ProcessManager.MappedSectionList[sectionID];
            save_buf += sectionID + "|";
            save_buf += String.Format("{0:X}", Address - mappedSection.Start) + "|";
            return save_buf;
        }

        public override string Dump(bool simpleFormat)
        {
            string save_buf = "";

            int sectionID = ProcessManager.MappedSectionList.GetMappedSectionID(Address);
            MappedSection mappedSection = ProcessManager.MappedSectionList[sectionID];
            save_buf += String.Format("@{0:X}", Address) + "_";
            save_buf += sectionID + "_";
            save_buf += String.Format("{0:X}", Address - mappedSection.Start);
            return save_buf;
        }

        public override bool Parse(string[] cheat_elements, ref int start_idx, bool simple_format)
        {
            if (simple_format)
            {
                string address = cheat_elements[start_idx++];
                string[] address_elements = address.Split('_');

                int sectionID = int.Parse(address_elements[1]);
                if (sectionID >= ProcessManager.MappedSectionList.Count || sectionID < 0)
                {
                    return false;
                }

                ulong addressOffset = ulong.Parse(address_elements[2], NumberStyles.HexNumber);

                Address = addressOffset + ProcessManager.MappedSectionList[sectionID].Start;
            }
            return false;
        }

        public bool ParseOldFormat(string[] cheat_elements, ref int start_idx)
        {
            int sectionID = int.Parse(cheat_elements[start_idx + SECTION_ID]);
            if (sectionID >= ProcessManager.MappedSectionList.Count || sectionID < 0)
            {
                start_idx += 2;
                return false;
            }

            ulong addressOffset = ulong.Parse(cheat_elements[start_idx + ADDRESS_OFFSET], NumberStyles.HexNumber);

            Address = addressOffset + ProcessManager.MappedSectionList[sectionID].Start;

            start_idx += 2;
            return true;
        }

        public override string Display()
        {
            return Address.ToString("X");
        }

        public override string ToString()
        {
            return Address.ToString("X");
        }
    }

    public class SimplePointerCheatOperator : CheatOperator
    {
        private AddressCheatOperator Address { get; set; }
        private List<OffsetCheatOperator> Offsets { get; set; }

        public SimplePointerCheatOperator(AddressCheatOperator Address, List<OffsetCheatOperator> Offsets, ValueType valueType, ProcessManager processManager)
            : base(valueType, processManager)
        {
            this.Address = Address;
            this.Offsets = Offsets;
            CheatOperatorType = CheatOperatorType.SIMPLE_POINTER_TYPE;
        }

        public SimplePointerCheatOperator(ProcessManager processManager)
            : base(ValueType.NONE_TYPE, processManager)
        {
            Address = new AddressCheatOperator(ProcessManager);
            Offsets = new List<OffsetCheatOperator>();

            CheatOperatorType = CheatOperatorType.SIMPLE_POINTER_TYPE;
        }

        public override byte[] Get(int idx = 0)
        {
            return Address.Get();
        }

        public override byte[] GetRuntime()
        {
            return MemoryHelper.ReadMemory(GetAddress(), MemoryHelper.Length);
        }

        public override int GetSectionID()
        {
            return ProcessManager.MappedSectionList.GetMappedSectionID(GetAddress());
        }

        public override void Set(CheatOperator SourceCheatOperator, int idx = 0)
        {
            throw new Exception("Pointer Set!!");
        }

        public ulong GetAddress()
        {
            ulong address = BitConverter.ToUInt64(Address.GetRuntime(), 0);
            int i = 0;
            for (; i < Offsets.Count - 1; ++i)
            {
                Byte[] new_address = MemoryHelper.ReadMemory((ulong)((long)address + Offsets[i].Offset), 8);
                address = BitConverter.ToUInt64(new_address, 0);
            }

            if (i < Offsets.Count)
            {
                address += (ulong)Offsets[i].Offset;
            }

            return address;
        }

        public override void SetRuntime(CheatOperator SourceCheatOperator, int idx = 0)
        {
            byte[] buf = new byte[MemoryHelper.Length];
            Buffer.BlockCopy(SourceCheatOperator.GetRuntime(), 0, buf, 0, MemoryHelper.Length);

            MemoryHelper.WriteMemory(GetAddress(), buf);
        }

        public override bool Parse(string[] cheat_elements, ref int start_idx, bool simple_format)
        {
            ValueType = MemoryHelper.GetValueTypeByString(cheat_elements[start_idx + 0]);
            string pointer_str = cheat_elements[start_idx + 1];
            int pointer_idx = 0;
            string[] pointer_list = pointer_str.Split('+');

            Address.Parse(pointer_list, ref pointer_idx, simple_format);

            for (int i = 1; i < pointer_list.Length; ++i)
            {
                OffsetCheatOperator offset = new OffsetCheatOperator(ProcessManager);
                offset.Parse(pointer_list, ref pointer_idx, simple_format);
                Offsets.Add(offset);
            }

            start_idx += 2;

            return true;
        }

        public override string Display()
        {
            return "p->" + GetAddress().ToString("X");
        }

        public override string Dump(bool simpleFormat)
        {
            string dump_buf = "";

            dump_buf += MemoryHelper.GetStringOfValueType(ValueType) + "|";
            dump_buf += Address.Dump(simpleFormat);
            for (int i = 0; i < Offsets.Count; ++i)
            {
                dump_buf += Offsets[i].Dump(simpleFormat);
            }
            return dump_buf;
        }
    }

    public enum ArithmeticType
    {
        ADD_TYPE,
        SUB_TYPE,
        MUL_TYPE,
        DIV_TYPE,
    }

    public class BinaryArithmeticCheatOperator : CheatOperator
    {
        public CheatOperator Left { get; set; }
        public CheatOperator Right { get; set; }

        private ArithmeticType ArithmeticType { get; set; }

        public BinaryArithmeticCheatOperator(CheatOperator left, CheatOperator right, ArithmeticType ArithmeticType,
            ProcessManager processManager)
            : base(left.ValueType, processManager)
        {
            Left = left;
            Right = right;
            this.ArithmeticType = ArithmeticType;
            CheatOperatorType = CheatOperatorType.ARITHMETIC_TYPE;
        }

        public override byte[] Get(int idx)
        {
            if (idx == 0) return Left.Get();
            return Right.Get();
        }

        public byte[] GetRuntime(int idx)
        {
            byte[] left_buf = new byte[MemoryHelper.Length];
            Buffer.BlockCopy(Left.Get(), 0, left_buf, 0, MemoryHelper.Length);
            byte[] right_buf = new byte[MemoryHelper.Length];
            Buffer.BlockCopy(Right.Get(), 0, right_buf, 0, MemoryHelper.Length);
            ulong left = BitConverter.ToUInt64(left_buf, 0);
            ulong right = BitConverter.ToUInt64(right_buf, 0);
            ulong result = 0;

            switch (ArithmeticType)
            {
                case ArithmeticType.ADD_TYPE:
                    result = left + right;
                    break;
                case ArithmeticType.SUB_TYPE:
                    result = left - right;
                    break;
                case ArithmeticType.MUL_TYPE:
                    result = left * right;
                    break;
                case ArithmeticType.DIV_TYPE:
                    result = left / right;
                    break;
                default:
                    throw new Exception("ArithmeticType!!!");
            }
            return MemoryHelper.StringToBytes(result.ToString());
        }

        public override void Set(CheatOperator SourceCheatOperator, int idx = 0)
        {
            throw new Exception("Set BinaryArithmeticCheatOperator");
        }

        public override void SetRuntime(CheatOperator SourceCheatOperator, int idx = 0)
        {
            throw new Exception("SetRuntime BinaryArithmeticCheatOperator");
        }

        public override bool Parse(string[] cheat_elements, ref int start_idx, bool simple_format)
        {
            if (Left.Parse(cheat_elements, ref start_idx, simple_format))
            {
                return false;
            }

            switch (cheat_elements[start_idx])
            {
                case "+":
                    ArithmeticType = ArithmeticType.ADD_TYPE;
                    break;
                case "-":
                    ArithmeticType = ArithmeticType.SUB_TYPE;
                    break;
                case "*":
                    ArithmeticType = ArithmeticType.MUL_TYPE;
                    break;
                case "/":
                    ArithmeticType = ArithmeticType.DIV_TYPE;
                    break;
                default:
                    throw new Exception("ArithmeticType parse!!!");
            }
            ++start_idx;

            if (Right.Parse(cheat_elements, ref start_idx, simple_format))
            {
                return false;
            }

            return true;
        }

        public override string Display()
        {
            return "";
        }

        public override string Dump(bool simpleFormat)
        {
            return Left.Dump(simpleFormat) + Right.Dump(simpleFormat);
        }
    }
}
