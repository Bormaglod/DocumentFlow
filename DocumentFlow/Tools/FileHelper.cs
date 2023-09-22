//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.06.2022
//-----------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

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

    public static void OpenFile(Data.DocumentRefs doc)
    {
        DeleteTempFiles("DocumentRefs");
        if (doc.FileName != null && doc.FileContent != null)
        {
            string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", "DocumentRefs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string file = Path.Combine(path, doc.FileName);
            using (FileStream stream = new(file, FileMode.Create, FileAccess.Write))
            {
                stream.Write(doc.FileContent, 0, doc.FileContent.Length);
            }

            Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });
        }
    }
}
