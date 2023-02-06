//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.08.2019
//
// Версия 2023.1.24
//  - добавлено наследование от IDocumentRefs
// Версия 2023.2.6
//  - добавлен метод CreateThumbnailImage
//
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Infrastructure.Data;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DocumentFlow.Data.Core;

public class DocumentRefs : Entity<long>, IDocumentRefs
{

    [Display(Name = "Имя файла", Order = 2)]
    public string? file_name { get; set; }

    [Display(Name = "Описание", Order = 1)]
    public string? note { get; set; }

    [Display(Name = "Размер", Order = 3)]
    public long file_length { get; set; }

    [Display(AutoGenerateField = false)]
    public string? thumbnail { get; set; }

    [Display(Name = "Галлерея", Order = 4)]
    public bool thumbnail_exist => !string.IsNullOrEmpty(thumbnail);

    [Display(AutoGenerateField = false)]
    public byte[]? file_content { get; set; }

    public void CreateThumbnailImage()
    {
        string[] images = { ".jpg", ".jpeg", ".png", ".bmp" };
        if (images.Contains(Path.GetExtension(file_name)) && file_content != null)
        {
            using MemoryStream stream = new(file_content);
            Image image = Image.FromStream(stream);
            Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            thumbnail = ImageHelper.ImageToBase64(thumb, ImageFormat.Bmp);
        }

        thumbnail = null;
    }
}
