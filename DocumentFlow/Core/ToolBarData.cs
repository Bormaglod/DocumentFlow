//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.12.2020
// Time: 22:03
//-----------------------------------------------------------------------

using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Code.System;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public class ToolBarData : ToolStripData, IToolBar
    {
        private ToolStripItemDisplayStyle buttonStyle;
        private ButtonIconSize iconSize;

        public ToolBarData(ToolStrip toolStrip, CommandCollection commandCollection) : base(toolStrip, commandCollection) 
        {
            buttonStyle = ToolStripItemDisplayStyle.ImageAndText;
            iconSize = ButtonIconSize.Large;
        }

        ToolStripItemDisplayStyle IToolBar.ButtonStyle
        {
            get => buttonStyle;
            set
            {
                if (buttonStyle != value)
                {
                    buttonStyle = value;
                    foreach (ToolStripItem item in ToolStrip.Items)
                    {
                        item.DisplayStyle = buttonStyle;
                    }
                }
            }
        }

        ButtonIconSize IToolBar.IconSize
        {
            get => iconSize;
            set
            {
                if (iconSize != value)
                {
                    iconSize = value;
                    foreach (var c in GetCommands())
                    {
                        UpdateToolStripItem(c, GetCommandPicture(c));
                    }
                }
            }
        }

        protected override ToolStripItem CreateToolStripItem(ICommand command, Picture picture)
        {
            IToolBar toolBar = this;

            return new ToolStripButton()
            {
                Text = command.Title,
                Image = toolBar.IconSize == ButtonIconSize.Small ? picture?.GetImageSmall() : picture?.GetImageLarge(),
                DisplayStyle = toolBar.ButtonStyle,
                ImageScaling = ToolStripItemImageScaling.None,
                TextImageRelation = TextImageRelation.ImageAboveText,
                Tag = "user-defined"
            };
        }

        protected override void UpdateToolStripItem(ICommand command, Picture picture)
        {
            IToolBar toolBar = this;
            if (this[command] is ToolStripButton button)
            {
                button.Image = toolBar.IconSize == ButtonIconSize.Small ? picture?.GetImageSmall() : picture?.GetImageLarge();
            }
        }
    }
}
