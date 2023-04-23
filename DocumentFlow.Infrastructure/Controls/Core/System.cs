//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.DataGrid;

using System.Collections;

namespace DocumentFlow.Infrastructure.Controls.Core;

public delegate void OpenDialog<T>(T obj);

public delegate void ControlValueChanged<T>(T? obj);

public delegate void MultiSelectValueChanged(Actions actions, IList items);

public delegate void ControlValueCleared();

public delegate IEnumerable<T>? GettingDataSource<T>();

public delegate bool DataGridOperation<T>(T row);

public delegate DataOperationResult DataGridUpdateOperation<T>(T row);

public delegate bool DataGridGenerateColumn(GridColumn column);

public delegate void DataGridSummaryRow<T>(IDataGridSummary<T> summary) where T : IEntity<long>;

public delegate void DataGridCommand<T>(IDataGridControl<T> dataGrid) where T : IEntity<long>, IEntityClonable, ICloneable, new();

public delegate void CreateColumns(IList<GridColumn> columns);