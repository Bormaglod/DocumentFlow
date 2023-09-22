//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Tools;

public static class ControlExtension
{
    public static IEnumerable<Control> Childs(this Control control)
    {
        if (control.GetType().Name.StartsWith("Df"))
        {
            return Array.Empty<Control>();
        }

        var controls = control.Controls.Cast<Control>().ToArray();
        return controls
            .SelectMany(Childs)
            .Concat(controls);
    }
}
