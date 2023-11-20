using Microsoft.Win32;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Timers;
using Microsoft.Win32;



namespace HWID_Changer
{
    class Program
    {


        public static void CheckRegistryKeys()
        {
            try
            {
                CheckRegistryKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "InstallationID");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName", "ComputerName");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName", "ActiveComputerName");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerNamePhysicalDnsDomain", "");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName", "ComputerName");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName", "ActiveComputerName");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName", "ComputerNamePhysicalDnsDomain");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", "Hostname");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", "NV Hostname");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces", "Hostname");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces", "NV Hostname");
                CheckRegistryKey("HARDWARE\\DEVICEMAP\\Scsi", ""); // ScsiPorts
                CheckRegistryKey("HARDWARE\\DEVICEMAP\\Scsi\\{port}", ""); // ScsiBuses
                CheckRegistryKey("HARDWARE\\DEVICEMAP\\Scsi\\{port}\\{bus}\\Target Id 0\\Logical Unit Id 0", "DeviceIdentifierPage");
                CheckRegistryKey("HARDWARE\\DEVICEMAP\\Scsi\\{port}\\{bus}\\Target Id 0\\Logical Unit Id 0", "Identifier");
                CheckRegistryKey("HARDWARE\\DEVICEMAP\\Scsi\\{port}\\{bus}\\Target Id 0\\Logical Unit Id 0", "InquiryData");
                CheckRegistryKey("HARDWARE\\DEVICEMAP\\Scsi\\{port}\\{bus}\\Target Id 0\\Logical Unit Id 0", "SerialNumber");
                CheckRegistryKey("HARDWARE\\DESCRIPTION\\System\\MultifunctionAdapter\\0\\DiskController\\0\\DiskPeripheral", ""); // DiskPeripherals
                CheckRegistryKey("HARDWARE\\DESCRIPTION\\System\\MultifunctionAdapter\\0\\DiskController\\0\\DiskPeripheral\\{disk}", "Identifier");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", "HwProfileGuid");
                CheckRegistryKey("SOFTWARE\\Microsoft\\Cryptography", "MachineGuid");
                CheckRegistryKey("SOFTWARE\\Microsoft\\SQMClient", "MachineId");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "BIOSReleaseDate");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "BIOSVersion");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "ComputerHardwareId");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "ComputerHardwareIds");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "ComputerManufacturer");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "ComputerModel");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "InstallDate");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "SystemBiosMajorVersion");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "SystemBiosMinorVersion");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "SystemBiosVersion");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "SystemManufacturer");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "SystemProductName");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "SystemSku");
                CheckRegistryKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", "SystemVersion");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error to check the Registry-Key: " + ex.Message);
            }
        }

        public static void CheckRegistryKey(string keyPath, string valueName)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath);
            if (key != null)
            {
                if (!string.IsNullOrEmpty(valueName))
                {
                    if (key.GetValue(valueName) == null)
                    {
                        Console.WriteLine("Registry-Key not found: " + keyPath + "\\" + valueName);
                    }
                }
                else
                {
                    if (key.SubKeyCount == 0)
                    {
                        Console.WriteLine("Registry-Key not found: " + keyPath);
                    }
                }
            }
            else
            {
                Console.WriteLine("Registry-Key not found: " + keyPath);
            }
        }















        public static void SpoofInstallationID()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true))
            {
                if (key != null)
                {
                    string newInstallationID = Guid.NewGuid().ToString();
                    key.SetValue("InstallationID", newInstallationID);
                    key.Close();
                }
            }
        }

        public static void SpoofPCName()
        {
            string randomName = RandomId(8); // Generate a random PC name
            using RegistryKey computerName = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName", true);
            computerName.SetValue("ComputerName", randomName);
            computerName.SetValue("ActiveComputerName", randomName);
            computerName.SetValue("ComputerNamePhysicalDnsDomain", "");

            using RegistryKey activeComputerName = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName", true);
            activeComputerName.SetValue("ComputerName", randomName);
            activeComputerName.SetValue("ActiveComputerName", randomName);
            activeComputerName.SetValue("ComputerNamePhysicalDnsDomain", "");

            using RegistryKey tcpipParams = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", true);
            tcpipParams.SetValue("Hostname", randomName);
            tcpipParams.SetValue("NV Hostname", randomName);

            using RegistryKey tcpipInterfaces = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces", true);
            foreach (string interfaceKey in tcpipInterfaces.GetSubKeyNames())
            {
                using RegistryKey interfaceSubKey = tcpipInterfaces.OpenSubKey(interfaceKey, true);
                interfaceSubKey.SetValue("Hostname", randomName);
                interfaceSubKey.SetValue("NV Hostname", randomName);
            }
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


        public static void SpoofDisks()
        {
            using RegistryKey ScsiPorts = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\Scsi");
            foreach (string port in ScsiPorts.GetSubKeyNames())
            {
                using RegistryKey ScsiBuses = Registry.LocalMachine.OpenSubKey($"HARDWARE\\DEVICEMAP\\Scsi\\{port}");
                foreach (string bus in ScsiBuses.GetSubKeyNames())
                {
                    using RegistryKey ScsuiBus = Registry.LocalMachine.OpenSubKey($"HARDWARE\\DEVICEMAP\\Scsi\\{port}\\{bus}\\Target Id 0\\Logical Unit Id 0", true);
                    if (ScsuiBus != null)
                    {
                        if (ScsuiBus.GetValue("DeviceType").ToString() == "DiskPeripheral")
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

            using RegistryKey DiskPeripherals = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\MultifunctionAdapter\\0\\DiskController\\0\\DiskPeripheral");
            foreach (string disk in DiskPeripherals.GetSubKeyNames())
            {
                using RegistryKey DiskPeripheral = Registry.LocalMachine.OpenSubKey($"HARDWARE\\DESCRIPTION\\System\\MultifunctionAdapter\\0\\DiskController\\0\\DiskPeripheral\\{disk}", true);
                DiskPeripheral.SetValue("Identifier", $"{RandomId(8)}-{RandomId(8)}-A");
            }
        }

        public static void SpoofGUIDs()
        {
            using RegistryKey HardwareGUID = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true);
            HardwareGUID.SetValue("HwProfileGuid", $"{{{Guid.NewGuid()}}}");

            using RegistryKey MachineGUID = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", true);
            MachineGUID.SetValue("MachineGuid", Guid.NewGuid().ToString());

            using RegistryKey MachineId = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\SQMClient", true);
            MachineId.SetValue("MachineId", $"{{{Guid.NewGuid()}}}");

            using RegistryKey SystemInfo = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\SystemInformation", true);

            Random rnd = new Random();
            int day = rnd.Next(1, 31);
            string dayStr = "";
            if (day < 10) dayStr = $"0{day}";
            else dayStr = day.ToString();

            int month = rnd.Next(1, 13);
            string monthStr = "";
            if (month < 10) monthStr = $"0{month}";
            else monthStr = month.ToString();

            SystemInfo.SetValue("BIOSReleaseDate", $"{dayStr}/{monthStr}/{rnd.Next(2000, 2023)}");
            SystemInfo.SetValue("BIOSVersion", RandomId(10));
            SystemInfo.SetValue("ComputerHardwareId", $"{{{Guid.NewGuid()}}}");
            SystemInfo.SetValue("ComputerHardwareIds", $"{{{Guid.NewGuid()}}}\n{{{Guid.NewGuid()}}}\n{{{Guid.NewGuid()}}}\n");
            SystemInfo.SetValue("SystemManufacturer", RandomId(15));
            SystemInfo.SetValue("SystemProductName", RandomId(6));

            using RegistryKey Update = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate", true);
            Update.SetValue("SusClientId", Guid.NewGuid().ToString());
            Update.SetValue("SusClientIdValidation", Encoding.UTF8.GetBytes(RandomId(25)));
        }


        public static void UbisoftCache()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string ubisoftPath = Path.Combine("Ubisoft Game Launcher", "cache");
            string ubisoftLogsPath = Path.Combine("Ubisoft Game Launcher", "logs");
            string ubisoftSavegamesPath = Path.Combine("Ubisoft Game Launcher", "savegames");
            string ubisoftSpoolPath = Path.Combine("Ubisoft Game Launcher", "spool");

            DirectoryInfo di = new DirectoryInfo(Path.Combine("C:", "Program Files (x86)", "Ubisoft", ubisoftPath));
            DirectoryInfo di2 = new DirectoryInfo(Path.Combine("C:", "Program Files (x86)", "Ubisoft", ubisoftLogsPath));
            DirectoryInfo di3 = new DirectoryInfo(Path.Combine("C:", "Program Files (x86)", "Ubisoft", ubisoftSavegamesPath));
            DirectoryInfo di4 = new DirectoryInfo(Path.Combine(appDataPath, "Ubisoft Game Launcher", ubisoftSpoolPath));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            foreach (FileInfo file in di2.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di2.GetDirectories())
            {
                dir.Delete(true);
            }
            foreach (FileInfo file in di3.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di3.GetDirectories())
            {
                dir.Delete(true);
            }
            foreach (FileInfo file in di4.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di4.GetDirectories())
            {
                dir.Delete(true);
            }
        }



        public static void DeleteValorantCache()
        {
            string valorantPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VALORANT\\saved";

            if (Directory.Exists(valorantPath))
            {
                DirectoryInfo di = new DirectoryInfo(valorantPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
        }



        public static bool SpoofMAC() //SpoofMacREAL

        {
            bool err = false;

            using RegistryKey NetworkAdapters = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}");
            foreach (string adapter in NetworkAdapters.GetSubKeyNames())
            {
                if (adapter != "Properties")
                {
                    try
                    {
                        using RegistryKey NetworkAdapter = Registry.LocalMachine.OpenSubKey($"SYSTEM\\CurrentControlSet\\Control\\Class\\{{4d36e972-e325-11ce-bfc1-08002be10318}}\\{adapter}", true);
                        if (NetworkAdapter.GetValue("BusType") != null)
                        {
                            NetworkAdapter.SetValue("NetworkAddress", RandomMac());
                            string adapterId = NetworkAdapter.GetValue("NetCfgInstanceId").ToString();
                            Enable_LocalAreaConection(adapterId, false);
                            Enable_LocalAreaConection(adapterId, true);

                        }
                    }
                    catch (System.Security.SecurityException ex)
                    {
                        Console.WriteLine("\n[X] Start the spoofer in admin mode to spoof your MAC address!");
                        err = true;
                        break;
                    }
                }
            }

            return err;
        }


        public static void SpoofGPU()
        {
            string keyName = @"SYSTEM\CurrentControlSet\Enum\PCI\VEN_10DE&DEV_0DE1&SUBSYS_37621462&REV_A1";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    string newHardwareID = "PCIVEN_8086&DEV_1234&SUBSYS_5678ABCD&REV_01";
                    string oldHardwareID = key.GetValue("HardwareID") as string;

                    key.SetValue("HardwareID", newHardwareID);
                    key.SetValue("CompatibleIDs", new string[] { newHardwareID });
                    key.SetValue("Driver", "pci.sys");
                    key.SetValue("ConfigFlags", 0x00000000, RegistryValueKind.DWord);
                    key.SetValue("ClassGUID", "{4d36e968-e325-11ce-bfc1-08002be10318}");
                    key.SetValue("Class", "Display");

                    key.Close();
                }
            }
        }


        public static void SpoofEFIVariableId()
        {
            try
            {
                RegistryKey efiVariables = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26", true);
                if (efiVariables != null)
                {
                    string efiVariableId = Guid.NewGuid().ToString();
                    efiVariables.SetValue("VariableId", efiVariableId);
                    efiVariables.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\n[X] Start the spoofer in admin mode to spoof your MAC address!");
            }
        }



        public static void SpoofSMBIOSSerialNumber()
        {
            try
            {
                RegistryKey smbiosData = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS", true);

                if (smbiosData != null)
                {
                    string serialNumber = RandomId(10);
                    smbiosData.SetValue("SystemSerialNumber", serialNumber);
                    smbiosData.Close();
                }
                else
                {
                    Console.WriteLine("\n[X] Cant find the SMBIOS");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\n[X] Start the spoofer in admin mode to spoof your MAC address!");
            }
        }



        public static void DisplaySystemData()
        {
            Console.WriteLine("System Data:");
            Console.WriteLine("------------------------------------------------");

            try
            {
                // Display HWID
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true))
                {
                    string installationID = key.GetValue("InstallationID") as string;
                    Console.WriteLine("HWID:              " + installationID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving HWID: " + ex.Message);
            }

            try
            {
                // Display GUIDs
                using (RegistryKey machineGuidKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography"))
                {
                    string machineGuid = machineGuidKey.GetValue("MachineGuid") as string;
                    Console.WriteLine("Machine GUID:      " + machineGuid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving Machine GUID: " + ex.Message);
            }

            try
            {
                // Display MAC ID
                foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                    Console.WriteLine("MAC ID (" + networkInterface.Name + "):     " + physicalAddress.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving MAC ID: " + ex.Message);
            }

            try
            {
                // Display Installation ID
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", true))
                {
                    string installationID = key.GetValue("InstallationID") as string;
                    Console.WriteLine("Installation ID:    " + installationID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving Installation ID: " + ex.Message);
            }

            try
            {
                // Display PC Name
                using (RegistryKey computerName = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName"))
                {
                    string pcName = computerName.GetValue("ComputerName") as string;
                    Console.WriteLine("PC Name:           " + pcName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving PC Name: " + ex.Message);
            }

            try
            {
                // Display GPU ID
                using (RegistryKey gpuKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Enum\PCI\VEN_10DE&DEV_0DE1&SUBSYS_37621462&REV_A1"))
                {
                    string hardwareID = gpuKey.GetValue("HardwareID") as string;
                    Console.WriteLine("GPU ID:            " + hardwareID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving GPU ID: " + ex.Message);
            }


            try
            {
                // Display CPU Information
                string cpuInfo = string.Empty;
                using (StreamReader reader = new StreamReader(@"C:\proc\cpuinfo"))
                {
                    cpuInfo = reader.ReadToEnd();
                }
                Console.WriteLine("CPU Information:   " + cpuInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving CPU Information: " + ex.Message);
            }

            try
            {
                // Display Memory Information
                using (StreamReader reader = new StreamReader("/proc/meminfo"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine("Memory Information: " + line);
                    }
                }
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error retrieving Memory Information: " + ex.Message);
            }
        }











        public static void Menu()
        {
            Console.WriteLine("\n  SecHex");
            Console.Write("  Select an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // Spoof disks
                    SpoofDisks();
                    Console.WriteLine("\n  [+] Disks spoofed");
                    ClearConsoleAfterDelay();

                    Menu();
                    break;

                case "2":
                    // Spoof GUIDs
                    SpoofGUIDs();
                    Console.WriteLine("\n  [+] GUIDs spoofed");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;

                case "3":
                    // Spoof MAC address
                    SpoofMAC();
                    Console.WriteLine("  [+] MAC address spoofed");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;

                case "4":
                    // Delete Ubisoft cache
                    UbisoftCache();
                    Console.WriteLine("\n  [+] Ubisoft Cache deleted");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;

                case "5":
                    // Delete Valorant cache
                    DeleteValorantCache();
                    Console.WriteLine("\n  [+] Valorant Cache deleted");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;

                case "6":
                    // Spoof GPU ID
                    SpoofGPU();
                    Console.WriteLine("\n  [+] GPU ID Spoofed");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;

                case "7":
                    // Spoof PC Name
                    SpoofPCName();
                    Console.WriteLine("\n  [+] PC name spoofed");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;

                case "8":
                    // Spoof Installation ID
                    SpoofInstallationID();
                    Console.WriteLine("\n  [+] Installation ID spoofed");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;


                case "9":
                    // Spoof EFI
                    SpoofEFIVariableId();
                    ClearConsoleAfterDelay();
                    Menu();
                    break;


                case "10":
                    // Spoof smbios
                    SpoofSMBIOSSerialNumber();
                    ClearConsoleAfterDelay();
                    Menu();
                    break;


                case "11":
                    // Check registry
                    CheckRegistryKeys();
                    ClearConsoleAfterDelay2();
                    Menu();
                    break;


                case "12":
                    // get sys data
                    DisplaySystemData();
                    ClearConsoleAfterDelay();
                    Menu();
                    break;


                case "13":
                    // Spoof all
                    SpoofDisks();
                    SpoofGUIDs();
                    SpoofMAC();
                    UbisoftCache();
                    DeleteValorantCache();
                    SpoofGPU();
                    SpoofPCName();
                    SpoofEFIVariableId();
                    SpoofInstallationID();
                    Console.WriteLine("\n  [+] All commands executed");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;

                case "exit":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("\n  [X] Invalid option!");
                    ClearConsoleAfterDelay();
                    Menu();
                    break;
            }
        }



        private static void ClearConsoleAfterDelay()
        {
            Thread.Sleep(3000); 
            Console.Clear(); 
            Main(); 
        }

        private static void ClearConsoleAfterDelay2()
        {
            Thread.Sleep(6000);
            Console.Clear();
            Main();
        }




        static void Main()
        {
            Console.Title = "SecHex | V1.3 | Open Source | github/SecHex";
            Console.ForegroundColor
          = ConsoleColor.Magenta;
            Console.Clear();
            Console.WriteLine("███████╗██████╗  ██████╗  ██████╗ ███████╗██╗   ██╗      ");
            Console.WriteLine("██╔════╝██╔══██╗██╔═══██╗██╔═══██╗██╔════╝╚██╗ ██╔╝      ");
            Console.WriteLine("███████╗██████╔╝██║   ██║██║   ██║█████╗   ╚████╔╝       ");
            Console.WriteLine("╚════██║██╔═══╝ ██║   ██║██║   ██║██╔══╝    ╚██╔╝        ");
            Console.WriteLine("███████║██║     ╚██████╔╝╚██████╔╝██║        ██║         ");
            Console.WriteLine("╚══════╝╚═╝      ╚═════╝  ╚═════╝ ╚═╝        ╚═╝         ");
            Console.WriteLine("https://github.com/SecHex                                ");
            Console.WriteLine("                                            ");
            Console.WriteLine("[1] Spoof HWID                       [7] Spoof PC Name            ");
            Console.WriteLine("[2] Spoof GUID                       [8] Spoof Installation ID    ");
            Console.WriteLine("[3] Spoof MAC ID                     [9] Spoof EFI                ");
            Console.WriteLine("[4] Delete UBI Cache                 [10] Spoof SMBIOS            ");
            Console.WriteLine("[5] Delete Valorant Cache                   ");
            Console.WriteLine("[6] Spoof GPU ID                            ");
            Console.WriteLine("                                            ");
            Console.WriteLine("[11] Check Registry Paths                   ");
            Console.WriteLine("[12] Get System informations                ");
            Console.WriteLine("[13] Spoof all                              ");
            Console.WriteLine("[exit] Exit                                 ");
            Console.WriteLine("  ");
            Console.ForegroundColor
          = ConsoleColor.Green;
            Menu();

        }
    }
}
