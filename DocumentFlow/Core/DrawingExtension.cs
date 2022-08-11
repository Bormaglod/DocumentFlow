//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2018
//-----------------------------------------------------------------------

namespace DocumentFlow.Core;

public static class DrawingExtension
{
    static readonly Pen pen = new(Color.Bisque);
    static readonly SolidBrush brush = new(Color.Bisque);

    public static Point Add(this Point point, Point other) => new(point.X + other.X, point.Y + other.Y);

    public static Point Sub(this Point point, Point other) => new(point.X - other.X, point.Y - other.Y);

    public static Point OffsetX(this Point point, int dx) => new(point.X + dx, point.Y);

    public static Color Opaque(this Color color, float opaque) => Color.FromArgb((opaque * color.A / 255).To255(), color);

    public static Size Resize(this Size size, int delta) => new(size.Width + delta, size.Height + delta);

    public static byte To255(this float v) => v switch
    {
        > 255 => 255,
        < 0 => 0,
        _ => (byte)v
    };

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
