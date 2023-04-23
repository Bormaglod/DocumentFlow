﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Infrastructure.Controls;

public interface IBaseNumericTextBoxControl<T> : IControl
    where T : struct, IComparable<T>
{
    T? NumericValue { get; set; }
    IBaseNumericTextBoxControl<T> ReadOnly();
    IBaseNumericTextBoxControl<T> Initial(T value);
    IBaseNumericTextBoxControl<T> ShowSuffix(string suffix);
    IBaseNumericTextBoxControl<T> HideSuffix();
    IBaseNumericTextBoxControl<T> ValueChanged(ControlValueChanged<T> action);
    IBaseNumericTextBoxControl<T> ValueCleared(ControlValueCleared action);
}