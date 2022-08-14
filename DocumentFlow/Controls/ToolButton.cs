//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2018
//-----------------------------------------------------------------------

using DocumentFlow.Core;
using DocumentFlow.Properties;

namespace DocumentFlow.Controls;

public enum ToolButtonKind { Back, Forward, Up, Down, Refresh, Home, Delete, Select, Edit, Open }

public class ToolButton : Control
{
    private bool clicked = false;
    private bool hovered = false;
    private Color borderColor = Color.FromArgb(217, 217, 217);
    private Color borderHoveredColor = Color.FromArgb(204, 232, 235);
    private Color borderClickedColor = Color.FromArgb(153, 209, 255);
    private Color backHoveredColor = Color.FromArgb(229, 243, 255);
    private Color backClickedColor = Color.FromArgb(204, 232, 255);
    private ToolButtonKind kind = ToolButtonKind.Back;

    public ToolButton()
    {
        ResizeRedraw = DoubleBuffered = true;
        BackColor = Color.White;
        TabStop = false;
        ResizeRedraw = true;
    }

    public Color BorderColor
    {
        get => borderColor;
        set
        {
            borderColor = value;
            Invalidate();
        }
    }

    public Color BorderHoveredColor
    {
        get => borderHoveredColor;
        set
        {
            borderHoveredColor = value;
            Invalidate();
        }
    }

    public Color BorderClickedColor
    {
        get => borderClickedColor;
        set
        {
            borderClickedColor = value;
            Invalidate();
        }
    }

    public Color BackHoveredColor
    {
        get => backHoveredColor;
        set
        {
            backHoveredColor = value;
            Invalidate();
        }
    }

    public Color BackClickedColor
    {
        get => backClickedColor;
        set
        {
            backClickedColor = value;
            Invalidate();
        }
    }

    public ToolButtonKind Kind
    {
        get => kind;
        set
        {
            kind = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Rectangle r = new(ClientRectangle.Location, ClientRectangle.Size.Resize(-1));

        if (Enabled)
        {
            if (clicked)
            {
                e.Graphics.FillRectangle(BackClickedColor.Brush(), r);
                e.Graphics.DrawRectangle(BorderClickedColor.Pen(), r);
            }
            else if (hovered)
            {
                e.Graphics.FillRectangle(BackHoveredColor.Brush(), r);
                e.Graphics.DrawRectangle(BorderHoveredColor.Pen(), r);
            }
            else
            {
                e.Graphics.FillRectangle(BackColor.Brush(), r);
                e.Graphics.DrawRectangle(BorderColor.Pen(), r);
            }
        }

        int x = (Width - 18) / 2 + 1;
        int y = (Height - 18) / 2 + 1;

        Bitmap? bitmap = kind switch
        {
            ToolButtonKind.Back => Resources.left_16,
            ToolButtonKind.Forward => Resources.right_16,
            ToolButtonKind.Up => Resources.up_16,
            ToolButtonKind.Refresh => Resources.refresh_16,
            ToolButtonKind.Home => Resources.icons8_home_16,
            ToolButtonKind.Delete => Resources.icons8_delete_16,
            ToolButtonKind.Select => Resources.icons8_select_16,
            ToolButtonKind.Down => Resources.down_16,
            ToolButtonKind.Edit => Resources.pencil_16,
            ToolButtonKind.Open => Resources.icons8_open_in_window_16,
            _ => null
        };

        if (bitmap != null)
        {
            if (Enabled)
            {
                e.Graphics.DrawImage(bitmap, x, y);
            }
            else
            {
                ControlPaint.DrawImageDisabled(e.Graphics, bitmap, x, y, BackColor);
            }
        }
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        base.OnEnabledChanged(e);
        Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        hovered = true;
        Refresh();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        hovered = false;
        clicked = false;
        Refresh();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            clicked = true;
        }

        Refresh();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        clicked = false;
        Refresh();
    }
}
