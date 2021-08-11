using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace WordpadKillerV2
{
    public partial class Form1 : Form
    {
        //public override Size MinimumSize { get; set; }
        public Form1()
        {
            InitializeComponent();
            MinimumSize = new Size(800, 300);
            richTextBox1.AllowDrop = true;
            richTextBox1.DragDrop += richTextBox1_DragDrop;
            richTextBox1.AllowDrop = true;

            colorComboBox.DataSource = typeof(Color).GetProperties()
                .Where(x => x.PropertyType == typeof(Color))
                .Select(x => x.GetValue(null)).ToList();
            colorComboBox.IntegralHeight = false;
            colorComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            colorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            colorComboBox.DataSource = typeof(Color).GetProperties()
                .Where(x => x.PropertyType == typeof(Color))
                .Select(x => x.GetValue(null)).ToList();
            colorComboBox.IntegralHeight = false;
            colorComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            colorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            colorComboBox.DrawItem += colorComboBox_DrawItem;


        }

        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {

            object filename = e.Data.GetData("FileDrop");
            if (filename != null)
            {
                var list = filename as string[];

                if (list != null && !string.IsNullOrWhiteSpace(list[0]))
                {
                    richTextBox1.Clear();
                    richTextBox1.LoadFile(list[0], RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var fontFamily in FontFamily.Families)
            {
                fontComboBox.Items.Add(fontFamily.Name);
            }

            for (int i = 8; i <= 150; i += 2)
            {
                fontSizeComboBox.Items.Add(i);
            }

            fontSizeComboBox.SelectedIndex = 0;
            fontComboBox.SelectedIndex = 0;
        }

        private void colorComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var txt = colorComboBox.GetItemText(colorComboBox.Items[e.Index]);
            var color = (Color)colorComboBox.Items[e.Index];
            var r1 = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, 5 * (e.Bounds.Height - 2), e.Bounds.Height - 2);
            var r2 = Rectangle.FromLTRB(r1.Right + 2, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);
            using (var b = new SolidBrush(color))
                e.Graphics.FillRectangle(b, r1);
            e.Graphics.DrawRectangle(Pens.Black, r1);
            TextRenderer.DrawText(e.Graphics, txt, colorComboBox.Font, r2,
                colorComboBox.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var txt = colorComboBox.GetItemText(colorComboBox.Items[e.Index]);
            var color = (Color)colorComboBox.Items[e.Index];
            var r1 = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, 5 * (e.Bounds.Height - 2), e.Bounds.Height - 2);
            var r2 = Rectangle.FromLTRB(r1.Right + 2, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);
            using (var b = new SolidBrush(color))
                e.Graphics.FillRectangle(b, r1);
            e.Graphics.DrawRectangle(Pens.Black, r1);
            TextRenderer.DrawText(e.Graphics, txt, colorComboBox.Font, r2,
                colorComboBox.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);


/*            var txt = comboBox1.GetItemText(comboBox1.Items[e.Index]);
            if (e.Index < 0) return;
            comboBox1.GetItemText(comboBox1.Items[e.Index]);
            var color = (Color)comboBox1.Items[e.Index];
            using (var b = new SolidBrush(color))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }
            TextRenderer.DrawText(e.Graphics, txt, comboBox1.Font, e.Bounds.Location, comboBox1.ForeColor);*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void colorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(fontComboBox.Text, int.Parse(fontSizeComboBox.Text));
        }

        private void fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font(fontComboBox.Text, int.Parse(fontSizeComboBox.Text));
        }

        private void allignText_Click(object sender, EventArgs e)
        {
            Guna2Button b = sender as Guna2Button;
            if (b == allignLeft)
            {
                allignCenter.FillColor = Color.White;
                allignCenter.ForeColor = Color.Black;

                allignRight.FillColor = Color.White;
                allignRight.ForeColor = Color.Black;

                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                allignLeft.FillColor = Color.FromArgb(61, 169, 252);
                allignLeft.ForeColor = Color.White;
            }
            else if (b == allignCenter)
            {
                allignLeft.FillColor= Color.White;
                allignLeft.ForeColor = Color.Black;

                allignRight.FillColor = Color.White;
                allignRight.ForeColor = Color.Black;

                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                allignCenter.FillColor = Color.FromArgb(61, 169, 252);
                allignCenter.ForeColor = Color.White;
            }
            else
            {
                allignLeft.FillColor = Color.White;
                allignLeft.ForeColor = Color.Black;

                allignCenter.FillColor = Color.White;
                allignCenter.ForeColor = Color.Black;

                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                allignRight.FillColor = Color.FromArgb(61, 169, 252);
                allignRight.ForeColor = Color.White;
            }
        }

        private void fontStyle_CheckedChanged(object sender, EventArgs e)
        {

            if (bold.Checked && underline.Checked && italic.Checked)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            }
            else if (bold.Checked && underline.Checked)
            {

                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold | FontStyle.Underline);
            }
            else if (bold.Checked && italic.Checked)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold | FontStyle.Italic);
            }
            else if (underline.Checked && italic.Checked)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline | FontStyle.Italic);
            }
            else if (bold.Checked)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
            }
            else if (underline.Checked)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline);
            }
            else if (italic.Checked)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Italic);
            }
            else
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Style == FontStyle.Bold)
            {
                italic.Checked = false;
                underline.Checked = false;
                bold.Checked = true;
            }
            else if (richTextBox1.SelectionFont.Style == FontStyle.Italic)
            {
                underline.Checked = false;
                bold.Checked = false;
                italic.Checked = true;
            }
            else if (richTextBox1.SelectionFont.Style == FontStyle.Underline)
            {
                bold.Checked = false;
                italic.Checked = false;
                underline.Checked = true;
            }
            else
            {
                bold.Checked = true;
                italic.Checked = true;
                underline.Checked = true;
            }
        }
    }
}
