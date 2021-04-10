//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.08.2019
// Time: 19:55
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;

namespace DocumentFlow.Core
{
    public static class ImageHelper
    {
        public static Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public static string ImageToBase64(string path)
        {
            using var image = Image.FromFile(path);
            using var m = new MemoryStream();
            image.Save(m, image.RawFormat);
            byte[] imageBytes = m.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
