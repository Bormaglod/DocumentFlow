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
                (Width, Height) = name switch
                {
                    PageSizeName.A0 => (841, 1189),
                    PageSizeName.A1 => (594, 841),
                    PageSizeName.A2 => (420, 594),
                    PageSizeName.A3 => (297, 420),
                    PageSizeName.A4 => (210, 297),
                    PageSizeName.A5 => (148, 210),
                    _ => throw new System.NotImplementedException()
                };
            }
        }
        public SizeF Size => new(Length.FromMillimeter(Width).ToPoint(), Length.FromMillimeter(Height).ToPoint());
    }
}
