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
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
		string[] title = { "title1", "title2", "title3" };	//此处填你的题目
		int index;
        public Form4()
        {
            InitializeComponent();
            //this.ControlBox = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
			if (this.textBox1.Text == "")
			{
				MessageBox.Show("You haven't wrote anything!");
				return;
			}
			int file_num = 0;
			string path = System.Environment.CurrentDirectory;
			try
			{
				file_num = System.IO.Directory.GetFiles(path + @"\Submitted_essay", "*.doc").Length + 1;
			}
			catch
			{
				MessageBox.Show("System can't find the storage path!");
				return;
			}

			this.button3.Enabled = false;
			object submit_filename = path + @"\Submitted_essay\essay" + file_num.ToString() + ".doc";
			Word._Application wordapp = new Word.ApplicationClass();
			string content;
			object nothing = Missing.Value;
			Word._Document worddoc = wordapp.Documents.Add(ref nothing, ref nothing, ref nothing, ref nothing);
			object format = Word.WdSaveFormat.wdFormatDocument;

			content = textBox1.Text;
			worddoc.Paragraphs.Last.Range.Text = content;

			worddoc.SaveAs2(ref submit_filename, ref format, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing);
			worddoc.Close(ref nothing, ref nothing, ref nothing);
			wordapp.Quit(ref nothing, ref nothing, ref nothing);

			MessageBox.Show("Submit successfully and the Model essay will show after closing this box!");

			this.label2.Text = "Model essay";
			this.textBox1.Text = "";
			string model_filename = path + @"\Model_essay\Title" + (index + 1) + ".doc";
			try
			{
				openword(ref textBox1, model_filename);
			}
			catch
			{
				MessageBox.Show("System can't find Model essay!");
				this.button3.Enabled = true;
				return;
			}
        }

        private void Form4_Load(object sender, EventArgs e)
        {
			Random ra = new Random();
			index = ra.Next(0, 3);
			this.label1.Text = title[index];
        }

		private void openword(ref TextBox TB, string filename)
		{
			Microsoft.Office.Interop.Word.ApplicationClass app = new Microsoft.Office.Interop.Word.ApplicationClass();
			Microsoft.Office.Interop.Word._Document doc = null;
			object missing = System.Reflection.Missing.Value;
			object File = filename;
			object readOnly = false;
			object isVisible = true;

			doc = app.Documents.Open(ref File, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);

			doc.ActiveWindow.Selection.WholeStory();//全选word文档中的数据  
			doc.ActiveWindow.Selection.Copy();//复制数据到剪切板  
			TB.Paste();
			

			if (doc != null)
			{
				doc.Close(ref missing, ref missing, ref missing);
				doc = null;
			}

			if (app != null)
			{
				app.Quit(ref missing, ref missing, ref missing);
				app = null;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

    }
}
