//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using DocumentFlow.Core;

namespace DocumentFlow.Reports
{
    public class Band
    {
        private float originalHeight;
        private float height;

        public ReportPage Page { get; set; }

        public string Name { get; set; }

        public float Height 
        {
            get => height;
            set
            {
                height = value;
                originalHeight = value;
            }
        }

        public ReportFont Font { get; set; } = new ReportFont();

        public List<TextObject> TextObjects { get; set; } = new List<TextObject>();

        public bool CanGrow { get; set; } = false;

        public bool CanShrink { get; set; } = false;

        public RectangleF Bounds => new RectangleF(0, Length.FromMillimeter(Page.CurrentBandTop).ToPoint(), Page.PageSize.Size.Width, Length.FromMillimeter(Height).ToPoint());

        public void GeneratePdf(PdfPage page)
        {
            height = originalHeight;
            foreach (TextObject text in TextObjects)
            {
                text.CalculateBounds();
            }

            if (CanGrow || CanShrink)
            {
                float max = TextObjects.Select(x => x.Height).Max();
                if ((max > Height && CanGrow) || (max < Height && CanShrink))
                {
                    height = max;
                }
            }

            foreach (TextObject text in TextObjects)
            {
                text.GeneratePdf(page);
            }
        }

        public virtual object Get(string source, string field) => Page.Report.Storage.Get(source, field);
    }
}
