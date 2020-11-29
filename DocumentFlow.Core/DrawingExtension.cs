//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2018
// Time: 13:04
//-----------------------------------------------------------------------

using System.Drawing;

namespace DocumentFlow.Core
{
    public static class DrawingExtension
    {
        static readonly Pen pen = new Pen(Color.Bisque);
        static readonly SolidBrush brush = new SolidBrush(Color.Bisque);

        public static Font Font = new Font(FontFamily.GenericSansSerif, 10);

        public static Point Add(this Point point, Point other)
        {
            return new Point(point.X + other.X, point.Y + other.Y);
        }

        public static Point Sub(this Point point, Point other)
        {
            return new Point(point.X - other.X, point.Y - other.Y);
        }

        public static Point OffsetX(this Point point, int dx)
        {
            return new Point(point.X + dx, point.Y);
        }

        public static Color Opaque(this Color color, float opaque)
        {
            return Color.FromArgb((opaque * color.A / 255).To255(), color);
        }

        public static Size Resize(this Size size, int delta)
        {
            return new Size(size.Width + delta, size.Height + delta);
        }

        public static byte To255(this float v)
        {
            if (v > 255) return 255;
            if (v < 0) return 0;
            return (byte)v;
        }

        public static Pen Pen(this Color color, float width = 1f)
        {
            pen.Color = color;
            pen.Width = width;
            return pen;
        }

        public static SolidBrush Brush(this Color color)
        {
            brush.Color = color;
            return brush;
        }
    }
}
