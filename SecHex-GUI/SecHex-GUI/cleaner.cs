using Microsoft.Win32;
using Siticone.Desktop.UI.WinForms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.ServiceProcess;

namespace SecHex_GUI
{

    public partial class south_africa : MetroFramework.Forms.MetroForm
    {
        private System.Windows.Forms.Timer timer;
        private float animationProgress = 0.0f;
        private int steps = 200;
        private Color startColor = Color.FromArgb(23, 23, 23);
        private Color middleColor = Color.FromArgb(248, 248, 248);
        private Color endColor = Color.FromArgb(23, 23, 23);
        private Color currentColor;
        private bool isDragging;
        private Point offset;
        private bool isAnimationRunning = false;
        private Color previousColor;

        public south_africa()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 340;


            this.DoubleBuffered = true;
            timer.Start();

            previousColor = currentColor;
        }



        // Functions 
        private void FlushDnsCache()
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = "ipconfig";
                startInfo.Arguments = "/flushdns";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                MessageBox.Show("DNS-Cache cleared.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void ClearWindowsLogs()
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = "wevtutil";
                startInfo.Arguments = "el";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                MessageBox.Show("Windows Logs cleared. ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearTemporaryCache()
        {
            try
            {
                string tempFolderPath = Path.GetTempPath();
                DirectoryInfo tempDirectory = new DirectoryInfo(tempFolderPath);

                DateTime thresholdDate = DateTime.Now.AddDays(-7);

                foreach (FileInfo file in tempDirectory.GetFiles())
                {
                    if (file.LastWriteTime < thresholdDate)
                    {
                        file.Delete();
                    }
                }

                foreach (DirectoryInfo subDirectory in tempDirectory.GetDirectories())
                {
                    subDirectory.Delete(true);
                }

                MessageBox.Show("Temporary cache cleared.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearWindowsTempLol()
        {
            try
            {
                string tempFolderPath = Path.GetTempPath();
                DirectoryInfo tempDirectory = new DirectoryInfo(tempFolderPath);

                foreach (FileInfo file in tempDirectory.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo subDirectory in tempDirectory.GetDirectories())
                {
                    subDirectory.Delete(true);
                }

                MessageBox.Show("Windows Temp folder cleared.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void TcpRst()
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = "netsh";
                startInfo.Arguments = "int ip reset";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                MessageBox.Show("TCP/IP reset successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CookieRst()
        {
            try
            {
                string chromeCookiesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google\\Chrome\\User Data\\Default\\Cookies");

                if (File.Exists(chromeCookiesPath))
                {
                    File.Delete(chromeCookiesPath);
                    MessageBox.Show("Chrome cookies cleared.");
                }
                else
                {
                    MessageBox.Show("Chrome cookies not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FirefoxCookieReset()
        {
            try
            {
                string firefoxCookiesPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Mozilla\\Firefox\\Profiles");

                string[] profileFolders = Directory.GetDirectories(firefoxCookiesPath);

                if (profileFolders.Length > 0)
                {
                    string selectedProfileFolder = profileFolders[0];

                    string cookiesFilePath = Path.Combine(selectedProfileFolder, "cookies.sqlite");

                    if (File.Exists(cookiesFilePath))
                    {
                        File.Delete(cookiesFilePath);
                        MessageBox.Show("Firefox cookies cleared.");
                    }
                    else
                    {
                        MessageBox.Show("Firefox cookies not found.");
                    }
                }
                else
                {
                    MessageBox.Show("No Firefox profiles found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // THANKS TO Starcharms -> github.com/starcharms
        private void rstreset()
        {
            try
            {
                RunCommand("reg", "add \"HKLM\\System\\CurrentControlSet\\Services\\BFE\" /v \"Start\" /t REG_DWORD /d \"2\" /f");
                RunCommand("reg", "add \"HKLM\\System\\CurrentControlSet\\Services\\Dnscache\" /v \"Start\" /t REG_DWORD /d \"2\" /f");
                RunCommand("reg", "add \"HKLM\\System\\CurrentControlSet\\Services\\MpsSvc\" /v \"Start\" /t REG_DWORD /d \"2\" /f");
                RunCommand("reg", "add \"HKLM\\System\\CurrentControlSet\\Services\\WinHttpAutoProxySvc\" /v \"Start\" /t REG_DWORD /d \"3\" /f");
                RunCommand("sc", "config Dhcp start= auto");
                RunCommand("sc", "config DPS start= auto");
                RunCommand("sc", "config lmhosts start= auto");
                RunCommand("sc", "config NlaSvc start= auto");
                RunCommand("sc", "config nsi start= auto");
                RunCommand("sc", "config RmSvc start= auto");
                RunCommand("sc", "config Wcmsvc start= auto");
                RunCommand("sc", "config WdiServiceHost start= demand");
                RunCommand("sc", "config Winmgmt start= auto");
                RunCommand("sc", "config NcbService start= demand");
                RunCommand("sc", "config Netman start= demand");
                RunCommand("sc", "config netprofm start= demand");
                RunCommand("sc", "config WlanSvc start= auto");
                RunCommand("sc", "config WwanSvc start= demand");
                RunCommand("net", "start Dhcp");
                RunCommand("net", "start DPS");
                RunCommand("net", "start NlaSvc");
                RunCommand("net", "start nsi");
                RunCommand("net", "start RmSvc");
                RunCommand("net", "start Wcmsvc");

                DisableNetworkAdapter(0);
                DisableNetworkAdapter(1);
                DisableNetworkAdapter(2);
                DisableNetworkAdapter(3);
                DisableNetworkAdapter(4);
                DisableNetworkAdapter(5);

                Thread.Sleep(6000);

                EnableNetworkAdapter(0);
                EnableNetworkAdapter(1);
                EnableNetworkAdapter(2);
                EnableNetworkAdapter(3);
                EnableNetworkAdapter(4);
                EnableNetworkAdapter(5);

                RunCommand("arp", "-d *");
                RunCommand("route", "-f");
                RunCommand("nbtstat", "-R");
                RunCommand("nbtstat", "-RR");
                RunCommand("netsh", "advfirewall reset");
                RunCommand("netcfg", "-d");
                RunCommand("netsh", "winsock reset");
                RunCommand("netsh", "int 6to4 reset all");
                RunCommand("netsh", "int httpstunnel reset all");
                RunCommand("netsh", "int ip reset");
                RunCommand("netsh", "int isatap reset all");
                RunCommand("netsh", "int portproxy reset all");
                RunCommand("netsh", "int tcp reset all");
                RunCommand("netsh", "int teredo reset all");
                RunCommand("ipconfig", "/release");
                RunCommand("ipconfig", "/flushdns");
                RunCommand("ipconfig", "/flushdns");
                RunCommand("ipconfig", "/flushdns");
                RunCommand("ipconfig", "/renew");
            }
            catch
            { }
        }

        // THANKS TO Starcharms -> github.com/starcharms

        private void Fortnite()
        {
            try
            {
                KillProcess("epicgameslauncher.exe");
                KillProcess("FortniteClient-Win64-Shipping_EAC.exe");
                KillProcess("FortniteClient-Win64-Shipping.exe");
                KillProcess("FortniteClient-Win64-Shipping_BE.exe");
                KillProcess("FortniteLauncher.exe");
                KillProcess("EpicGamesLauncher.exe");
                KillProcess("FortniteClient-Win64-Shipping.exe");
                KillProcess("EpicGamesLauncher.exe");
                KillProcess("EasyAntiCheat_Setup.exe");
                KillProcess("FortniteLauncher.exe");
                KillProcess("EpicWebHelper.exe");
                KillProcess("FortniteClient-Win64-Shipping.exe");
                KillProcess("EasyAntiCheat.exe");
                KillProcess("BEService_x64.exe");
                KillProcess("EpicGamesLauncher.exe");
                KillProcess("FortniteClient-Win64-Shipping_BE.exe");
                KillProcess("FortniteClient-Win64-Shipping_EAC.exe");
                KillProcess("EasyAntiCheat.exe");
                KillProcess("dnf.exe");
                KillProcess("DNF.exe");
                KillProcess("BattleEye.exe");
                KillProcess("BEService.exe");
                KillProcess("BEServices.exe");
                StopService("BEService");
                StopService("EasyAntiCheat");

                MessageBox.Show("Fortnite ANTI Cheat successfully terminated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // THANKS TO Starcharms -> github.com/starcharms

        private void FiveM()
        {
            try
            {
                KillProcess("FiveM.exe");
                KillProcess("FiveM_b2802_DumpServer.exe");
                KillProcess("FiveM_b2802_GTAProcess.exe");
                KillProcess("FiveM_ChromeBrowser.exe");
                KillProcess("FiveM_ROSLauncher.exe");
                KillProcess("FiveM_FiveM_ROSService.exe");
                KillProcess("Steam.exe");
                KillProcess("steam.exe");
                KillProcess("EpicGamesLauncher.exe");
                KillProcess("EpicGamesLauncher.exe");
                KillProcess("EpicWebHelper.exe");
                KillProcess("EpicGamesLauncher.exe");
                KillProcess("smartscreen.exe");
                KillProcess("dnf.exe");
                KillProcess("DNF.exe");
                KillProcess("CrossProxy.exe");
                KillProcess("tensafe_1.exe");
                KillProcess("TenSafe_1.exe");
                KillProcess("tensafe_2.exe");
                KillProcess("tencentdl.exe");
                KillProcess("TenioDL.exe");
                KillProcess("uishell.exe");
                KillProcess("BackgroundDownloader.exe");
                KillProcess("conime.exe");
                KillProcess("QQDL.EXE");
                KillProcess("qqlogin.exe");
                KillProcess("dnfchina.exe");
                KillProcess("dnfchinatest.exe");
                KillProcess("dnf.exe");
                KillProcess("txplatform.exe");
                KillProcess("TXPlatform.exe");
                KillProcess("Launcher.exe");
                KillProcess("LauncherPatcher.exe");
                KillProcess("SocialClubHelper.exe");
                KillProcess("RockstarErrorHandler.exe");
                KillProcess("RockstarService.exe");
                StopService("Steam");
                StopService("Rockstar Games");
                StopService("FiveM");

                MessageBox.Show("FiveM ANTI Cheat successfully terminated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Valoranto()
        {
            try
            {
                KillProcess("EasyAntiCheat_Setup.exe");
                KillProcess("EasyAntiCheat.exe");
                KillProcess("BEService_x64.exe");
                KillProcess("smartscreen.exe");
                KillProcess("EasyAntiCheat.exe");
                KillProcess("dnf.exe");
                KillProcess("DNF.exe");
                KillProcess("CrossProxy.exe");
                KillProcess("tensafe_1.exe");
                KillProcess("TenSafe_1.exe");
                KillProcess("tensafe_2.exe");
                KillProcess("tencentdl.exe");
                KillProcess("TenioDL.exe");
                KillProcess("uishell.exe");
                KillProcess("BackgroundDownloader.exe");
                KillProcess("conime.exe");
                KillProcess("QQDL.EXE");
                KillProcess("qqlogin.exe");
                KillProcess("dnfchina.exe");
                KillProcess("dnfchinatest.exe");
                KillProcess("dnf.exe");
                KillProcess("txplatform.exe");
                KillProcess("TXPlatform.exe");
                KillProcess("Launcher.exe");
                KillProcess("LauncherPatcher.exe");
                KillProcess("OriginWebHelperService.exe");
                KillProcess("Origin.exe");
                KillProcess("OriginClientService.exe");
                KillProcess("OriginER.exe");
                KillProcess("OriginThinSetupInternal.exe");
                KillProcess("OriginLegacyCLI.exe");
                KillProcess("Agent.exe");
                KillProcess("Client.exe");
                KillProcess("BattleEye.exe");
                KillProcess("BEService.exe");
                KillProcess("BEServices.exe");
                StopService("BEService");
                StopService("EasyAntiCheat");
                StopService("PunkBuster");
                StopService("Vanguard");
                StopService("ricocheat");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                MessageBox.Show("Valorant ANTI Cheat successfully terminated.");
            }
        }


        // THANKS TO Starcharms -> github.com/starcharms


        // Currently updating...
        private int directoriesDeleted = 0;
        private void CleanAnticheatTraces()
        {
            try
            {
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "documents\\Call of Duty Modern Warfare"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Blizzard Entertainment"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Battle.net"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Battle.net"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Blizzard Entertainment"));

                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents\\Call of Duty Black Ops Cold War\\report"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents\\Call of Duty Black Ops Cold War"));

                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_0.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_1.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_2.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_3.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\f_000001.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\index.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\index"));

                //thats boring to code. hust.

                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_0"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_1"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_2"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\data_3"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\f_000001"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\GPUCache\\index"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\index.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_0.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_1.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_2.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_3.dcache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_0"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_1"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_2"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\BrowserCache\\Cache\\data_3"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\Cache"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\Logs"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\WidevineCdm"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Battle.net\\CachedData"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Blizzard Entertainment"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Roaming\\Battle.net"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Battle.net"));
                DeleteDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Blizzard Entertainment"));

                DeleteRegistryKey("SOFTWARE\\WOW6432Node\\EasyAntiCheat");
                DeleteRegistryKey("SYSTEM\\ControlSet001\\Services\\EasyAntiCheat");
                DeleteRegistryKey("SYSTEM\\ControlSet001\\Services\\EasyAntiCheat\\Security");
                DeleteRegistryKey("SOFTWARE\\WOW6432Node\\EasyAntiCheat");
                DeleteRegistryKey("SYSTEM\\ControlSet001\\Services\\EasyAntiCheat");
                DeleteRegistryKey("SOFTWARE\\WOW6432Node\\EasyAntiCheat");

                MessageBox.Show("All Anti-Cheat traces have been successfully deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cleaning Anti-Cheat traces: " + ex.Message);
            }
        }

        // THANKS TO Starcharms -> github.com/starcharms
        private void UnlinkXbox()
        {
            try
            {
                ShellTankstelle("Get-AppxPackage -AllUsers xbox | Remove-AppxPackage");
                StopAndDeleteService("XblAuthManager");
                StopAndDeleteService("XblGameSave");
                StopAndDeleteService("XboxNetApiSvc");
                StopAndDeleteService("XboxGipSvc");
                DeleteRegistryKey(@"HKLM\SYSTEM\CurrentControlSet\Services\xbgm");
                ScheduledTask("Microsoft\\XblGameSave\\XblGameSaveTask");
                ScheduledTask("Microsoft\\XblGameSave\\XblGameSaveTaskLogon");
                ModifyHosts("xboxlive.com", "127.0.0.1");
                ModifyHosts("user.auth.xboxlive.com", "127.0.0.1");
                ModifyHosts("presence-heartbeat.xboxlive.com", "127.0.0.1");

                MessageBox.Show("Xbox successfully terminated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UnlinkDiscord()
        {
            try
            {
                string IODJwadsioamvdosas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Discord", "app-1.0.9015", "modules", "discord_rpc-1");
                string dwaNIOdsmadiowaios = "SecHex_Was_Here";
                RenameDir(IODJwadsioamvdosas, dwaNIOdsmadiowaios);
                MessageBox.Show("Discord successfully terminated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // THANKS TO Starcharms -> github.com/starcharms



        private void RenameDir(string IODJwadsioamvdosas, string dwaNIOdsmadiowaios)
        {
            try
            {
                if (Directory.Exists(IODJwadsioamvdosas))
                {
                    string DIWAJNEWNDOWAS = Path.Combine(Path.GetDirectoryName(IODJwadsioamvdosas), dwaNIOdsmadiowaios);
                    Directory.Move(IODJwadsioamvdosas, DIWAJNEWNDOWAS);
                }
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while unlinking discord: " + ex.Message, NotificationType.Error);
            }
        }

        private static void ScheduledTask(string taskName)
        {
            RunCommand("schtasks", $"/Change /TN \"{taskName}\" /disable");
        }

        private static void StopAndDeleteService(string serviceName)
        {
            RunCommand("sc", $"stop {serviceName}");
            RunCommand("sc", $"delete {serviceName}");
        }

        private static void ModifyHosts(string domain, string ipAddress)
        {
            string hostsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers", "etc", "hosts");
            string lineToAdd = $"{ipAddress} {domain}";

            File.AppendAllText(hostsPath, lineToAdd + Environment.NewLine);
        }

        private static void ShellTankstelle(string command)
        {
            var processInfo = new ProcessStartInfo("powershell.exe")
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = $"-ExecutionPolicy Bypass -NoProfile -Command \"{command}\""
            };

            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();

                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception("PowerShell Error: " + error);
                }
            }
        }

        static void DisableNetworkAdapter(int index)
        {
            string command = $"wmic path win32_networkadapter where index={index} call disable";
            RunCommand("cmd", $"/c {command}");
        }

        static void EnableNetworkAdapter(int index)
        {
            string command = $"wmic path win32_networkadapter where index={index} call enable";
            RunCommand("cmd", $"/c {command}");
        }

        static void RunCommand(string command, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(command);
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
        }


        // Kill a specifg Windows Task
        static void KillProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
        static void StopService(string serviceName)
        {
            ServiceController service = new ServiceController(serviceName);
            if (service.Status == ServiceControllerStatus.Running)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }

        private void DeleteDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    directoriesDeleted++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting directory '{path}': {ex.Message}");
            }
        }

        private void DeleteRegistryKey(string keyPath)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath, true))
            {
                if (key != null)
                {
                    foreach (string subKeyName in key.GetSubKeyNames())
                    {
                        key.DeleteSubKeyTree(subKeyName);
                    }
                    Registry.LocalMachine.DeleteSubKey(keyPath, false);
                }
            }
        }

        private void ShowNotification(string message, NotificationType type)
        {
            MessageBox.Show(message, "Spoofy [Open Source]", MessageBoxButtons.OK, GetNotificationIcon(type));
        }
        private MessageBoxIcon GetNotificationIcon(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Success:
                    return MessageBoxIcon.Information;
                case NotificationType.Error:
                    return MessageBoxIcon.Error;
                case NotificationType.Warning:
                    return MessageBoxIcon.Warning;
                default:
                    return MessageBoxIcon.None;
            }
        }

        private enum NotificationType
        {
            Success,
            Error,
            Warning
        }


        // Cleaning Functions
        private bool isTempClearCheckboxChecked = false;
        private bool isWinLogCheckboxChecked = false;
        private bool isDnsCheckboxChecked = false;
        private bool isWinTempCheckboxChecked = false;
        private bool isTcpCheckboxChecked = false;
        private bool isCookieCheckboxChecked = false;
        private bool isDocsCheckboxChecked = false;
        private bool isrstconnectCheckBoxChecked = false;
        private bool isFirefoxCookieResetChecked = false;


        // Anti Cheat Terminator 
        private bool isfortniteChecked = false;
        private bool isfivemChecked = false;
        private bool isValorantChecked = false;
        private bool isAntiCheatTracerChecked = false;


        //Unlink Functions
        private bool isXboxChecked = false;
        private bool isDiscordChecked = false;

        private void dnsflush_CheckedChanged(object sender, EventArgs e)
        {
            isDnsCheckboxChecked = dnsflush.Checked;
        }

        private void tcpp_CheckedChanged(object sender, EventArgs e)
        {
            isTcpCheckboxChecked = tcpp.Checked;
        }

        private void wifireset_CheckedChanged(object sender, EventArgs e)
        {
            isrstconnectCheckBoxChecked = wifireset.Checked;
        }

        private void windowslogs_CheckedChanged(object sender, EventArgs e)
        {
            isWinLogCheckboxChecked = windowslogs.Checked;

        }

        private void tempfi_CheckedChanged(object sender, EventArgs e)
        {
            isTempClearCheckboxChecked = tempfi.Checked;
        }

        private void wintempp_CheckedChanged(object sender, EventArgs e)
        {
            isWinTempCheckboxChecked = wintempp.Checked;
        }

        private void chromecookies_CheckedChanged(object sender, EventArgs e)
        {
            isCookieCheckboxChecked = chromecookies.Checked;
        }

        private void firefoxcookies_CheckedChanged(object sender, EventArgs e)
        {
            isFirefoxCookieResetChecked = firefoxcookies.Checked;
        }


        // Anti Cheat Terminator

        private void fortnite_CheckedChanged(object sender, EventArgs e)
        {
            isfortniteChecked = fortnite.Checked;
        }

        private void fivemm_CheckedChanged(object sender, EventArgs e)
        {
            isfivemChecked = fivemm.Checked;
        }

        private void valorant_CheckedChanged(object sender, EventArgs e)
        {
            isValorantChecked = valorant.Checked;
        }
        private void antishittracer_CheckedChanged(object sender, EventArgs e)
        {
            isAntiCheatTracerChecked = antishittracer.Checked;
        }



        // Unlink Functions

        private void unlinkxbox_CheckedChanged(object sender, EventArgs e)
        {
            isXboxChecked = unlinkxbox.Checked;
        }
        private void unlinkdc_CheckedChanged(object sender, EventArgs e)
        {
            isDiscordChecked = unlinkdc.Checked;
        }


        private void spoofall_Click(object sender, EventArgs e)
        {
            if (isDnsCheckboxChecked)
            {
                FlushDnsCache();
            }

            if (isWinLogCheckboxChecked)
            {
                ClearWindowsLogs();
            }

            if (isTempClearCheckboxChecked)
            {
                ClearTemporaryCache();
            }

            if (isWinTempCheckboxChecked)
            {
                ClearWindowsTempLol();
            }

            if (isTcpCheckboxChecked)
            {
                TcpRst();
            }

            if (isCookieCheckboxChecked)
            {
                CookieRst();
            }

            if (isrstconnectCheckBoxChecked)
            {
                rstreset();
            }

            if (isFirefoxCookieResetChecked)
            {
                FirefoxCookieReset();
            }

            if (isfortniteChecked)
            {
                Fortnite();
            }

            if (isfivemChecked)
            {
                FiveM();
            }

            if (isValorantChecked)
            {
                Valoranto();
            }

            if (isAntiCheatTracerChecked)
            {
                CleanAnticheatTraces();
            }

            if (isXboxChecked)
            {
                UnlinkXbox();
            }

            if (isDiscordChecked)
            {
                UnlinkDiscord();
            }

        }

        private void south_africa_Load(object sender, EventArgs e)
        {

        }

    }
}
