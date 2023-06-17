using Microsoft.Win32;
using Siticone.Desktop.UI.WinForms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
//sechex.me
//sechex.me
//sechex.me
//sechex.me

namespace SecHex_GUI
{
    public partial class Form1 : Form
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

        public Form1()
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
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Region = new Region(path);
            this.SetGraphicsQuality();
        }

        private void SetGraphicsQuality()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
        }



        private async void AnimateButtonsBorderColor(params SiticoneButton[] buttons)
        {
            if (isAnimationRunning)
                return;

            isAnimationRunning = true;

            float hue = 0;
            float saturation = 1;
            float value = 1;
            int animationDuration = 500;
            int animationSteps = 100;
            float colorSpeed = 0.2f;

            while (isAnimationRunning)
            {
                for (int i = 0; i <= animationSteps; i++)
                {
                    if (!isAnimationRunning)
                        break;

                    float progress = (float)i / animationSteps;
                    float currentHue = hue + progress * 360 * colorSpeed;
                    Color currentColor = ColorFromHsv(currentHue, saturation, value);

                    foreach (SiticoneButton button in buttons)
                    {
                        button.BorderThickness = 1;
                        button.BorderColor = currentColor;
                    }

                    await Task.Delay((int)(animationDuration / (animationSteps * colorSpeed)));
                }

                if (!isAnimationRunning)
                    break;

                Color startColor = ColorFromHsv(hue + 360 * colorSpeed, saturation, value);
                Color endColor = Color.Red;

                for (int i = 0; i <= animationSteps; i++)
                {
                    if (!isAnimationRunning)
                        break;

                    float progress = (float)i / animationSteps;
                    Color currentColor = InterpolateColor(startColor, endColor, progress);

                    foreach (SiticoneButton button in buttons)
                    {
                        button.BorderThickness = 1;
                        button.BorderColor = currentColor;
                    }

                    await Task.Delay((int)(animationDuration / (animationSteps * colorSpeed)));
                }
            }
        }

        private Color ColorFromHsv(float hue, float saturation, float value)
        {
            int rgbMax = 255;
            float chroma = value * saturation;
            float huePrime = hue / 60f;
            float x = chroma * (1 - Math.Abs(huePrime % 2 - 1));
            float m = value - chroma;

            float red = 0, green = 0, blue = 0;

            if (huePrime >= 0 && huePrime < 1)
            {
                red = chroma;
                green = x;
            }
            else if (huePrime >= 1 && huePrime < 2)
            {
                red = x;
                green = chroma;
            }
            else if (huePrime >= 2 && huePrime < 3)
            {
                green = chroma;
                blue = x;
            }
            else if (huePrime >= 3 && huePrime < 4)
            {
                green = x;
                blue = chroma;
            }
            else if (huePrime >= 4 && huePrime < 5)
            {
                red = x;
                blue = chroma;
            }
            else if (huePrime >= 5 && huePrime < 6)
            {
                red = chroma;
                blue = x;
            }

            int r = (int)Math.Round((red + m) * rgbMax);
            int g = (int)Math.Round((green + m) * rgbMax);
            int b = (int)Math.Round((blue + m) * rgbMax);

            return Color.FromArgb(r, g, b);
        }

        private Color InterpolateColor(Color startColor, Color endColor, float progress)
        {
            int r = (int)(startColor.R + (endColor.R - startColor.R) * progress);
            int g = (int)(startColor.G + (endColor.G - startColor.G) * progress);
            int b = (int)(startColor.B + (endColor.B - startColor.B) * progress);

            return Color.FromArgb(r, g, b);
        }

        private void siticoneToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            SiticoneToggleSwitch toggleSwitch = (SiticoneToggleSwitch)sender;

            if (toggleSwitch.Checked)
            {
                AnimateButtonsBorderColor(disk, winid, disk, efi, GUID, spoofall, mac, tracercl, display, pcname, backup, product, req, sm);
            }
            else
            {
                StopButtonAnimation(disk, winid, disk, efi, GUID, spoofall, mac, tracercl, display, pcname, backup, product, req, sm);
            }
        }

        private void StopButtonAnimation(params SiticoneButton[] buttons)
        {
            isAnimationRunning = false;
            timer.Stop();

            foreach (SiticoneButton button in buttons)
            {

                button.BorderThickness = 1;
                button.BorderColor = Color.White;
            }
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }




        private void SaveLogs(string id, string logBefore, string logAfter)
        {
            string logsFolderPath = Path.Combine(Application.StartupPath, "Logs");
            if (!Directory.Exists(logsFolderPath))
                Directory.CreateDirectory(logsFolderPath);

            string logFileName = Path.Combine(logsFolderPath, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
            string logEntryBefore = $"{DateTime.Now:HH:mm:ss}: ID {id} -  {logBefore} (Before)";
            string logEntryAfter = $"{DateTime.Now:HH:mm:ss}: ID {id} -  {logAfter} (After)";

            File.AppendAllText(logFileName, logEntryBefore + Environment.NewLine);
            File.AppendAllText(logFileName, logEntryAfter + Environment.NewLine);
        }



        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        public static void Enable_LocalAreaConection(string adapterId, bool enable = true)
        {
            string interfaceName = "Ethernet";
            foreach (NetworkInterface i in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (i.Id == adapterId)
                {
                    interfaceName = i.Name;
                    break;
                }
            }

            string control;
            if (enable)
                control = "enable";
            else
                control = "disable";

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("netsh", $"interface set interface \"{interfaceName}\" {control}");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        public static string RandomId(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = "";
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                result += chars[random.Next(chars.Length)];
            }

            return result;
        }


        private string RandomIdprid(int length)
        {
            const string digits = "0123456789";
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            var id = new char[length];
            int dashIndex = 5;
            int letterIndex = 17;
            for (int i = 0; i < length; i++)
            {
                if (i == dashIndex)
                {
                    id[i] = '-';
                    dashIndex += 6;
                }
                else if (i == letterIndex)
                {
                    id[i] = letters[random.Next(letters.Length)];
                }
                else if (i == letterIndex + 1)
                {
                    id[i] = letters[random.Next(letters.Length)];
                }
                else
                {
                    id[i] = digits[random.Next(digits.Length)];
                }
            }
            return new string(id);
        }


        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        public static string RandomMac()
        {
            string chars = "ABCDEF0123456789";
            string windows = "26AE";
            string result = "";
            Random random = new Random();

            result += chars[random.Next(chars.Length)];
            result += windows[random.Next(windows.Length)];

            for (int i = 0; i < 5; i++)
            {
                result += "-";
                result += chars[random.Next(chars.Length)];
                result += chars[random.Next(chars.Length)];

            }

            return result;
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me







        private void spoofall_Click(object sender, EventArgs e)
        {
            bool registryEntriesExist = false;

            try
            {
                req_Click(sender, e);
                registryEntriesExist = true;
            }
            catch (Exception ex)
            {
                ShowNotification("Error executing functions: " + ex.Message, NotificationType.Error);
            }

            if (registryEntriesExist)
            {
                disk_Click(sender, e);
                mac_Click(sender, e);
                GUID_Click(sender, e);
                winid_Click(sender, e);
                pcname_Click(sender, e);
                display_Click(sender, e);
                efi_Click(sender, e);
                siticoneButton1_Click(sender, e);
                product_Click(sender, e);

                ShowNotification("All functions executed successfully.", NotificationType.Success);
            }
            else
            {
                ShowNotification("Error: One or more required registry entries are missing.", NotificationType.Error);
            }
        }


        private async void disk_Click(object sender, EventArgs e)
        {

            try
            {
                using (RegistryKey ScsiPorts = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\Scsi"))
                {
                    if (ScsiPorts != null)
                    {
                        foreach (string port in ScsiPorts.GetSubKeyNames())
                        {
                            using (RegistryKey ScsiBuses = Registry.LocalMachine.OpenSubKey($"HARDWARE\\DEVICEMAP\\Scsi\\{port}"))
                            {
                                if (ScsiBuses != null)
                                {
                                    foreach (string bus in ScsiBuses.GetSubKeyNames())
                                    {
                                        using (RegistryKey ScsuiBus = Registry.LocalMachine.OpenSubKey($"HARDWARE\\DEVICEMAP\\Scsi\\{port}\\{bus}\\Target Id 0\\Logical Unit Id 0", true))
                                        {
                                            if (ScsuiBus != null)
                                            {
                                                object deviceTypeValue = ScsuiBus.GetValue("DeviceType");
                                                if (deviceTypeValue != null && deviceTypeValue.ToString() == "DiskPeripheral")
                                                {
                                                    string identifierBefore = ScsuiBus.GetValue("Identifier").ToString();
                                                    string serialNumberBefore = ScsuiBus.GetValue("SerialNumber").ToString();

                                                    string identifierAfter = RandomId(14);
                                                    string serialNumberAfter = RandomId(14);
                                                    string logBefore = $"DiskPeripheral {bus}\\Target Id 0\\Logical Unit Id 0 - Identifier: {identifierBefore}, SerialNumber: {serialNumberBefore}";
                                                    string logAfter = $"DiskPeripheral {bus}\\Target Id 0\\Logical Unit Id 0 - Identifier: {identifierAfter}, SerialNumber: {serialNumberAfter}";
                                                    SaveLogs("disk", logBefore, logAfter);

                                                    ScsuiBus.SetValue("DeviceIdentifierPage", Encoding.UTF8.GetBytes(serialNumberAfter));
                                                    ScsuiBus.SetValue("Identifier", identifierAfter);
                                                    ScsuiBus.SetValue("InquiryData", Encoding.UTF8.GetBytes(identifierAfter));
                                                    ScsuiBus.SetValue("SerialNumber", serialNumberAfter);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ShowNotification("ScsiBuses key not found.", NotificationType.Error);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowNotification("ScsiPorts key not found.", NotificationType.Error);
                        return;
                    }
                }

                using (RegistryKey diskKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\IDE"))
                {
                    if (diskKey != null)
                    {
                        foreach (string controllerId in diskKey.GetSubKeyNames())
                        {
                            using (RegistryKey controller = diskKey.OpenSubKey(controllerId))
                            {
                                if (controller != null)
                                {
                                    foreach (string diskId in controller.GetSubKeyNames())
                                    {
                                        using (RegistryKey disk = controller.OpenSubKey(diskId, true))
                                        {
                                            if (disk != null)
                                            {
                                                string serialNumberBefore = disk.GetValue("SerialNumber")?.ToString();

                                                string serialNumberAfter = RandomId(14);
                                                string logBefore = $"Hard Disk {diskId} - SerialNumber: {serialNumberBefore}";
                                                string logAfter = $"Hard Disk {diskId} - SerialNumber: {serialNumberAfter}";
                                                SaveLogs("disk", logBefore, logAfter);

                                                disk.SetValue("SerialNumber", serialNumberAfter);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                ShowNotification("DISK successfully spoofed.", NotificationType.Success);
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while spoofing the DISk: " + ex.Message, NotificationType.Error);
            }

        }


        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me



        private void mac_Click(object sender, EventArgs e)
        {
            try
            {
                bool spoofSuccess = SpoofMAC();

                if (!spoofSuccess)
                {
                    ShowNotification("MAC address successfully spoofed.", NotificationType.Success);
                }
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while spoofing the MAC address: " + ex.Message, NotificationType.Error);
            }
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        private bool SpoofMAC()
        {
            bool err = false;

            using (RegistryKey NetworkAdapters = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}"))
            {
                foreach (string adapter in NetworkAdapters.GetSubKeyNames())
                {
                    if (adapter != "Properties")
                    {
                        try
                        {
                            using (RegistryKey NetworkAdapter = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Control\\Class\\{{4d36e972-e325-11ce-bfc1-08002be10318}}\\{adapter}", true))
                            {
                                if (NetworkAdapter.GetValue("BusType") != null)
                                {
                                    string adapterId = NetworkAdapter.GetValue("NetCfgInstanceId").ToString();
                                    string macBefore = NetworkAdapter.GetValue("NetworkAddress")?.ToString();
                                    string macAfter = RandomMac();
                                    string logBefore = $"MAC Address {adapterId} - Before: {macBefore}";
                                    string logAfter = $"MAC Address {adapterId} - After: {macAfter}";
                                    SaveLogs("mac", logBefore, logAfter);

                                    NetworkAdapter.SetValue("NetworkAddress", macAfter);
                                    Enable_LocalAreaConection(adapterId, false);
                                    Enable_LocalAreaConection(adapterId, true);
                                }
                            }
                        }
                        catch (System.Security.SecurityException)
                        {
                            err = true;
                            break;
                        }
                    }
                }
            }

            return err;
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        private void GUID_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey HardwareGUID = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true))
                {
                    if (HardwareGUID != null)
                    {
                        HardwareGUID.SetValue("HwProfileGuid", $"{{{Guid.NewGuid()}}}");
                        string logBefore = "HwProfileGuid - Before: " + HardwareGUID.GetValue("HwProfileGuid");
                        string logAfter = "HwProfileGuid - After: " + HardwareGUID.GetValue("HwProfileGuid");
                        SaveLogs("guid", logBefore, logAfter);
                    }
                    else
                    {
                        ShowNotification("HardwareGUID key not found.", NotificationType.Error);
                        return;
                    }
                }

                using (RegistryKey MachineGUID = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", true))
                {
                    if (MachineGUID != null)
                    {
                        MachineGUID.SetValue("MachineGuid", Guid.NewGuid().ToString());
                        string logBefore = "MachineGuid - Before: " + MachineGUID.GetValue("MachineGuid");
                        string logAfter = "MachineGuid - After: " + MachineGUID.GetValue("MachineGuid");
                        SaveLogs("guid", logBefore, logAfter);
                    }
                    else
                    {
                        ShowNotification("MachineGUID key not found.", NotificationType.Error);
                        return;
                    }
                }

                using (RegistryKey MachineId = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\SQMClient", true))
                {
                    if (MachineId != null)
                    {
                        MachineId.SetValue("MachineId", $"{{{Guid.NewGuid()}}}");
                        string logBefore = "MachineId - Before: " + MachineId.GetValue("MachineId");
                        string logAfter = "MachineId - After: " + MachineId.GetValue("MachineId");
                        SaveLogs("guid", logBefore, logAfter);
                    }
                    else
                    {
                        ShowNotification("MachineId key not found.", NotificationType.Error);
                        return;
                    }
                }

                using (RegistryKey SystemInfo = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true))
                {
                    if (SystemInfo != null)
                    {
                        Random rnd = new Random();
                        int day = rnd.Next(1, 31);
                        string dayStr = (day < 10) ? $"0{day}" : day.ToString();

                        int month = rnd.Next(1, 13);
                        string monthStr = (month < 10) ? $"0{month}" : month.ToString();

                        int year = rnd.Next(1990, 2023);
                        string yearStr = year.ToString();

                        string randomDate = $"{monthStr}/{dayStr}/{yearStr}";

                        SystemInfo.SetValue("BIOSReleaseDate", randomDate);
                        string logBefore = "BIOSReleaseDate - Before: " + SystemInfo.GetValue("BIOSReleaseDate");
                        string logAfter = "BIOSReleaseDate - After: " + SystemInfo.GetValue("BIOSReleaseDate");
                        SaveLogs("guid", logBefore, logAfter);
                    }
                    else
                    {
                        ShowNotification("SystemInformation key not found.", NotificationType.Error);
                        return;
                    }
                }

                ShowNotification("GUIDs successfully generated.", NotificationType.Success);
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred: " + ex.Message, NotificationType.Error);
            }
        }









        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        private bool SpoofWinID()
        {
            bool err = false;

            try
            {
                using (RegistryKey winIDKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", true))
                {
                    if (winIDKey != null)
                    {
                        string winIDBefore = winIDKey.GetValue("MachineGuid").ToString();
                        byte[] spoofedWinIDBytes = new byte[16];
                        using (var rng = new RNGCryptoServiceProvider())
                        {
                            rng.GetBytes(spoofedWinIDBytes);
                        }
                        string spoofedWinID = BitConverter.ToString(spoofedWinIDBytes).Replace("-", "").ToLowerInvariant();
                        winIDKey.SetValue("MachineGuid", spoofedWinID);

                        string logBefore = "MachineGuid - Before: " + winIDBefore;
                        string logAfter = "MachineGuid - After: " + winIDKey.GetValue("MachineGuid");
                        SaveLogs("guid", logBefore, logAfter);

                        ShowNotification("Windows ID spoofed successfully.", NotificationType.Success);
                    }
                    else
                    {
                        err = true;
                        ShowNotification("Windows ID spoofing failed: Registry key not found.", NotificationType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                err = true;
                ShowNotification("An error occurred while spoofing the Windows ID: " + ex.Message, NotificationType.Error);
            }

            return err;
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void winid_Click(object sender, EventArgs e)
        {
            if (SpoofWinID())
            {
                ShowNotification("An error occurred while spoofing the Windows ID.", NotificationType.Error);
            }
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me


        private void pcname_Click(object sender, EventArgs e)
        {
            try
            {
                string originalName;
                string newName = RandomId(8);
                using (RegistryKey computerNameKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName", true))
                {
                    if (computerNameKey != null)
                    {
                        originalName = computerNameKey.GetValue("ComputerName").ToString();

                        computerNameKey.SetValue("ComputerName", newName);
                        computerNameKey.SetValue("ActiveComputerName", newName);
                        computerNameKey.SetValue("ComputerNamePhysicalDnsDomain", "");
                    }
                    else
                    {
                        ShowNotification("ComputerName key not found.", NotificationType.Error);
                        return;
                    }
                }
                using (RegistryKey activeComputerNameKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName", true))
                {
                    if (activeComputerNameKey != null)
                    {
                        activeComputerNameKey.SetValue("ComputerName", newName);
                        activeComputerNameKey.SetValue("ActiveComputerName", newName);
                        activeComputerNameKey.SetValue("ComputerNamePhysicalDnsDomain", "");
                    }
                    else
                    {
                        ShowNotification("ActiveComputerName key not found.", NotificationType.Error);
                        return;
                    }
                }

                using (RegistryKey hostnameKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true))
                {
                    if (hostnameKey != null)
                    {
                        hostnameKey.SetValue("Hostname", newName);
                        hostnameKey.SetValue("NV Hostname", newName);
                    }
                    else
                    {
                        ShowNotification("Hostname key not found.", NotificationType.Error);
                        return;
                    }
                }
                using (RegistryKey interfacesKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces", true))
                {
                    if (interfacesKey != null)
                    {
                        foreach (string interfaceName in interfacesKey.GetSubKeyNames())
                        {
                            using (RegistryKey interfaceKey = interfacesKey.OpenSubKey(interfaceName, true))
                            {
                                if (interfaceKey != null)
                                {
                                    interfaceKey.SetValue("Hostname", newName);
                                    interfaceKey.SetValue("NV Hostname", newName);
                                }
                            }
                        }
                    }
                }
                string logBefore = "ComputerName - Before: " + originalName;
                string logAfter = "ComputerName - After: " + newName;
                SaveLogs("pcname", logBefore, logAfter);

                ShowNotification("PC name spoofed successfully.", NotificationType.Success);
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while spoofing the PC name: " + ex.Message, NotificationType.Error);
            }
        }

        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void display_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey displaySettings = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RunMRU", true);

                if (displaySettings != null)
                {
                    Random rnd = new Random();
                    int displayId = rnd.Next(1, 100);
                    displaySettings.SetValue("MRU0", $"Display{displayId}");
                    string spoofedDisplayId = $"SpoofedDisplay{displayId}";
                    displaySettings.SetValue("MRU1", spoofedDisplayId);
                    displaySettings.SetValue("MRU2", spoofedDisplayId);
                    displaySettings.SetValue("MRU3", spoofedDisplayId);
                    displaySettings.SetValue("MRU4", spoofedDisplayId);
                    string logBefore = "Display ID - Before: " + displayId;
                    string logAfter = "Display ID - After: " + spoofedDisplayId;
                    SaveLogs("display", logBefore, logAfter);

                    ShowNotification("Display Function executed successfully.", NotificationType.Success);
                }
                else
                {
                    ShowNotification("Display settings registry key not found.", NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while changing the display ID: " + ex.Message, NotificationType.Error);
            }
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        private void efi_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey efiVariables = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26", true))
                {
                    if (efiVariables != null)
                    {
                        string efiVariableIdBefore = efiVariables.GetValue("VariableId")?.ToString();

                        string newEfiVariableId = Guid.NewGuid().ToString();
                        efiVariables.SetValue("VariableId", newEfiVariableId);
                        string logBefore = "EFI Variable ID - Before: " + efiVariableIdBefore;
                        string logAfter = "EFI Variable ID - After: " + newEfiVariableId;
                        SaveLogs("efi", logBefore, logAfter);

                        ShowNotification("EFI Function executed successfully.", NotificationType.Success);
                    }
                    else
                    {
                        ShowNotification("EFI variables registry key not found.", NotificationType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while executing the EFI Function: " + ex.Message, NotificationType.Error);
            }
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey smbiosData = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS", true))
                {
                    if (smbiosData != null)
                    {
                        string serialNumberBefore = smbiosData.GetValue("SystemSerialNumber")?.ToString();

                        string newSerialNumber = RandomId(10);
                        smbiosData.SetValue("SystemSerialNumber", newSerialNumber);
                        string logBefore = "SMBIOS SystemSerialNumber - Before: " + serialNumberBefore;
                        string logAfter = "SMBIOS SystemSerialNumber - After: " + newSerialNumber;
                        SaveLogs("smbios", logBefore, logAfter);

                        ShowNotification("SMBIOS Function executed successfully.", NotificationType.Success);
                    }
                    else
                    {
                        ShowNotification("SMBIOS data registry key not found.", NotificationType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while executing the SMBIOS Function: " + ex.Message, NotificationType.Error);
            }
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me


        private void product_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey productKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true))
                {
                    if (productKey != null)
                    {
                        string originalProductId = productKey.GetValue("ProductId")?.ToString();

                        string newProductId = RandomIdprid(20);
                        productKey.SetValue("ProductId", newProductId);

                        string logBefore = "Product ID - Before: " + originalProductId;
                        string logAfter = "Product ID - After: " + newProductId;
                        SaveLogs("product", logBefore, logAfter);

                        ShowNotification("Product Function executed successfully.", NotificationType.Success);
                    }
                    else
                    {
                        ShowNotification("Product registry key not found.", NotificationType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while changing the Product ID: " + ex.Message, NotificationType.Error);
            }
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me

        private void backup_Click(object sender, EventArgs e)
        {
            try
            {
                string programDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string backupFolder = Path.Combine(programDirectory, "Backup");
                string backupPath = Path.Combine(backupFolder, "backup.reg");
                Directory.CreateDirectory(backupFolder);
                Process.Start("reg", $"export HKEY_LOCAL_MACHINE\\SYSTEM \"{backupPath}\" /y").WaitForExit();
                Process.Start("reg", $"export HKEY_LOCAL_MACHINE\\HARDWARE \"{backupPath}\" /y").WaitForExit();
                Process.Start("reg", $"export HKEY_LOCAL_MACHINE\\SOFTWARE \"{backupPath}\" /y").WaitForExit();

                MessageBox.Show("Registry backup created successfully.", "Backup Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while creating the registry backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void tracercl_Click(object sender, EventArgs e)
        {
            //soon...
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void req_Click(object sender, EventArgs e)
        {
            string[] registryEntries = new string[]
            {
        "HARDWARE\\DEVICEMAP\\Scsi",
        "HARDWARE\\DESCRIPTION\\System\\MultifunctionAdapter\\0\\DiskController\\0\\DiskPeripheral",
        "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName",
        "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName",
        "SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters",
        "SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces",
        "SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001",
        "SOFTWARE\\Microsoft\\Cryptography",
        "SOFTWARE\\Microsoft\\SQMClient",
        "SYSTEM\\CurrentControlSet\\Control\\SystemInformation",
        "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate",
        "SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}",
        "SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26",
        "HARDWARE\\DESCRIPTION\\System\\BIOS"
            };

            List<string> missingEntries = new List<string>();

            foreach (string entry in registryEntries)
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(entry))
                {
                    if (key == null)
                    {
                        missingEntries.Add(entry);
                    }
                }
            }

            if (missingEntries.Count > 0)
            {
                string errorMessage = Encoding.UTF8.GetString(Convert.FromBase64String("UmVnaXN0cnkgZW50cmllcyBub3QgZm91bmQ6"));
                foreach (string entry in missingEntries)
                {
                    errorMessage += Encoding.UTF8.GetString(Convert.FromBase64String("Cg==")) + entry;
                }
                ShowNotification(errorMessage, NotificationType.Error);
            }
            else
            {
                ShowNotification(Encoding.UTF8.GetString(Convert.FromBase64String("QWxsIHJlZ2lzdHJ5IGVudHJpZXMgZXhpc3Qu")), NotificationType.Success);
            }
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void Enable_LocalAreaConnection(string adapterId, bool enable)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = $"interface set interface \"{adapterId}\" {(enable ? "enable" : "disable")}";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
        }

        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void ShowNotification(string message, NotificationType type)
        {
            MessageBox.Show(message, "Spoofy [Open Source]", MessageBoxButtons.OK, GetNotificationIcon(type));
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private enum NotificationType
        {
            Success,
            Error,
            Warning
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void Form1_Load(object sender, EventArgs e)
        {

        }




        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
    }
}
