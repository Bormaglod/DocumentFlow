//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls;

/// <summary>
/// https://stackoverflow.com/questions/6953487/hide-tab-header-on-c-sharp-tabcontrol
/// </summary>
public class TablessControl : TabControl
{
    protected override void WndProc(ref Message m)
    {
        // Hide tabs by trapping the TCM_ADJUSTRECT message
        if (m.Msg == 0x1328 && !DesignMode)
        {
            m.Result = (IntPtr)1;
        }
        else
        {
            base.WndProc(ref m);
        }
    }
}
