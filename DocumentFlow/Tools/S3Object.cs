//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Interfaces;
using DocumentFlow.Settings.Authentification;

using Microsoft.Extensions.Logging;

using Minio;

using System.IO;
using System.Text.Json;

namespace DocumentFlow.Tools;

public class S3Object : IS3Object
{
    private readonly ILogger<S3Object> logger;
    private readonly MinioClient? minio;

    private bool disposed = false;

    public S3Object(ILogger<S3Object> logger)
    {
        this.logger = logger;

        if (File.Exists("passwords.json"))
        {
            var auth = JsonSerializer.Deserialize<AuthentificationInfo>(File.ReadAllText("passwords.json"));
            if (auth != null)
            {
                minio = new MinioClient()
                    .WithEndpoint(auth.S3.EndPoint)
                    .WithCredentials(auth.S3.AccessKey, auth.S3.SecretKey)
                    .Build();
                logger.LogInformation($"Minio client connected to {auth.S3.EndPoint}");
                return;
            }
        }
    }

    ~S3Object()
    {
        Dispose(disposing: false);
        logger.LogInformation($"Minio client destroy");
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
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
        logger.LogInformation($"Downloaded the object {objectName} in bucket {bucketName}");
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
            logger.LogInformation($"Created bucket {bucketName}");
        }

        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

        var obj = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType("application/octet-stream");

        _ = await minio.PutObjectAsync(obj).ConfigureAwait(false);
        logger.LogInformation($"Uploaded object {objectName} to bucket {bucketName}");
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
        logger.LogInformation($"Removed object {objectName} from bucket {bucketName} successfully");
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                if (minio != null)
                {
                    minio.Dispose();
                    logger.LogInformation($"Minio client disconnected");
                }
            }

            disposed = true;
        }
    }
}
