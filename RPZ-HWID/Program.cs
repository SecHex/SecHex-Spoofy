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



namespace HWID_Changer
{
    class Program
    {


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







        public static bool SpoofMA2C() //SpoofMacNUMMER 2

        {
            bool err2 = false;

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
                        err2 = true;
                        break;
                    }
                }
            }

            return err2;
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







        public static void Menu()
        {

            Console.WriteLine("\n  Rapunzel Spoofer");
            Console.Write("  Select an option: ");
            string input = Console.ReadLine();




            switch (input)
            {
                case "disk":
                    SpoofDisks();
                    Console.WriteLine("\n  [+] Disks spoofed");
                    Menu();
                    break;

                case "guid":
                    SpoofGUIDs();
                    Console.WriteLine("\n  [+] GUIDs spoofed");
                    Menu();
                    break;

                case "mac":
                    bool err = SpoofMAC();
                    if (!err) Console.WriteLine("  [+] MAC address spoofed");
                    Menu();
                    break;

                case "ubi-cache":
                    UbisoftCache();
                    Console.WriteLine("\n  [+] Ubisoft Cache deletet");
                    Menu();
                    break;



                case "valo-cache":
                    DeleteValorantCache();
                    Console.WriteLine("\n  [+] Valorant Cache deletet");
                    Menu();
                    break;


                case "gpu":
                    SpoofGPU();
                    Console.WriteLine("\n  [+] GPU ID Spoofed");
                    Menu();
                    break;



                case "spoof-all":
                    SpoofDisks();
                    Console.WriteLine("\n  [1] Disks spoofed");
                    SpoofGUIDs();
                    Console.WriteLine("\n  [2] GUIDs Spoofed");
                    UbisoftCache();
                    Console.WriteLine("\n  [3] GPU ID Spoofed");
                    SpoofGPU();
                    Console.WriteLine("\n  [4] Ubisoft Tracer deletet");
                    bool err2 = SpoofMA2C();
                    if (!err2) Console.WriteLine("  [5] MAC address spoofed");
                    Menu();
                    break;





                case "exit":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("\n  [X] Invalid option!");
                    Menu();
                    break;




            }
        }




        static void Main()
        {


            Console.Title = "Rapunzel V.1.0";
            Console.ForegroundColor
          = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine("  ┌ HWID CHANGER - Rapunzel  ──────────────────┐       https://github.com/Rapunzel-ware");
            Console.WriteLine("  │ [disk] Spoof HWID                          │");
            Console.WriteLine("  │ [guid] Spoof GUID                          │");
            Console.WriteLine("  │ [mac] Spoof MAC ID                         │");
            Console.WriteLine("  │ [PC-Name] Spoof PC Name    (soon)          │");
            Console.WriteLine("  │ [ubi-cache] Delete UBI Cache               │");
            Console.WriteLine("  │ [valo-cache] Delete Valoant Cache          │");
            Console.WriteLine("  │ [gpu] Spoof GPU ID                         │");
            Console.WriteLine("  │ [spoof-all] Spoof all                      │");
            Console.WriteLine("  │ [exit] Exit                                │");
            Console.WriteLine("  └────────────────────────────────────────────┘");
            Console.ForegroundColor
          = ConsoleColor.Green;
            Menu();


        }
    }
}