using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Threading;

namespace Webhook_
{
    public partial class Form1 : Form
    {
        private Thread t;
        public Form1()
        {
            InitializeComponent();
        }

        List<string> Urls = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains("https://"))
            {
                MessageBox.Show("It's wrong url");
                textBox1.Clear();
            }
            else
            {
                listBox1.Items.Add(textBox1.Text);
                Urls.Add(textBox1.Text);
                textBox1.Clear();
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                for (int i = 0; i < Urls.Count; i++)
                {
                    Functions.Promotion(this.Urls[i], richTextBox1.Text, textBox2.Text);
                    Thread.Sleep(Convert.ToInt32(numericUpDown1.Value));
                }
            }
            else
                return;
        }

        private void Thr()
        {
            while (true)
            {
                for (int i = 0; i < Urls.Count; i++)
                {
                    Functions.Promotion(this.Urls[i], richTextBox1.Text, textBox2.Text);
                    Thread.Sleep(Convert.ToInt32(numericUpDown1.Value));
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                t = new Thread(Thr) { IsBackground = true };
                t.Start();
                MessageBox.Show("loop start; sleep time: " + numericUpDown1.Value);
            }
            else
            {
                t.Abort();
                t.Join();
                MessageBox.Show("loop stop");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!textBox1.Text.Contains("https://"))
                {
                    MessageBox.Show("It's wrong url");
                    textBox1.Clear();
                }
                else
                {
                    listBox1.Items.Add(textBox1.Text);
                    Urls.Add(textBox1.Text);
                    textBox1.Clear();
                }
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                using (StreamWriter sw = new StreamWriter("./webhook_urls.txt"))
                {
                    for (int i = 0; i < Urls.Count; i++)
                    {
                        sw.WriteLine(Urls[i]);
                    }
                    sw.Close();
                }
                MessageBox.Show("urls save;\n"+ Application.StartupPath);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("./webhook_urls.txt");
            if (fi.Exists)
            {
                string[] textLines = File.ReadAllLines("./webhook_urls.txt");
                for (int i = 0; i < textLines.Length; i++)
                {
                    listBox1.Items.Add(textLines[i]);
                }
            }
            else
            {
                return;
            }
        }
    }
}
