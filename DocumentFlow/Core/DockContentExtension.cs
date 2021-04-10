//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.03.2021
// Time: 23:48
//-----------------------------------------------------------------------

using WeifenLuo.WinFormsUI.Docking;

namespace DocumentFlow.Core
{
    public static class DockContentExtension
    {
        public static void ShowPanel(this DockContent content, DockPanel dockPanel, DockState defaultDockState)
        {
            if (content.DockState == DockState.Hidden)
            {
                content.Activate();
            }
            else if (content.DockState == DockState.Unknown)
            {
                content.Show(dockPanel, defaultDockState);
            }
            else
            {
                content.Activate();
            }
        }
    }
}
