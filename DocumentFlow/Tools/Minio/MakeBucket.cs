//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.10.2023
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Minio;
using Minio.DataModel.Args;

namespace DocumentFlow.Tools.Minio;

public static class MakeBucket
{
    public static async Task Run(IMinioClient minio, string bucketName)
    {
        if (minio is null)
        {
            throw new ArgumentNullException(nameof(minio));
        }

        await minio.MakeBucketAsync(
            new MakeBucketArgs()
                .WithBucket(bucketName)
        ).ConfigureAwait(false);

        var logger = CurrentApplicationContext.GetServices().GetService<ILogger<MinioClient>>();
        logger?.LogInformation($"Created bucket {bucketName}");
    }
}
