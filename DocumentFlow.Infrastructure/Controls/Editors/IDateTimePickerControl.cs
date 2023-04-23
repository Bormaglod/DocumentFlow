//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Infrastructure.Controls;

public interface IDateTimePickerControl : IControl 
{
    DateTime? Value { get; }
    IDateTimePickerControl DateChanged(ControlValueChanged<DateTime?> action);
    IDateTimePickerControl ReadOnly();
    IDateTimePickerControl NotRequired();
    IDateTimePickerControl SetFormat(DateTimePickerFormat format);
    IDateTimePickerControl SetCustomFormat(string format);
}