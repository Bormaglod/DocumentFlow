//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 19.03.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Core;

public class SelectedValueChanged<T> : EventArgs
{
    public SelectedValueChanged(T? value) => Value = value;
    public T? Value { get; }
}
