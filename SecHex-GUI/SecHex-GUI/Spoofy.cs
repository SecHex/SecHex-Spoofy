using Microsoft.Win32;
using Siticone.Desktop.UI.WinForms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;
using MetroFramework.Controls;


namespace SecHex_GUI
{
    public partial class Spoofy : MetroFramework.Forms.MetroForm
    {
        private System.Windows.Forms.Timer timer;
        private bool isAnimationRunning = false;

        public Spoofy()
        {
            InitializeComponent();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;

            this.DoubleBuffered = true;
            timer.Start();
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


        private bool isLogToggleOn = false;
        private bool isAfricaToggleOn = false;
        private south_africa WindowSouthAfrica;
        private logs logWindow;

        private void OpenAfrica()
        {
            if (WindowSouthAfrica == null || WindowSouthAfrica.IsDisposed)
            {
                Console.WriteLine("OpenAfrica() called");
                WindowSouthAfrica = new south_africa();
                WindowSouthAfrica.Show();
            }
            else
            {
                WindowSouthAfrica.Show();
            }
        }

        private void CloseAfrica()
        {
            if (WindowSouthAfrica != null && !WindowSouthAfrica.IsDisposed)
            {
                WindowSouthAfrica.Close();
                WindowSouthAfrica.Dispose();
                WindowSouthAfrica = null;
            }
        }

        private void OpenLogWindow()
        {
            if (logWindow == null || logWindow.IsDisposed)
            {
                logWindow = new logs();
                logWindow.Show();
            }
            else
            {
                logWindow.Show();
            }
        }

        private void CloseLogWindow()
        {
            if (logWindow != null && !logWindow.IsDisposed)
            {
                logWindow.Hide();
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

            AppendLogEntryToWindow(logEntryBefore, logEntryAfter);
        }

        private void AppendLogEntryToWindow(string logEntryBefore, string logEntryAfter)
        {
            if (logWindow != null && !logWindow.IsDisposed)
            {
                logWindow.AddLogEntry(logEntryBefore, logEntryAfter);
            }
        }

        public static void LocalAreaConection(string adapterId, bool enable = true)
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


        private string RandomIdprid2(int length)
        {
            const string digits = "0123456789";
            const string letters = "abcdefghijklmnopqrstuvwxyz";
            var random = new Random();
            var id = new char[32];
            int letterIndex = 0;

            for (int i = 0; i < 32; i++)
            {
                if (i == 8 || i == 13 || i == 18 || i == 23)
                {
                    id[i] = '-';
                }
                else if (i % 5 == 4)
                {
                    id[i] = letters[random.Next(letters.Length)];
                    letterIndex++;
                }
                else
                {
                    id[i] = digits[random.Next(digits.Length)];
                }
            }

            return new string(id);
        }


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




        private List<string> diskNames = new List<string>()
{
    "Samsung SSD 870 QVO 1TB",
    "NVMe KINGSTON SA2000M2105",
    "Crucial MX500 1TB",
    "WD Blue 2TB",
    "Seagate Barracuda 4TB",
    "Intel 660p 1TB",
    "SanDisk Ultra 3D 2TB",
    "Toshiba X300 6TB",
    "Adata XPG SX8200 Pro 1TB",
    "HP EX920 512GB",
    "Kingston A2000 500GB",
    "Corsair MP600 2TB",
    "Western Digital Black 6TB",
    "Crucial P1 1TB",
    "Seagate FireCuda 2TB",
    "Samsung 970 EVO Plus 1TB",
    "ADATA Swordfish 500GB",
    "Toshiba N300 8TB",
    "WD Red Pro 10TB",
    "Kingston KC600 256GB",
};

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

                                                    string identifierAfter = GetRandomDiskName();
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
                ShowNotification("An error occurred while spoofing the DISK: " + ex.Message, NotificationType.Error);
            }
        }

        private string GetRandomDiskName()
        {
            Random random = new Random();
            int index = random.Next(diskNames.Count);
            return diskNames[index];
        }




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
                                    LocalAreaConection(adapterId, false);
                                    LocalAreaConection(adapterId, true);
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


