using System.Windows.Forms;

namespace Hook
{
    partial class Hook
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
            this.SuspendLayout();
            // 
            // tb
            // 
            this.tb.Location = new System.Drawing.Point(12, 12);
            this.tb.Name = "tb";
            this.tb.ReadOnly = true;
            this.tb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tb.Size = new System.Drawing.Size(473, 419);
            this.tb.TabIndex = 0;
            this.tb.Text = "";
            this.tb.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPressed);
            // 
            // Hook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 443);
            this.Controls.Add(this.tb);
            this.Name = "Hook";
            this.Text = "Hook";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tb;
    }
}

