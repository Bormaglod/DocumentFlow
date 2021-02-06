//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Drawing;
using Syncfusion.Pdf.Graphics;
using DocumentFlow.Core;

namespace DocumentFlow.Reports
{
    public class Band
    {
        public ReportPage Page { get; set; }

        public string Name { get; set; }

        public float Height { get; set; } = 1;

        public ReportFont Font { get; set; } = new ReportFont();

        public List<TextObject> TextObjects { get; set; } = new List<TextObject>();

        public RectangleF Bounds => new RectangleF(0, Length.FromMillimeter(Page.CurrentBandTop).ToPoint(), Page.PageSize.Size.Width, Length.FromMillimeter(Height).ToPoint());

        public void GeneratePdf(PdfGraphics g)
        {
            foreach (TextObject text in TextObjects)
            {
                text.GeneratePdf(g);
            }
        }

        public virtual object Get(string source, string field) => Page.Report.Storage.Get(source, field);
    }
}
