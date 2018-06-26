namespace PS4_Cheater
{
    using System.Drawing;
    using System.Windows.Forms;
    partial class HexEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.cheat_btn = new System.Windows.Forms.Button();
            this.input_box = new System.Windows.Forms.TextBox();
            this.find = new System.Windows.Forms.Button();
            this.refresh_btn = new System.Windows.Forms.Button();
            this.previous_btn = new System.Windows.Forms.Button();
            this.commit_btn = new System.Windows.Forms.Button();
            this.next_btn = new System.Windows.Forms.Button();
            this.page_list = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.hexBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cheat_btn);
            this.splitContainer1.Panel2.Controls.Add(this.input_box);
            this.splitContainer1.Panel2.Controls.Add(this.find);
            this.splitContainer1.Panel2.Controls.Add(this.refresh_btn);
            this.splitContainer1.Panel2.Controls.Add(this.previous_btn);
            this.splitContainer1.Panel2.Controls.Add(this.commit_btn);
            this.splitContainer1.Panel2.Controls.Add(this.next_btn);
            this.splitContainer1.Panel2.Controls.Add(this.page_list);
            this.splitContainer1.Size = new System.Drawing.Size(1182, 726);
            this.splitContainer1.SplitterDistance = 859;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 4;
            // 
            // hexBox
            // 
            this.hexBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Location = new System.Drawing.Point(0, 0);
            this.hexBox.Margin = new System.Windows.Forms.Padding(4);
            this.hexBox.Name = "hexBox";
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.ShadowSelectionVisible = false;
            this.hexBox.Size = new System.Drawing.Size(859, 726);
            this.hexBox.StringViewVisible = true;
            this.hexBox.TabIndex = 11;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            this.hexBox.Copied += new System.EventHandler(this.hexBox_Copied);
            this.hexBox.CopiedHex += new System.EventHandler(this.hexBox_CopiedHex);
            this.hexBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hexBox_KeyPress);
            this.hexBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hexBox_KeyDown);
            // 
            // cheat_btn
            // 
            this.cheat_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cheat_btn.Location = new System.Drawing.Point(20, 290);
            this.cheat_btn.Margin = new System.Windows.Forms.Padding(4);
            this.cheat_btn.Name = "cheat_btn";
            this.cheat_btn.Size = new System.Drawing.Size(171, 31);
            this.cheat_btn.TabIndex = 19;
            this.cheat_btn.Text = "Add Cheat";
            this.cheat_btn.UseVisualStyleBackColor = true;
            this.cheat_btn.Click += new System.EventHandler(this.cheat_btn_Click);
            // 
            // input_box
            // 
            this.input_box.Location = new System.Drawing.Point(20, 351);
            this.input_box.Margin = new System.Windows.Forms.Padding(4);
            this.input_box.Name = "input_box";
            this.input_box.Size = new System.Drawing.Size(212, 25);
            this.input_box.TabIndex = 18;
            // 
            // find
            // 
            this.find.Location = new System.Drawing.Point(20, 409);
            this.find.Margin = new System.Windows.Forms.Padding(4);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(213, 29);
            this.find.TabIndex = 17;
            this.find.Text = "Find";
            this.find.UseVisualStyleBackColor = true;
            this.find.Click += new System.EventHandler(this.find_Click);
            // 
            // refresh_btn
            // 
            this.refresh_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh_btn.Location = new System.Drawing.Point(20, 192);
            this.refresh_btn.Margin = new System.Windows.Forms.Padding(4);
            this.refresh_btn.Name = "refresh_btn";
            this.refresh_btn.Size = new System.Drawing.Size(171, 31);
            this.refresh_btn.TabIndex = 16;
            this.refresh_btn.Text = "Refresh";
            this.refresh_btn.UseVisualStyleBackColor = true;
            this.refresh_btn.Click += new System.EventHandler(this.refresh_btn_Click);
            // 
            // previous_btn
            // 
            this.previous_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previous_btn.Location = new System.Drawing.Point(20, 42);
            this.previous_btn.Margin = new System.Windows.Forms.Padding(4);
            this.previous_btn.Name = "previous_btn";
            this.previous_btn.Size = new System.Drawing.Size(171, 31);
            this.previous_btn.TabIndex = 12;
            this.previous_btn.Text = "Previous";
            this.previous_btn.UseVisualStyleBackColor = true;
            this.previous_btn.Click += new System.EventHandler(this.previous_btn_Click);
            // 
            // commit_btn
            // 
            this.commit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commit_btn.Location = new System.Drawing.Point(20, 242);
            this.commit_btn.Margin = new System.Windows.Forms.Padding(4);
            this.commit_btn.Name = "commit_btn";
            this.commit_btn.Size = new System.Drawing.Size(171, 31);
            this.commit_btn.TabIndex = 15;
            this.commit_btn.Text = "Commit";
            this.commit_btn.UseVisualStyleBackColor = true;
            this.commit_btn.Click += new System.EventHandler(this.commit_btn_Click);
            // 
            // next_btn
            // 
            this.next_btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.next_btn.Location = new System.Drawing.Point(20, 92);
            this.next_btn.Margin = new System.Windows.Forms.Padding(4);
            this.next_btn.Name = "next_btn";
            this.next_btn.Size = new System.Drawing.Size(171, 31);
            this.next_btn.TabIndex = 13;
            this.next_btn.Text = "Next";
            this.next_btn.UseVisualStyleBackColor = true;
            this.next_btn.Click += new System.EventHandler(this.next_btn_Click);
            // 
            // page_list
            // 
            this.page_list.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.page_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.page_list.FormattingEnabled = true;
            this.page_list.Location = new System.Drawing.Point(20, 142);
            this.page_list.Margin = new System.Windows.Forms.Padding(4);
            this.page_list.Name = "page_list";
            this.page_list.Size = new System.Drawing.Size(171, 23);
            this.page_list.TabIndex = 14;
            this.page_list.SelectedIndexChanged += new System.EventHandler(this.page_list_SelectedIndexChanged);
            // 
            // HexEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 726);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HexEditor";
            this.Text = "HexEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HexEdit_FormClosing);
            this.Load += new System.EventHandler(this.HexEdit_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private SplitContainer splitContainer1;
        private Be.Windows.Forms.HexBox hexBox;
        private Button refresh_btn;
        private Button previous_btn;
        private Button commit_btn;
        private Button next_btn;
        private ComboBox page_list;
        private TextBox input_box;
        private Button find;
        private Button cheat_btn;
    }
}