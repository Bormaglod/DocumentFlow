//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.10.2021
//
// Версия 2022.11.16
//  - мелкие исправления
// Версия 2022.11.26
//  - добавлен метод RefreshDataSourceOnLoad
// Версия 2022.12.7
//  - добапвлена кнопка Refresh
// Версия 2022.12.11
//  - запрещено редактирование строки, если установлен режим "Только для 
//    чтения"
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.2.6
//  - добавлено свойство Header
// Версия 2023.5.5
//  - удалён конструктор с параметром content
// Версия 2023.5.17
//  - удалены методы CreateTableSummaryRow и AddCommand
// Версия 2023.5.20
//  - корректировка логики изменения записи пользователем
// Версия 2023.5.21
//  - добавлен отступ снизу (после grid'а)
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Controls.Core;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Dialogs;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace DocumentFlow.Controls.Editors;

public partial class DfDataGrid<T> : BaseControl, IDataSourceControl, IGridDataSource, IAccess, IDataGridControl<T>
    where T : IEntity<long>, IEntityClonable, ICloneable, new()
{
    private ObservableCollection<T>? list;
    private Guid? ownerId;
    private IOwnedRepository<long, T>? repository;
    private readonly List<T> deleted = new();
    private readonly List<T> created = new();
    private readonly List<T> updated = new();

    private DataGridGenerateColumn? autoGeneratingColumn;

    private DataGridOperation<T>? dataCreate;
    private DataGridUpdateOperation<T>? dataUpdate;
    private DataGridOperation<T>? dataRemove;
    private DataGridOperation<T>? dataCopy;

    private IDataGridDialog<T>? dialog;

    public DfDataGrid() : base(string.Empty)
    {
        InitializeComponent();
        SetLabelControl(labelHeader, string.Empty);
        SetNestedControl(gridMain);

        gridMain.CellRenderers.Remove("TableSummary");
        gridMain.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer());

        labelHeader.Visible = false;
    }

    public bool ReadOnly
    {
        get => toolStrip1.Enabled;
        set
        {
            toolStrip1.Enabled = !value;

            menuCreate.Enabled = !value;
            menuEdit.Enabled = !value;
            menuDelete.Enabled = !value;
            menuCopy.Enabled = !value;
        }
    }

    #region IDataSourceControl interface

    public void RemoveDataSource()
    {
        gridMain.DataSource = null;
    }

    public void RefreshDataSource()
    {
        if (ownerId != null && repository != null)
        {
            list = new ObservableCollection<T>(repository.GetByOwner(ownerId));
        }
        else
        {
            list = new ObservableCollection<T>();
        }

        gridMain.DataSource = list;
    }

    public void RefreshDataSourceOnLoad() => RefreshDataSource();

    #endregion

    #region IGridDataSource interface

    public void SetOwner(Guid owner_id) => ownerId = owner_id;

    public void UpdateData(IDbTransaction transaction)
    {
        if (repository == null)
        {
            return;
        }

        if (ownerId == null)
        {
            throw new ArgumentNullException(nameof(ownerId), "Не определено значение owner_id.");
        }

        foreach (var item in created)
        {
            item.OwnerId = ownerId;
            repository.Add(item, transaction);
        }

        foreach (var item in updated)
        {
            repository.Update(item, transaction);
        }

        foreach (var item in deleted)
        {
            repository.Wipe(item, transaction);
        }

        created.Clear();
        updated.Clear();
        deleted.Clear();
    }

    #endregion

    public void Fill(IEnumerable<T> rows)
    {
        if (ownerId == null)
        {
            throw new ArgumentNullException(nameof(ownerId), "Не определено значение owner_id.");
        }

        created.Clear();
        deleted.AddRange(updated);
        updated.Clear();

        if (list == null)
        {
            list = new ObservableCollection<T>();
            gridMain.DataSource = list;
        }
        else
        {
            list.Clear();
        }

        foreach (var row in rows)
        {
            row.OwnerId = ownerId;
            list.Add(row);
            created.Add(row);
        }
    }

    protected override void OnHeaderChanged()
    {
        HeaderVisible = !string.IsNullOrEmpty(Header);
    }

    private bool CreateDialog(T creatingData)
    {
        if (dialog != null)
        {
            return dialog.Create(creatingData);
        }

        return false;
    }

    private DataOperationResult EditDialog(T editingData)
    {
        if (dialog != null)
        {
            return dialog.Edit(editingData) ? DataOperationResult.Update : DataOperationResult.Cancel;
        }

        return DataOperationResult.Cancel;
    }

    private bool CopyDialog(T copiedData)
    {
        if (dialog != null)
        {
            return dialog.Edit(copiedData);
        }

        return false;
    }

    private bool RemoveDialog(T removingData)
    {
        if (dialog != null && dialog is IDataGridDialogExt<T> removeDialog)
        {
            return removeDialog.Remove(removingData);
        }

        return false;
    }

    private void Edit()
    {
        if (!toolStrip1.Enabled)
        {
            return;
        }

        if (dataUpdate != null && gridMain.SelectedItem is T item && list != null)
        {
            var editingData = (T)item.Clone();
            var res = dataUpdate(editingData);

            if (res != DataOperationResult.Cancel)
            {
                list[list.IndexOf(item)] = editingData;

                if (res == DataOperationResult.Update)
                {
                    if (item.Id != 0)
                    {
                        if (updated.Contains(item))
                        {
                            updated.Remove(item);
                        }

                        updated.Add(editingData);
                    }
                    else
                    {
                        if (created.Contains(item))
                        {
                            created.Remove(item);
                        }

                        created.Add(editingData);
                    }
                }
                else
                {
                    // Если при изменении записи необходимо сначала удалить запись, в потом добавить, то...
                    if (item.Id == 0)
                    {
                        // Изменяемая запись была новая. Удалим её из новых.
                        created.Remove(item);
                    }
                    else
                    {
                        if (updated.Contains(item))
                        {
                            // Если запись уже изменялась, то удалим её из списка изменённых
                            updated.Remove(item);
                        }

                        // Запись существует физически, добавим её к удаляемым
                        deleted.Add(item);

                        editingData.Id = 0;
                        created.Add(editingData);
                    }
                }
            }
        }
    }

    private void ButtonCreate_Click(object sender, EventArgs e)
    {
        if (dataCreate != null && list != null)
        {
            var creatingData = new T();
            if (dataCreate(creatingData))
            {
                list.Add(creatingData);
                created.Add(creatingData);
            }
        }
    }

    private void ButtonEdit_Click(object sender, EventArgs e) => Edit();

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (gridMain.SelectedItem is T item && list != null)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool res = true;
                if (dataRemove != null)
                {
                    res = dataRemove(item);
                }

                if (res)
                {
                    bool last = list.IndexOf(item) == list.Count - 1;
                    list.Remove(item);
                    if (item.Id == 0)
                    {
                        created.Remove(item);
                    }
                    else
                    {
                        if (updated.Contains(item))
                        {
                            updated.Remove(item);
                        }

                        deleted.Add(item);
                    }

                    // FIX: Если в таблицу добавлены GridSummaryRow, то удаление последней строки приводит к Exception
                    if (last)
                    {
                        gridMain.DataSource = null;
                        gridMain.DataSource = list;
                    }
                }
            }
        }
    }

    private void ButtonCopy_Click(object sender, EventArgs e)
    {
        if (dataCopy != null && gridMain.SelectedItem is T item && list != null)
        {
            var copiedData = (T)item.Copy();
            if (dataCopy(copiedData))
            {
                list.Add(copiedData);
                created.Add(copiedData);
            }
        }
    }

    private void MenuCopyToClipboard_Click(object sender, EventArgs e)
    {
        if (gridMain.CurrentCell == null || gridMain.SelectedItem == null)
            return;

        PropertyInfo? prop = typeof(T).GetProperty(gridMain.CurrentCell.Column.MappingName);
        if (prop != null)
        {
            object? value = prop.GetValue(gridMain.SelectedItem);
            if (value != null)
            {
                Clipboard.SetText(value.ToString() ?? string.Empty);
            }
        }
    }

    private void GridMain_CellDoubleClick(object sender, CellClickEventArgs e) => Edit();

    private void GridMain_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
    {
        var prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name == e.Column.MappingName);

        if (prop != null)
        {
            var attr = prop.GetCustomAttribute<ColumnModeAttribute>();
            if (attr != null)
            {
                if (attr.AutoSizeColumnsMode != AutoSizeColumnsMode.None)
                {
                    e.Column.AutoSizeColumnsMode = attr.AutoSizeColumnsMode;
                }

                if (attr.Width > 0)
                {
                    e.Column.Width = attr.Width;
                }

                e.Column.CellStyle.HorizontalAlignment = attr.Alignment;

                switch (attr.Format)
                {
                    case ColumnFormat.Currency:
                        if (e.Column is GridNumericColumn c)
                        {
                            c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
                        }

                        e.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

                        break;
                    case ColumnFormat.Progress:
                        var disp = prop.GetCustomAttribute<DisplayAttribute>();
                        var header = disp?.Name ?? e.Column.MappingName;
                        e.Column = new GridProgressBarColumn()
                        {
                            MappingName = e.Column.MappingName,
                            HeaderText = header,
                            Maximum = 100,
                            Minimum = 0,
                            ValueMode = ProgressBarValueMode.Percentage
                        };

                        break;
                    default:
                        break;
                }
            }
        }

        if (autoGeneratingColumn != null)
        {
            e.Cancel = autoGeneratingColumn(e.Column);
        }
    }

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshDataSource();

    #region IDataGridControl interface

    IDataGridControl<T> IDataGridControl<T>.AutoGeneratingColumn(DataGridGenerateColumn action)
    {
        autoGeneratingColumn = action;
        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.SetHeader(string header)
    {
        Header = header;
        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.SetRepository<R>()
    {
        repository = Services.Provider.GetService<R>()!;
        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.Dialog<D>()
    {
        dialog = Services.Provider.GetService<D>();

        dataCreate = CreateDialog;
        dataUpdate = EditDialog;
        dataCopy = CopyDialog;

        if (dialog is IDataGridDialogExt<T>)
        {
            dataRemove = RemoveDialog;
        }

        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.GridSummaryRow(VerticalPosition position, DataGridSummaryRow<T> summaryRow)
    {
        gridMain.TableSummaryRows.Clear();
        var tableSummaryRow = new GridTableSummaryRow()
        {
            Name = "TableRowSummary",
            ShowSummaryInRow = false,
            Position = position
        };

        gridMain.TableSummaryRows.Add(tableSummaryRow);

        var summary = new DataGridSummary<T>(tableSummaryRow);
        summaryRow(summary);

        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.DoCreate(DataGridOperation<T> func)
    {
        dataCreate = func;
        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.DoUpdate(DataGridUpdateOperation<T> func)
    {
        dataUpdate = func;
        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.DoRemove(DataGridOperation<T> func)
    {
        dataRemove = func;
        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.DoCopy(DataGridOperation<T> func)
    {
        dataCopy = func;
        return this;
    }

    IDataGridControl<T> IDataGridControl<T>.AddCommand(string text, Image image, DataGridCommand<T> action)
    {
        toolStripSeparatorCustom1.Visible = true;
        toolStripSeparatorCustom2.Visible = true;

        ToolStripButton button = new(text, image, (sender, e) => action(this))
        {
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };

        toolStrip1.Items.Add(button);

        return this;
    }

    #endregion
}
