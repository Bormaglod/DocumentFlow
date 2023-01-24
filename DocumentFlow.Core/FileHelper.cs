//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.06.2022
//
// Версия 2023.1.24
//  - DocumentRefs заменен на IDocumentRefs
//  - уровень защиты класса и методов изменён с internal на public
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

using System.Diagnostics;

namespace DocumentFlow.Core;

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

    public static void OpenFile(IDocumentRefs doc)
    {
        FileHelper.DeleteTempFiles("DocumentRefs");
        if (doc.file_name != null && doc.file_content != null)
        {
            string path = Path.Combine(Path.GetTempPath(), "DocumentFlow", "DocumentRefs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
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
