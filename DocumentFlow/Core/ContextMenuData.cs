//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.12.2020
// Time: 22:03
//-----------------------------------------------------------------------

using System.Windows.Forms;
using DocumentFlow.Code;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public class ContextMenuData : ToolStripData, IContextMenu
    {
        public ContextMenuData(ToolStrip toolStrip, CommandCollection commandCollection) : base(toolStrip, commandCollection) 
        { 
            if (toolStrip is ContextMenuStrip menu)
            {
                menu.Opened += Menu_Opened; ;
            }
        }

        private void Menu_Opened(object sender, System.EventArgs e)
        {
            UpdateButtonVisibleStatus();
        }

        protected override ToolStripItem CreateToolStripItem(ICommand command, Picture picture)
        {
            return new ToolStripMenuItem
            {
                Text = command.Title,
                Image = picture?.GetImageSmall(),
                Tag = $"{command.Code}|user-defined"
            };
        }

        protected override void UpdateToolStripItem(ICommand command, Picture picture)
        {
            this[command].Image = picture?.GetImageSmall();
        }
    }
}
