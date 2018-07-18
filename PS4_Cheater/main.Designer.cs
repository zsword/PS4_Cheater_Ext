using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace PS4_Cheater
{
    partial class main
    {
        private ColumnHeader result_list_view_value;
        private ColumnHeader result_list_view_section;
        private Button refresh_cheat_list_btn;
        private Button load_cheat_list_btn;
        private ColumnHeader result_list_view_hex;
        private Button save_cheat_list_btn;
        private Button new_cheat_list_btn;
        private CheckedListBox section_list_box;
        private ContextMenuStrip section_list_menu;
        private ToolStripMenuItem section_view_menu;
        private ToolStripMenuItem section_dump_menu;
        private TextBox value_box;
        private Timer refresh_lock;
        private SaveFileDialog save_file_dialog;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer4;
        private ListView result_list_view;
        private ColumnHeader result_list_view_address;
        private ColumnHeader result_list_view_type;
        private SplitContainer splitContainer2;
        private ComboBox processes_comboBox;
        private TextBox ip_box;
        private TextBox port_box;
        private Button send_payload_btn;
        private Button get_processes_btn;
        private Label msg;
        private CheckBox select_all;
        private ProgressBar progressBar;
        private ComboBox compareTypeList;
        private ComboBox valueTypeList;
        private Label value_1_label;
        private Button next_scan_btn;
        private Button new_scan_btn;
        private Button refresh_btn;
        private BackgroundWorker new_scan_worker;
        private BackgroundWorker next_scan_worker;
        private OpenFileDialog open_file_dialog;
        private Panel parent;
        private IContainer components = null;
        private List<object> sectionInfos = new List<object>();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.parent = new System.Windows.Forms.Panel();
            this.result_list_view_value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.result_list_view_section = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.refresh_cheat_list_btn = new System.Windows.Forms.Button();
            this.load_cheat_list_btn = new System.Windows.Forms.Button();
            this.result_list_view_hex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.save_cheat_list_btn = new System.Windows.Forms.Button();
            this.new_cheat_list_btn = new System.Windows.Forms.Button();
            this.cheat_list_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cheat_list_item_hex_view = new System.Windows.Forms.ToolStripMenuItem();
            this.cheat_list_item_separator_1 = new System.Windows.Forms.ToolStripSeparator();
            this.cheat_list_item_lock = new System.Windows.Forms.ToolStripMenuItem();
            this.cheat_list_item_unlock = new System.Windows.Forms.ToolStripMenuItem();
            this.cheat_list_item_active = new System.Windows.Forms.ToolStripMenuItem();
            this.cheat_list_item_separator_0 = new System.Windows.Forms.ToolStripSeparator();
            this.cheat_list_item_find_pointer = new System.Windows.Forms.ToolStripMenuItem();
            this.cheat_list_item_separator_2 = new System.Windows.Forms.ToolStripSeparator();
            this.cheat_list_item_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.section_list_box = new System.Windows.Forms.CheckedListBox();
            this.section_list_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.section_view_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.section_dump_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.value_box = new System.Windows.Forms.TextBox();
            this.refresh_lock = new System.Windows.Forms.Timer(this.components);
            this.save_file_dialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.result_list_view = new System.Windows.Forms.ListView();
            this.result_list_view_address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.result_list_view_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.result_list_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.result_list_view_add_to_cheat_list = new System.Windows.Forms.ToolStripMenuItem();
            this.result_list_view_separator = new System.Windows.Forms.ToolStripSeparator();
            this.result_list_view_view_item = new System.Windows.Forms.ToolStripMenuItem();
            this.result_list_view_dump_item = new System.Windows.Forms.ToolStripMenuItem();
            this.cheat_list_view = new System.Windows.Forms.DataGridView();
            this.cheat_list_view_del = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cheat_list_view_address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cheat_list_view_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cheat_list_view_active = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cheat_list_view_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cheat_list_view_section = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cheat_list_view_lock = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cheat_list_view_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.version_list = new System.Windows.Forms.ComboBox();
            this.processes_comboBox = new System.Windows.Forms.ComboBox();
            this.ip_box = new System.Windows.Forms.TextBox();
            this.port_box = new System.Windows.Forms.TextBox();
            this.send_payload_btn = new System.Windows.Forms.Button();
            this.get_processes_btn = new System.Windows.Forms.Button();
            this.sectionsize_box = new System.Windows.Forms.NumericUpDown();
            this.langCombo = new System.Windows.Forms.ComboBox();
            this.value_label = new System.Windows.Forms.Label();
            this.value_1_box = new System.Windows.Forms.TextBox();
            this.and_label = new System.Windows.Forms.Label();
            this.value_1_label = new System.Windows.Forms.Label();
            this.select_all = new System.Windows.Forms.CheckBox();
            this.sectionfilter_button = new System.Windows.Forms.Button();
            this.sectionfilter_box = new System.Windows.Forms.TextBox();
            this.hex_box = new System.Windows.Forms.CheckBox();
            this.alignment_box = new System.Windows.Forms.CheckBox();
            this.msg = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.compareTypeList = new System.Windows.Forms.ComboBox();
            this.valueTypeList = new System.Windows.Forms.ComboBox();
            this.next_scan_btn = new System.Windows.Forms.Button();
            this.new_scan_btn = new System.Windows.Forms.Button();
            this.refresh_btn = new System.Windows.Forms.Button();
            this.new_scan_worker = new System.ComponentModel.BackgroundWorker();
            this.next_scan_worker = new System.ComponentModel.BackgroundWorker();
            this.open_file_dialog = new System.Windows.Forms.OpenFileDialog();
            this.update_result_list_worker = new System.ComponentModel.BackgroundWorker();
            this.cheat_list_menu.SuspendLayout();
            this.section_list_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.result_list_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cheat_list_view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sectionsize_box)).BeginInit();
            this.SuspendLayout();
            // 
            // parent
            // 
            this.parent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parent.Location = new System.Drawing.Point(0, 0);
            this.parent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.parent.Name = "parent";
            this.parent.Size = new System.Drawing.Size(1254, 708);
            this.parent.TabIndex = 11;
            // 
            // result_list_view_value
            // 
            this.result_list_view_value.Text = "Value";
            this.result_list_view_value.Width = 120;
            // 
            // result_list_view_section
            // 
            this.result_list_view_section.Text = "Section";
            this.result_list_view_section.Width = 180;
            // 
            // refresh_cheat_list_btn
            // 
            this.refresh_cheat_list_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.refresh_cheat_list_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.refresh_cheat_list_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.refresh_cheat_list_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.refresh_cheat_list_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_cheat_list_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh_cheat_list_btn.ForeColor = System.Drawing.Color.White;
            this.refresh_cheat_list_btn.Location = new System.Drawing.Point(247, 18);
            this.refresh_cheat_list_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refresh_cheat_list_btn.Name = "refresh_cheat_list_btn";
            this.refresh_cheat_list_btn.Size = new System.Drawing.Size(180, 28);
            this.refresh_cheat_list_btn.TabIndex = 31;
            this.refresh_cheat_list_btn.TabStop = false;
            this.refresh_cheat_list_btn.Text = "Refresh";
            this.refresh_cheat_list_btn.UseMnemonic = false;
            this.refresh_cheat_list_btn.UseVisualStyleBackColor = false;
            this.refresh_cheat_list_btn.Click += new System.EventHandler(this.refresh_cheat_list_Click);
            // 
            // load_cheat_list_btn
            // 
            this.load_cheat_list_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.load_cheat_list_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.load_cheat_list_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.load_cheat_list_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.load_cheat_list_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.load_cheat_list_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load_cheat_list_btn.ForeColor = System.Drawing.Color.White;
            this.load_cheat_list_btn.Location = new System.Drawing.Point(711, 18);
            this.load_cheat_list_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.load_cheat_list_btn.Name = "load_cheat_list_btn";
            this.load_cheat_list_btn.Size = new System.Drawing.Size(180, 28);
            this.load_cheat_list_btn.TabIndex = 33;
            this.load_cheat_list_btn.TabStop = false;
            this.load_cheat_list_btn.Text = "Load";
            this.load_cheat_list_btn.UseMnemonic = false;
            this.load_cheat_list_btn.UseVisualStyleBackColor = false;
            this.load_cheat_list_btn.Click += new System.EventHandler(this.load_address_btn_Click);
            // 
            // result_list_view_hex
            // 
            this.result_list_view_hex.Text = "Hex";
            this.result_list_view_hex.Width = 120;
            // 
            // save_cheat_list_btn
            // 
            this.save_cheat_list_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.save_cheat_list_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.save_cheat_list_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.save_cheat_list_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.save_cheat_list_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save_cheat_list_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_cheat_list_btn.ForeColor = System.Drawing.Color.White;
            this.save_cheat_list_btn.Location = new System.Drawing.Point(479, 18);
            this.save_cheat_list_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.save_cheat_list_btn.Name = "save_cheat_list_btn";
            this.save_cheat_list_btn.Size = new System.Drawing.Size(180, 28);
            this.save_cheat_list_btn.TabIndex = 32;
            this.save_cheat_list_btn.TabStop = false;
            this.save_cheat_list_btn.Text = "Save";
            this.save_cheat_list_btn.UseMnemonic = false;
            this.save_cheat_list_btn.UseVisualStyleBackColor = false;
            this.save_cheat_list_btn.Click += new System.EventHandler(this.save_address_btn_Click);
            // 
            // new_cheat_list_btn
            // 
            this.new_cheat_list_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.new_cheat_list_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.new_cheat_list_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.new_cheat_list_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.new_cheat_list_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.new_cheat_list_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_cheat_list_btn.ForeColor = System.Drawing.Color.White;
            this.new_cheat_list_btn.Location = new System.Drawing.Point(15, 18);
            this.new_cheat_list_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.new_cheat_list_btn.Name = "new_cheat_list_btn";
            this.new_cheat_list_btn.Size = new System.Drawing.Size(180, 28);
            this.new_cheat_list_btn.TabIndex = 30;
            this.new_cheat_list_btn.TabStop = false;
            this.new_cheat_list_btn.Text = "New";
            this.new_cheat_list_btn.UseMnemonic = false;
            this.new_cheat_list_btn.UseVisualStyleBackColor = false;
            this.new_cheat_list_btn.Click += new System.EventHandler(this.add_address_btn_Click);
            // 
            // cheat_list_menu
            // 
            this.cheat_list_menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cheat_list_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cheat_list_item_hex_view,
            this.cheat_list_item_separator_1,
            this.cheat_list_item_lock,
            this.cheat_list_item_unlock,
            this.cheat_list_item_active,
            this.cheat_list_item_separator_0,
            this.cheat_list_item_find_pointer,
            this.cheat_list_item_separator_2,
            this.cheat_list_item_delete});
            this.cheat_list_menu.Name = "cheat_list_menu";
            this.cheat_list_menu.Size = new System.Drawing.Size(186, 190);
            // 
            // cheat_list_item_hex_view
            // 
            this.cheat_list_item_hex_view.Name = "cheat_list_item_hex_view";
            this.cheat_list_item_hex_view.Size = new System.Drawing.Size(185, 28);
            this.cheat_list_item_hex_view.Text = "Hex Editor";
            this.cheat_list_item_hex_view.Click += new System.EventHandler(this.cheat_list_item_hex_view_Click);
            // 
            // cheat_list_item_separator_1
            // 
            this.cheat_list_item_separator_1.Name = "cheat_list_item_separator_1";
            this.cheat_list_item_separator_1.Size = new System.Drawing.Size(182, 6);
            // 
            // cheat_list_item_lock
            // 
            this.cheat_list_item_lock.Name = "cheat_list_item_lock";
            this.cheat_list_item_lock.Size = new System.Drawing.Size(185, 28);
            this.cheat_list_item_lock.Text = "Lock";
            this.cheat_list_item_lock.Click += new System.EventHandler(this.cheat_list_item_lock_Click);
            // 
            // cheat_list_item_unlock
            // 
            this.cheat_list_item_unlock.Name = "cheat_list_item_unlock";
            this.cheat_list_item_unlock.Size = new System.Drawing.Size(185, 28);
            this.cheat_list_item_unlock.Text = "Unlock";
            this.cheat_list_item_unlock.Click += new System.EventHandler(this.cheat_list_time_unlock_Click);
            // 
            // cheat_list_item_active
            // 
            this.cheat_list_item_active.Name = "cheat_list_item_active";
            this.cheat_list_item_active.Size = new System.Drawing.Size(185, 28);
            this.cheat_list_item_active.Text = "Active";
            this.cheat_list_item_active.Click += new System.EventHandler(this.cheat_list_item_active_Click);
            // 
            // cheat_list_item_separator_0
            // 
            this.cheat_list_item_separator_0.Name = "cheat_list_item_separator_0";
            this.cheat_list_item_separator_0.Size = new System.Drawing.Size(182, 6);
            // 
            // cheat_list_item_find_pointer
            // 
            this.cheat_list_item_find_pointer.Name = "cheat_list_item_find_pointer";
            this.cheat_list_item_find_pointer.Size = new System.Drawing.Size(185, 28);
            this.cheat_list_item_find_pointer.Text = "Find Pointer";
            this.cheat_list_item_find_pointer.Click += new System.EventHandler(this.cheat_list_item_find_pointer_Click);
            // 
            // cheat_list_item_separator_2
            // 
            this.cheat_list_item_separator_2.Name = "cheat_list_item_separator_2";
            this.cheat_list_item_separator_2.Size = new System.Drawing.Size(182, 6);
            // 
            // cheat_list_item_delete
            // 
            this.cheat_list_item_delete.Name = "cheat_list_item_delete";
            this.cheat_list_item_delete.Size = new System.Drawing.Size(185, 28);
            this.cheat_list_item_delete.Text = "Delete";
            this.cheat_list_item_delete.Click += new System.EventHandler(this.cheat_list_item_delete_Click);
            // 
            // section_list_box
            // 
            this.section_list_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.section_list_box.CheckOnClick = true;
            this.section_list_box.ContextMenuStrip = this.section_list_menu;
            this.section_list_box.FormattingEnabled = true;
            this.section_list_box.HorizontalScrollbar = true;
            this.section_list_box.Location = new System.Drawing.Point(5, 75);
            this.section_list_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.section_list_box.Name = "section_list_box";
            this.section_list_box.Size = new System.Drawing.Size(337, 142);
            this.section_list_box.TabIndex = 7;
            this.section_list_box.TabStop = false;
            this.section_list_box.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.section_list_box_ItemCheck);
            // 
            // section_list_menu
            // 
            this.section_list_menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.section_list_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.section_view_menu,
            this.section_dump_menu});
            this.section_list_menu.Name = "section_list_boxMenu";
            this.section_list_menu.Size = new System.Drawing.Size(171, 60);
            // 
            // section_view_menu
            // 
            this.section_view_menu.Name = "section_view_menu";
            this.section_view_menu.Size = new System.Drawing.Size(170, 28);
            this.section_view_menu.Text = "Hex Editor";
            this.section_view_menu.Click += new System.EventHandler(this.sectionView_Click);
            // 
            // section_dump_menu
            // 
            this.section_dump_menu.Name = "section_dump_menu";
            this.section_dump_menu.Size = new System.Drawing.Size(170, 28);
            this.section_dump_menu.Text = "Dump";
            this.section_dump_menu.Click += new System.EventHandler(this.sectionDump_Click);
            // 
            // value_box
            // 
            this.value_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.value_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.value_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.value_box.ForeColor = System.Drawing.Color.White;
            this.value_box.Location = new System.Drawing.Point(69, 311);
            this.value_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.value_box.MaxLength = 500;
            this.value_box.Name = "value_box";
            this.value_box.Size = new System.Drawing.Size(115, 28);
            this.value_box.TabIndex = 10;
            this.value_box.Text = "0";
            // 
            // refresh_lock
            // 
            this.refresh_lock.Enabled = true;
            this.refresh_lock.Interval = 500;
            this.refresh_lock.Tick += new System.EventHandler(this.refresh_lock_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1254, 708);
            this.splitContainer1.SplitterDistance = 900;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 47;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.result_list_view);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.cheat_list_view);
            this.splitContainer4.Panel2.Controls.Add(this.refresh_cheat_list_btn);
            this.splitContainer4.Panel2.Controls.Add(this.load_cheat_list_btn);
            this.splitContainer4.Panel2.Controls.Add(this.save_cheat_list_btn);
            this.splitContainer4.Panel2.Controls.Add(this.new_cheat_list_btn);
            this.splitContainer4.Panel2.Controls.Add(this.toolbar);
            this.splitContainer4.Size = new System.Drawing.Size(900, 708);
            this.splitContainer4.SplitterDistance = 313;
            this.splitContainer4.SplitterWidth = 6;
            this.splitContainer4.TabIndex = 0;
            this.splitContainer4.TabStop = false;
            // 
            // result_list_view
            // 
            this.result_list_view.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.result_list_view_address,
            this.result_list_view_type,
            this.result_list_view_value,
            this.result_list_view_hex,
            this.result_list_view_section});
            this.result_list_view.ContextMenuStrip = this.result_list_menu;
            this.result_list_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.result_list_view.FullRowSelect = true;
            this.result_list_view.GridLines = true;
            this.result_list_view.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.result_list_view.Location = new System.Drawing.Point(0, 0);
            this.result_list_view.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.result_list_view.Name = "result_list_view";
            this.result_list_view.Size = new System.Drawing.Size(900, 313);
            this.result_list_view.TabIndex = 16;
            this.result_list_view.TabStop = false;
            this.result_list_view.UseCompatibleStateImageBehavior = false;
            this.result_list_view.View = System.Windows.Forms.View.Details;
            this.result_list_view.DoubleClick += new System.EventHandler(this.result_list_view_DoubleClick);
            // 
            // result_list_view_address
            // 
            this.result_list_view_address.Text = "Address";
            this.result_list_view_address.Width = 120;
            // 
            // result_list_view_type
            // 
            this.result_list_view_type.Text = "Type";
            this.result_list_view_type.Width = 120;
            // 
            // result_list_menu
            // 
            this.result_list_menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.result_list_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.result_list_view_add_to_cheat_list,
            this.result_list_view_separator,
            this.result_list_view_view_item,
            this.result_list_view_dump_item});
            this.result_list_menu.Name = "result_list_menu";
            this.result_list_menu.Size = new System.Drawing.Size(230, 94);
            // 
            // result_list_view_add_to_cheat_list
            // 
            this.result_list_view_add_to_cheat_list.Name = "result_list_view_add_to_cheat_list";
            this.result_list_view_add_to_cheat_list.Size = new System.Drawing.Size(229, 28);
            this.result_list_view_add_to_cheat_list.Text = "Add to Cheat List";
            this.result_list_view_add_to_cheat_list.Click += new System.EventHandler(this.result_list_view_add_to_cheat_list_Click);
            // 
            // result_list_view_separator
            // 
            this.result_list_view_separator.Name = "result_list_view_separator";
            this.result_list_view_separator.Size = new System.Drawing.Size(226, 6);
            // 
            // result_list_view_view_item
            // 
            this.result_list_view_view_item.Name = "result_list_view_view_item";
            this.result_list_view_view_item.Size = new System.Drawing.Size(229, 28);
            this.result_list_view_view_item.Text = "Hex Editor";
            this.result_list_view_view_item.Click += new System.EventHandler(this.result_list_view_hex_item_Click);
            // 
            // result_list_view_dump_item
            // 
            this.result_list_view_dump_item.Name = "result_list_view_dump_item";
            this.result_list_view_dump_item.Size = new System.Drawing.Size(229, 28);
            this.result_list_view_dump_item.Text = "Dump";
            this.result_list_view_dump_item.Click += new System.EventHandler(this.dump_item_Click);
            // 
            // cheat_list_view
            // 
            this.cheat_list_view.AllowUserToAddRows = false;
            this.cheat_list_view.AllowUserToResizeRows = false;
            this.cheat_list_view.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cheat_list_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cheat_list_view.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.cheat_list_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cheat_list_view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cheat_list_view_del,
            this.cheat_list_view_address,
            this.cheat_list_view_type,
            this.cheat_list_view_active,
            this.cheat_list_view_value,
            this.cheat_list_view_section,
            this.cheat_list_view_lock,
            this.cheat_list_view_description});
            this.cheat_list_view.ContextMenuStrip = this.cheat_list_menu;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cheat_list_view.DefaultCellStyle = dataGridViewCellStyle4;
            this.cheat_list_view.Location = new System.Drawing.Point(0, 55);
            this.cheat_list_view.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cheat_list_view.Name = "cheat_list_view";
            this.cheat_list_view.RowHeadersVisible = false;
            this.cheat_list_view.RowHeadersWidth = 50;
            this.cheat_list_view.RowTemplate.Height = 23;
            this.cheat_list_view.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cheat_list_view.Size = new System.Drawing.Size(900, 330);
            this.cheat_list_view.TabIndex = 50;
            this.cheat_list_view.TabStop = false;
            this.cheat_list_view.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cheat_list_view_CellClick);
            this.cheat_list_view.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.cheat_list_view_CellEndEdit);
            this.cheat_list_view.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.cheat_list_view_RowsRemoved);
            // 
            // cheat_list_view_del
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "Delete";
            this.cheat_list_view_del.DefaultCellStyle = dataGridViewCellStyle2;
            this.cheat_list_view_del.FillWeight = 10F;
            this.cheat_list_view_del.HeaderText = "Delete";
            this.cheat_list_view_del.Name = "cheat_list_view_del";
            // 
            // cheat_list_view_address
            // 
            this.cheat_list_view_address.FillWeight = 10F;
            this.cheat_list_view_address.HeaderText = "Address";
            this.cheat_list_view_address.Name = "cheat_list_view_address";
            this.cheat_list_view_address.ReadOnly = true;
            this.cheat_list_view_address.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cheat_list_view_type
            // 
            this.cheat_list_view_type.FillWeight = 5F;
            this.cheat_list_view_type.HeaderText = "Type";
            this.cheat_list_view_type.Name = "cheat_list_view_type";
            this.cheat_list_view_type.ReadOnly = true;
            this.cheat_list_view_type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cheat_list_view_active
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "X";
            this.cheat_list_view_active.DefaultCellStyle = dataGridViewCellStyle3;
            this.cheat_list_view_active.FillWeight = 5F;
            this.cheat_list_view_active.HeaderText = "X";
            this.cheat_list_view_active.Name = "cheat_list_view_active";
            this.cheat_list_view_active.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cheat_list_view_active.Text = "X";
            // 
            // cheat_list_view_value
            // 
            this.cheat_list_view_value.FillWeight = 45F;
            this.cheat_list_view_value.HeaderText = "Value";
            this.cheat_list_view_value.Name = "cheat_list_view_value";
            this.cheat_list_view_value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cheat_list_view_section
            // 
            this.cheat_list_view_section.FillWeight = 10F;
            this.cheat_list_view_section.HeaderText = "Section";
            this.cheat_list_view_section.Name = "cheat_list_view_section";
            this.cheat_list_view_section.ReadOnly = true;
            this.cheat_list_view_section.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cheat_list_view_lock
            // 
            this.cheat_list_view_lock.FillWeight = 5F;
            this.cheat_list_view_lock.HeaderText = "Lock";
            this.cheat_list_view_lock.Name = "cheat_list_view_lock";
            // 
            // cheat_list_view_description
            // 
            this.cheat_list_view_description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cheat_list_view_description.FillWeight = 10F;
            this.cheat_list_view_description.HeaderText = "Description";
            this.cheat_list_view_description.Name = "cheat_list_view_description";
            this.cheat_list_view_description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // toolbar
            // 
            this.toolbar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolbar.Location = new System.Drawing.Point(0, -7);
            this.toolbar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(899, 45);
            this.toolbar.TabIndex = 51;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.version_list);
            this.splitContainer2.Panel1.Controls.Add(this.processes_comboBox);
            this.splitContainer2.Panel1.Controls.Add(this.ip_box);
            this.splitContainer2.Panel1.Controls.Add(this.port_box);
            this.splitContainer2.Panel1.Controls.Add(this.send_payload_btn);
            this.splitContainer2.Panel1.Controls.Add(this.get_processes_btn);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.sectionsize_box);
            this.splitContainer2.Panel2.Controls.Add(this.langCombo);
            this.splitContainer2.Panel2.Controls.Add(this.value_label);
            this.splitContainer2.Panel2.Controls.Add(this.value_1_box);
            this.splitContainer2.Panel2.Controls.Add(this.and_label);
            this.splitContainer2.Panel2.Controls.Add(this.value_1_label);
            this.splitContainer2.Panel2.Controls.Add(this.select_all);
            this.splitContainer2.Panel2.Controls.Add(this.sectionfilter_button);
            this.splitContainer2.Panel2.Controls.Add(this.sectionfilter_box);
            this.splitContainer2.Panel2.Controls.Add(this.value_box);
            this.splitContainer2.Panel2.Controls.Add(this.hex_box);
            this.splitContainer2.Panel2.Controls.Add(this.alignment_box);
            this.splitContainer2.Panel2.Controls.Add(this.section_list_box);
            this.splitContainer2.Panel2.Controls.Add(this.msg);
            this.splitContainer2.Panel2.Controls.Add(this.progressBar);
            this.splitContainer2.Panel2.Controls.Add(this.compareTypeList);
            this.splitContainer2.Panel2.Controls.Add(this.valueTypeList);
            this.splitContainer2.Panel2.Controls.Add(this.next_scan_btn);
            this.splitContainer2.Panel2.Controls.Add(this.new_scan_btn);
            this.splitContainer2.Panel2.Controls.Add(this.refresh_btn);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(6);
            this.splitContainer2.Size = new System.Drawing.Size(348, 708);
            this.splitContainer2.SplitterDistance = 150;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 47;
            this.splitContainer2.TabStop = false;
            // 
            // version_list
            // 
            this.version_list.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.version_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.version_list.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.version_list.ForeColor = System.Drawing.Color.White;
            this.version_list.FormattingEnabled = true;
            this.version_list.Items.AddRange(new object[] {
            "5.05",
            "4.55",
            "4.05"});
            this.version_list.Location = new System.Drawing.Point(6, 11);
            this.version_list.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.version_list.Name = "version_list";
            this.version_list.Size = new System.Drawing.Size(90, 26);
            this.version_list.TabIndex = 0;
            this.version_list.SelectedIndexChanged += new System.EventHandler(this.version_list_SelectedIndexChanged);
            // 
            // processes_comboBox
            // 
            this.processes_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processes_comboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.processes_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.processes_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.processes_comboBox.ForeColor = System.Drawing.Color.White;
            this.processes_comboBox.FormattingEnabled = true;
            this.processes_comboBox.Location = new System.Drawing.Point(6, 83);
            this.processes_comboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.processes_comboBox.Name = "processes_comboBox";
            this.processes_comboBox.Size = new System.Drawing.Size(338, 26);
            this.processes_comboBox.TabIndex = 6;
            this.processes_comboBox.TabStop = false;
            this.processes_comboBox.SelectedIndexChanged += new System.EventHandler(this.processes_comboBox_SelectedIndexChanged);
            // 
            // ip_box
            // 
            this.ip_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ip_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ip_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ip_box.ForeColor = System.Drawing.Color.White;
            this.ip_box.Location = new System.Drawing.Point(107, 11);
            this.ip_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ip_box.MaxLength = 15;
            this.ip_box.Name = "ip_box";
            this.ip_box.Size = new System.Drawing.Size(165, 28);
            this.ip_box.TabIndex = 1;
            this.ip_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // port_box
            // 
            this.port_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.port_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.port_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.port_box.ForeColor = System.Drawing.Color.White;
            this.port_box.Location = new System.Drawing.Point(280, 11);
            this.port_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.port_box.MaxLength = 4;
            this.port_box.Name = "port_box";
            this.port_box.Size = new System.Drawing.Size(64, 28);
            this.port_box.TabIndex = 2;
            this.port_box.TabStop = false;
            this.port_box.Text = "9020";
            this.port_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // send_payload_btn
            // 
            this.send_payload_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.send_payload_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.send_payload_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.send_payload_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.send_payload_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.send_payload_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.send_payload_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send_payload_btn.ForeColor = System.Drawing.Color.White;
            this.send_payload_btn.Location = new System.Drawing.Point(6, 47);
            this.send_payload_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.send_payload_btn.Name = "send_payload_btn";
            this.send_payload_btn.Size = new System.Drawing.Size(338, 26);
            this.send_payload_btn.TabIndex = 3;
            this.send_payload_btn.TabStop = false;
            this.send_payload_btn.Text = "Send Payload";
            this.send_payload_btn.UseMnemonic = false;
            this.send_payload_btn.UseVisualStyleBackColor = false;
            this.send_payload_btn.Click += new System.EventHandler(this.send_payload_btn_Click);
            // 
            // get_processes_btn
            // 
            this.get_processes_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.get_processes_btn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.get_processes_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.get_processes_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.get_processes_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.get_processes_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.get_processes_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.get_processes_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.get_processes_btn.ForeColor = System.Drawing.Color.White;
            this.get_processes_btn.Location = new System.Drawing.Point(6, 119);
            this.get_processes_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.get_processes_btn.Name = "get_processes_btn";
            this.get_processes_btn.Size = new System.Drawing.Size(338, 26);
            this.get_processes_btn.TabIndex = 5;
            this.get_processes_btn.TabStop = false;
            this.get_processes_btn.Text = "Refresh Processes";
            this.get_processes_btn.UseMnemonic = false;
            this.get_processes_btn.UseVisualStyleBackColor = false;
            this.get_processes_btn.Click += new System.EventHandler(this.get_processes_btn_Click);
            // 
            // sectionsize_box
            // 
            this.sectionsize_box.Enabled = false;
            this.sectionsize_box.Location = new System.Drawing.Point(5, 42);
            this.sectionsize_box.Maximum = new decimal(new int[] {
            1073741824,
            0,
            0,
            0});
            this.sectionsize_box.Name = "sectionsize_box";
            this.sectionsize_box.Size = new System.Drawing.Size(120, 28);
            this.sectionsize_box.TabIndex = 36;
            this.sectionsize_box.ValueChanged += new System.EventHandler(this.sectionsize_box_ValueChanged);
            // 
            // langCombo
            // 
            this.langCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.langCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.langCombo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.langCombo.FormattingEnabled = true;
            this.langCombo.Location = new System.Drawing.Point(5, 514);
            this.langCombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.langCombo.Name = "langCombo";
            this.langCombo.Size = new System.Drawing.Size(112, 26);
            this.langCombo.TabIndex = 35;
            this.langCombo.SelectedIndexChanged += new System.EventHandler(this.langCombo_SelectedChanged);
            // 
            // value_label
            // 
            this.value_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.value_label.AutoSize = true;
            this.value_label.ForeColor = System.Drawing.Color.White;
            this.value_label.Location = new System.Drawing.Point(69, 289);
            this.value_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.value_label.Name = "value_label";
            this.value_label.Size = new System.Drawing.Size(62, 18);
            this.value_label.TabIndex = 31;
            this.value_label.Text = "Value:";
            // 
            // value_1_box
            // 
            this.value_1_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.value_1_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.value_1_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.value_1_box.ForeColor = System.Drawing.Color.White;
            this.value_1_box.Location = new System.Drawing.Point(229, 311);
            this.value_1_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.value_1_box.MaxLength = 31;
            this.value_1_box.Name = "value_1_box";
            this.value_1_box.Size = new System.Drawing.Size(115, 28);
            this.value_1_box.TabIndex = 30;
            this.value_1_box.Text = "0";
            // 
            // and_label
            // 
            this.and_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.and_label.AutoSize = true;
            this.and_label.Location = new System.Drawing.Point(189, 316);
            this.and_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.and_label.Name = "and_label";
            this.and_label.Size = new System.Drawing.Size(35, 18);
            this.and_label.TabIndex = 29;
            this.and_label.Text = "and";
            // 
            // value_1_label
            // 
            this.value_1_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.value_1_label.AutoSize = true;
            this.value_1_label.ForeColor = System.Drawing.Color.White;
            this.value_1_label.Location = new System.Drawing.Point(229, 289);
            this.value_1_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.value_1_label.Name = "value_1_label";
            this.value_1_label.Size = new System.Drawing.Size(62, 18);
            this.value_1_label.TabIndex = 28;
            this.value_1_label.Text = "Value:";
            // 
            // select_all
            // 
            this.select_all.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.select_all.AutoSize = true;
            this.select_all.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.select_all.Location = new System.Drawing.Point(5, 264);
            this.select_all.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.select_all.Name = "select_all";
            this.select_all.Size = new System.Drawing.Size(124, 22);
            this.select_all.TabIndex = 8;
            this.select_all.Text = "Select All";
            this.select_all.UseVisualStyleBackColor = true;
            this.select_all.Click += new System.EventHandler(this.select_all_check_box_Click);
            // 
            // sectionfilter_button
            // 
            this.sectionfilter_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sectionfilter_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.sectionfilter_button.Enabled = false;
            this.sectionfilter_button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.sectionfilter_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.sectionfilter_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.sectionfilter_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sectionfilter_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sectionfilter_button.ForeColor = System.Drawing.Color.White;
            this.sectionfilter_button.Location = new System.Drawing.Point(225, 7);
            this.sectionfilter_button.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sectionfilter_button.Name = "sectionfilter_button";
            this.sectionfilter_button.Size = new System.Drawing.Size(118, 26);
            this.sectionfilter_button.TabIndex = 34;
            this.sectionfilter_button.TabStop = false;
            this.sectionfilter_button.Text = "Filter";
            this.sectionfilter_button.UseMnemonic = false;
            this.sectionfilter_button.UseVisualStyleBackColor = false;
            this.sectionfilter_button.Click += new System.EventHandler(this.filter_sections);
            // 
            // sectionfilter_box
            // 
            this.sectionfilter_box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sectionfilter_box.Enabled = false;
            this.sectionfilter_box.Location = new System.Drawing.Point(5, 9);
            this.sectionfilter_box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sectionfilter_box.Name = "sectionfilter_box";
            this.sectionfilter_box.Size = new System.Drawing.Size(213, 28);
            this.sectionfilter_box.TabIndex = 33;
            // 
            // hex_box
            // 
            this.hex_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hex_box.AutoSize = true;
            this.hex_box.Location = new System.Drawing.Point(5, 314);
            this.hex_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.hex_box.Name = "hex_box";
            this.hex_box.Size = new System.Drawing.Size(61, 22);
            this.hex_box.TabIndex = 32;
            this.hex_box.Text = "Hex";
            this.hex_box.UseVisualStyleBackColor = true;
            // 
            // alignment_box
            // 
            this.alignment_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.alignment_box.AutoSize = true;
            this.alignment_box.Checked = true;
            this.alignment_box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alignment_box.Location = new System.Drawing.Point(229, 264);
            this.alignment_box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.alignment_box.Name = "alignment_box";
            this.alignment_box.Size = new System.Drawing.Size(115, 22);
            this.alignment_box.TabIndex = 9;
            this.alignment_box.Text = "Alignment";
            this.alignment_box.UseVisualStyleBackColor = true;
            // 
            // msg
            // 
            this.msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msg.AutoSize = true;
            this.msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg.ForeColor = System.Drawing.Color.Lime;
            this.msg.Location = new System.Drawing.Point(5, 459);
            this.msg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(103, 25);
            this.msg.TabIndex = 21;
            this.msg.Text = "Messages";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(5, 486);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(338, 26);
            this.progressBar.TabIndex = 22;
            // 
            // compareTypeList
            // 
            this.compareTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.compareTypeList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.compareTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.compareTypeList.Enabled = false;
            this.compareTypeList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.compareTypeList.ForeColor = System.Drawing.Color.White;
            this.compareTypeList.FormattingEnabled = true;
            this.compareTypeList.Location = new System.Drawing.Point(179, 342);
            this.compareTypeList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.compareTypeList.Name = "compareTypeList";
            this.compareTypeList.Size = new System.Drawing.Size(165, 26);
            this.compareTypeList.TabIndex = 12;
            this.compareTypeList.SelectedIndexChanged += new System.EventHandler(this.compareList_SelectedIndexChanged);
            // 
            // valueTypeList
            // 
            this.valueTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTypeList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.valueTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valueTypeList.Enabled = false;
            this.valueTypeList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.valueTypeList.ForeColor = System.Drawing.Color.White;
            this.valueTypeList.FormattingEnabled = true;
            this.valueTypeList.Location = new System.Drawing.Point(5, 342);
            this.valueTypeList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.valueTypeList.Name = "valueTypeList";
            this.valueTypeList.Size = new System.Drawing.Size(165, 26);
            this.valueTypeList.TabIndex = 11;
            this.valueTypeList.SelectedIndexChanged += new System.EventHandler(this.valueTypeList_SelectedIndexChanged);
            // 
            // next_scan_btn
            // 
            this.next_scan_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.next_scan_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.next_scan_btn.Enabled = false;
            this.next_scan_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.next_scan_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.next_scan_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.next_scan_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.next_scan_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.next_scan_btn.ForeColor = System.Drawing.Color.White;
            this.next_scan_btn.Location = new System.Drawing.Point(5, 431);
            this.next_scan_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.next_scan_btn.Name = "next_scan_btn";
            this.next_scan_btn.Size = new System.Drawing.Size(338, 26);
            this.next_scan_btn.TabIndex = 15;
            this.next_scan_btn.UseMnemonic = false;
            this.next_scan_btn.UseVisualStyleBackColor = false;
            this.next_scan_btn.Click += new System.EventHandler(this.next_scan_btn_Click);
            // 
            // new_scan_btn
            // 
            this.new_scan_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.new_scan_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.new_scan_btn.Enabled = false;
            this.new_scan_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.new_scan_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.new_scan_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.new_scan_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.new_scan_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.new_scan_btn.ForeColor = System.Drawing.Color.White;
            this.new_scan_btn.Location = new System.Drawing.Point(5, 375);
            this.new_scan_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.new_scan_btn.Name = "new_scan_btn";
            this.new_scan_btn.Size = new System.Drawing.Size(338, 26);
            this.new_scan_btn.TabIndex = 13;
            this.new_scan_btn.UseMnemonic = false;
            this.new_scan_btn.UseVisualStyleBackColor = false;
            this.new_scan_btn.Click += new System.EventHandler(this.new_scan_btn_Click);
            // 
            // refresh_btn
            // 
            this.refresh_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.refresh_btn.Enabled = false;
            this.refresh_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.refresh_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.refresh_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(92)))));
            this.refresh_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refresh_btn.ForeColor = System.Drawing.Color.White;
            this.refresh_btn.Location = new System.Drawing.Point(5, 403);
            this.refresh_btn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refresh_btn.Name = "refresh_btn";
            this.refresh_btn.Size = new System.Drawing.Size(338, 26);
            this.refresh_btn.TabIndex = 14;
            this.refresh_btn.UseMnemonic = false;
            this.refresh_btn.UseVisualStyleBackColor = false;
            this.refresh_btn.Click += new System.EventHandler(this.refresh_Click);
            // 
            // new_scan_worker
            // 
            this.new_scan_worker.WorkerReportsProgress = true;
            this.new_scan_worker.WorkerSupportsCancellation = true;
            this.new_scan_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.new_scan_worker_DoWork);
            this.new_scan_worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.new_scan_worker_ProgressChanged);
            this.new_scan_worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.new_scan_worker_RunWorkerCompleted);
            // 
            // next_scan_worker
            // 
            this.next_scan_worker.WorkerReportsProgress = true;
            this.next_scan_worker.WorkerSupportsCancellation = true;
            this.next_scan_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.next_scan_worker_DoWork);
            this.next_scan_worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.update_result_list_worker_ProgressChanged);
            this.next_scan_worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.next_scan_worker_RunWorkerCompleted);
            // 
            // update_result_list_worker
            // 
            this.update_result_list_worker.WorkerReportsProgress = true;
            this.update_result_list_worker.WorkerSupportsCancellation = true;
            this.update_result_list_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.update_result_list_worker_DoWork);
            this.update_result_list_worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.update_result_list_worker_ProgressChanged);
            this.update_result_list_worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.update_result_list_worker_RunWorkerCompleted);
            // 
            // main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1254, 708);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.parent);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "main";
            this.Opacity = 0.99D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PS4 Cheater";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Load += new System.EventHandler(this.main_Load);
            this.cheat_list_menu.ResumeLayout(false);
            this.section_list_menu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.result_list_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cheat_list_view)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sectionsize_box)).EndInit();
            this.ResumeLayout(false);

        }

        private ComboBox version_list;
        private ContextMenuStrip result_list_menu;
        private ToolStripMenuItem result_list_view_view_item;
        private ToolStripMenuItem result_list_view_dump_item;
        private TextBox value_1_box;
        private Label value_label;
        private CheckBox alignment_box;
        private BackgroundWorker update_result_list_worker;
        private ToolStripSeparator result_list_view_separator;
        private ToolStripMenuItem result_list_view_add_to_cheat_list;
        private ContextMenuStrip cheat_list_menu;
        private ToolStripMenuItem cheat_list_item_lock;
        private ToolStripMenuItem cheat_list_item_active;
        private ToolStripSeparator cheat_list_item_separator_0;
        private ToolStripMenuItem cheat_list_item_delete;
        private ToolStripMenuItem cheat_list_item_unlock;
        private ToolStripSeparator cheat_list_item_separator_1;
        private ToolStripMenuItem cheat_list_item_hex_view;
        private CheckBox hex_box;
        private ToolStripMenuItem cheat_list_item_find_pointer;
        private ToolStripSeparator cheat_list_item_separator_2;
        private TextBox sectionfilter_box;
        private Button sectionfilter_button;
        private Label and_label;
        private MappedSection curSection;
        private ComboBox langCombo;
        private FlowLayoutPanel toolbar;
        private DataGridView cheat_list_view;
        private DataGridViewButtonColumn cheat_list_view_del;
        private DataGridViewTextBoxColumn cheat_list_view_address;
        private DataGridViewTextBoxColumn cheat_list_view_type;
        private DataGridViewButtonColumn cheat_list_view_active;
        private DataGridViewTextBoxColumn cheat_list_view_value;
        private DataGridViewTextBoxColumn cheat_list_view_section;
        private DataGridViewCheckBoxColumn cheat_list_view_lock;
        private DataGridViewTextBoxColumn cheat_list_view_description;
        private NumericUpDown sectionsize_box;
    }
}
