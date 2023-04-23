//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Infrastructure.Controls;

public interface IToggleButtonControl : IControl 
{
    bool ToggleValue { get; }
    IToggleButtonControl ToggleChanged(ControlValueChanged<bool> action);
}
