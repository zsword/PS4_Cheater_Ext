using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace debugwatch
{
    partial class AssemblerView
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
            this.AssembleButton = new Button();
            this.CloseButton = new Button();
            this.AssemblerTextBox = new TextBox();
            this.SuspendLayout();
            // 
            // AssembleButton
            // 
            this.AssembleButton.Location = new Point(12, 326);
            this.AssembleButton.Name = "AssembleButton";
            this.AssembleButton.Size = new Size(75, 23);
            this.AssembleButton.TabIndex = 0;
            this.AssembleButton.Text = "Assemble";
            this.AssembleButton.UseVisualStyleBackColor = true;
            this.AssembleButton.Click += new EventHandler(this.AssembleButton_Click);
            // 
            // CloseButton
            //
            this.CloseButton.Location = new Point(93, 326);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new EventHandler(this.CloseButton_Click);
            // 
            // AssemblerTextBox
            //
            this.AssemblerTextBox.BackColor = SystemColors.ScrollBar;
            this.AssemblerTextBox.Font = new Font("Consolas", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.AssemblerTextBox.ForeColor = Color.Black;
            this.AssemblerTextBox.Location = new Point(12, 12);
            this.AssemblerTextBox.Multiline = true;
            this.AssemblerTextBox.Name = "AssemblerTextBox";
            this.AssemblerTextBox.ScrollBars = ScrollBars.Both;
            this.AssemblerTextBox.Size = new Size(460, 308);
            this.AssemblerTextBox.TabIndex = 2;
            this.AssemblerTextBox.WordWrap = false;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(484, 361);
            this.Controls.Add(this.AssemblerTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.AssembleButton);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssemblerView";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Assembler";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Button AssembleButton;
        private Button CloseButton;
        private TextBox AssemblerTextBox;
    }
}