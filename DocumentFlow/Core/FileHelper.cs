//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using System.Diagnostics;
using System.IO;

namespace DocumentFlow.Core;

internal static class FileHelper
{
    internal static void DeleteTempFiles(string category)
    {
        string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", category);
        if (System.IO.Directory.Exists(path))
        {
            var di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }

    internal static void OpenFile(DocumentRefs doc)
    {
        FileHelper.DeleteTempFiles("DocumentRefs");
        if (doc.file_name != null && doc.file_content != null)
        {
            string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", "DocumentRefs");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            string file = Path.Combine(path, doc.file_name);
            using (FileStream stream = new(file, FileMode.Create, FileAccess.Write))
            {
                stream.Write(doc.file_content, 0, doc.file_content.Length);
            }

            Process.Start(new ProcessStartInfo(file) { UseShellExecute = true });

        }
    }
}
