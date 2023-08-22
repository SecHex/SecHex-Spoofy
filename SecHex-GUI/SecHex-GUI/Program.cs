namespace SecHex_GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Spoofy());
            Application.EnableVisualStyles();
        }
    }
}