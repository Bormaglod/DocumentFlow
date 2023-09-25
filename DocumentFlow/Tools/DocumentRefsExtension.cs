//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.08.2019
//-----------------------------------------------------------------------

using DocumentFlow.Ghostscript;
using System.Drawing.Imaging;
using System.IO;

namespace DocumentFlow.Tools;

public static class DocumentRefsExtension
{
    public static void CreateThumbnailImage(this Data.DocumentRefs document, string fileName, int imageSize)
    {
        string ext = Path.GetExtension(fileName);

        string[] images = { ".jpg", ".jpeg", ".png", ".bmp" };
        if (images.Contains(ext))
        {
            InternalCreateThumbnailImage(document, fileName, imageSize);
        }
        else if (ext == ".pdf")
        {
            var path = FileHelper.PrepareTempPath("Thumbnails");
            var name = Path.Combine(path, Path.GetFileName(fileName));

            GhostscriptWrapper.GeneratePageThumb(fileName, name, 1, 200, 200);
            InternalCreateThumbnailImage(document, name, imageSize);
        }
        else
        {
            document.Thumbnail = null;
        }
    }

    private static void InternalCreateThumbnailImage(Data.DocumentRefs document, string fileName, int imageSize)
    {
        using FileStream stream = new(fileName, FileMode.Open, FileAccess.Read);
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
}
