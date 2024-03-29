﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Tools.Minio;

using Minio;

using System.IO;

namespace DocumentFlow.Tools;

public static class FileHelper
{
    public static string GetTempPath(string category) => Path.Combine(Path.GetTempPath(), "DocumentFlow", category);

    public static string PrepareTempPath(string category)
    {
        DeleteTempFiles(category);
        string path = GetTempPath("DocumentRefs");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }

    public static void DeleteTempFiles(string category)
    {
        string path = GetTempPath(category);
        if (Directory.Exists(path))
        {
            var di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }

    public static void OpenFile(IMinioClient client, string fileName, string bucket, string s3object)
    {
        string path = PrepareTempPath("DocumentRefs");
        string file = Path.Combine(path, fileName);

        GetObject.Run(client, bucket, s3object, file).Wait();
        WorkOperations.OpenFile(file);
    }
}
