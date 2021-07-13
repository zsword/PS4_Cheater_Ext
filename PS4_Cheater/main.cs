﻿namespace PS4_Cheater
{
    using librpc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Linq;
    using System.Threading;
    using System.Reflection;

    public partial class main : Form
    {
        private ProcessManager processManager = new ProcessManager();
        private MemoryHelper memoryHelper = new MemoryHelper(true, 0);
        private CheatList cheatList = new CheatList();
        private CheatPluginHelper cheatPluginHelper = new CheatPluginHelper();

        private const int CHEAT_LIST_DEL = 0;
        private const int CHEAT_LIST_ADDRESS = 1;
        private const int CHEAT_LIST_TYPE = 2;
        private const int CHEAT_LIST_ENABLED = 3;
        private const int CHEAT_LIST_VALUE = 4;
        private const int CHEAT_LIST_SECTION = 5;
        private const int CHEAT_LIST_LOCK = 6;
        private const int CHEAT_LIST_DESC = 7;

        private const int RESULT_LIST_ADDRESS = 0;
        private const int RESULT_LIST_TYPE = 1;
        private const int RESULT_LIST_VALUE = 2;
        private const int RESULT_LIST_SECTION = 4;

        private const int VERSION_LIST_405 = 2;
        private const int VERSION_LIST_455 = 1;
        private const int VERSION_LIST_505 = 0;

        private const int VERSION_LIST_DEFAULT = VERSION_LIST_505;

        private string[] SEARCH_BY_FLOAT_FIRST = new string[]
        {
             CONSTANT.EXACT_VALUE,
             CONSTANT.FUZZY_VALUE,
             CONSTANT.BIGGER_THAN,
             CONSTANT.SMALLER_THAN,
             CONSTANT.BETWEEN_VALUE,
             CONSTANT.UNKNOWN_INITIAL_VALUE
        };

        private string[] SEARCH_BY_BYTES_FIRST = new string[]
        {
            CONSTANT.EXACT_VALUE,
            CONSTANT.BIGGER_THAN,
            CONSTANT.SMALLER_THAN,
            CONSTANT.BETWEEN_VALUE,
            CONSTANT.UNKNOWN_INITIAL_VALUE
        };

        private string[] SEARCH_BY_FLOAT_NEXT = new string[]
        {
             CONSTANT.EXACT_VALUE,
             CONSTANT.FUZZY_VALUE,
             CONSTANT.INCREASED_VALUE,
             CONSTANT.INCREASED_VALUE_BY,
             CONSTANT.DECREASED_VALUE,
             CONSTANT.DECREASED_VALUE_BY,
             CONSTANT.BIGGER_THAN,
             CONSTANT.SMALLER_THAN,
             CONSTANT.CHANGED_VALUE,
             CONSTANT.UNCHANGED_VALUE,
             CONSTANT.BETWEEN_VALUE,
        };

        private string[] SEARCH_BY_BYTES_NEXT = new string[]
        {
            CONSTANT.EXACT_VALUE,
            CONSTANT.INCREASED_VALUE,
            CONSTANT.INCREASED_VALUE_BY,
            CONSTANT.DECREASED_VALUE,
            CONSTANT.DECREASED_VALUE_BY,
            CONSTANT.BIGGER_THAN,
            CONSTANT.SMALLER_THAN,
            CONSTANT.CHANGED_VALUE,
            CONSTANT.UNCHANGED_VALUE,
            CONSTANT.BETWEEN_VALUE,
        };

        private string[] SEARCH_BY_HEX = new string[]
        {
            CONSTANT.EXACT_VALUE
        };

        public main()
        {
            this.InitializeComponent();
            this.cheatPluginHelper.loadPlugins();
        }

        private void main_Load(object sender, EventArgs e)
        {
            langCombo.Items.Add("EN");
            List<String> locales = LangHelper.GetLocales();
            string language = Config.getSetting("language");
            int idx =   1;
            int langId = 0;
            foreach (string lang in locales)
            {
                langCombo.Items.Add(lang);
                if (lang.Equals(language)) {
                    langId = idx;
                }
                idx++;
            }
            this.langCombo.SelectedIndex = langId;
            valueTypeList.SelectedIndex = 2;

            this.Text += " " + CONSTANT.MAJOR_VERSION + "." + CONSTANT.SECONDARY_VERSION + "." + CONSTANT.THIRD_VERSION + " (Ext)";
            string version = Config.getSetting("ps4 version");
            string ip = Config.getSetting("ip");
			if (version == "5.05")
            {
                version_list.SelectedIndex = VERSION_LIST_505;
                Util.Version = 505;
            }
            else if (version == "4.05")
            {
                version_list.SelectedIndex = VERSION_LIST_405;
                Util.Version = 405;
            }
            else if (version == "4.55")
            {
                Util.Version = 455;
                version_list.SelectedIndex = VERSION_LIST_455;
            }
            else
            {
                Util.Version = 505;
                version_list.SelectedIndex = VERSION_LIST_DEFAULT;
            }

            if (!string.IsNullOrEmpty(ip))
            {
                ip_box.Text = ip;
            }

        }

        private string GetLang(string text)
        {
            return LangHelper.GetLang(text);
        }

        public void SetLocale(string locale)
        {
            LangHelper.SetLocale(locale);
            Config.updateSeeting("language", locale);
            string[] values = LangHelper.GetLangs(CONSTANT.SEARCH_VALUE_TYPE);
            valueTypeList.Items.AddRange(values);

            this.conn_btn.Text = GetLang(CONSTANT.CONNECT_VALUE);
            this.next_scan_btn.Text = GetLang(CONSTANT.NEXT_SCAN);
            this.new_scan_btn.Text = GetLang(CONSTANT.FIRST_SCAN);
            this.refresh_btn.Text = GetLang(CONSTANT.REFRESH);
            
            this.select_all.Text = GetLang("Select All");
            this.alignment_box.Text = GetLang("Alignment");
            this.value_1_label.Text = GetLang("Value:");
            this.value_label.Text = GetLang("Value:");
            this.hex_box.Text = GetLang("Hex");
            this.sectionfilter_button.Text = GetLang("Filter");

            this.send_payload_btn.Text = GetLang("Send Payload");
            this.get_processes_btn.Text = GetLang("Refresh Processes");

            this.new_cheat_list_btn.Text = GetLang("New");
            this.refresh_cheat_list_btn.Text = GetLang("Refresh");
            this.save_cheat_list_btn.Text = GetLang("Save");
            this.load_cheat_list_btn.Text = GetLang("Load");

            this.section_view_menu.Text = GetLang("Hex Editor");
            this.section_dump_menu.Text = GetLang("Dump");

            updateMessage("Messages");

            this.result_list_view_add_to_cheat_list.Text = GetLang("Add to Cheat List");
            this.result_list_view_view_item.Text = GetLang("Hex Editor");
            this.result_list_view_dump_item.Text = GetLang("Dump");

            this.result_list_view_address.Text = GetLang("Address");
            this.result_list_view_type.Text = GetLang("Type");
            this.result_list_view_value.Text = GetLang("Value");
            this.result_list_view_hex.Text = GetLang("Hex");
            this.result_list_view_section.Text = GetLang("Section");

            this.cheat_list_view_del.HeaderText = GetLang("Delete");
            this.cheat_list_view_address.HeaderText = GetLang("Address");
            this.cheat_list_view_type.HeaderText = GetLang("Type");
            this.cheat_list_view_active.HeaderText = GetLang("X");
            this.cheat_list_view_value.HeaderText = GetLang("Value");
            this.cheat_list_view_section.HeaderText = GetLang("Section");
            this.cheat_list_view_lock.HeaderText = GetLang("Lock");
            this.cheat_list_view_description.HeaderText = GetLang("Description");

            this.cheat_list_item_hex_view.Text = GetLang("Hex Editor");
            this.cheat_list_item_lock.Text = GetLang("Lock");
            this.cheat_list_item_unlock.Text = GetLang("Unlock");
            this.cheat_list_item_active.Text = GetLang("Active");
            this.cheat_list_item_find_pointer.Text = GetLang("Find Pointer");
            this.cheat_list_item_delete.Text = GetLang("Delete");
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {

            string ip = Config.getSetting("ip");
            string version = "";
            switch (version_list.SelectedIndex)
            {
                case VERSION_LIST_405:
                    version = "4.05";
                    break;
                case VERSION_LIST_455:
                    version = "4.55";
                    break;
				case VERSION_LIST_505:
                    version = "5.05";
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(version))
            {
                Config.updateSeeting("ps4 version", version);
            }

            if (!string.IsNullOrWhiteSpace(ip_box.Text))
            {
                Config.updateSeeting("ip", ip_box.Text);
            }

            //MemoryHelper.Disconnect();
        }

        class WorkerReturn
        {
            public List<ListViewItem> ListViewItems { get; set; }
            public bool[] MappedSectionCheckeSet { get; set; }
            public ulong Results { get; set; }
        }

        private void update_result_list_view(BackgroundWorker worker, bool refresh, int start, float percent)
        {
            worker.ReportProgress(start);

            List<ListViewItem> listViewItems = new List<ListViewItem>();
            bool[] mappedSectionCheckeSet = new bool[processManager.MappedSectionList.Count];

            ulong totalResultCount = processManager.MappedSectionList.TotalResultCount();
            ulong curResultCount = 0;
            string value_type = MemoryHelper.GetStringOfValueType(memoryHelper.ValueType);

            const int MAX_RESULTS_NUM = 0x1000;

            for (int idx = 0; idx < processManager.MappedSectionList.Count; ++idx)
            {
                MappedSection mapped_section = processManager.MappedSectionList[idx];
                ResultList result_list = mapped_section.ResultList;
                if (result_list == null)
                {
                    continue;
                }
                if (!mapped_section.Check)
                {
                    continue;
                }

                mappedSectionCheckeSet[idx] = result_list.Count > 0;

                for (result_list.Begin(); !result_list.End(); result_list.Next())
                {
                    if (curResultCount >= MAX_RESULTS_NUM)
                    {
                        break;
                    }

                    uint memory_address_offset = 0;
                    byte[] memory_value = null;

                    result_list.Get(ref memory_address_offset, ref memory_value);

                    curResultCount++;
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = String.Format("{0:X}", memory_address_offset + mapped_section.Start);

                    if (refresh && !worker.CancellationPending)
                    {
                        memory_value = memoryHelper.GetBytesByType(memory_address_offset + mapped_section.Start);
                        result_list.Set(memory_value);
                        worker.ReportProgress(start + (int)(100.0f * curResultCount / MAX_RESULTS_NUM));
                    }

                    lvi.SubItems.Add(value_type);
                    lvi.SubItems.Add(memoryHelper.BytesToString(memory_value));
                    lvi.SubItems.Add(memoryHelper.BytesToHexString(memory_value));
                    lvi.SubItems.Add(processManager.MappedSectionList.GetSectionName(idx));

                    listViewItems.Add(lvi);
                }
            }

            WorkerReturn workerReturn = new WorkerReturn();
            workerReturn.ListViewItems = listViewItems;
            workerReturn.MappedSectionCheckeSet = mappedSectionCheckeSet;
            workerReturn.Results = totalResultCount;

            worker.ReportProgress(start + (int)(100 * percent), workerReturn);
        }


        void setButtons(bool enabled)
        {
            new_scan_btn.Enabled = enabled;
            refresh_btn.Enabled = enabled;
            next_scan_btn.Enabled = enabled;
            processes_comboBox.Enabled = enabled;
            get_processes_btn.Enabled = enabled;
            section_list_menu.Enabled = enabled;
            compareTypeList.Enabled = enabled;
        }

        private void new_scan_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (new_scan_btn.Text == GetLang(CONSTANT.FIRST_SCAN))
                {
                    if (MessageBox.Show("search size:" + (processManager.MappedSectionList.TotalMemorySize / 1024).ToString() + "KB") != DialogResult.OK)
                    {
                        return;
                    }

                    memoryHelper.InitMemoryHandler((string)valueTypeList.SelectedItem,
                        (string)compareTypeList.SelectedItem, alignment_box.Checked, value_box.Text.Length);

                    setButtons(false);
                    new_scan_btn.Enabled = true;
                    valueTypeList.Enabled = false;
                    alignment_box.Enabled = false;
                    //section_list_box.lo = false;

                    new_scan_worker.RunWorkerAsync();

                    new_scan_btn.Text = GetLang(CONSTANT.STOP);

                    sectionfilter_box.Enabled = false;
                    sectionfilter_button.Enabled = false;
                    sectionsize_box.Enabled = false;
                }
                else if (new_scan_btn.Text == GetLang(CONSTANT.NEW_SCAN))
                {
                    valueTypeList.Enabled = true;
                    alignment_box.Enabled = true;
                    //section_list_box.Enabled = true;
                    refresh_btn.Enabled = false;
                    next_scan_btn.Enabled = false;
                    new_scan_btn.Text = GetLang(CONSTANT.FIRST_SCAN);

                    result_list_view.Items.Clear();
                    processManager.MappedSectionList.ClearResultList();
                    InitCompareTypeListOfFirstScan();

                    sectionfilter_box.Enabled = true;
                    sectionfilter_button.Enabled = true;
                    sectionsize_box.Enabled = true;

                }
                else if (new_scan_btn.Text == GetLang(CONSTANT.STOP))
                {
                    new_scan_worker.CancelAsync();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (refresh_btn.Text == GetLang(CONSTANT.REFRESH))
                {
                    setButtons(false);
                    refresh_btn.Enabled = true;
                    refresh_btn.Text = GetLang(CONSTANT.STOP);
                    update_result_list_worker.RunWorkerAsync();
                }
                else if (refresh_btn.Text == GetLang(CONSTANT.STOP))
                {
                    update_result_list_worker.CancelAsync();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void next_scan_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (next_scan_btn.Text == GetLang(CONSTANT.NEXT_SCAN))
                {
                    memoryHelper.InitNextScanMemoryHandler((string)compareTypeList.SelectedItem);
                    setButtons(false);
                    next_scan_btn.Enabled = true;
                    next_scan_btn.Text = GetLang(CONSTANT.STOP);
                    next_scan_worker.RunWorkerAsync();
                }
                else if (next_scan_btn.Text == GetLang(CONSTANT.STOP))
                {
                    next_scan_worker.CancelAsync();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void set_section_list_box(bool check)
        {
            for (int i = 0; i < section_list_box.Items.Count; ++i)
            {
                section_list_box.SetItemChecked(i, check);
            }
        }

        private void select_all_check_box_Click(object sender, EventArgs e)
        {
            bool check = select_all.Checked;
            set_section_list_box(check);
        }

        private void update_result_list_view_ui(WorkerReturn ret)
        {
            result_list_view.Items.Clear();
            result_list_view.BeginUpdate();
            result_list_view.Items.AddRange(ret.ListViewItems.ToArray());
            result_list_view.EndUpdate();

            for (int i = 0; i < section_list_box.Items.Count; ++i)
            {
                string secName = (string)section_list_box.Items[i];
                int idx = sectionInfos.IndexOf(secName);
                section_list_box.SetItemChecked(i, ret.MappedSectionCheckeSet[idx]);
            }
            updateMessage(ret.Results + GetLang(" results"));
        }
        
        private void updateMessage(string message, string type = "info")
        {
            Color color = Color.Lime;
            switch(type)
            {
                case "info":
                    color = Color.Lime;break;
                case "error":
                    color = Color.Red;break;
                case "warning":
                    color = Color.Yellow;
                    break;
            
            }
            this.msg.ForeColor = color;
            this.msg.Text = GetLang(message);
        }

        private void scan_worker_DoWorker(object sender, DoWorkEventArgs e, bool isFirstScan)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            string value_0 = value_box.Text;
            string value_1 = value_1_box.Text;
            worker.ReportProgress(0);

            Mutex scan_wait_mutex = new Mutex();
            scan_wait_mutex.WaitOne();

            List<byte[]> buffer_queue = new List<byte[]>(CONSTANT.MAX_PEEK_QUEUE);

            Semaphore consumerMutex = new Semaphore(0, CONSTANT.MAX_PEEK_QUEUE);
            Semaphore producerMutex = new Semaphore(CONSTANT.MAX_PEEK_QUEUE, CONSTANT.MAX_PEEK_QUEUE);

            PeekThread peekThread = new PeekThread(processManager, buffer_queue, consumerMutex, producerMutex);
            ComparerThread comparer_thread = new ComparerThread(processManager, memoryHelper, buffer_queue,
                value_0, value_1, worker, consumerMutex, producerMutex, scan_wait_mutex);

            Thread producer = new Thread(peekThread.Peek);
            producer.Start();

            Thread consumer = null;
            if (isFirstScan)
            {
                consumer = new Thread(comparer_thread.ResultListOfNewScan);
            }
            else
            {
                consumer = new Thread(comparer_thread.ResultListOfNextScan);
            }
            consumer.Start();

            scan_wait_mutex.WaitOne();
            producer.Abort();
            consumer.Abort();

            worker.ReportProgress(80);

            update_result_list_view(worker, false, 80, 0.2f);
        }

        private void next_scan_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            long processed_memory_len = 0;
            ulong total_memory_size = processManager.MappedSectionList.TotalMemorySize + 1;
            string value_0 = value_box.Text;
            string value_1 = value_1_box.Text;
            next_scan_worker.ReportProgress(0);
            for (int section_idx = 0; section_idx < processManager.MappedSectionList.Count; ++section_idx)
            {
                if (next_scan_worker.CancellationPending) break;                
                MappedSection mappedSection = processManager.MappedSectionList[section_idx];
                if (!mappedSection.Check) continue;
                mappedSection.UpdateResultList(processManager, memoryHelper, value_0, value_1, hex_box.Checked, false);
                if (mappedSection.Check) processed_memory_len += mappedSection.Length;
                next_scan_worker.ReportProgress((int)(((float)processed_memory_len / total_memory_size) * 80));
            }
            next_scan_worker.ReportProgress(80);

            update_result_list_view(next_scan_worker, false, 80, 0.2f);
        }

        private void new_scan_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //scan_worker_DoWorker(sender, e, true);
            long processed_memory_len = 0;
            ulong total_memory_size = processManager.MappedSectionList.TotalMemorySize + 1;
            string value_0 = value_box.Text;
            string value_1 = value_1_box.Text;
            new_scan_worker.ReportProgress(0);
            for (int section_idx = 0; section_idx < processManager.MappedSectionList.Count; ++section_idx)
            {
                if (new_scan_worker.CancellationPending) break;
                MappedSection mappedSection = processManager.MappedSectionList[section_idx];
                if (!mappedSection.Check) continue;
                curSection = mappedSection;
                mappedSection.UpdateResultList(processManager, memoryHelper, value_0, value_1, hex_box.Checked, true);
                if (mappedSection.Check) processed_memory_len += mappedSection.Length;
                new_scan_worker.ReportProgress((int)(((float)processed_memory_len / total_memory_size) * 80));
            }
            new_scan_worker.ReportProgress(80);
            update_result_list_view(new_scan_worker, false, 80, 0.2f);
        }

        private void update_result_list_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            update_result_list_view(update_result_list_worker, true, 0, 1.0f);
        }

        private void new_scan_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                msg.Text = GetLang("Peeking memory...");
            }

            if (e.ProgressPercentage == 50)
            {
                msg.Text = GetLang("Analysing memory...");
            }
            msg.Text = String.Format(GetLang("Search Section: ") +curSection.Name+" ({0}KB)",  curSection.Length/1024/1024);

            if (e.ProgressPercentage == 100)
            {
                if (e.UserState is WorkerReturn)
                {
                    update_result_list_view_ui((WorkerReturn)e.UserState);
                }
            }
            
            progressBar.Value = e.ProgressPercentage;
        }

        private void update_result_list_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                msg.Text = GetLang("Processing memory...");
            }

            if (e.ProgressPercentage == 100 && e.UserState is WorkerReturn)
            {
                update_result_list_view_ui((WorkerReturn)e.UserState);
            }

            progressBar.Value = e.ProgressPercentage;
        }

        private void dump_dialog(int sectionID)
        {
            if (sectionID >= 0)
            {
                MappedSection section = processManager.MappedSectionList[sectionID];

                save_file_dialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                save_file_dialog.FilterIndex = 1;
                save_file_dialog.RestoreDirectory = true;
                save_file_dialog.FileName = (string)section_list_box.Items[section_list_box.SelectedIndex];

                if (save_file_dialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] buffer = memoryHelper.ReadMemory(section.Start, (int)section.Length);

                    FileStream myStream = new FileStream(save_file_dialog.FileName, FileMode.OpenOrCreate);
                    myStream.Write(buffer, 0, buffer.Length);
                    myStream.Close();
                }
            }
        }

        private void result_list_view_hex_item_Click(object sender, EventArgs e)
        {
            if (result_list_view.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = result_list_view.SelectedItems;

                ulong address = ulong.Parse(result_list_view.SelectedItems[0].
                    SubItems[RESULT_LIST_ADDRESS].Text, NumberStyles.HexNumber);
                int sectionID = processManager.MappedSectionList.GetMappedSectionID(address);

                if (sectionID >= 0)
                {
                    this.ShowHexEdit(sectionID, address);
                }
            } else if(CheatList.IS_DEV)
            {
                this.ShowHexEdit(-1, 0);
            }
        }

        private void ShowHexEdit(int sectionID, ulong address)
        {
            MappedSection section = null;
            int offset = 0;
            if (CheatList.IS_DEV && sectionID <0)
            {
                section = new MappedSection();
                section.Name = "_Dev";
                section.Length = 0x1000;
            }
            else
            {
                section = processManager.MappedSectionList[sectionID];
                offset = (int)(address - section.Start);
            }

            HexEditor hexEdit = new HexEditor(memoryHelper, offset, section);
            hexEdit.Show(this);
        }


        private void dump_item_Click(object sender, EventArgs e)
        {
            if (result_list_view.SelectedItems.Count == 1)
            {
                ulong address = ulong.Parse(result_list_view.SelectedItems[0].
                    SubItems[RESULT_LIST_ADDRESS].Text, NumberStyles.HexNumber);

                int sectionID = processManager.MappedSectionList.GetMappedSectionID(address);

                dump_dialog(sectionID);
            }
        }

        private void sectionView_Click(object sender, EventArgs e)
        {
            int sectionID = -1;
            int offset = 0;

            sectionID = section_list_box.SelectedIndex;

            if (sectionID >= 0)
            {
                string secName = (string)section_list_box.SelectedItem;
                sectionID = sectionInfos.IndexOf(secName);
                this.ShowHexEdit(sectionID, (ulong)offset);
            } else if(CheatList.IS_DEV)
            {
                this.ShowHexEdit(-1, 0);
            }
        }

        private void sectionDump_Click(object sender, EventArgs e)
        {
            int sectionID = section_list_box.SelectedIndex;
            if (sectionID >= 0)
            {
                string secName = (string)section_list_box.SelectedItem;
                sectionID = sectionInfos.IndexOf(secName);
                dump_dialog(section_list_box.SelectedIndex);
            }
        }

        private void section_list_box_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int idx = e.Index;
            string secName = (string)section_list_box.Items[idx];
            idx = sectionInfos.IndexOf(secName);
            processManager.MappedSectionList.SectionCheck(idx, e.NewValue == CheckState.Checked);
        }

        void add_new_row_to_cheat_list_view(Cheat cheat)
        {
            int index = this.cheat_list_view.Rows.Add();

            DataGridViewRow cheat_list_view_item = cheat_list_view.Rows[index];
            CheatOperator destination = cheat.GetDestination();
            CheatOperator source = cheat.GetSource();

            cheat_list_view_item.Cells[CHEAT_LIST_ADDRESS].Value = destination.Display();
            cheat_list_view_item.Cells[CHEAT_LIST_TYPE].Value = MemoryHelper.GetStringOfValueType(source.ValueType);
            cheat_list_view_item.Cells[CHEAT_LIST_VALUE].Value = source.Display();
            cheat_list_view_item.Cells[CHEAT_LIST_SECTION].Value = processManager.MappedSectionList.GetSectionName(destination.GetSectionID());
            cheat_list_view_item.Cells[CHEAT_LIST_LOCK].Value = cheat.Lock;
            cheat_list_view_item.Cells[CHEAT_LIST_DESC].Value = cheat.Description;
        }

        void new_data_cheat(ulong address, string type, string data, bool lock_, string description)
        {
            try
            {
                int sectionID = processManager.MappedSectionList.GetMappedSectionID(address);

                if (sectionID == -1)
                {
                    MessageBox.Show(GetLang("Address is out of range!"));
                    return;
                }

                if (cheatList.Exist(address))
                {
                    return;
                }

                ValueType valueType = MemoryHelper.GetValueTypeByString(type);

                DataCheatOperator dataCheatOperator = new DataCheatOperator(data, valueType, processManager);
                AddressCheatOperator addressCheatOperator = new AddressCheatOperator(address, processManager);

                DataCheat dataCheat = new DataCheat(dataCheatOperator, addressCheatOperator, lock_, description, processManager);
                add_new_row_to_cheat_list_view(dataCheat);
                cheatList.Add(dataCheat);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void new_pointer_cheat(ulong address, List<long> offset_list, string type, string data, bool lock_, string description)
        {
            try
            {
                int sectionID = processManager.MappedSectionList.GetMappedSectionID(address);

                if (sectionID == -1)
                {
                    MessageBox.Show(GetLang("Address is out of range!"));
                    return;
                }

                if (cheatList.Exist(address))
                {
                    return;
                }

                ValueType valueType = MemoryHelper.GetValueTypeByString(type);

                DataCheatOperator sourceOperator = new DataCheatOperator(data, valueType, processManager);
                AddressCheatOperator addressCheatOperator = new AddressCheatOperator(address, processManager);
                List<OffsetCheatOperator> offsetOperators = new List<OffsetCheatOperator>();

                for (int i = 0; i < offset_list.Count; ++i)
                {
                    OffsetCheatOperator offsetOperator = new OffsetCheatOperator(offset_list[i],
                        ValueType.ULONG_TYPE, processManager);
                    offsetOperators.Add(offsetOperator);
                }

                SimplePointerCheatOperator destOperator = new SimplePointerCheatOperator(addressCheatOperator, offsetOperators, valueType, processManager);

                SimplePointerCheat simplePointerCheat = new SimplePointerCheat(sourceOperator, destOperator,
                    lock_, description, processManager);

                add_new_row_to_cheat_list_view(simplePointerCheat);
                cheatList.Add(simplePointerCheat);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void add_to_cheat_list(ListViewItem item)
        {
            string address = item.SubItems[RESULT_LIST_ADDRESS].Text;
            string type = item.SubItems[RESULT_LIST_TYPE].Text;
            string value = item.SubItems[RESULT_LIST_VALUE].Text;
            bool lock_ = false;
            string description = "";

            new_data_cheat(ulong.Parse(address, NumberStyles.HexNumber), type, value, lock_, description);
        }

        public void AddCheat()
        {
        }

        private void result_list_view_DoubleClick(object sender, EventArgs e)
        {
            if (result_list_view.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = result_list_view.SelectedItems;
                add_to_cheat_list(items[0]);
            }
        }

        private void cheat_list_view_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow edited_row = cheat_list_view.Rows[e.RowIndex];
            object edited_col = edited_row.Cells[e.ColumnIndex].Value;

            switch (e.ColumnIndex)
            {
                case CHEAT_LIST_VALUE:
                    Cheat cheat = cheatList[e.RowIndex];
                    switch (cheat.CheatType)
                    {
                        case CheatType.BATCH_CODE_TYPE:
                            BatchCodeCheat batchCheat = (BatchCodeCheat)cheat;
                            batchCheat.ParseCode((string)edited_col);
                            batchCheat.Execute();
                            break;
                        default:
                            DataCheatOperator dataCheatOperator = (DataCheatOperator)cheat.GetSource();
                            CheatOperator destOperator = cheat.GetDestination();
                            dataCheatOperator.Set((string)edited_col);
                            destOperator.SetRuntime(dataCheatOperator);
                            break;
                    }
                    break;
                case CHEAT_LIST_DESC:
                    cheatList[e.RowIndex].Description = (string)edited_col;
                    break;
            }
        }

        private void cheat_list_view_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;

            DataGridViewRow edited_row = cheat_list_view.Rows[e.RowIndex];
            object edited_col = null;

            switch (e.ColumnIndex)
            {
                case CHEAT_LIST_ENABLED:
                    cheat_list_view.EndEdit();
                    Cheat cheat = cheatList[e.RowIndex];
                    if(CheatType.BATCH_CODE_TYPE.Equals(cheat.CheatType))
                    {
                        BatchCodeCheat batchCheat = (BatchCodeCheat)cheat;
                        batchCheat.Execute();
                        break;
                    }
                    DataCheatOperator dataCheatOperator = (DataCheatOperator)cheatList[e.RowIndex].GetSource();
                    CheatOperator destOperator = cheatList[e.RowIndex].GetDestination();
                    edited_col = edited_row.Cells[CHEAT_LIST_VALUE].Value;
                    dataCheatOperator.Set((string)edited_col);
                    destOperator.SetRuntime(dataCheatOperator);
                    break;
                case CHEAT_LIST_DEL:
                    cheat_list_view.Rows.RemoveAt(e.RowIndex);
                    break;
                case CHEAT_LIST_LOCK:
                    cheat_list_view.EndEdit();
                    edited_col = edited_row.Cells[e.ColumnIndex].Value;
                    cheatList[e.RowIndex].Lock = (bool)edited_col;
                    if(!(bool)edited_col)
                    {
                        Cheat cheatObj = cheatList[e.RowIndex];
                        if (CheatType.BATCH_CODE_TYPE.Equals(cheatObj.CheatType))
                        {
                            BatchCodeCheat batchCheat = (BatchCodeCheat)cheatObj;
                            batchCheat.Reset();
                            return;
                        }
                    }
                    break;
            }
        }

        public void AddNewCheat(ulong addr)
        {
            NewAddress newAddress = new NewAddress(processManager);
            if (addr != 0) newAddress.Address = addr;
            if (newAddress.ShowDialog() != DialogResult.OK)
                return;

            ulong address = newAddress.Address;
            string value_type = newAddress.ValueTypeStr;
            string value = newAddress.Value;
            bool lock_ = newAddress.Lock;
            string description = newAddress.Descriptioin;

            int sectionID = processManager.MappedSectionList.GetMappedSectionID(address);

            if (sectionID < 0)
            {
                MessageBox.Show(GetLang("Invalid Address!!"));
                return;
            }

            if (!newAddress.Pointer)
            {
                new_data_cheat(address, value_type, value, lock_, description);
            }
            else
            {
                new_pointer_cheat(address, newAddress.OffsetList, value_type, value, lock_, description);
            }
        }

        private void add_address_btn_Click(object sender, EventArgs e)
        {
            this.AddNewCheat(0);
        }

        private void cheat_list_view_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                cheatList.RemoveAt(e.RowIndex);
            }
        }

        private void save_address_btn_Click(object sender, EventArgs e)
        {
            save_file_dialog.Filter = "Cheat files (*.cht)|*.cht";
            save_file_dialog.FilterIndex = 1;
            save_file_dialog.RestoreDirectory = true;

            if (save_file_dialog.ShowDialog() == DialogResult.OK)
            {
                cheatList.SaveFile(save_file_dialog.FileName, (string)processes_comboBox.SelectedItem, processManager);
            }
        }

        private void load_address_btn_Click(object sender, EventArgs e)
        {
            string filter = "Cheat files (*.cht)|*.cht";
            List<ICheatPlugin> cheatPlugins = this.cheatPluginHelper.cheatPlugins;
            List<Type> cheatTypes = this.cheatPluginHelper.cheatTypes;
            foreach(ICheatPlugin plugin in cheatPlugins)
            {
                string typefile = plugin.GetCodeFileFilter();
                filter+=String.Format("|{0}",typefile);
            }
            open_file_dialog.Filter = filter;
            open_file_dialog.FilterIndex = 1;
            open_file_dialog.RestoreDirectory = true;

            if (open_file_dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            cheat_list_view.Rows.Clear();
            int filterIdx = open_file_dialog.FilterIndex;
            if (filterIdx > 1)
            {
                ICheatPlugin cheatPlugin = cheatPlugins[filterIdx - 2];
                cheatList.LoadFile(open_file_dialog.FileName, processManager, processes_comboBox, cheatPlugin);
            }
            else
            {
                cheatList.LoadFile(open_file_dialog.FileName, processManager, processes_comboBox);
            }

            for (int i = 0; i < cheatList.Count; ++i)
            {
                Cheat cheat = cheatList[i];
                switch(cheat.CheatType)
                {
                    case CheatType.DATA_TYPE:
                        add_new_row_to_cheat_list_view((DataCheat)cheat);
                        break;
                    case CheatType.SIMPLE_POINTER_TYPE:
                        add_new_row_to_cheat_list_view((SimplePointerCheat)cheat);
                        break;
                    case CheatType.BATCH_CODE_TYPE:
                        add_new_row_to_cheat_list_view((BatchCodeCheat)cheat);
                        break;
                    case CheatType.CHEAT_CODE_TYPE:
                        //add_new_row_to_cheat_list_view((CheatCodeCheat)cheat);
                        break;
                    default:
                        break;
                }
            }
        }

        private void refresh_cheat_list_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < cheatList.Count; ++i)
                {
                    DataGridViewRow row = cheat_list_view.Rows[i];

                    Cheat cheat = cheatList[i];
                    if (CheatType.BATCH_CODE_TYPE.Equals(cheat.CheatType)) {
                        continue;
                    }
                    DataCheatOperator dataCheatOperator = (DataCheatOperator)cheat.GetSource();
                    CheatOperator destOperator = cheatList[i].GetDestination();
                    dataCheatOperator.Set(destOperator.GetRuntime());
                    row.Cells[CHEAT_LIST_VALUE].Value = dataCheatOperator.Display();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void executeCheat(Cheat cheat)
        {
            if(CheatType.BATCH_CODE_TYPE.Equals(cheat.CheatType))
            {
                BatchCodeCheat batchCheat = (BatchCodeCheat)cheat;
                batchCheat.Execute();
                return;
            }
            DataCheatOperator dataCheatOperator = (DataCheatOperator)cheat.GetSource();
            CheatOperator destOperator = cheat.GetDestination();
            destOperator.SetRuntime(dataCheatOperator);
        }

        private void refresh_lock_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < cheatList.Count; ++i)
            {
                Cheat cheat = cheatList[i];
                if (!cheat.Lock)
                {
                    continue;
                }
                executeCheat(cheat);
            }
        }
        private void processes_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                section_list_box.Items.Clear();
                result_list_view.Items.Clear();
                new_scan_btn.Enabled = true;
                valueTypeList.Enabled = true;
                compareTypeList.Enabled = true;
                section_list_box.Enabled = true;

                ProcessInfo processInfo = processManager.GetProcessInfo(processes_comboBox.Text);
                Util.DefaultProcessID = processInfo.pid;
                processManager.MappedSectionList.InitMemorySectionList(processInfo);

                section_list_box.BeginUpdate();
                for (int i = 0; i < processManager.MappedSectionList.Count; ++i)
                {
                    section_list_box.Items.Add(processManager.MappedSectionList.GetSectionName(i), false);
                }
                section_list_box.EndUpdate();
                sectionInfos.Clear();
                for (int i = 0; i < section_list_box.Items.Count; i++)
                {
                    sectionInfos.Add(section_list_box.Items[i]);
                }
                sectionfilter_box.Enabled = true;
                sectionfilter_button.Enabled = true;
                sectionsize_box.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
		
        private void get_processes_btn_Click(object sender, EventArgs e)
        {
            try
            {	                
                MemoryHelper.Connect(ip_box.Text,(Util.Version == 505));

                this.processes_comboBox.Items.Clear();
                ProcessList pl = MemoryHelper.GetProcessList();
                foreach (Process process in pl.processes)
                {
                    this.processes_comboBox.Items.Add(process.name);
                }
                this.processes_comboBox.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void send_pay_load(string IP, string payloadPath, int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(new IPEndPoint(IPAddress.Parse(IP), port));
            socket.SendFile(payloadPath);
            socket.Close();
        }

        private void send_payload_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string patch_path = Application.StartupPath;
                switch (version_list.SelectedIndex)
                {
                    case VERSION_LIST_405:
                        patch_path += @"\4.05\";
                        break;
                    case VERSION_LIST_455:
                        patch_path += @"\4.55\";
                        break;
					case VERSION_LIST_505:
                        patch_path += @"\5.05\";
                        break;
                    default:
                        throw new System.ArgumentException(GetLang("Unknown version."));
                }

                this.send_pay_load(this.ip_box.Text, patch_path + @"payload.bin", Convert.ToInt32(this.port_box.Text));
                Thread.Sleep(1000);
                updateMessage("Injecting kpayload.elf...");
                this.send_pay_load(this.ip_box.Text, patch_path + @"kpayload.elf", 9023);
                Thread.Sleep(2500);
                this.msg.ForeColor = Color.Green;
                updateMessage("Payload injected successfully!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitCompareTypeListOfFirstScan()
        {
            string selected_type = (string)compareTypeList.SelectedItem;
            compareTypeList.Items.Clear();
            switch (MemoryHelper.GetValueTypeByString((string)valueTypeList.SelectedItem))
            {
                case ValueType.ULONG_TYPE:
                case ValueType.UINT_TYPE:
                case ValueType.USHORT_TYPE:
                case ValueType.BYTE_TYPE:
                    hex_box.Enabled = true;
                    compareTypeList.Items.AddRange(SEARCH_BY_BYTES_FIRST);
                    break;
                case ValueType.DOUBLE_TYPE:
                case ValueType.FLOAT_TYPE:
                    hex_box.Enabled = true;
                    compareTypeList.Items.AddRange(SEARCH_BY_FLOAT_FIRST);
                    break;
                case ValueType.HEX_TYPE:
                case ValueType.STRING_TYPE:
                    hex_box.Enabled = false;
                    hex_box.Checked = false;
                    compareTypeList.Items.AddRange(SEARCH_BY_HEX);
                    break;
                default:
                    throw new Exception(GetLang("GetStringOfValueType!!!"));
            }

            int list_idx = 0;
            int list_count = compareTypeList.Items.Count;

            for (; list_idx < list_count; ++list_idx)
            {
                if ((string)compareTypeList.Items[list_idx] == selected_type)
                {
                    break;
                }
            }

            compareTypeList.SelectedIndex = list_idx == list_count ? 0 : list_idx;
        }

        private void InitCompareTypeListOfNextScan()
        {
            string selected_type = (string)compareTypeList.SelectedItem;
            compareTypeList.Items.Clear();
            switch (MemoryHelper.GetValueTypeByString((string)valueTypeList.SelectedItem))
            {
                case ValueType.ULONG_TYPE:
                case ValueType.UINT_TYPE:
                case ValueType.USHORT_TYPE:
                case ValueType.BYTE_TYPE:
                    compareTypeList.Items.AddRange(SEARCH_BY_BYTES_NEXT);
                    break;
                case ValueType.DOUBLE_TYPE:
                case ValueType.FLOAT_TYPE:
                    compareTypeList.Items.AddRange(SEARCH_BY_FLOAT_NEXT);
                    break;
                case ValueType.HEX_TYPE:
                case ValueType.STRING_TYPE:
                    compareTypeList.Items.AddRange(SEARCH_BY_HEX);
                    break;
                default:
                    throw new Exception(GetLang("GetStringOfValueType!!!"));
            }

            int list_idx = 0;
            int list_count = compareTypeList.Items.Count;

            for (; list_idx < list_count; ++list_idx)
            {
                if ((string)compareTypeList.Items[list_idx] == selected_type)
                {
                    break;
                }
            }

            compareTypeList.SelectedIndex = list_idx == list_count ? 0 : list_idx;
        }

        private void valueTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCompareTypeListOfFirstScan();
        }

        private void compareList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)compareTypeList.SelectedItem == GetLang(CONSTANT.BETWEEN_VALUE))
            {
                value_1_box.Visible = true;
                value_1_label.Visible = true;
                and_label.Visible = true;
                value_box.Width = 100;
            }
            else
            {
                value_1_box.Visible = false;
                value_1_label.Visible = false;
                and_label.Visible = false;
                value_box.Width = 215;
            }
        }

        private void new_scan_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            new_scan_btn.Text = GetLang(CONSTANT.NEW_SCAN);
            InitCompareTypeListOfNextScan();
            if (e.Error != null)
            {
                msg.Text = e.Error.Message;
            }
            setButtons(true);
        }

        private void update_result_list_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refresh_btn.Text = GetLang(CONSTANT.REFRESH);
            if (e.Error != null)
            {
                msg.Text = e.Error.Message;
            }
            setButtons(true);
        }

        private void next_scan_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            next_scan_btn.Text = GetLang( CONSTANT.NEXT_SCAN);
            if (e.Error != null)
            {
                msg.Text = e.Error.Message;
            }
            setButtons(true);
        }

        private void result_list_view_add_to_cheat_list_Click(object sender, EventArgs e)
        {
            if (result_list_view.SelectedItems == null)
                return;

            ListView.SelectedListViewItemCollection items = result_list_view.SelectedItems;
            for (int i = 0; i < items.Count; ++i)
            {
                add_to_cheat_list(items[i]);
            }
        }

        private void cheat_list_item_active_Click(object sender, EventArgs e)
        {
            if (cheat_list_view.SelectedRows == null)
                return;

            DataGridViewSelectedRowCollection items = cheat_list_view.SelectedRows;
            for (int i = 0; i < items.Count; ++i)
            {
                Cheat cheat = cheatList[items[i].Index];

                cheat.GetDestination().SetRuntime(cheat.GetSource());
            }
        }

        private void cheat_list_item_delete_Click(object sender, EventArgs e)
        {
            if (cheat_list_view.SelectedRows == null)
                return;

            DataGridViewSelectedRowCollection items = cheat_list_view.SelectedRows;
            for (int i = 0; i < items.Count; ++i)
            {
                cheat_list_view.Rows.Remove(items[i]);
            }
        }

        private void cheat_list_item_lock_Click(object sender, EventArgs e)
        {
            if (cheat_list_view.SelectedRows == null)
                return;

            DataGridViewSelectedRowCollection items = cheat_list_view.SelectedRows;
            for (int i = 0; i < items.Count; ++i)
            {
                cheatList[items[i].Index].Lock = true;
                items[i].Cells[CHEAT_LIST_LOCK].Value = true;
            }
        }

        private void cheat_list_time_unlock_Click(object sender, EventArgs e)
        {
            if (cheat_list_view.SelectedRows == null)
                return;

            DataGridViewSelectedRowCollection items = cheat_list_view.SelectedRows;
            for (int i = 0; i < items.Count; ++i)
            {
                cheatList[items[i].Index].Lock = false;
                items[i].Cells[CHEAT_LIST_LOCK].Value = false;
            }
        }

        private void cheat_list_item_hex_view_Click(object sender, EventArgs e)
        {
            if (cheat_list_view.SelectedRows == null)
                return;

            if (cheat_list_view.SelectedRows.Count != 1)
                return;

            DataGridViewSelectedRowCollection items = cheat_list_view.SelectedRows;

            string addrStr = (string)items[0].Cells[CHEAT_LIST_ADDRESS].Value;
            addrStr = addrStr.Replace("p->", "");
            ulong address = ulong.Parse(addrStr, NumberStyles.HexNumber);
            int sectionID = processManager.MappedSectionList.GetMappedSectionID(address);

            if (sectionID >= 0)
            {
                this.ShowHexEdit(sectionID, address);
            } else if(CheatList.IS_DEV)
            {
                this.ShowHexEdit(-1, 0);
            }
        }

        private void cheat_list_item_find_pointer_Click(object sender, EventArgs e)
        {
            if (cheat_list_view.SelectedRows == null)
            {
                return;
            }

            if (cheat_list_view.SelectedRows.Count != 1)
            {
                if (CheatList.IS_DEV)
                {
                    PointerFinder pointerFinder = new PointerFinder(this, 0, "byte", processManager, cheat_list_view);
                    pointerFinder.Show();
                }
                return;
            }

            DataGridViewSelectedRowCollection items = cheat_list_view.SelectedRows;

            try
            {
                ulong address = ulong.Parse((string)items[0].Cells[CHEAT_LIST_ADDRESS].Value, NumberStyles.HexNumber);
                string type = (string)items[0].Cells[CHEAT_LIST_TYPE].Value;

                PointerFinder pointerFinder = new PointerFinder(this, address, type, processManager, cheat_list_view);
                pointerFinder.Show();
            }
            catch(Exception fe)
            {
                System.Diagnostics.Debug.Write(fe);
            }
        }

        private void version_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (version_list.SelectedIndex)
            {
                case VERSION_LIST_405:
                    Util.Version = 405;
					
                    break;
                case VERSION_LIST_455:
                    Util.Version = 455;
					
                    break;
				case VERSION_LIST_505:
                    Util.Version = 505;
					
                    break;
            }
        }

        private void filter_sections(object sender, EventArgs e)
        {
            string filterStr = sectionfilter_box.Text;
            bool noFilter = string.IsNullOrWhiteSpace(filterStr);
            bool isMatch = true;
            if (!noFilter) {
                isMatch = !filterStr.StartsWith("^");
                if (!isMatch) filterStr = filterStr.Substring(1);
                filterStr = filterStr.ToUpper();
            }
            CheckedListBox.ObjectCollection sections = section_list_box.Items;
            sections.Clear();
            int filterSize = Decimal.ToInt32(sectionsize_box.Value);
            if(filterSize>0)
            {
                filterSize = filterSize * 1024;
            }
            for (int i = 0; i < this.sectionInfos.Count; i++)
            {
                string secName = (string)sectionInfos[i];
                if(filterSize > 0)
                {
                    MappedSection section = this.processManager.MappedSectionList[i];
                    int sectionSize = section.Length;
                    if (sectionSize < filterSize) continue;
                }
                if (noFilter) {
                    sections.Add(secName, false);
                } else {
                    int idx = secName.IndexOf(filterStr);
                    if(isMatch? idx>-1 : idx<0)
                    {
                        sections.Add(secName, false);
                    }
                }
            }
            section_list_box.BeginUpdate();
            section_list_box.EndUpdate();
         }

        private void langCombo_SelectedChanged(object sender, EventArgs e)
        {
            string locale = (string)this.langCombo.SelectedItem;
            this.SetLocale(locale);
        }

        private void sectionsize_box_ValueChanged(object sender, EventArgs e)
        {
            this.filter_sections(sender, e);
        }

        private void conn_btn_Click(object sender, EventArgs e)
        {
            MemoryHelper.Connect(ip_box.Text, (Util.Version == 505));
        }
    }
}

