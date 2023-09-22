//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.08.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls;

public enum LinePlacement { HorizontalTop, HorizontalCenter, HorizontalBottom, VerticalLeft, VerticalCenter, VerticalRight }

public class LineSplitter : Control
{
    private Color lineColor = SystemColors.ControlDark;
    private LinePlacement linePlacement = LinePlacement.HorizontalCenter;
    private int lineSize = 3;
    private Brush? lineBrush = null;

    public Color LineColor
    {
        get => lineColor; 
        set
        {
            if (lineColor != value) 
            { 
                lineColor = value;
                DisposeBrush();
                Invalidate();
            }
        }
    }

    public LinePlacement LinePlacement
    {
        get => linePlacement;
        set
        {
            if (linePlacement != value) 
            { 
                linePlacement = value;
                Invalidate();
            }
        }
    }

    public int LineSize
    {
        get => lineSize;
        set
        {
            if (lineSize != value) 
            { 
                lineSize = value;
                Invalidate();
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        float x = 0;
        float y = 0;
        float width = Width;
        float height = Height;

        switch (linePlacement)
        {
            case LinePlacement.HorizontalCenter:
                y = (Height - LineSize) / 2;
                height = LineSize;
                break;
            case LinePlacement.VerticalCenter:
                x = (Width - LineSize) / 2;
                width = LineSize;
                break;
            case LinePlacement.HorizontalTop:
                height = LineSize;
                break;
            case LinePlacement.HorizontalBottom:
                y = Height - LineSize;
                height = LineSize;
                break;
            case LinePlacement.VerticalLeft:
                width = LineSize;
                break;
            case LinePlacement.VerticalRight:
                x = Width - LineSize;
                width = LineSize;
                break;
        }

        lineBrush ??= new SolidBrush(lineColor);

        e.Graphics.FillRectangle(lineBrush, x, y, width, height);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        DisposeBrush();
    }

    private void DisposeBrush()
    {
        if (lineBrush != null)
        {
            lineBrush.Dispose();
            lineBrush = null;
        }
    }
}
