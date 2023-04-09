//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IChoiceControl<T> : IControl
    where T : struct, IComparable
{
    T? Value { get; }
    IChoiceControl<T> ReadOnly();
    IChoiceControl<T> Required();
    IChoiceControl<T> SetChoiceValues(IReadOnlyDictionary<T, string> keyValues, bool autoRefresh = false);
    IChoiceControl<T> SetDataSource(Func<IEnumerable<IChoice<T>>?> func, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad);
    IChoiceControl<T> ManualValueChanged(Action<T?> action);
}
