using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DeepAI;
using Newtonsoft.Json.Linq;

namespace TextureToolkitProject
{
    public partial class Form1 : Form
    {
        private bool isFileSelected = false;
        public Form1()
        {
            InitializeComponent();
        }
        
        
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            label1.Text = openFileDialog1.FileName;
            isFileSelected = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.CheckFileExists && isFileSelected)
            {
                pictureBox1.Visible = false;
                MessageBox.Show("Please wait a few seconds! Your file is being uploaded and upscaled.");
                DeepAI_API api = new DeepAI_API(apiKey: "Enter your DeepAI Licencekey here.");

                StandardApiResponse resp = api.callStandardApi("torch-srgan", new {
                    image = File.OpenRead(openFileDialog1.FileName),
                });
                dynamic d = JObject.Parse(api.objectAsJsonString(resp));
                Console.Write(d.output_url);

                pictureBox1.ImageLocation = d.output_url;
                pictureBox1.Visible = true;
                
                MessageBox.Show("Your file has been upscaled and is now available in the output folder.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.ShowDialog();
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            pictureBox1.Image.Save(Application.StartupPath + "/" + openFileDialog1.SafeFileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe" , Application.StartupPath);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {
            OpenUrl("https://discord.gg/4PAAwDW");
        }
        
        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") {CreateNoWindow = true});
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}