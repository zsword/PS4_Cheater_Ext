// Decompiled with JetBrains decompiler
// Type: debugwatch.DebugWatchForm
// Assembly: dbgw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C42DB83B-FBD0-4471-97D2-F43102A97A5F
// Assembly location: C:\Users\x\Downloads\PS4\debugwatch\dbgw\dbgw.exe

using Be.Windows.Forms;
using libdebug;
using SharpDisasm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace debugwatch
{
    public partial class DebugWatchForm : Form
    {
        private PS4DBG ps4;
        private ProcessList proclist;
        private int attachpid;
        private MemoryMapView mapview;
        private byte[] data;
        private MemoryScanner.SCAN_TYPE lastScanType = MemoryScanner.SCAN_TYPE.LONG;

        private ulong address
        {
            get { return Convert.ToUInt64(this.AddressTextBox.Text.Trim().Replace("0x", ""), 16); }
        }

        private int length
        {
            get { return Convert.ToInt32(this.LengthTextBox.Text.Trim().Replace("0x", ""), 16); }
        }

        public DebugWatchForm()
        {
            this.InitializeComponent();
            this.IpTextBox.Text = Settings.ip;
            this.FilterProcessListCheckBox.Checked = Settings.filter;
            this.WatchpointLengthComboBox.SelectedIndex = 0;
            this.BreaktypeComboBox.SelectedIndex = 0;
            this.ScanTypeComboBox.SelectedIndex = 0;
            Singleton = this;
        }

        private T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] objArray = new T[length];
            Array.Copy((Array) data, index, (Array) objArray, 0, length);
            return objArray;
        }

        private void DebugWatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ps4 != null && this.ps4.IsConnected)
            {
                if (this.ps4.IsDebugging)
                    this.ps4.DetachDebugger();
                if(this.Owner == null) {
                    this.ps4.Disconnect();
                }
            }

            Settings.ip = this.IpTextBox.Text;
            Settings.filter = this.FilterProcessListCheckBox.Checked;
            if (this.Owner == null)
            {
                Application.Exit();
            }
        }

        private string HexDump(byte[] bytes, int bytesPerLine = 16)
        {
            if (bytes == null)
                return "<null>";
            int length = bytes.Length;
            char[] charArray1 = "0123456789ABCDEF".ToCharArray();
            int num1 = 11;
            int num2 = num1 + bytesPerLine * 3 + (bytesPerLine - 1) / 8 + 2;
            int num3 = num2 + bytesPerLine + Environment.NewLine.Length;
            char[] charArray2 =
                (new string(' ', num3 - Environment.NewLine.Length) + Environment.NewLine).ToCharArray();
            StringBuilder stringBuilder = new StringBuilder((length + bytesPerLine - 1) / bytesPerLine * num3);
            for (int index1 = 0; index1 < length; index1 += bytesPerLine)
            {
                charArray2[0] = charArray1[index1 >> 28 & 15];
                charArray2[1] = charArray1[index1 >> 24 & 15];
                charArray2[2] = charArray1[index1 >> 20 & 15];
                charArray2[3] = charArray1[index1 >> 16 & 15];
                charArray2[4] = charArray1[index1 >> 12 & 15];
                charArray2[5] = charArray1[index1 >> 8 & 15];
                charArray2[6] = charArray1[index1 >> 4 & 15];
                charArray2[7] = charArray1[index1 & 15];
                int index2 = num1;
                int index3 = num2;
                for (int index4 = 0; index4 < bytesPerLine; ++index4)
                {
                    if (index4 > 0 && (index4 & 7) == 0)
                        ++index2;
                    if (index1 + index4 >= length)
                    {
                        charArray2[index2] = ' ';
                        charArray2[index2 + 1] = ' ';
                        charArray2[index3] = ' ';
                    }
                    else
                    {
                        byte num4 = bytes[index1 + index4];
                        charArray2[index2] = charArray1[(int) num4 >> 4 & 15];
                        charArray2[index2 + 1] = charArray1[(int) num4 & 15];
                        charArray2[index3] = num4 < (byte) 32 ? '·' : (char) num4;
                    }

                    index2 += 3;
                    ++index3;
                }

                stringBuilder.Append(charArray2);
            }

            return stringBuilder.ToString();
        }

        private string[] GetDisassembly(ulong address, byte[] data)
        {
            List<string> stringList = new List<string>();
            ArchitectureMode architecture = ArchitectureMode.x86_64;
            Disassembler.Translator.IncludeAddress = true;
            Disassembler.Translator.IncludeBinary = true;
            foreach (Instruction instruction in new Disassembler(data, architecture, address, true, Vendor.Any, 0UL)
                .Disassemble())
                stringList.Add(instruction.ToString());
            return stringList.ToArray();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Yes;            
            if (this.ps4 == null)
            {
                this.ps4 = new PS4DBG(this.IpTextBox.Text);
                this.ps4.Connect();
            } 
            /*else if (this.ps4 != null && this.ps4.IsConnected) {
                    dialogResult = MessageBox.Show("You are already connected...\nWould you like to reconnect?",
                        "Debug Watch", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            }*/
            if (dialogResult != DialogResult.Yes)
                return;
            this.ps4.Notify(210, "debug watch");
            this.AttachButton.Enabled = true;
            this.RefreshButton.Enabled = true;
            this.RebootButton.Enabled = true;
            this.MainPanel.Enabled = false;
            Settings.ip = this.IpTextBox.Text;
            Settings.filter = this.FilterProcessListCheckBox.Checked;
            this.RefreshButton_Click((object) null, (EventArgs) null);
        }

        private void AttachButton_Click(object sender, EventArgs e)
        {
            if (this.ProcessComboBox.Text.Contains(":"))
            {
                string[] strArray = this.ProcessComboBox.Text.Split(':');
                int int32 = Convert.ToInt32(strArray[0], 10);
                this.ps4.AttachDebugger(int32, new PS4DBG.DebuggerInterruptCallback(this.DebuggerInterruptCallback));
                this.attachpid = int32;
                this.mapview = new MemoryMapView(ps4.GetProcessInfo(attachpid), this.ps4.GetProcessMaps(attachpid));
                this.ps4.Notify(222, "attached to " + strArray[1]);
                this.MainPanel.Enabled = true;
                this.AttachButton.Enabled = false;
                this.DetachButton.Enabled = true;
            }
            else
            {
                int num = (int) MessageBox.Show("Please select a process in the list! Or press refresh then select!",
                    "Debug Watch", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DetachButton_Click(object sender, EventArgs e)
        {
            if (!this.ps4.IsDebugging)
                return;
            try
            {
                this.ps4.DetachDebugger();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.MainPanel.Enabled = false;
            this.AttachButton.Enabled = true;
            this.DetachButton.Enabled = false;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[2]
            {
                "eboot",
                "default"
            };
            this.proclist = this.ps4.GetProcessList();
            this.ProcessComboBox.Items.Clear();
            foreach (libdebug.Process process in this.proclist.processes)
            {
                if (this.FilterProcessListCheckBox.Checked)
                {
                    foreach (string str in strArray)
                    {
                        if (process.name.Contains(str))
                            this.ProcessComboBox.Items.Add((object) (process.pid.ToString() + ":" + process.name));
                    }
                }
                else
                    this.ProcessComboBox.Items.Add((object) (process.pid.ToString() + ":" + process.name));
            }

            if (this.ProcessComboBox.Items.Count <= 0)
                return;
            this.ProcessComboBox.SelectedIndex = 0;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            this.ps4.ProcessResume();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            this.ps4.ProcessStop();
        }

        private void DebuggerInterruptCallback(
            uint lwpid,
            uint status,
            string tdname,
            regs regs,
            fpregs fpregs,
            dbregs dbregs)
        {
            var tabControlDelegate =
                // new Action<object, object>((_param1, _param2) => this.TabControl.SelectedIndex = 0);
                new Action(() => this.TabControl.SelectedIndex = 0);
            var registersTextBoxDelegate = new Action(() =>
                this.RegistersTextBox.Text = "r15 = 0x" + regs.r_r15.ToString("X") +
                                             ", r14 = 0x" + regs.r_r14.ToString("X") +
                                             ", r13 = 0x" + regs.r_r13.ToString("X") +
                                             ", r12 = 0x" + regs.r_r12.ToString("X") +
                                             ", r11 = 0x" + regs.r_r11.ToString("X") +
                                             ", r10 = 0x" + regs.r_r10.ToString("X") +
                                             ", r9 = 0x" + regs.r_r9.ToString("X") +
                                             ", r8 = 0x" + regs.r_r8.ToString("X") +
                                             ", rdi = 0x" + regs.r_rdi.ToString("X") +
                                             ", rsi = 0x" + regs.r_rsi.ToString("X") +
                                             ", rbp = 0x" + regs.r_rbp.ToString("X") +
                                             ", rbx = 0x" + regs.r_rbx.ToString("X") +
                                             ", rdx = 0x" + regs.r_rdx.ToString("X") +
                                             ", rcx = 0x" + regs.r_rcx.ToString("X") +
                                             ", rax = 0x" + regs.r_rax.ToString("X") +
                                             ", trapno = 0x" + regs.r_trapno.ToString("X") +
                                             ", fs = 0x" + regs.r_fs.ToString("X") +
                                             ", gs = 0x" + regs.r_gs.ToString("X") +
                                             ", err = 0x" + regs.r_err.ToString("X") +
                                             ", es = 0x" + regs.r_es.ToString("X") +
                                             ", ds = 0x" + regs.r_ds.ToString("X") +
                                             ", rip = 0x" + regs.r_rip.ToString("X") +
                                             ", cs = 0x" + regs.r_cs.ToString("X") +
                                             ", rflags = 0x" + regs.r_rflags.ToString("X") +
                                             ", rsp = 0x" + regs.r_rsp.ToString("X") +
                                             ", ss = 0x" + regs.r_ss.ToString("X"));
            var addressTextBoxDelegate = new Action(() =>
                this.AddressTextBox.Text = "0x" + regs.r_rip.ToString("X"));
            var tryFindButtonDelegate = new Action(() =>
                this.TryFindButton_Click((object) null, (EventArgs) null));

            this.ps4.Notify(222, "interrupt hit\n(thread: " + tdname + " id: " + (object) lwpid + ")");
            this.TabControl.Invoke(tabControlDelegate);
            this.RegistersTextBox.Invoke(registersTextBoxDelegate);
            this.AddressTextBox.Invoke(addressTextBoxDelegate);
            this.TryFindButton.Invoke(tryFindButtonDelegate);
            this.data = this.ps4.ReadMemory(this.attachpid, this.address, this.length);
            string[] lines = this.GetDisassembly(this.address, this.data);
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 1; index < lines.Length; ++index)
                stringBuilder.AppendLine(lines[index]);
            string after = stringBuilder.ToString();
            var disassemblyTextBoxDelegate = new Action(() =>
            {
                this.DisassemblyTextBox.Clear();
                this.DisassemblyTextBox.AppendText(lines[0] + Environment.NewLine, Color.Salmon);
                this.DisassemblyTextBox.AppendText(after);
            });
            this.DisassemblyTextBox.Invoke(disassemblyTextBoxDelegate);
        }

        private void ClearBreakpoints_Click(object sender, EventArgs e)
        {
            for (int index = 0; (long) index < (long) PS4DBG.MAX_BREAKPOINTS; ++index)
                this.ps4.ChangeBreakpoint(index, false, 0UL);
        }

        private void ClearWatchPoints_Click(object sender, EventArgs e)
        {
            for (int index = 0; (long) index < (long) PS4DBG.MAX_WATCHPOINTS; ++index)
                this.ps4.ChangeWatchpoint(index, false, PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_1,
                    PS4DBG.WATCHPT_BREAKTYPE.DBREG_DR7_EXEC, 0UL);
        }

        private void SetWatchpointButton_Click(object sender, EventArgs e)
        {
            PS4DBG.WATCHPT_LENGTH watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_1;
            string text1 = this.WatchpointLengthComboBox.Text;
            if (!(text1 == "1 byte"))
            {
                if (!(text1 == "2 bytes"))
                {
                    if (!(text1 == "4 bytes"))
                    {
                        if (text1 == "8 bytes")
                            watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_8;
                    }
                    else
                        watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_4;
                }
                else
                    watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_2;
            }
            else
                watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_1;

            PS4DBG.WATCHPT_BREAKTYPE watchptBreaktype = PS4DBG.WATCHPT_BREAKTYPE.DBREG_DR7_EXEC;
            string text2 = this.BreaktypeComboBox.Text;
            if (!(text2 == "execute"))
            {
                if (!(text2 == "write"))
                {
                    if (text2 == "read/write")
                        watchptBreaktype = PS4DBG.WATCHPT_BREAKTYPE.DBREG_DR7_RDWR;
                }
                else
                    watchptBreaktype = PS4DBG.WATCHPT_BREAKTYPE.DBREG_DR7_WRONLY;
            }
            else
                watchptBreaktype = PS4DBG.WATCHPT_BREAKTYPE.DBREG_DR7_EXEC;

            this.ps4.ChangeWatchpoint((int) this.WatchpointNumericUpDown.Value, true, watchptLength, watchptBreaktype,
                this.address);
        }

        private void ClearWatchpointButton_Click(object sender, EventArgs e)
        {
            this.ps4.ChangeWatchpoint((int) this.WatchpointNumericUpDown.Value, false,
                PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_1, PS4DBG.WATCHPT_BREAKTYPE.DBREG_DR7_EXEC, 0UL);
        }

        private void SetBreakpointButton_Click(object sender, EventArgs e)
        {
            this.ps4.ChangeBreakpoint((int) this.BreakpointNumericUpDown.Value, true, this.address);
        }

        private void ClearBreakpoint_Click(object sender, EventArgs e)
        {
            this.ps4.ChangeBreakpoint((int) this.BreakpointNumericUpDown.Value, false, 0UL);
        }

        private void RebootButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to reboot?", "Debug Watch", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            this.ps4.Reboot();
            this.RebootButton.Enabled = false;
            this.MainPanel.Enabled = false;
            this.AttachButton.Enabled = true;
            this.DetachButton.Enabled = false;
        }

        private void PeekButton_Click(object sender, EventArgs e)
        {
            this.data = this.ps4.ReadMemory(this.attachpid, this.address, this.length);
            this.DisassemblyTextBox.Clear();
            this.DisassemblyTextBox.Lines = this.GetDisassembly(this.address, this.data);
            this.MemoryHexBox.ByteProvider = (IByteProvider) new MemoryViewByteProvider(this.data);
        }

        private void PokeButton_Click(object sender, EventArgs e)
        {
            this.ps4.WriteMemory(this.attachpid, this.address,
                ((MemoryViewByteProvider) this.MemoryHexBox.ByteProvider).Bytes.ToArray());
        }

        private void TryFindButton_Click(object sender, EventArgs e)
        {
            ArchitectureMode architecture = ArchitectureMode.x86_64;
            Disassembler.Translator.IncludeAddress = true;
            Disassembler.Translator.IncludeBinary = true;
            ulong address = this.address - 50UL;
            Instruction[] array =
                new Disassembler(this.ps4.ReadMemory(this.attachpid, address, 100), architecture, address, true,
                    Vendor.Any, 0UL).Disassemble().ToArray<Instruction>();
            for (int index = 0; index < array.Length; ++index)
            {
                if ((long) array[index].PC == (long) this.address)
                {
                    this.AddressTextBox.Text = "0x" + array[index - 1].PC.ToString("X");
                    this.PeekButton_Click((object) null, (EventArgs) null);
                    break;
                }
            }
        }

        private void AutoPatchButton_Click(object sender, EventArgs e)
        {
            ArchitectureMode architecture = ArchitectureMode.x86_64;
            Disassembler.Translator.IncludeAddress = true;
            Disassembler.Translator.IncludeBinary = true;
            ulong uint64 = Convert.ToUInt64(this.AddressTextBox.Text.Trim().Replace("0x", ""), 16);
            Instruction[] array =
                new Disassembler(this.ps4.ReadMemory(this.attachpid, uint64, 50), architecture, uint64, true,
                    Vendor.Any, 0UL).Disassemble().ToArray<Instruction>();
            for (int index = 0; index < array[0].Length; ++index)
                this.ps4.WriteMemory(this.attachpid, array[0].Offset + (ulong) index, (byte) 144);
            this.PeekButton_Click((object) null, (EventArgs) null);
        }

        private void MemoryMapButton_Click(object sender, EventArgs e)
        {
            this.mapview = new MemoryMapView(this.ps4.GetProcessInfo(this.attachpid), ps4.GetProcessMaps(attachpid));
            int num = (int) this.mapview.ShowDialog();
        }

        private void KillProcessButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to kill the process?\nThis may break your game until a reboot.",
                "Debug Watch", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            this.ps4.ProcessKill();
            this.MainPanel.Enabled = false;
            this.AttachButton.Enabled = true;
            this.DetachButton.Enabled = false;
            this.RefreshButton_Click((object) null, (EventArgs) null);
        }

        private void NewScanButton_Click(object sender, EventArgs e)
        {
            this.TabControl.SelectedIndex = 2;
            this.ScanHistoryListBox.Items.Clear();
            this.ScanHistoryListBox.Items.Add((object) (this.ValueTextBox.Text + " " + this.ScanTypeComboBox.Text.ToLower()));
            this.ScanDataGridView.Rows.Clear();
            MemoryScanner.SCAN_TYPE type = MemoryScanner.StringToType(this.ScanTypeComboBox.Text);
            string str = MemoryScanner.TypeToString(type);
            byte[] numArray = (byte[]) null;

            if (isSearchValueInvalid(type)) return;

            Task.Factory.StartNew(() => searchMemeoryForValue(type, numArray, str));
        }

        private void searchMemeoryForValue(MemoryScanner.SCAN_TYPE type, byte[] numArray, string str)
        {
            this.disableInterfaceWhileSearching();

            switch (type)
            {
                case MemoryScanner.SCAN_TYPE.BYTE:
                    numArray = new byte[1]
                    {
                        Convert.ToByte(this.ValueTextBox.Text)
                    };
                    break;
                case MemoryScanner.SCAN_TYPE.SHORT:
                    numArray = BitConverter.GetBytes(Convert.ToUInt16(this.ValueTextBox.Text));
                    break;
                case MemoryScanner.SCAN_TYPE.INTEGER:
                    numArray = BitConverter.GetBytes(Convert.ToUInt32(this.ValueTextBox.Text));
                    break;
                case MemoryScanner.SCAN_TYPE.LONG:
                    numArray = BitConverter.GetBytes(Convert.ToUInt64(this.ValueTextBox.Text));
                    break;
                case MemoryScanner.SCAN_TYPE.FLOAT:
                    numArray = BitConverter.GetBytes(Convert.ToSingle(this.ValueTextBox.Text));
                    break;
                case MemoryScanner.SCAN_TYPE.DOUBLE:
                    numArray = BitConverter.GetBytes(Convert.ToDouble(this.ValueTextBox.Text));
                    break;
            }

            var memoryEntriesToSearchThrough = this.mapview.GetSelectedEntries();
            this.ScanProgressBar.Invoke((p) => p.Minimum = 0);
            this.ScanProgressBar.Invoke((p) => p.Maximum = memoryEntriesToSearchThrough.Length);
            this.ScanProgressBar.Invoke((p) => p.Value = 0);
            foreach (MemoryEntry selectedEntry in memoryEntriesToSearchThrough)
            {
                byte[] data = this.ps4.ReadMemory(this.attachpid,
                    selectedEntry.start,
                    (int) ((long) selectedEntry.end - (long) selectedEntry.start));
                if (data != null && data.Length != 0)
                {
                    // Get the numeral value of the memory segment and returns it as a string.
                    string ConvertMemorySegmentToValue(byte[] memorySegment, MemoryScanner.SCAN_TYPE scanType)
                    {
                        switch (scanType)
                        {
                            case MemoryScanner.SCAN_TYPE.BYTE:
                                return memorySegment[0].ToString();
                            case MemoryScanner.SCAN_TYPE.SHORT:
                                return BitConverter.ToInt16(memorySegment, 0).ToString();
                            case MemoryScanner.SCAN_TYPE.INTEGER:
                                return BitConverter.ToInt32(memorySegment, 0).ToString();
                            case MemoryScanner.SCAN_TYPE.LONG:
                                return BitConverter.ToInt64(memorySegment, 0).ToString();
                            case MemoryScanner.SCAN_TYPE.FLOAT:
                                return BitConverter.ToSingle(memorySegment, 0).ToString();
                            case MemoryScanner.SCAN_TYPE.DOUBLE:
                                return BitConverter.ToDouble(memorySegment, 0).ToString();
                            default:
                                throw new ArgumentOutOfRangeException(nameof(scanType), scanType, null);
                        }
                    }

                    foreach (KeyValuePair<ulong, byte[]> keyValuePair in MemoryScanner.ScanMemory(selectedEntry.start, data, type, numArray, new MemoryScanner.CompareFunction(MemoryScanner.CompareEqual)))
                    {
                        this.ScanDataGridView.Invoke((gridview )=> gridview.Rows.Add((object) ("0x" + keyValuePair.Key.ToString("X")), (object) str,
                            ConvertMemorySegmentToValue(keyValuePair.Value, type)));
                    }

                    GC.Collect();
                }
            }

            this.reEnableInterfaceAfterDoneSearching();
            this.ScanProgressBar.Invoke((p) => p.Value = 0);
            this.NextScanButton.Invoke((b) => b.Enabled = true);
        }

        private void disableInterfaceWhileSearching()
        {
            // Keep Next Scan button the same
            var cachedNextScan = NextScanButton;

            var allFields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var buttonFieldInfos = allFields.Where(field => field.FieldType == typeof(Button));
            foreach (var buttonFieldInfo in buttonFieldInfos)
            {
                var btn = (buttonFieldInfo.GetValue(this) as Button);
                btn.Invoke((b) => { b.Enabled = false; });
            }

            NextScanButton = cachedNextScan;
        }

        private void reEnableInterfaceAfterDoneSearching()
        {
            // Keep Next Scan button the same
            var cachedNextScan = NextScanButton.Enabled;

            var allFields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var buttonFieldInfos = allFields.Where(field => field.FieldType == typeof(Button));
            foreach (var buttonFieldInfo in buttonFieldInfos)
            {
                var btn = (buttonFieldInfo.GetValue(this) as Button);
                btn.Invoke((b) => b.Enabled = true);
            }

            NextScanButton.Invoke(b => b.Enabled = cachedNextScan);
        }

        private bool isSearchValueInvalid(MemoryScanner.SCAN_TYPE type)
        {
            // Validate the text entered before we go any further
            // First, check the integer isn't too small for the type and make note of what type we're searching for
            UInt64 maxValueInt = 0;
            double maxValueDouble = 0.0;
            bool isIntType = true;
            switch (type)
            {
                case MemoryScanner.SCAN_TYPE.BYTE:
                    maxValueInt = Byte.MaxValue;
                    break;
                case MemoryScanner.SCAN_TYPE.SHORT:
                    maxValueInt = UInt16.MaxValue;
                    break;
                case MemoryScanner.SCAN_TYPE.INTEGER:
                    maxValueInt = UInt32.MaxValue;
                    break;
                case MemoryScanner.SCAN_TYPE.LONG:
                    maxValueInt = UInt64.MaxValue;
                    break;
                case MemoryScanner.SCAN_TYPE.FLOAT:
                    isIntType = false;
                    maxValueDouble = float.MaxValue;
                    break;
                case MemoryScanner.SCAN_TYPE.DOUBLE:
                    isIntType = false;
                    maxValueDouble = double.MaxValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Check if we can parse the value
            ulong searchValueAsInt = 0;
            var searchValueAsDouble = 0.0;
            if ((isIntType && !UInt64.TryParse(this.ValueTextBox.Text, out searchValueAsInt)) ||
                (!isIntType && !double.TryParse(this.ValueTextBox.Text, out searchValueAsDouble)))
            {
                MessageBox.Show("Value entered could not be parsed.", "Parse Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return true;
            }

            // Check the value isn't too big
            if (isIntType ? maxValueInt < searchValueAsInt : maxValueDouble < searchValueAsDouble)
            {
                MessageBox.Show("Value entered was too large for the specified type.", "Value Too Large", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }

        private void NextScanButton_Click(object sender, EventArgs e)
        {
            this.TabControl.SelectedIndex = 2;
            this.ScanHistoryListBox.Items.Add(
                (object) (this.ValueTextBox.Text + " " + this.ScanTypeComboBox.Text.ToLower()));
            disableInterfaceWhileSearching();
            var updateValuesTask = Task.Factory.StartNew(() => recheckSavedValues());
        }

        private void recheckSavedValues()
        {
            List<string[]> strArrayList = new List<string[]>();
            this.ScanProgressBar.Invoke(s => s.Maximum = this.ScanDataGridView.Rows.Count);
            this.ScanProgressBar.Invoke(s => s.Value = 0);

            foreach (DataGridViewRow row in (IEnumerable) this.ScanDataGridView.Rows)
            {
                ulong uint64 = Convert.ToUInt64(row.Cells[0].Value.ToString().Replace("0x", ""), 16);
                MemoryScanner.SCAN_TYPE type = MemoryScanner.StringToType(row.Cells[1].Value.ToString());
                bool flag = false;
                switch (type)
                {
                    case MemoryScanner.SCAN_TYPE.BYTE:
                        if ((int) this.ps4.ReadMemory(this.attachpid, uint64, 1)[0] ==
                            (int) Convert.ToByte(this.ValueTextBox.Text))
                        {
                            flag = true;
                            break;
                        }

                        break;
                    case MemoryScanner.SCAN_TYPE.SHORT:
                        if ((int) BitConverter.ToInt16(this.ps4.ReadMemory(this.attachpid, uint64, 2), 0) ==
                            (int) Convert.ToUInt16(this.ValueTextBox.Text))
                        {
                            flag = true;
                            break;
                        }

                        break;
                    case MemoryScanner.SCAN_TYPE.INTEGER:
                        if ((int) BitConverter.ToInt32(this.ps4.ReadMemory(this.attachpid, uint64, 4), 0) ==
                            (int) Convert.ToUInt32(this.ValueTextBox.Text))
                        {
                            flag = true;
                            break;
                        }

                        break;
                    case MemoryScanner.SCAN_TYPE.LONG:
                        if ((long) BitConverter.ToInt64(this.ps4.ReadMemory(this.attachpid, uint64, 8), 0) ==
                            (long) Convert.ToUInt64(this.ValueTextBox.Text))
                        {
                            flag = true;
                            break;
                        }

                        break;
                    case MemoryScanner.SCAN_TYPE.FLOAT:
                        if (BitConverter.ToSingle(ps4.ReadMemory(this.attachpid, uint64, 4), 0) ==
                            (double) Convert.ToSingle(this.ValueTextBox.Text))
                        {
                            flag = true;
                            break;
                        }

                        break;
                    case MemoryScanner.SCAN_TYPE.DOUBLE:
                        if (BitConverter.ToDouble(this.ps4.ReadMemory(this.attachpid, uint64, 8), 0) ==
                            Convert.ToDouble(this.ValueTextBox.Text))
                        {
                            flag = true;
                            break;
                        }

                        break;
                        
                }

                if (flag)
                {
                    string[] strArray = new string[3]
                    {
                        row.Cells[0].Value.ToString(),
                        row.Cells[1].Value.ToString(),
                        this.ValueTextBox.Text
                    };
                    strArrayList.Add(strArray);
                }

                this.ScanProgressBar.Invoke(s => s.Increment(1));
            }

            this.ScanDataGridView.Invoke(s => s.Rows.Clear());
            foreach (string[] strArray in strArrayList)
            {
                this.ScanDataGridView.Invoke(s => s.Rows.Add((object)strArray[0], (object)strArray[1], (object)strArray[2]));
            }
            this.ScanProgressBar.Invoke(s => s.Value = 0);
            reEnableInterfaceAfterDoneSearching();
        }

        private void FilterProcessListCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.filter = this.FilterProcessListCheckBox.Checked;
        }

        private void ScanDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView) sender;
            if (!(dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn) || e.RowIndex < 0)
                return;
            ulong uint64 = Convert.ToUInt64(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim().Replace("0x", ""),
                16);
            ulong typeLength =
                (ulong) MemoryScanner.GetTypeLength(dataGridView.Rows[e.RowIndex].Cells[1].ToString().Trim());
            PS4DBG.WATCHPT_LENGTH watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_1;
            switch ((long) typeLength - 1L)
            {
                case 0:
                    watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_1;
                    goto case 2;
                case 1:
                    watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_2;
                    goto case 2;
                case 2:
                    this.ps4.ChangeWatchpoint((int) this.WatchpointNumericUpDown.Value, true, watchptLength,
                        PS4DBG.WATCHPT_BREAKTYPE.DBREG_DR7_WRONLY, uint64);
                    break;
                case 3:
                    watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_4;
                    goto case 2;
                default:
                    if (typeLength == 8UL)
                    {
                        watchptLength = PS4DBG.WATCHPT_LENGTH.DBREG_DR7_LEN_8;
                        goto case 2;
                    }
                    else
                        goto case 2;
            }
        }

        private void SupportLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/WXgmWFh");
        }

        private void AssembleButton_Click(object sender, EventArgs e)
        {
            ulong uint64 = Convert.ToUInt64(this.AddressTextBox.Text.Trim().Replace("0x", ""), 16);
            AssemblerView assemblerView = new AssemblerView(uint64);
            int num = (int) assemblerView.ShowDialog();
            byte[] assemblerResult = assemblerView.GetAssemblerResult();
            if (assemblerResult != null)
                this.ps4.WriteMemory(this.attachpid, uint64, assemblerResult);
            this.PeekButton_Click((object) null, (EventArgs) null);
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            int num = (int) MessageBox.Show(
                "This is really just a simple simple tool, you can do much much more with ps4debug if you want.\n\nDebug Watch, ps4debug, and jkpatch all created by golden\n\nShout out to all my testers, especially PS4 Guru, Shiningami, and Weysincha! Hit the link, to the left, and join the discord!",
                "Debug Watch", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        
        public void attachDebug(PS4DBG ps4, int pid, string ip, object sender, EventArgs e)
        {
            this.ps4 = ps4;
            this.IpTextBox.Text = ip;            
            this.ConnectButton_Click(sender, e);
            this.ConnectButton.Dispose();
            this.DetachButton.Dispose();
            if (pid > 0)
            {
                int index = this.ProcessComboBox.FindString(pid + ":");
                this.ProcessComboBox.SelectedIndex = index;
                this.AttachButton_Click(sender, e);
            }
        }
    }
}