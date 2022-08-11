using Npgsql.Logging;
using System.IO;

namespace DocumentFlow;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(File.ReadLines("license.txt").First());
        }
        catch (FileNotFoundException e)
        {
            MessageBox.Show($"не найден файл {e.FileName}. Выполнение программы невозможно.");
            return;
        }
        
        NpgsqlLogManager.Provider = new Core.NLogLoggingProvider();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        Dapper.SqlMapper.AddTypeHandler(new Data.Core.DapperSqlDateOnlyTypeHandler());

        FastReport.Utils.RegisteredObjects.AddConnection(typeof(FastReport.Data.PostgresDataConnection));

        ApplicationConfiguration.Initialize();
        Application.Run(new CurrentApplicationContext());
    }
}
