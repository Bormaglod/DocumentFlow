//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.06.2018
// Time: 11:02
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DocumentFlow.Core;

namespace DocumentFlow.Controls
{
    public class SplitButton : Control
    {
        private bool clicked = false;
        private bool hovered = false;
        private Color backHoveredColor = Color.FromArgb(229, 243, 255);
        private Color backClickedColor = Color.FromArgb(204, 232, 255);
        private Color borderColor = Color.FromArgb(217, 217, 217);
        private Color borderHoveredColor = Color.FromArgb(204, 232, 235);
        private Color borderClickedColor = Color.FromArgb(153, 209, 255);
        private readonly int splitWidth = 16;
        private readonly int borderWidth = 1;
        private readonly ContextMenuStrip buttonDropDown;
        private Guid identifier;

        public SplitButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DropDownItems = new(this);
            BackColor = Color.White;
            buttonDropDown = new();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SplitButtonItemsCollection DropDownItems { get; }

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

        public Guid Identifier
        {
            get => identifier;
            set => identifier = value;
       }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            ResizeWidth();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            ResizeWidth();
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
                if (e.Location.X > Width - splitWidth)
                {
                    ShowDropDown();
                }
            }

            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            clicked = false;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Rectangle r = new(e.ClipRectangle.Location, e.ClipRectangle.Size.Resize(-1));

            if (Enabled)
            {
                if (clicked)
                {
                    e.Graphics.FillRectangle(BackClickedColor.Brush(), r);
                    DrawBorder(e.Graphics, BorderClickedColor, r);
                }
                else if (hovered)
                {
                    e.Graphics.FillRectangle(BackHoveredColor.Brush(), r);
                    DrawBorder(e.Graphics, BorderHoveredColor, r);
                }
                else
                {
                    e.Graphics.FillRectangle(BackColor.Brush(), r);
                    DrawBorder(e.Graphics, BorderColor, r);
                }
            }
            else
            {
                e.Graphics.FillRectangle(BackColor.Brush(), r);
            }

            DrawArrow(e.Graphics, r);

            StringFormat stringFormat = new()
            {
                Trimming = StringTrimming.EllipsisCharacter,
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            r = new(1, 1, Width - splitWidth - borderWidth * 2, Height - borderWidth * 2);
            e.Graphics.DrawString(Text, Font, ForeColor.Brush(), r, stringFormat);
        }

        private void DrawBorder(Graphics graphics, Color color, Rectangle rectangle)
        {
            graphics.DrawRectangle(color.Pen(borderWidth), rectangle);

            int x = rectangle.Width - splitWidth - 1;
            graphics.DrawLine(color.Pen(borderWidth), x, 0, x, rectangle.Height);
        }

        private void DrawArrow(Graphics graphics, Rectangle rectangle)
        {
            Bitmap bitmap = clicked ? Properties.Resources.split_down : Properties.Resources.split_right;

            int x = rectangle.Width - splitWidth + (splitWidth - bitmap.Width) / 2 + 1;
            int y = (rectangle.Height - bitmap.Height) / 2 + 1;

            if (Enabled)
                graphics.DrawImage(bitmap, x, y);
            else
                ControlPaint.DrawImageDisabled(graphics, bitmap, x, y, BackColor);
        }

        private void ResizeWidth()
        {
            Graphics graphics = CreateGraphics();
            int length = Convert.ToInt32(graphics.MeasureString(Text, Font).Width);
            Width = borderWidth * 2 + 5 + length + splitWidth;
        }

        private void ShowDropDown()
        {
            buttonDropDown.Items.Clear();
            int num = 0;
            foreach (ToolSplitItem dropDownItem in DropDownItems)
            {
                if (dropDownItem.Image != null)
                {
                    buttonDropDown.Items.Add(dropDownItem.Text, dropDownItem.Image);
                    buttonDropDown.Items[num].ImageScaling = dropDownItem.ImageScaling;
                    buttonDropDown.Items[num].ImageTransparentColor = dropDownItem.ImageTransparentColor;
                }
                else
                {
                    buttonDropDown.Items.Add(dropDownItem.Text);
                }

                buttonDropDown.Items[num].Enabled = dropDownItem.Enabled;
                buttonDropDown.Items[num].BackColor = dropDownItem.BackColor;
                buttonDropDown.Items[num].BackgroundImage = dropDownItem.BackgroundImage;
                buttonDropDown.Items[num].BackgroundImageLayout = dropDownItem.BackgroundImageLayout;
                buttonDropDown.Items[num].DisplayStyle = dropDownItem.DisplayStyle;
                buttonDropDown.Items[num].Font = dropDownItem.Font;
                buttonDropDown.Items[num].ForeColor = dropDownItem.ForeColor;
                buttonDropDown.Items[num].RightToLeft = dropDownItem.RightToLeft;
                buttonDropDown.Items[num].TextImageRelation = dropDownItem.TextImageRelation;
                buttonDropDown.Items[num].TextDirection = dropDownItem.TextDirection;
                buttonDropDown.Items[num].TextAlign = dropDownItem.TextAlign;
                buttonDropDown.Items[num].AutoToolTip = dropDownItem.AutoToolTip;
                buttonDropDown.Items[num].ToolTipText = dropDownItem.ToolTipText;
                buttonDropDown.Items[num].Margin = dropDownItem.Margin;
                num++;
            }

            buttonDropDown.Show(this, new Point(0, Height));
        }
    }
}
