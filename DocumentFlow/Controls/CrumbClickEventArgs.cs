//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2018
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls;

public class CrumbClickEventArgs : EventArgs
{
    public CrumbClickEventArgs(ToolButtonKind buttonKind) => Kind = buttonKind;

    public ToolButtonKind Kind { get; }
}
