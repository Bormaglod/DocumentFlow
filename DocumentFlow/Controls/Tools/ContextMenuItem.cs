//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.07.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Controls.Tools;

public class ContextMenuItem : IContextMenuItem
{
    private readonly ToolStripItem toolStripItem;
    private bool visible = true;

    public event EventHandler? ItemVisibleChanged;

    public ContextMenuItem(ToolStripItem toolStripItem)
    {
        this.toolStripItem = toolStripItem;
    }

    public ToolStripItem ToolStripItem => toolStripItem;

    public bool Visible
    {
        get => visible;
        set
        {
            if (visible != value)
            {
                visible = value;
                toolStripItem.Visible = visible;
                ItemVisibleChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
