﻿using System.Windows.Forms;

namespace Hook
{
    partial class KeysSpy
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
            this.tb = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelHide = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb
            // 
            this.tb.Location = new System.Drawing.Point(12, 12);
            this.tb.Name = "tb";
            this.tb.ReadOnly = true;
            this.tb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tb.ShortcutsEnabled = false;
            this.tb.Size = new System.Drawing.Size(473, 485);
            this.tb.TabIndex = 0;
            this.tb.Text = "";
            this.tb.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPressed);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 523);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(410, 523);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelHide
            // 
            this.labelHide.AutoSize = true;
            this.labelHide.Location = new System.Drawing.Point(500, 389);
            this.labelHide.Name = "labelHide";
            this.labelHide.Size = new System.Drawing.Size(0, 12);
            this.labelHide.TabIndex = 3;
            // 
            // KeysSpy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 572);
            this.Controls.Add(this.labelHide);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(513, 611);
            this.MinimumSize = new System.Drawing.Size(513, 611);
            this.Name = "KeysSpy";
            this.ShowIcon = false;
            this.Text = "KeysSpy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox tb;
        private Button button1;
        private Button button2;
        private Label labelHide;
    }
}

