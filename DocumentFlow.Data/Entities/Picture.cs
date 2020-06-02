//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.06.2018
// Time: 12:51
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using DocumentFlow.Core;

    public class Picture : Directory
    {
        public virtual string SizeSmall { get; set; }
        public virtual string SizeLarge { get; set; }
        public virtual string ImgName { get; set; }
        public virtual string Note { get; set; }

        public virtual Image GetImageSmall() => ImageHelper.Base64ToImage(SizeSmall);

        public virtual Image GetImageLarge() => ImageHelper.Base64ToImage(SizeLarge);

        public virtual byte[] ImageSmall => GetImage(GetImageSmall());

        byte[] GetImage(Image image)
        {
            if (image == null)
                return null;

            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
