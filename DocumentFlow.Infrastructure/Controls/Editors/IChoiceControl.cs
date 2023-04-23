//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IChoiceControl<T> : IControl
    where T : struct, IComparable
{
    T? SelectedValue { get; set; }
    void ClearSelectedValue();
    IChoiceControl<T> ReadOnly();
    IChoiceControl<T> Required();
    IChoiceControl<T> SetChoiceValues(IReadOnlyDictionary<T, string> keyValues, bool autoRefresh = false);
    IChoiceControl<T> SetDataSource(GettingDataSource<IChoice<T>> func, DataRefreshMethod refreshMethod = DataRefreshMethod.OnLoad);
    IChoiceControl<T> ChoiceChanged(ControlValueChanged<T?> action);
    IChoiceControl<T> ChoiceSelected(ControlValueChanged<T?> action);
}
