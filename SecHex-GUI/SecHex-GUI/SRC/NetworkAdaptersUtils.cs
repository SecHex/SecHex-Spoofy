using System.Management;

namespace SecHex_GUI.SRC
{
    /// <summary>
    /// The dependencies are: https://www.nuget.org/packages/System.Management/
    /// </summary>
    static public class NetworkAdaptersUtils
    {
        public enum AdapterOperations
        {
            Enable = 0, Disable = 1
        }

        private static ManagementObjectCollection adapterCollection;
        static NetworkAdaptersUtils()
        {
            SelectQuery query = new SelectQuery("Win32_NetworkAdapter", "" /*"NetConnectionStatus=2"*/);
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);
            adapterCollection = search.Get();
        }
        public static void EnableDisableAdapter(string adapterId, AdapterOperations operation)
        {
            foreach (ManagementObject result in adapterCollection)
            {                
                NetworkAdapter Myadapter = new NetworkAdapter(result);
                if ((Myadapter.GUID!=null) &&(Myadapter.GUID.Equals(adapterId)))
                {
                    switch (operation)
                    {
                        case AdapterOperations.Enable: Myadapter.Enable(); break;
                        case AdapterOperations.Disable: Myadapter.Disable(); break;
                    }
                }
            }
        }
        
        public static void RestartNetworkAdapter(string adapterId)
        {
            EnableDisableAdapter(adapterId, AdapterOperations.Disable);
            EnableDisableAdapter(adapterId, AdapterOperations.Enable);
        }
    }
}
