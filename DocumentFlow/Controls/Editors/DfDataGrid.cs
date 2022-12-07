//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Data.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;

using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace DocumentFlow.Controls.Editors;

public partial class DfDataGrid<T> : BaseControl, IDataSourceControl, IGridDataSource, IAccess
    where T : IEntity<long>, IEntityClonable, ICloneable, new()
{
    private ObservableCollection<T>? list;
    private Guid? owner_id;
    private readonly IOwnedRepository<long, T> repository;
    private readonly List<T> deleted = new();
    private readonly List<T> created = new();
    private readonly List<T> updated = new();

    public DfDataGrid(IOwnedRepository<long, T> content) : base(string.Empty)
    {
        InitializeComponent();

        repository = content;

        gridMain.CellRenderers.Remove("TableSummary");
        gridMain.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer());
    }

    public event AutoGeneratingColumnEventHandler? AutoGeneratingColumn;
    public event EventHandler<DataCreateEventArgs<T>>? DataCreate;
    public event EventHandler<DataEditEventArgs<T>>? DataEdit;
    public event EventHandler<DataDeleteEventArgs<T>>? DataDelete;
    public event EventHandler<DataCopyEventArgs<T>>? DataCopy;

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

    public void RefreshDataSource()
    {
        if (owner_id != null)
        {
            list = new ObservableCollection<T>(repository.GetByOwner(owner_id));
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

    public void SetOwner(Guid owner_id) => this.owner_id = owner_id;

    public void UpdateData(IDbTransaction transaction)
    {
        if (owner_id == null)
        {
            throw new ArgumentNullException(nameof(owner_id), "Не определено значение owner_id.");
        }

        foreach (var item in created)
        {
            item.owner_id = owner_id;
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

    public void AddCommand(string text, Image image, EventHandler onClick)
    {
        toolStripSeparatorCustom1.Visible = true;
        toolStripSeparatorCustom2.Visible = true;

        ToolStripButton button = new(text, image, onClick)
        {
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
            TextImageRelation = TextImageRelation.ImageBeforeText
        };

        toolStrip1.Items.Add(button);
    }

    public void Fill(IEnumerable<T> rows)
    {
        if (owner_id == null)
        {
            throw new ArgumentNullException(nameof(owner_id), "Не определено значение owner_id.");
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
            row.owner_id = owner_id;
            list.Add(row);
            created.Add(row);
        }
    }

    public ISummary CreateTableSummaryRow(VerticalPosition position)
    {
        gridMain.TableSummaryRows.Clear();
        var tableSummaryRow = new GridTableSummaryRow()
        {
            Name = "TableRowSummary",
            ShowSummaryInRow = false,
            Position = position
        };

        gridMain.TableSummaryRows.Add(tableSummaryRow);

        return new SummaryRowData(tableSummaryRow, null);
    }

    private void Edit()
    {
        if (DataEdit != null && gridMain.SelectedItem is T item && list != null)
        {
            DataEditEventArgs<T> eventArgs = new((T)item.Clone());
            DataEdit(this, eventArgs);
            if (!eventArgs.Cancel)
            {
                list[list.IndexOf(item)] = eventArgs.EditingData;

                if (eventArgs.Rule == RuleChange.Update)
                {
                    if (item.id != 0)
                    {
                        if (updated.Contains(item))
                        {
                            updated.Remove(item);
                        }

                        updated.Add(eventArgs.EditingData);
                    }
                }
                else
                {
                    // Если при изменении записи необходимо сначала удалить запись, в потом добавить, то...
                    if (item.id == 0)
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

                        eventArgs.EditingData.id = 0;
                        created.Add(eventArgs.EditingData);
                    }
                }
            }
        }
    }

    private void ButtonCreate_Click(object sender, EventArgs e)
    {
        if (DataCreate != null && list != null)
        {
            DataCreateEventArgs<T> eventArgs = new(new T());
            DataCreate(this, eventArgs);
            if (!eventArgs.Cancel)
            {
                list.Add(eventArgs.CreatingData);
                created.Add(eventArgs.CreatingData);
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
                DataDeleteEventArgs<T> eventArgs = new(item);
                DataDelete?.Invoke(this, eventArgs);

                if (!eventArgs.Cancel)
                {
                    bool last = list.IndexOf(item) == list.Count - 1;
                    list.Remove(item);
                    if (item.id == 0)
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

                    // FIX: если в таблицу добавлены GridSummaryRow, то удаление последней строки приводит к Exception
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
        if (gridMain.SelectedItem is T item && list != null)
        {
            DataCopyEventArgs<T> eventArgs = new((T)item.Copy());
            DataCopy?.Invoke(this, eventArgs);
            if (!eventArgs.Cancel)
            {
                list.Add(eventArgs.CopiedData);
                created.Add(eventArgs.CopiedData);
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

    private void GridMain_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e) => AutoGeneratingColumn?.Invoke(this, e);

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshDataSource();
}
