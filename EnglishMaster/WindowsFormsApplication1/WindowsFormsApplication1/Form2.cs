using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            richTextBox2.Enabled = false;
        }
        string[] title = { "Dic1_Easy","Dic2_Normal", "Dic3_Hard","Dic4_video"};	//此处填你的题目
		int index=0;
        int file_num = 4;
       
        string path = System.Environment.CurrentDirectory+@"\listening\";
        string[] sourcename;
        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "音频文件（*.mp3)|*.mp3|视频文件（*.flv)|*.flv|所有文件|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {sourcename= openFileDialog1.FileNames;
            MessageBox.Show("Open successfully!Listen and writedown in the upper box!");
            
            System.Diagnostics.Process.Start(sourcename[0]);
            }
            else MessageBox.Show("Fail to open,try again!");
        }
      
      



        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fm1 = new Form1();
            fm1.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {   string ansname=sourcename[0];
            if (ansname.Length == 0)
            {
                MessageBox.Show("You Should open a test file first!");
                return;
             }
            richTextBox2.Text = "";
            int i = ansname.Length;
            while (ansname[i-1] != '.') i=i-1;
            i=i-1;int j=0;
            string openname = "";
            for (j = 0; j <= i - 1; j++) openname += ansname[j];
            openname += "_ans.txt";
            FileStream ans = File.OpenRead(openname);
            ans.Seek(0,SeekOrigin.Begin);
            for (i = 0; i < ans.Length; i++)
                richTextBox2.Text += (char)ans.ReadByte();
            richTextBox2.Enabled = true;
            richTextBox2.ReadOnly = true;
            MessageBox.Show("Load answer successfully!Compare it carefully!");
            ans.Close();
        }

        
        
    }
}
