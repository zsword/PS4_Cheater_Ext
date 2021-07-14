using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace debugwatch
{
    partial class MemoryMapView
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
            this.CloseButton = new Button();
            this.MemoryMapCheckedListBox = new CheckedListBox();
            this.HelpLabel = new Label();
            this.AutoSelectButton = new Button();
            this.ClearSelectButton = new Button();
            this.SearchTextBox = new TextBox();
            this.SearchLabel = new Label();
            this.SuspendLayout();
            this.CloseButton.Location = new Point(204, 363);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new Size(75, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new EventHandler(this.CloseButton_Click);
            this.MemoryMapCheckedListBox.BackColor = Color.LightGray;
            this.MemoryMapCheckedListBox.Font = new Font("Consolas", 9f);
            this.MemoryMapCheckedListBox.FormattingEnabled = true;
            this.MemoryMapCheckedListBox.Location = new Point(12, 12);
            this.MemoryMapCheckedListBox.Name = "MemoryMapCheckedListBox";
            this.MemoryMapCheckedListBox.ScrollAlwaysVisible = true;
            this.MemoryMapCheckedListBox.Size = new Size(560, 344);
            this.MemoryMapCheckedListBox.TabIndex = 1;
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.Font = new Font("Consolas", 8.25f);
            this.HelpLabel.Location = new Point(12, 389);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new Size(421, 13);
            this.HelpLabel.TabIndex = 23;
            this.HelpLabel.Text = "Please select memory regions from above view in order to search them.";
            this.AutoSelectButton.Location = new Point(12, 363);
            this.AutoSelectButton.Name = "AutoSelectButton";
            this.AutoSelectButton.Size = new Size(75, 23);
            this.AutoSelectButton.TabIndex = 24;
            this.AutoSelectButton.Text = "Auto Select";
            this.AutoSelectButton.UseVisualStyleBackColor = true;
            this.AutoSelectButton.Click += new EventHandler(this.AutoSelectButton_Click);
            this.ClearSelectButton.Location = new Point(93, 363);
            this.ClearSelectButton.Name = "ClearSelectButton";
            this.ClearSelectButton.Size = new Size(105, 23);
            this.ClearSelectButton.TabIndex = 25;
            this.ClearSelectButton.Text = "Clear Selection";
            this.ClearSelectButton.UseVisualStyleBackColor = true;
            this.ClearSelectButton.Click += new EventHandler(this.ClearSelectButton_Click);
            this.SearchTextBox.Location = new Point(374, 365);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new Size(198, 20);
            this.SearchTextBox.TabIndex = 26;
            this.SearchTextBox.TextChanged += new EventHandler(this.SearchTextBox_TextChanged);
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Font = new Font("Consolas", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.SearchLabel.Location = new Point(319, 368);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new Size(49, 14);
            this.SearchLabel.TabIndex = 27;
            this.SearchLabel.Text = "Search";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(584, 411);
            this.Controls.Add((Control)this.SearchLabel);
            this.Controls.Add((Control)this.SearchTextBox);
            this.Controls.Add((Control)this.ClearSelectButton);
            this.Controls.Add((Control)this.AutoSelectButton);
            this.Controls.Add((Control)this.HelpLabel);
            this.Controls.Add((Control)this.MemoryMapCheckedListBox);
            this.Controls.Add((Control)this.CloseButton);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemoryMapView";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Memory Map";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}