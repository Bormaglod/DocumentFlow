//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Interfaces;
using DocumentFlow.Settings.Authentification;

using Minio;

using System.IO;
using System.Text.Json;

namespace DocumentFlow.Tools;

public class S3Object : IS3Object
{
    private readonly MinioClient? minio;

    public S3Object()
    {
        if (File.Exists("passwords.json"))
        {
            var auth = JsonSerializer.Deserialize<AuthentificationInfo>(File.ReadAllText("passwords.json"));
            if (auth != null)
            {
                minio = new MinioClient()
                    .WithEndpoint(auth.S3.EndPoint)
                    .WithCredentials(auth.S3.AccessKey, auth.S3.SecretKey)
                    .Build();
                return;
            }
        }
    }

    public async Task GetObject(string bucketName, string objectName, string fileName)
    {
        if (minio == null)
        {
            return;
        }

        var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithFile(fileName);
        _ = await minio.GetObjectAsync(args).ConfigureAwait(false);
    }

    public async Task PutObject(string bucketName, string objectName, string fileName)
    {
        if (minio == null)
        {
            return;
        }

        var args = new BucketExistsArgs()
            .WithBucket(bucketName);
        var found = await minio.BucketExistsAsync(args).ConfigureAwait(false);

        if (!found)
        {
            await minio.MakeBucketAsync(
                new MakeBucketArgs()
                    .WithBucket(bucketName)
                ).ConfigureAwait(false);
        }

        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

        var obj = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType("application/octet-stream");

        _ = await minio.PutObjectAsync(obj).ConfigureAwait(false);
    }

    public async Task RemoveObject(string bucketName, string objectName)
    {
        if (minio == null)
        {
            return;
        }

        var args = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);
        await minio.RemoveObjectAsync(args).ConfigureAwait(false);
    }
}
