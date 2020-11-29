using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DocumentFlow.Authorization;

namespace DocumentFlow
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(File.ReadLines("license.txt").First());

#if DEBUG
            Npgsql.Logging.NpgsqlLogManager.Provider = new Core.NLogLoggingProvider();
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(typeof(DocumentFlowForm)));
        }
    }
}
