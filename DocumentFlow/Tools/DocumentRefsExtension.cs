//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.08.2019
//-----------------------------------------------------------------------

using DocumentFlow.Data;

using System.Drawing.Imaging;
using System.IO;

namespace DocumentFlow.Tools;

public static class DocumentRefsExtension
{
    public static void CreateThumbnailImage(this DocumentRefs document, int imageSize)
    {
        string[] images = { ".jpg", ".jpeg", ".png", ".bmp" };
        if (images.Contains(Path.GetExtension(document.FileName)) && document.FileContent != null)
        {
            using MemoryStream stream = new(document.FileContent);
            Image image = Image.FromStream(stream);

            int width = imageSize;
            int height = imageSize;
            if (image.Width > image.Height)
            {
                height = imageSize * image.Height / image.Width;
            }
            else if (image.Height > image.Width) 
            {
                width = imageSize * image.Width / image.Height;
            }

            Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);
            document.Thumbnail = ImageHelper.ImageToBase64(thumb, ImageFormat.Bmp);
        }
        else
        {
            document.Thumbnail = null;
        }
    }
}
