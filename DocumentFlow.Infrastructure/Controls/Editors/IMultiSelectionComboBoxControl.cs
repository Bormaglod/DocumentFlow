//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IMultiSelectionComboBoxControl : IControl 
{
    IMultiSelectionComboBoxControl SetDataSource(GettingDataSource<IItem> func);
    IMultiSelectionComboBoxControl ValueChanged(MultiSelectValueChanged action);
}
