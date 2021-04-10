using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentFlow.Data.Sync
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(System.IO.File.ReadLines("license.txt").First());
            Inflector.Inflector.SetDefaultCultureFunc = () => new CultureInfo("en");

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
