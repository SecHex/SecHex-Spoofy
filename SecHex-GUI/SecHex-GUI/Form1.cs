using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
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
        private int steps = 170;
        private Color startColor = Color.FromArgb(23, 23, 23);
        private Color middleColor = Color.FromArgb(180, 0, 158);
        private Color endColor = Color.FromArgb(23, 23, 23);
        private Color currentColor;
        private bool isDragging;
        private Point offset;

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
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
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
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Region = new Region(path);
            this.SetGraphicsQuality();
        }
        //sechex.me
        //sechex.me
        //sechex.me
        //sechex.me
        private void SetGraphicsQuality()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
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

        private void disk_Click(object sender, EventArgs e)
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
                                                    string identifier = RandomId(14);
                                                    string serialNumber = RandomId(14);

                                                    ScsuiBus.SetValue("DeviceIdentifierPage", Encoding.UTF8.GetBytes(serialNumber));
                                                    ScsuiBus.SetValue("Identifier", identifier);
                                                    ScsuiBus.SetValue("InquiryData", Encoding.UTF8.GetBytes(identifier));
                                                    ScsuiBus.SetValue("SerialNumber", serialNumber);
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
                //sechex.me
                //sechex.me
                //sechex.me
                //sechex.me
                using (RegistryKey DiskPeripherals = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\MultifunctionAdapter\\0\\DiskController\\0\\DiskPeripheral"))
                {
                    if (DiskPeripherals != null)
                    {
                        foreach (string disk in DiskPeripherals.GetSubKeyNames())
                        {
                            using (RegistryKey DiskPeripheral = Registry.LocalMachine.OpenSubKey($"HARDWARE\\DESCRIPTION\\System\\MultifunctionAdapter\\0\\DiskController\\0\\DiskPeripheral\\{disk}", true))
                            {
                                if (DiskPeripheral != null)
                                {
                                    DiskPeripheral.SetValue("Identifier", $"{RandomId(8)}-{RandomId(8)}-A");
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowNotification("DiskPeripherals key not found.", NotificationType.Error);
                        return;
                    }
                }

                ShowNotification("Disk Function executed successfully.", NotificationType.Success);
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while executing the Disk Function: " + ex.Message, NotificationType.Error);
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
                                    NetworkAdapter.SetValue("NetworkAddress", RandomMac());
                                    string adapterId = NetworkAdapter.GetValue("NetCfgInstanceId").ToString();
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
            using (RegistryKey HardwareGUID = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true))
            {
                if (HardwareGUID != null)
                {
                    HardwareGUID.SetValue("HwProfileGuid", $"{{{Guid.NewGuid()}}}");
                }
                else
                {
                    ShowNotification("HardwareGUID key not found.", NotificationType.Error);
                    return;
                }
            }
            //sechex.me
            //sechex.me
            //sechex.me
            //sechex.me
            using (RegistryKey MachineGUID = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", true))
            {
                if (MachineGUID != null)
                {
                    MachineGUID.SetValue("MachineGuid", Guid.NewGuid().ToString());
                }
                else
                {
                    ShowNotification("MachineGUID key not found.", NotificationType.Error);
                    return;
                }
            }
            //sechex.me
            //sechex.me
            //sechex.me
            //sechex.me
            using (RegistryKey MachineId = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\SQMClient", true))
            {
                if (MachineId != null)
                {
                    MachineId.SetValue("MachineId", $"{{{Guid.NewGuid()}}}");
                }
                else
                {
                    ShowNotification("MachineId key not found.", NotificationType.Error);
                    return;
                }
            }
            //sechex.me
            //sechex.me
            //sechex.me
            //sechex.me
            using (RegistryKey SystemInfo = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true))
            {
                if (SystemInfo != null)
                {
                    Random rnd = new Random();
                    int day = rnd.Next(1, 31);
                    string dayStr = (day < 10) ? $"0{day}" : day.ToString();

                    int month = rnd.Next(1, 13);
                    string monthStr = (month < 10) ? $"0{month}" : month.ToString();

                    SystemInfo.SetValue("BIOSReleaseDate", $"{dayStr}/{monthStr}/{rnd.Next(2000, 2023)}");
                    SystemInfo.SetValue("BIOSVersion", RandomId(10));
                    SystemInfo.SetValue("ComputerHardwareId", $"{{{Guid.NewGuid()}}}");
                    SystemInfo.SetValue("ComputerHardwareIds", $"{{{Guid.NewGuid()}}}\n{{{Guid.NewGuid()}}}\n{{{Guid.NewGuid()}}}\n");
                    SystemInfo.SetValue("SystemManufacturer", RandomId(15));
                    SystemInfo.SetValue("SystemProductName", RandomId(6));
                }
                else
                {
                    ShowNotification("SystemInfo key not found.", NotificationType.Error);
                    return;
                }
            }
            //sechex.me
            //sechex.me
            //sechex.me
            //sechex.me
            using (RegistryKey Update = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate", true))
            {
                if (Update != null)
                {
                    Update.SetValue("SusClientId", Guid.NewGuid().ToString());
                    Update.SetValue("SusClientIdValidation", Encoding.UTF8.GetBytes(RandomId(25)));
                }
                else
                {
                    ShowNotification("Update key not found.", NotificationType.Error);
                    return;
                }
            }

            ShowNotification("SpoofGUIDs Function executed successfully.", NotificationType.Success);
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
                RegistryKey winIDKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", true);

                if (winIDKey != null)
                {
                    // Generiere eine zufällige Windows ID
                    string spoofedWinID = RandomId(10);
                    winIDKey.SetValue("MachineGuid", spoofedWinID);
                    ShowNotification("Windows ID spoofed successfully.", NotificationType.Success);
                }
                else
                {
                    err = true;
                    ShowNotification("Windows ID spoofing failed: Registry key not found.", NotificationType.Error);
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
                string M = RandomId(8);
                using (RegistryKey N = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName", true))
                {
                    N.SetValue("ComputerName", M);
                    N.SetValue("ActiveComputerName", M);
                    N.SetValue("ComputerNamePhysicalDnsDomain", "");
                }
                using (RegistryKey O = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName", true))
                {
                    O.SetValue("ComputerName", M);
                    O.SetValue("ActiveComputerName", M);
                    O.SetValue("ComputerNamePhysicalDnsDomain", "");
                }
                using (RegistryKey P = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true))
                {
                    P.SetValue("Hostname", M);
                    P.SetValue("NV Hostname", M);
                }
                using (RegistryKey Q = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces", true))
                {
                    if (Q != null)
                    {
                        foreach (string R in Q.GetSubKeyNames())
                        {
                            using (RegistryKey interfaceSubKey = Q.OpenSubKey(R, true))
                            {
                                interfaceSubKey.SetValue("Hostname", M);
                                interfaceSubKey.SetValue("NV Hostname", M);
                            }
                        }
                    }
                }
                ShowNotification("PC Name Function executed successfully.", NotificationType.Success);
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while executing the PC Function: " + ex.Message, NotificationType.Error);
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

                    // Ändere die Display ID
                    displaySettings.SetValue("MRU0", $"Display{displayId}");

                    // Spoof die Display ID
                    string spoofedDisplayId = $"SpoofedDisplay{displayId}";
                    displaySettings.SetValue("MRU1", spoofedDisplayId);
                    displaySettings.SetValue("MRU2", spoofedDisplayId);
                    displaySettings.SetValue("MRU3", spoofedDisplayId);
                    displaySettings.SetValue("MRU4", spoofedDisplayId);
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
                RegistryKey efiVariables = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26", true);

                if (efiVariables != null)
                {
                    string efiVariableId = Guid.NewGuid().ToString();
                    efiVariables.SetValue("VariableId", efiVariableId);
                    efiVariables.Close();
                    ShowNotification("EFI Function executed successfully.", NotificationType.Success);
                }
                else
                {
                    ShowNotification("EFI variables registry key not found.", NotificationType.Error);
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
                RegistryKey smbiosData = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS", true);
                if (smbiosData != null)
                {
                    string serialNumber = RandomId(10);
                    smbiosData.SetValue("SystemSerialNumber", serialNumber);
                    smbiosData.Close();
                    ShowNotification("SMBIOS Function executed successfully.", NotificationType.Success);
                }
                else
                {
                    ShowNotification("SMBIOS data registry key not found.", NotificationType.Error);
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
