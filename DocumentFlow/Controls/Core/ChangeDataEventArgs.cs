//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Core;

public class ChangeDataEventArgs<T> : EventArgs
{
    public ChangeDataEventArgs(T? oldValue, T? newValue) => (OldValue, NewValue) = (oldValue, newValue);
    public T? OldValue { get; }
    public T? NewValue { get; }
}
