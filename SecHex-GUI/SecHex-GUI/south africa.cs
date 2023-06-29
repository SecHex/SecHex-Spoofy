using Microsoft.Win32;
using Siticone.Desktop.UI.WinForms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Net;

//sechex.me
//sechex.me
//sechex.me
//sechex.me
namespace SecHex_GUI
{

    public partial class south_africa : Form
    {
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
            SetRoundedCorners();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 340;
            timer.Tick += timer_Tick;

            this.DoubleBuffered = true;
            timer.Start();

            previousColor = currentColor;
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me


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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        private bool isTempClearCheckboxChecked = false;
        private bool isWinLogCheckboxChecked = false;
        private bool isDnsCheckboxChecked = false;
        private bool isWinTempCheckboxChecked = false;
        private bool isTcpCheckboxChecked = false;


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

        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

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


        }









        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void south_africa_Load(object sender, EventArgs e)
        {

        }

    }
}
