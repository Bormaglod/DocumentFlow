//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Infrastructure.Controls;

public enum CalculationState { Prepare, Approved, Expired }

public interface IStateControl : IControl 
{
    CalculationState Current { get; }
    IStateControl StateChanged(ControlValueChanged<CalculationState> action);
}
