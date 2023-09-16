using Microsoft.Win32;
using Siticone.Desktop.UI.WinForms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Net;

namespace SecHex_GUI
{

    public partial class south_africa : Form
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
            SetRoundedCorners();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 340;
            timer.Tick += timer_Tick;

            this.DoubleBuffered = true;
            timer.Start();

            previousColor = currentColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);
            SetRoundedCorners();

            if (previousColor != currentColor)
            {
                this.Invalidate();
                previousColor = currentColor;
            }

            Rectangle gradientRect = new Rectangle(0, 0, this.Width, this.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(gradientRect, startColor, currentColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, gradientRect);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int currentR, currentG, currentB;
            if (animationProgress < 0.5f)
            {
                float subProgress = animationProgress * 2;
                currentR = (int)(startColor.R + (middleColor.R - startColor.R) * subProgress);
                currentG = (int)(startColor.G + (middleColor.G - startColor.G) * subProgress);
                currentB = (int)(startColor.B + (middleColor.B - startColor.B) * subProgress);
            }
            else
            {
                float subProgress = (animationProgress - 0.5f) * 2;
                currentR = (int)(middleColor.R + (endColor.R - middleColor.R) * subProgress);
                currentG = (int)(middleColor.G + (endColor.G - middleColor.G) * subProgress);
                currentB = (int)(middleColor.B + (endColor.B - middleColor.B) * subProgress);
            }
            currentColor = Color.FromArgb(currentR, currentG, currentB);

            animationProgress += 1.0f / steps;
            if (animationProgress >= 1.0f)
            {
                animationProgress = 0.0f;
            }

            this.Invalidate();
        }

        private void SetRoundedCorners()
        {
            int radius = 18;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                offset = new Point(e.X, e.Y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDragging)
            {
                Point newLocation = PointToScreen(new Point(e.X, e.Y));
                newLocation.Offset(-offset.X, -offset.Y);
                Location = newLocation;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
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

        private void DocsClear()
        {
            try
            {
                string recentDocumentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Recent), "*.*");
                string[] recentDocuments = Directory.GetFiles(recentDocumentsPath);

                foreach (string document in recentDocuments)
                {
                    File.Delete(document);
                }

                MessageBox.Show("Recent documents cleared.");
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

        // THANKS TO Starcharms -> github.com/starcharms



        private bool isTempClearCheckboxChecked = false;
        private bool isWinLogCheckboxChecked = false;
        private bool isDnsCheckboxChecked = false;
        private bool isWinTempCheckboxChecked = false;
        private bool isTcpCheckboxChecked = false;
        private bool isCookieCheckboxChecked = false;
        private bool isDocsCheckboxChecked = false;
        private bool isrstconnectCheckBoxChecked = false;


        private void dns_CheckedChanged(object sender, EventArgs e)
        {
            isDnsCheckboxChecked = dns.Checked;
        }

        private void winlogs_CheckedChanged(object sender, EventArgs e)
        {
            isWinLogCheckboxChecked = winlogs.Checked;
        }

        private void tempclear_CheckedChanged(object sender, EventArgs e)
        {
            isTempClearCheckboxChecked = tempclear.Checked;
        }

        private void wintemp_CheckedChanged(object sender, EventArgs e)
        {
            isWinTempCheckboxChecked = wintemp.Checked;
        }

        private void tcp_CheckedChanged(object sender, EventArgs e)
        {
            isTcpCheckboxChecked = tcp.Checked;
        }

        private void cookie_CheckedChanged(object sender, EventArgs e)
        {
            isCookieCheckboxChecked = cookie.Checked;
        }

        private void docs_CheckedChanged(object sender, EventArgs e)
        {
            isDocsCheckboxChecked = docs.Checked;
        }

        private void rstconnect_CheckedChanged(object sender, EventArgs e)
        {
            isrstconnectCheckBoxChecked = rstconnect.Checked;
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

            if (isDocsCheckboxChecked)
            {
                DocsClear();
            }

            if (isrstconnectCheckBoxChecked)
            {
                rstreset();
            }


        }

        private void south_africa_Load(object sender, EventArgs e)
        {

        }


    }
}
