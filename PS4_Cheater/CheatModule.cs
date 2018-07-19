using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS4_Cheater
{
    public enum CheatType
    {
        DATA_TYPE,
        SIMPLE_POINTER_TYPE,
        NONE_TYPE,
        BATCH_CODE_TYPE,
        CHEAT_CODE_TYPE,
    }

    public enum CheatOperatorType
    {
        DATA_TYPE,
        OFFSET_TYPE,
        ADDRESS_TYPE,
        SIMPLE_POINTER_TYPE,
        POINTER_TYPE,
        ARITHMETIC_TYPE,
        BATCH_CODE_TYPE,
        CHEAT_CODE_TYPE
    }

    public enum ToStringType
    {
        DATA_TYPE,
        ADDRESS_TYPE,
        ARITHMETIC_TYPE,
        BATCH_CODE_TYPE,
        CHEAT_CODE_TYPE
    }

    public class CheatOperator
    {
        public CheatOperator(ValueType valueType, ProcessManager processManager)
        {
            ProcessManager = processManager;
            ValueType = valueType;
        }

        private ValueType _valueType;

        protected MemoryHelper MemoryHelper = new MemoryHelper(true, 0);

        public ProcessManager ProcessManager { get; set; }

        public ValueType ValueType
        {
            get
            {
                return _valueType;
            }
            set
            {
                _valueType = value;
                MemoryHelper.InitMemoryHandler(ValueType, CompareType.NONE, false);
            }
        }
        public CheatOperatorType CheatOperatorType { get; set; }

        public virtual byte[] Get(int idx = 0) { return null; }

        public virtual byte[] GetRuntime() { return null; }

        public virtual void Set(CheatOperator SourceCheatOperator, int idx = 0) { }

        public virtual void SetRuntime(CheatOperator SourceCheatOperator, int idx = 0) { }

        public virtual int GetSectionID() { return -1; }

        public virtual bool Parse(string[] cheat_elements, ref int start_idx, bool simple_format) { return false; }

        public virtual string ToString(bool simple) { return null; }

        public virtual string Dump(bool simpleFormat) { return null; }

        public virtual string Display() { return null; }
    }

    public interface ICodeCheat
    {

    }

    public class Cheat : ICodeCheat
    {

        public CheatType CheatType { get; set; }

        protected ProcessManager ProcessManager;

        public string Description { get; set; }

        public bool Lock { get; set; }

        public bool AllowLock { get; set; }

        public virtual bool Parse(string[] cheat_elements)
        {
            return false;
        }

        public Cheat(ProcessManager ProcessManager)
        {
            this.ProcessManager = ProcessManager;
        }

        protected CheatOperator Source { get; set; }
        protected CheatOperator Destination { get; set; }

        public CheatOperator GetSource()
        {
            return Source;
        }

        public CheatOperator GetDestination()
        {
            return Destination;
        }
    }

    public class PointerResult
    {
        public int BaseSectionID { get; }
        public ulong BaseOffset { get; }
        public long[] Offsets { get; }

        public PointerResult(int BaseSectionID, ulong BaseOffset, List<long> Offsets)
        {
            this.BaseSectionID = BaseSectionID;
            this.BaseOffset = BaseOffset;
            this.Offsets = new long[Offsets.Count];
            for (int i = 0; i < this.Offsets.Length; ++i)
            {
                this.Offsets[i] = Offsets[this.Offsets.Length - 1 - i];
            }

        }

        public ulong GetBaseAddress(MappedSectionList mappedSectionList)
        {
            if (BaseSectionID >= mappedSectionList.Count)
                return 0;

            MappedSection section = mappedSectionList[BaseSectionID];

            return section.Start + BaseOffset;
        }
    }
}
