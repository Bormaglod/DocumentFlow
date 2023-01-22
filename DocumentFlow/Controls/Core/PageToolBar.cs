//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Core;

internal class PageToolBar : IToolBar
{
    private readonly ToolStrip toolStrip;
    private bool isFirst = true;
    private ButtonIconSize size = ButtonIconSize.Large;
    private readonly Dictionary<ToolStripItem, (Image, Image)> buttons;

    public PageToolBar(ToolStrip tools, Dictionary<ToolStripItem, (Image, Image)> standardButtons) => (toolStrip, buttons) = (tools, standardButtons);

    public ButtonIconSize IconSize
    {
        get => size;

        set
        {
            if (size == value)
            {
                return;
            }

            size = value;
            foreach (var item in toolStrip.Items.OfType<ToolStripItem>())
            {
                if (buttons.ContainsKey(item))
                {
                    var (Image16, Image30) = buttons[item];

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

    public ToolStripButton Add(string text, Image image16, Image image32, Action action)
    {
        if (isFirst)
        {
            AddSeparator();
            isFirst = false;
        }

        Image image = IconSize == ButtonIconSize.Large ? image32 : image16;
        ToolStripItemDisplayStyle style = IconSize == ButtonIconSize.Large ? ToolStripItemDisplayStyle.ImageAndText : ToolStripItemDisplayStyle.Image;
        ToolStripButton button = new(text, image, (s, e) => action())
        {
            ImageScaling = ToolStripItemImageScaling.None,
            TextImageRelation = TextImageRelation.ImageAboveText,
            DisplayStyle = style
        };

        buttons.Add(button, (image16, image32));
        toolStrip.Items.Add(button);

        return button;
    }

    public void AddSeparator() => toolStrip.Items.Add(new ToolStripSeparator());
}
