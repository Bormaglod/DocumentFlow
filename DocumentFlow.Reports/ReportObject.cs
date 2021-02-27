//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using DocumentFlow.Core;

namespace DocumentFlow.Reports
{
    public class ReportObject
    {
        public Band Band { get; set; }

        public string Name { get; set; }

        public float Left { get; set; } = 0;

        public float Top { get; set; } = 0;

        public float Width { get; set; } = 50;

        public float Height { get; set; } = 5;

        public Borders Borders { get; set; } = new Borders();

        public Fill Fill { get; set; } = new Fill();
        
        public RectangleF Bounds => new RectangleF(
            Length.FromMillimeter(Left).ToPoint(),
            Length.FromMillimeter(Top + (Band?.Page?.CurrentBandTop ?? 0)).ToPoint(),
            Length.FromMillimeter(Width).ToPoint(),
            Length.FromMillimeter(Height).ToPoint());

        public PointF TopLeft => new PointF(
            Length.FromMillimeter(Left).ToPoint(),
            Length.FromMillimeter(Top + (Band?.Page?.CurrentBandTop ?? 0)).ToPoint());

        public PointF TopRight => new PointF(
            Length.FromMillimeter(Left + Width).ToPoint(),
            Length.FromMillimeter(Top + (Band?.Page?.CurrentBandTop ?? 0)).ToPoint());

        public PointF BottomLeft => new PointF(
            Length.FromMillimeter(Left).ToPoint(),
            Length.FromMillimeter(Top + (Band?.Page?.CurrentBandTop ?? 0) + Height).ToPoint());

        public PointF BottomRight => new PointF(
            Length.FromMillimeter(Left + Width).ToPoint(),
            Length.FromMillimeter(Top + (Band?.Page?.CurrentBandTop ?? 0) + Height).ToPoint());

        public virtual void GeneratePdf(PdfPage page)
        {
            PdfGraphics g = page.Graphics;
            PdfPen pen = new PdfPen(Borders.Color, Borders.Width);
            PdfBrush brush = new PdfSolidBrush(Fill.Color);
            if (Borders.All)
            {
                g.DrawRectangle(pen, brush, Bounds);
            }
            else
            {
                g.DrawRectangle(brush, Bounds);
                if (Borders.Left)
                {
                    g.DrawLine(pen, TopLeft, BottomLeft);
                }

                if (Borders.Top)
                {
                    g.DrawLine(pen, TopLeft, TopRight);
                }

                if (Borders.Right)
                {
                    g.DrawLine(pen, TopRight, BottomRight);
                }

                if (Borders.Bottom)
                {
                    g.DrawLine(pen, BottomLeft, BottomRight);
                }
            }
        }
    }
}
