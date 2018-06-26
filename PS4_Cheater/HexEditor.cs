using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using librpc;
using static System.Windows.Forms.Menu;

namespace PS4_Cheater
{
    public partial class HexEditor : Form
    {
        private MappedSection section;
        private MemoryHelper memoryHelper;

        private int page;
        private int page_count;
        private long line;
        private int column;

        const int page_size = 8 * 1024 * 1024;

        public HexEditor(MemoryHelper memoryHelper, int offset, MappedSection section)
        {
            InitializeComponent();

            this.memoryHelper = memoryHelper;
            this.section = section;
            this.page = offset / page_size;
            this.line = (offset - page * page_size) / hexBox.BytesPerLine;
            this.column = (offset - page * page_size) % hexBox.BytesPerLine;

            this.page_count = divup((int)section.Length, page_size);

            for (int i = 0; i < page_count; ++i)
            {
                ulong start = section.Start + (ulong)i * page_size;
                ulong end = section.Start + (ulong)(i + 1) * page_size;
                page_list.Items.Add((i + 1).ToString() + String.Format(" {0:X}-{1:X}", start, end));
            }

            this.Text = LangHelper.GetLang("HexEditor");
            this.previous_btn.Text = LangHelper.GetLang("Previous");
            this.next_btn.Text = LangHelper.GetLang("Next");
            this.refresh_btn.Text = LangHelper.GetLang("Refresh");
            this.commit_btn.Text = LangHelper.GetLang("Commit");
            this.find.Text = LangHelper.GetLang("Find");
            this.cheat_btn.Text = LangHelper.GetLang("Add Cheat");
        }

        private void update_ui(int page, long line)
        {
            hexBox.LineInfoOffset = (uint)((ulong)section.Start + (ulong)(page_size * page));

            int mem_size = page_size;

            if (section.Length - page_size * page < mem_size)
            {
                mem_size = section.Length - page_size * page;
            }

            if (CheatList.IS_DEV && this.hexBox.ByteProvider != null)
            {
            }
            else
            {
                byte[] dst = memoryHelper.ReadMemory(section.Start + (ulong)page * page_size, (int)mem_size);
                MemoryViewByteProvider dataProvider = new MemoryViewByteProvider(dst);
                //hexBox.ByteProvider = new MemoryViewByteProvider(dst);
                hexBox.ByteProvider = dataProvider;
            }

            if (line != 0)
            {
                hexBox.SelectionStart = line * hexBox.BytesPerLine + column;
                hexBox.SelectionLength = 4;
                hexBox.ScrollByteIntoView((line + hexBox.Height / (int)hexBox.CharSize.Height - 1) * hexBox.BytesPerLine + column);
            }
        }

        private void HexEdit_Load(object sender, EventArgs e)
        {
            page_list.SelectedIndex = page;
        }

        private void HexEdit_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        int divup(int sum, int div)
        {
            return sum / div + ((sum % div != 0) ? 1 : 0);
        }

        private void next_btn_Click(object sender, EventArgs e)
        {
            if (page + 1 >= page_count)
            {
                return;
            }

            page++;
            line = 0;
            column = 0;

            page_list.SelectedIndex = page;
        }

        private void previous_btn_Click(object sender, EventArgs e)
        {
            if (page <= 0)
            {
                return;
            }

            page--;
            line = 0;
            column = 0;
            page_list.SelectedIndex = page;
        }

        private void page_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = page_list.SelectedIndex;

            update_ui(page, line);
        }

        private void commit_btn_Click(object sender, EventArgs e)
        {
            MemoryViewByteProvider mvbp = (MemoryViewByteProvider)this.hexBox.ByteProvider;
            if (mvbp.HasChanges())
            {
                byte[] buffer = mvbp.Bytes.ToArray();
                List<int> change_list = mvbp.change_list;

                for (int i = 0; i < change_list.Count; ++i)
                {
                    byte[] b = { buffer[change_list[i]]  };
                    memoryHelper.WriteMemory(section.Start + (ulong)(page * page_size + change_list[i]), b);
                }
                mvbp.change_list.Clear();
            }
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            page_list.SelectedIndex = page;
            line = hexBox.CurrentLine - 1;
            column = 0;
            update_ui(page, line);
        }

        private void find_Click(object sender, EventArgs e)
        {
            FindOptions findOptions = new FindOptions();
            findOptions.Type = FindType.Hex;
            findOptions.Hex = MemoryHelper.string_to_hex_bytes(input_box.Text);
            hexBox.Find(findOptions);
        }

        private void menu_item_copy_Click(object sender, EventArgs e)
        {
            this.hexBox.CopyHex();
        }

        private void hexBox_Copied(object sender, EventArgs e)
        {
            this.hexBox.CopyHex();
        }

        private void hexBox_CopiedHex(object sender, EventArgs e)
        {
            Debug.Write(sender);
        }

        private void hexBox_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void hexBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.Write(e);
        }

        private void hexBox_Parse(string strData)
        {            
            string[] strs = strData.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            byte[] hexData = new byte[strs.Length];
            long index = this.hexBox.SelectionStart;
            try
            {
                for (int i = 0; i < strs.Length; i++)
                {
                    hexData[i] = Byte.Parse(strs[i], System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show(this, String.Format(LangHelper.GetLang("Invalid Hex String: [{0}] - {1}"), strData, fe.Message));
            }
            for (int i = 0; i < hexData.Length; i++)
            {
                this.hexBox.ByteProvider.WriteByte(index++, hexData[i]);
            }
            this.hexBox.Refresh();
        }

        private void hexBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyData)
            {
                case Keys.V | Keys.Control:
                    string strData = Clipboard.GetText();
                    this.hexBox_Parse(strData);
                    e.Handled = true;
                    break;
            }
        }

        private void cheat_btn_Click(object sender, EventArgs e)
        {
            ulong address = this.section.Start + (ulong)this.hexBox.SelectionStart;
            ((main)this.Owner).AddNewCheat(address);
        }
    }
}
