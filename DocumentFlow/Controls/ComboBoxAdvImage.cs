//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2018
//-----------------------------------------------------------------------

using Syncfusion.Windows.Forms.Tools;

using System.ComponentModel;

namespace DocumentFlow.Controls;

public class ComboBoxAdvImage : ComboBoxAdv
{
    private Image? imageNear;

    [Description("Near image")]
    [DefaultValue(null)]
    public Image? NearImage
    {
        get => imageNear;
        set
        {
            if (imageNear != value)
            {
                imageNear = value;
                MinimumSize = Size.Empty;
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (imageNear != null)
        {
            int y = TextBox.Bounds.Y + (TextBox.Bounds.Height - imageNear.Height) / 2;
            int h = imageNear.Height;
            int w = imageNear.Width;
            Rectangle rect = (!IsMirrored) ? new Rectangle(TextBox.Bounds.X - TextBox.Bounds.Height - 2, y, w, h) : new Rectangle(TextBox.Bounds.X + TextBox.Bounds.Width + 2, y, w, h);
            if (Enabled)
            {
                e.Graphics.DrawImage(imageNear, rect);
            }
            else
            {
                ControlPaint.DrawImageDisabled(e.Graphics, new Bitmap(imageNear, rect.Size), rect.X, rect.Y, BackColor);
            }
        }
    }

    protected override void SetTextBoxBounds(Rectangle rect)
    {
        Rectangle textBoxBounds;
        if (imageNear != null)
        {
            int num2 = rect.Height + 2;
            textBoxBounds = ((!IsMirrored) ? new Rectangle(rect.X + num2, rect.Y, rect.Width - num2, rect.Height) : new Rectangle(rect.X, rect.Y, rect.Width - num2, rect.Height));
        }
        else
        {
            textBoxBounds = rect;
        }

        base.SetTextBoxBounds(textBoxBounds);
    }
}
