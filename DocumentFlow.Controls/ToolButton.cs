//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2018
// Time: 17:34
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Core;

namespace DocumentFlow.Controls
{
    public enum ToolButtonKind { Back, Forward, Up, Down, Refresh, Home, Delete, Select }

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
            get { return backClickedColor; }
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
            Rectangle r = new Rectangle(e.ClipRectangle.Location, e.ClipRectangle.Size.Resize(-1));

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

            Bitmap bitmap = null;
            switch (Kind)
            {
                case ToolButtonKind.Back:
                    bitmap = Properties.Resources.left_16;
                    break;
                case ToolButtonKind.Forward:
                    bitmap = Properties.Resources.right_16;
                    break;
                case ToolButtonKind.Up:
                    bitmap = Properties.Resources.up_16;
                    break;
                case ToolButtonKind.Refresh:
                    bitmap = Properties.Resources.refresh_16;
                    break;
                case ToolButtonKind.Home:
                    bitmap = Properties.Resources.icons8_home_16;
                    break;
                case ToolButtonKind.Delete:
                    bitmap = Properties.Resources.icons8_delete_16;
                    break;
                case ToolButtonKind.Select:
                    bitmap = Properties.Resources.icons8_select_16;
                    break;
                case ToolButtonKind.Down:
                    bitmap = Properties.Resources.down_16;
                    break;
            }

            if (bitmap != null)
            {
                if (!Enabled)
                    ControlPaint.DrawImageDisabled(e.Graphics, bitmap, x, y, BackColor);
                else
                    e.Graphics.DrawImage(bitmap, x, y);
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
}
