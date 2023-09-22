//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.04.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Enums;
using DocumentFlow.Data.Interfaces;

using System.Linq.Expressions;

namespace DocumentFlow.Controls.Interfaces;

public interface IDataGridSummary<T>
    where T : IEntity<long>
{
    IDataGridSummary<T> AsSummary(Expression<Func<T, object?>> memberExpression, string format, SelectOptions options = SelectOptions.None);
    IDataGridSummary<T> AsSummary(Expression<Func<T, object?>> memberExpression, SummaryColumnFormat format = SummaryColumnFormat.None, SelectOptions options = SelectOptions.None);
}
