//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2020
// Time: 10:05
//-----------------------------------------------------------------------

using WeifenLuo.WinFormsUI.Docking;

namespace DocumentFlow
{
    public class ToolWindow : DockContent
    {
        protected ToolWindow()
        {
            HideOnClose = false;
        }

        public void ShowWindow(DockPanel dockPanel, DockState defaultDockState)
        {
            if (DockState == DockState.Hidden)
            {
                Activate();
            }
            else if (DockState == DockState.Unknown)
            {
                Show(dockPanel, defaultDockState);
            }
            else
            {
                Activate();
            }
        }
    }
}
