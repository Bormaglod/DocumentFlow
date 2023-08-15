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
// Версия 2023.2.7
//  - исправлена ошибка в CreateThumbnailImage (thumbnail всегда 
//    присваивалось null)
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
// Версия 2023.7.23
//  - изображение для предпросмотра сохраняется в БД с учётом
//    его пропорций
//
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.IO;

namespace DocumentFlow.Data;

public class DocumentRefs : Entity<long>, IDocumentRefs
{

    [Display(Name = "Имя файла", Order = 2)]
    public string? FileName { get; set; }

    [Display(Name = "Описание", Order = 1)]
    public string? Note { get; set; }

    [Display(Name = "Размер", Order = 3)]
    public long FileLength { get; set; }

    [Display(AutoGenerateField = false)]
    public string? Thumbnail { get; set; }

    [Display(Name = "Галлерея", Order = 4)]
    public bool ThumbnailExist => !string.IsNullOrEmpty(Thumbnail);

    [Display(AutoGenerateField = false)]
    public byte[]? FileContent { get; set; }

    public void CreateThumbnailImage()
    {
        string[] images = { ".jpg", ".jpeg", ".png", ".bmp" };
        if (images.Contains(Path.GetExtension(FileName)) && FileContent != null)
        {
            using MemoryStream stream = new(FileContent);
            Image image = Image.FromStream(stream);

            int width = 120;
            int height = 120;
            if (image.Width > image.Height)
            {
                height = 120 * image.Height / image.Width;
            }
            else if (image.Height > image.Width)
            {
                width = 120 * image.Width / image.Height;
            }

            Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
            Thumbnail = ImageHelper.ImageToBase64(thumb, ImageFormat.Bmp);
        }
        else
        {
            Thumbnail = null;
        }
    }
}
