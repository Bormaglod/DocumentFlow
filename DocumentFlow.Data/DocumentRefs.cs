//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

using Humanizer;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data;

public class DocumentRefs : Entity<long>
{

    [Display(Name = "Имя файла", Order = 2)]
    public string FileName { get; set; } = string.Empty;

    [Display(Name = "Описание", Order = 1)]
    public string? Note { get; set; }

    [Display(Name = "Размер", Order = 3)]
    public long FileLength { get; set; }

    [Display(AutoGenerateField = false)]
    public string? Thumbnail { get; set; }

    [Display(Name = "Галлерея", Order = 4)]
    public bool ThumbnailExist => !string.IsNullOrEmpty(Thumbnail);

    [Display(AutoGenerateField = false)]
    public string? S3object { get; set; }

    public static string GetBucketForEntity(IIdentifier<Guid> entity)
    {
        if (entity is IBucketInfo bucket)
        {
            return bucket.BucketName;
        }

        return entity.GetType().Name.Underscore().Replace('_', '-');
    }
}
