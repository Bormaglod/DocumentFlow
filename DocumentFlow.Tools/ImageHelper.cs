//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.08.2019
//-----------------------------------------------------------------------

using System.Drawing.Imaging;

namespace DocumentFlow.Tools;

public static class ImageHelper
{
    public static Image Base64ToImage(string base64String)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);

        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new(imageBytes, 0, imageBytes.Length);
        ms.Write(imageBytes, 0, imageBytes.Length);
        Image image = Image.FromStream(ms, true);
        return image;
    }

    public static string ImageToBase64(Image image, ImageFormat imageFormat)
    {
        using var m = new MemoryStream();
        image.Save(m, imageFormat);
        byte[] imageBytes = m.ToArray();
        string base64String = Convert.ToBase64String(imageBytes);
        return base64String;
    }
}
