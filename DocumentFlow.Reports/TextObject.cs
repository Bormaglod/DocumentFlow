//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Syncfusion.Pdf.Graphics;
using DocumentFlow.Core;

namespace DocumentFlow.Reports
{

    public class TextObject : ReportObject
    {
        public string Text { get; set; }
        public ReportFont Font { get; set; }
        public PdfTextAlignment HorizontalAlignment { get; set; } = PdfTextAlignment.Left;
        public PdfVerticalAlignment VerticalAlignment { get; set; } = PdfVerticalAlignment.Top;
        public Padding Padding { get; set; } = new Padding();
        public string ColorName { get; set; } = "Black";

        public static string Parse(string text, Func<string, string, string> getValue)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            string out_str = text;
            string pattern = @".*?(\[(.+?)\.(.+?)\])";

            foreach (Match m in Regex.Matches(text, pattern))
            {
                string data = m.Groups[1].Value;
                string source = m.Groups[2].Value;
                string field = m.Groups[3].Value;

                out_str = out_str.Replace(data, getValue(source, field));
            }

            return out_str;
        }

        public override void GeneratePdf(PdfGraphics g)
        {
            base.GeneratePdf(g);

            string out_str = Parse(Text, (source, field) => Band.Get(source, field).ToString());

            PdfFont font = new PdfTrueTypeFont((Font ?? Band.Font).Instance, true);
            PdfBrush brush = new PdfSolidBrush(Color.FromName(ColorName));

            RectangleF rect = Bounds;
            rect.Offset(
                Length.FromMillimeter(Padding.Left).ToPoint(), 
                Length.FromMillimeter(Padding.Top).ToPoint());
            rect.Size = new SizeF(
                rect.Width - Length.FromMillimeter(Padding.Width).ToPoint(), 
                rect.Height - Length.FromMillimeter(Padding.Height).ToPoint());

            PdfStringFormat stringFormat = new PdfStringFormat
            {
                Alignment = HorizontalAlignment,
                LineAlignment = VerticalAlignment
            };

            g.DrawString(out_str, font, brush, rect, stringFormat);
        }
    }
}