        private void GUID_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey HardwareGUID = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true))
                {
                    if (HardwareGUID != null)
                    {
                        string logBefore = "HwProfileGuid - Before: " + HardwareGUID.GetValue("HwProfileGuid");
                        HardwareGUID.DeleteValue("HwProfileGuid");
                        HardwareGUID.SetValue("HwProfileGuid", Guid.NewGuid().ToString());
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
                        string logBefore = "MachineGuid - Before: " + MachineGUID.GetValue("MachineGuid");
                        MachineGUID.DeleteValue("MachineGuid");
                        MachineGUID.SetValue("MachineGuid", Guid.NewGuid().ToString());
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
                        string logBefore = "MachineId - Before: " + MachineId.GetValue("MachineId");
                        MachineId.DeleteValue("MachineId");
                        MachineId.SetValue("MachineId", Guid.NewGuid().ToString());
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

                        string logBefore = "BIOSReleaseDate - Before: " + SystemInfo.GetValue("BIOSReleaseDate");
                        SystemInfo.SetValue("BIOSReleaseDate", randomDate);
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




        private void winid_Click(object sender, EventArgs e)
        {
            try
            {
                using (RegistryKey machineGuidKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", true))
                {
                    if (machineGuidKey != null)
                    {
                        string machineGuidBefore = machineGuidKey.GetValue("MachineGuid")?.ToString();

                        string newMachineGuid = RandomIdprid2(10);
                        machineGuidKey.SetValue("MachineGuid", newMachineGuid);

                        string logBefore = $"Machine GUID - Before: {machineGuidBefore}";
                        string logAfter = $"Machine GUID - After: {newMachineGuid}";
                        SaveLogs("ChangeMachineGuid", logBefore, logAfter);
                    }
                    else
                    {
                        ShowNotification("Machine GUID registry key not found.", NotificationType.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowNotification("An error occurred while changing the machine GUID: " + ex.Message, NotificationType.Error);
            }
        }



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


        private void display_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey displaySettings = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RunMRU", true);

                if (displaySettings != null)
                {
                    string originalDisplayId = displaySettings.GetValue("MRU0")?.ToString();
                    int displayId = RandomDisplayId();
                    string spoofedDisplayId = $"SpoofedDisplay{displayId}";

                    displaySettings.SetValue("MRU0", spoofedDisplayId);
                    displaySettings.SetValue("MRU1", spoofedDisplayId);
                    displaySettings.SetValue("MRU2", spoofedDisplayId);
                    displaySettings.SetValue("MRU3", spoofedDisplayId);
                    displaySettings.SetValue("MRU4", spoofedDisplayId);

                    string logBefore = "Display ID - Before: " + originalDisplayId;
                    string logAfter = "Display ID - After: " + displayId;
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

        private int RandomDisplayId()
        {
            Random rnd = new Random();
            return rnd.Next(1, 9);
        }


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


        private void tracercl_Click(object sender, EventArgs e)
        {
            //soon...
        }

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void systemcleaner_CheckedChanged(object sender, EventArgs e)
        {
            isAfricaToggleOn = systemcleaner.Checked;
            if (isAfricaToggleOn)
            {
                OpenAfrica();
            }
            else
            {
                CloseAfrica();
            }
        }

        private void logwind_CheckedChanged(object sender, EventArgs e)
        {
            if (logWindow == null || logWindow.IsDisposed)
            {
                logWindow = new logs();
                logWindow.Show();
            }
            else
            {
                logWindow.Show();
            }
        }

        private void lgbt_CheckedChanged(object sender, EventArgs e)
        {
            MetroCheckBox checkBox = (MetroCheckBox)sender;

            if (checkBox.Checked)
            {
                AnimateButtonsBorderColor(disk, winid, disk, efi, GUID, spoofall, mac, tracercl, display, pcname, backup, product, req, sm);
            }
            else
            {
                StopButtonAnimation(disk, winid, disk, efi, GUID, spoofall, mac, tracercl, display, pcname, backup, product, req, sm);
            }
        }



    }
}
