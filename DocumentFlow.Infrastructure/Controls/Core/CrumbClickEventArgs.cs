//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.06.2018
//
// Версия 2023.1.22
//  - перенесено из DocumentFlow.Controls в DocumentFlow.Infrastructure.Controls.Core
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls.Core;

public class CrumbClickEventArgs : EventArgs
{
    public CrumbClickEventArgs(ToolButtonKind buttonKind) => Kind = buttonKind;

    public ToolButtonKind Kind { get; }
}
