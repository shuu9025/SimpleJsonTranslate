using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace SimpleTranslate
{
    public partial class Form1 : Form
    {
        Dictionary<dynamic, dynamic> json;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || listBox1.Items.Count != 0)
            {
                return;
            }
            try
            {
                json = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(textBox1.Text);
                listBox1.Items.Clear();
                foreach (var translatestring in json)
                {
                    listBox1.Items.Add(@translatestring.Key);
                }
                button2.Enabled = true;
                label4.Text = "JSON";
            }
            catch (Exception ex)
            {
                label4.Text = "JSON " + ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = JsonConvert.SerializeObject(json, Formatting.Indented);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "JSON エラー: " + ex.Message,
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = json.ElementAt(listBox1.SelectedIndex).Key;
            textBox2.Text = json.ElementAt(listBox1.SelectedIndex).Value.Replace("\n", "\r\n");
            textBox3.Text = json.ElementAt(listBox1.SelectedIndex).Value.Replace("\n", "\r\n");
            label1.Text = "翻訳する文章 (" + listBox1.SelectedIndex + ")";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            json[textBox4.Text] = textBox3.Text.Replace("\r\n", "\n");
            textBox1.Text = JsonConvert.SerializeObject(json, Formatting.Indented);
            try
            {
                listBox1.SelectedIndex += 1;


            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            textBox3.Focus();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.LineFeed) && (ModifierKeys == Keys.Control))
            {
                e.Handled = true;
                button2.PerformClick();
            }
        }
    }
}
