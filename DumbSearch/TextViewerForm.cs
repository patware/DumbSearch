using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DumbSearch
{
    public partial class TextViewerForm : Form
    {
        public int Length = 1;

        public struct FoundItem
        {

            public FoundItem(int position)
            {
                Title = "Position: " + position;
                Position = position;
            }
            public int Position;
            public string Title;
            public override string ToString()
            {
                return Title;
            }
        }
        public TextViewerForm()
        {
            InitializeComponent();
        }

        internal void LoadFile(System.IO.FileInfo file, string searchTerm)
        {
            Length = searchTerm.Length;

            uxRichTextBox.LoadFile(file.FullName, RichTextBoxStreamType.PlainText);
            
            int position = -1;

            while ((position = uxRichTextBox.Find(searchTerm, position + 1,RichTextBoxFinds.None)) >= 0)
            {
                uxRichTextBox.SelectionBackColor = Color.Red;
                uxFoundPositionsListbox.Items.Add(new FoundItem(position));
            }
        }

        private void uxFoundPositionsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FoundItem fi = (FoundItem)uxFoundPositionsListbox.SelectedItem;

            uxRichTextBox.Select(fi.Position, Length);
        }

    }
}