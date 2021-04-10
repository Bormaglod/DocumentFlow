using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DocumentFlow.Authorization;

namespace DocumentFlow
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(File.ReadLines("license.txt").First());
#if DEBUG
            Npgsql.Logging.NpgsqlLogManager.Provider = new Core.NLogLoggingProvider();
#endif
            Inflector.Inflector.SetDefaultCultureFunc = () => new CultureInfo("en");

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(typeof(MainForm)));
        }
    }
}
