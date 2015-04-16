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
using System.Runtime.InteropServices;
using System.Net;
using SpeechLib;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form5 : Form
    {
		string path = "";
		string record_name = "";

        public Form5()
        {
            InitializeComponent();
        }

		private void Form5_Load(object sender, EventArgs e)
		{
			path = Environment.CurrentDirectory;
			string filename = path + @"\Audio_essay\Audio_essay.txt";
			FileStream fs = new FileStream(filename, FileMode.Open);
			StreamReader sr = new StreamReader(fs);
			this.textBox1.Text = sr.ReadToEnd();
			fs.Close();
			sr.Close();
		}

		[DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]

		public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private void button3_Click(object sender, EventArgs e)
        {
			this.button3.BackColor = Color.White;
			this.button3.Enabled = false;
			this.button1.Enabled = true;
			this.button1.BackColor = Color.Red;
			mciSendString("set wave bitpersample 8", "", 0, 0);

			mciSendString("set waveaudio samplespersec 16000", "", 0, 0);
			mciSendString("set wave channels 2", "", 0, 0);
			mciSendString("set wave format tag pcm", "", 0, 0);
			mciSendString("open new type WAVEAudio alias movie", "", 0, 0);

			mciSendString("record movie", "", 0, 0);
        }

		private void button1_Click(object sender, EventArgs e)
		{
			mciSendString("stop movie", "", 0, 0);

			if (!(MessageBox.Show("Are you sure to submit record?", "Confirm submitting", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
				return;

			int file_num = 0;
			try
			{
				file_num = System.IO.Directory.GetFiles(path + @"\audio", "*.wav").Length + 1;
			}
			catch
			{
				MessageBox.Show("System can't find the storage path!");
				return;
			}

			record_name = path + @"\audio\submitted_audio" + file_num.ToString() + ".wav";

			mciSendString("save movie " + record_name, "", 0, 0);
			mciSendString("close movie", "", 0, 0);

			MessageBox.Show("Stroed successfully, please wait for uploading!");

			this.button1.Enabled = false;
			this.button3.Enabled = true;

			this.textBox2.Text = GoogleSTT(record_name);
			//Application.DoEvents();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private string GoogleSTT(string filename)
		{
			string result = "";
			//try
			//{
				FileStream fs = new FileStream(filename, FileMode.Open);
				byte[] voice = new byte[fs.Length];
				fs.Read(voice, 0, voice.Length);
				fs.Close();
				HttpWebRequest request = null;
				string url = "http://www.google.com/speech-api/v1/recognize?xjerr=1&client=chromium&lang=en-US";
				Uri uri = new Uri(url);
				request = (HttpWebRequest)WebRequest.Create(uri);
				request.Method = "POST";
				request.ContentType = "audio/wav; rate=16000";
				request.ContentLength = voice.Length;
				
				using (Stream writeStream = request.GetRequestStream())
				{
					writeStream.Write(voice, 0, voice.Length);
					writeStream.Close();
				}
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
						{
							result = readStream.ReadToEnd();
						}
					}
				}
				//result = System.Text.Encoding.Default.GetString(voice);
			/*}
			catch
			{
				MessageBox.Show("Fail to send your record to our server! Please try again later.");
				this.button1.Enabled = true;
				this.button3.Enabled = false;
				this.button3.BackColor = Color.White;
				return null;
			}*/
			return result;
		}

        
    }
}
