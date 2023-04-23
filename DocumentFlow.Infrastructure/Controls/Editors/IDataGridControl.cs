//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Dialogs;

using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Infrastructure.Controls;

public enum DataOperationResult { Cancel, Update, DeleteAndInsert }

public interface IDataGridControl<T> : IControl
    where T : IEntity<long>, IEntityClonable, ICloneable, new()
{
    void Fill(IEnumerable<T> rows);
    IDataGridControl<T> AutoGeneratingColumn(DataGridGenerateColumn action);
    IDataGridControl<T> SetRepository<R>()
        where R : IOwnedRepository<long, T>;
    IDataGridControl<T> Dialog<D>()
        where D : IDataGridDialog<T>;
    IDataGridControl<T> SetHeader(string header);
    IDataGridControl<T> GridSummaryRow(VerticalPosition position, DataGridSummaryRow<T> summaryRow);
    IDataGridControl<T> DoCreate(DataGridOperation<T> func);
    IDataGridControl<T> DoUpdate(DataGridUpdateOperation<T> func);
    IDataGridControl<T> DoRemove(DataGridOperation<T> func);
    IDataGridControl<T> DoCopy(DataGridOperation<T> func);
    IDataGridControl<T> AddCommand(string text, Image image, DataGridCommand<T> action);
}