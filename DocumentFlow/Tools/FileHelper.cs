//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Interfaces;

using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Tools;

public static class FileHelper
{
    public static void DeleteTempFiles(string category)
    {
        string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", category);
        if (Directory.Exists(path))
        {
            var di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }

    public static void OpenFile(IServiceProvider services, string fileName, string bucket, string s3object)
    {
        DeleteTempFiles("DocumentRefs");
        string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", "DocumentRefs");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string file = Path.Combine(path, fileName);
        services
           .GetRequiredService<IS3Object>()
           .GetObject(bucket, s3object, file)
           .Wait();

        Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
    }
}
