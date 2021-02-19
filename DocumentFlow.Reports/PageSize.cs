//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Drawing;
using Syncfusion.Pdf;
using DocumentFlow.Core;

namespace DocumentFlow.Reports
{
    public enum PageSizeName { A0, A1, A2, A3, A4, A5 }
    public class PageSize
    {
        private PageSizeName name = PageSizeName.A4;

        public float Width { get; set; } = 210;
        public float Height { get; set; } = 297;
        public PdfPageOrientation Orientation { get; set; } = PdfPageOrientation.Portrait;

        public PageSizeName Name
        {
            get => name;

            set
            {
                name = value;
                switch (name)
                {
                    case PageSizeName.A0:
                        (Width, Height) = (841, 1189);
                        break;
                    case PageSizeName.A1:
                        (Width, Height) = (594, 841);
                        break;
                    case PageSizeName.A2:
                        (Width, Height) = (420, 594);
                        break;
                    case PageSizeName.A3:
                        (Width, Height) = (297, 420);
                        break;
                    case PageSizeName.A4:
                        (Width, Height) = (210, 297);
                        break;
                    case PageSizeName.A5:
                        (Width, Height) = (148, 210);
                        break;
                    default:
                        break;
                }
            }
        }
        public SizeF Size => new SizeF(Length.FromMillimeter(Width).ToPoint(), Length.FromMillimeter(Height).ToPoint());
    }
}
