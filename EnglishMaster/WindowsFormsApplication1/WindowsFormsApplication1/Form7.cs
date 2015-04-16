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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            richTextBox2.ReadOnly = true;
            richTextBox1.ReadOnly = true;
        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.ReadOnly=false;
            OpenFileDialog openFile1 = new OpenFileDialog();

            openFile1.Filter = "Word 文档 (*.docx)|*.docx|Word 97-2003文档 (*.doc)|*.doc";

            if (openFile1.ShowDialog() == DialogResult.OK)


                openword(ref richTextBox2, openFile1.FileName);

           richTextBox2.ReadOnly = true;


            /*SaveFileDialog saveFile1 = new SaveFileDialog();

           

            saveFile1.Filter = "Word文档 (*.doc)|*.doc";

            
            if (saveFile1.ShowDialog() == DialogResult.OK )
            
               
                richTextBox2.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);*/
            
           
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 fm1 = new Form1();
            this.Close();
            
            fm1.Show();
           
        }
        private void openword(ref RichTextBox RTB, string filename)
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

                           
                openword(ref richTextBox1,openFile1.FileName);

            
         


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
