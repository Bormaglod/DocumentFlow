//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.06.2018
// Time: 12:51
//-----------------------------------------------------------------------

using System.Drawing;
using System.Drawing.Imaging;
using DocumentFlow.Core;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class Picture : Directory
    {
        public string size_small { get; set; }
        public string size_large { get; set; }
        public string img_name { get; set; }
        public string note { get; set; }

        public Image GetImageSmall() => ImageHelper.Base64ToImage(size_small);

        public Image GetImageLarge() => ImageHelper.Base64ToImage(size_large);

        public byte[] ImageSmall => GetImage(GetImageSmall());

        byte[] GetImage(Image image)
        {
            if (image == null)
                return null;

            var ms = new System.IO.MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
