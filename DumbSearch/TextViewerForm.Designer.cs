namespace DumbSearch
{
    partial class TextViewerForm
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
            this.uxRichTextBox = new System.Windows.Forms.RichTextBox();
            this.uxFoundPositionsListbox = new System.Windows.Forms.ListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.uxRichTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uxFoundPositionsListbox);
            this.splitContainer1.Size = new System.Drawing.Size(458, 266);
            this.splitContainer1.SplitterDistance = 347;
            this.splitContainer1.TabIndex = 0;
            // 
            // uxRichTextBox
            // 
            this.uxRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.uxRichTextBox.Name = "uxRichTextBox";
            this.uxRichTextBox.ShowSelectionMargin = true;
            this.uxRichTextBox.Size = new System.Drawing.Size(347, 266);
            this.uxRichTextBox.TabIndex = 0;
            this.uxRichTextBox.Text = "";
            // 
            // uxFoundPositionsListbox
            // 
            this.uxFoundPositionsListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxFoundPositionsListbox.FormattingEnabled = true;
            this.uxFoundPositionsListbox.Location = new System.Drawing.Point(0, 0);
            this.uxFoundPositionsListbox.Name = "uxFoundPositionsListbox";
            this.uxFoundPositionsListbox.Size = new System.Drawing.Size(107, 264);
            this.uxFoundPositionsListbox.TabIndex = 0;
            this.uxFoundPositionsListbox.SelectedIndexChanged += new System.EventHandler(this.uxFoundPositionsListbox_SelectedIndexChanged);
            // 
            // TextViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 266);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TextViewerForm";
            this.Text = "TextViewerForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox uxRichTextBox;
        private System.Windows.Forms.ListBox uxFoundPositionsListbox;
    }
}