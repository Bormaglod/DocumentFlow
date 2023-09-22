//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Controls.Tools;

public class PageToolBar : IToolBar
{
    private readonly ToolStrip toolStrip;
    private ButtonIconSize size = ButtonIconSize.Large;
    private bool separateUserButtons = true;

    public PageToolBar(ToolStrip toolStrip) => this.toolStrip = toolStrip;

    public Dictionary<ToolStripItem, (Image, Image)> Buttons { get; set; } = new();

    public IToolBar LargeIcons()
    {
        SetIconSize(ButtonIconSize.Large);
        return this;
    }

    public IToolBar SmallIcons()
    {
        SetIconSize(ButtonIconSize.Small);
        return this;
    }

    public IToolBar DoNotSeparateUserButtons()
    {
        separateUserButtons = false;
        return this;
    }

    public IToolBar Add(string text, Image image16, Image image32, Action action)
    {
        if (separateUserButtons && !toolStrip.Items.OfType<ToolStripItem>().Any(x => x.Tag?.ToString() == "user defined"))
        {
            AddSeparator();
        }

        Image image = size == ButtonIconSize.Large ? image32 : image16;
        ToolStripItemDisplayStyle style = size == ButtonIconSize.Large ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image;
        ToolStripButton button = new(text, image, (s, e) => action())
        {
            ImageScaling = ToolStripItemImageScaling.None,
            TextImageRelation = TextImageRelation.ImageAboveText,
            DisplayStyle = style,
            Tag = "user defined"
        };

        Buttons.Add(button, (image16, image32));
        toolStrip.Items.Add(button);

        return this;
    }

    public IToolBar AddSeparator()
    {
        toolStrip.Items.Add(new ToolStripSeparator());
        return this;
    }

    private void SetIconSize(ButtonIconSize newSize)
    {
        if (size == newSize)
        {
            return;
        }

        size = newSize;
        foreach (var item in toolStrip.Items.OfType<ToolStripItem>())
        {
            if (Buttons.ContainsKey(item))
            {
                var (Image16, Image30) = Buttons[item];

                if (size == ButtonIconSize.Large)
                {
                    item.Image = Image30;
                    item.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                }
                else
                {
                    item.Image = Image16;
                    item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                }
            }
        }
    }
}
