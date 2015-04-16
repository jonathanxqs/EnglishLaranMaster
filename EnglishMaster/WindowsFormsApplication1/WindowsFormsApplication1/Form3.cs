using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            richTextBox1.ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 fm1 = new Form1();
            
            this.Hide();
            fm1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
           
                  
                  
                  
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void openword(ref RichTextBox RTB, string filename)
        {
            Microsoft.Office.Interop.Word.ApplicationClass app = new Microsoft.Office.Interop.Word.ApplicationClass();
            Microsoft.Office.Interop.Word._Document doc = null;
            object missing = System.Reflection.Missing.Value;
            object File = filename;
            object readOnly =false;
            object isVisible = true;

            doc = app.Documents.Open(ref File, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);

            doc.ActiveWindow.Selection.WholeStory();//全选word文档中的数据  
            doc.ActiveWindow.Selection.Copy();//复制数据到剪切板  
            RTB.Paste();


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


        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = false;
            OpenFileDialog openFile1 = new OpenFileDialog();

            openFile1.Filter = "Word 文档 (*.docx)|*.docx|Word 97-2003文档 (*.doc)|*.doc";

            if (openFile1.ShowDialog() == DialogResult.OK)

           openword(ref richTextBox1, openFile1.FileName);

            richTextBox1.ReadOnly = true;
          


        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form7 fm7 = new Form7();
            fm7.Show();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            
            f7.Show();
           
        }

        

       
    }
}
