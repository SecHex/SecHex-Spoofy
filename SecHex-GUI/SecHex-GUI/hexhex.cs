using Microsoft.Win32;
using Siticone.Desktop.UI.WinForms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;





namespace SecHex_GUI
{


    public partial class hexhex : Form
    {
        private System.Windows.Forms.Timer timer;
        private float animationProgress = 0.0f;
        private int steps = 270;
        private Color startColor = Color.FromArgb(23, 23, 23);
        private Color middleColor = Color.FromArgb(248, 248, 248);
        private Color endColor = Color.FromArgb(23, 23, 23);
        private Color currentColor;
        private bool isDragging;
        private Point offset;
        private bool isAnimationRunning = false;

        public hexhex()
        {
            InitializeComponent();
            SetRoundedCorners();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += timer_Tick;

            this.DoubleBuffered = true;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SetRoundedCorners();

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

        public void AddLogEntry(string logEntryBefore, string logEntryAfter)
        {
            richTextBoxLogs.SelectionColor = Color.Gray;
            richTextBoxLogs.AppendText(logEntryBefore + Environment.NewLine);

            richTextBoxLogs.SelectionColor = Color.White;
            richTextBoxLogs.AppendText(logEntryAfter + Environment.NewLine);
        }

        private void richTextBoxLogs_TextChanged(object sender, EventArgs e)
        {

        }







        private void hexhex_Load(object sender, EventArgs e)
        {

        }




        private void sechex_Click(object sender, EventArgs e)
        {

        }
    }
}
