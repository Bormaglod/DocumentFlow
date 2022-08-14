//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Wages;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;

using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;

namespace DocumentFlow.Controls.Editors;

public partial class DfReportCard : BaseControl, IDataSourceControl, IGridDataSource, IAccess
{
    private int year;
    private int month;
    private Guid? owner_id;
    private ObservableCollection<ReportCardEmployee>? list;
    private readonly List<ReportCardEmployee> deleted = new();
    private readonly List<ReportCardEmployee> created = new();
    private readonly List<ReportCardEmployee> updated = new();
    private RowAutoFitOptions autoFitOptions = new();

    public DfReportCard() : base(string.Empty)
    {
        InitializeComponent();

        year = DateTime.Now.Year;
        month = DateTime.Now.Month;

        BuildCalendar();
    }

    public int Year
    {
        get => year;
        set
        {
            if (year != value)
            {
                year = value;
                BuildCalendar();
            }
        }
    }

    public int Month
    {
        get => month;
        set
        {
            if (month != value)
            {
                month = value;
                BuildCalendar();
            }
        }
    }

    public bool ReadOnly
    {
        get => toolStrip1.Enabled;
        set
        {
            toolStrip1.Enabled = !value;

            menuAdd.Enabled = !value;
            menuEdit.Enabled = !value;
            menuDelete.Enabled = !value;
            menuPopulate.Enabled = !value;
        }
    }

    #region IDataSourceControl interface

    public void RefreshDataSource()
    {
        var repo = Services.Provider.GetService<IReportCardEmployeeRepository>();
        if (repo != null)
        {
            if (owner_id != null)
            {
                list = new ObservableCollection<ReportCardEmployee>(repo.GetByOwner(owner_id));
            }
            else
            {
                list = new ObservableCollection<ReportCardEmployee>();
            }

            foreach (var item in list)
            {
                item.UpdateSummary();
            }

            gridContent.DataSource = list;
        }
    }

    #endregion

    #region IGridDataSource interface

    public void SetOwner(Guid owner_id) => this.owner_id = owner_id;

    public void UpdateData(IDbTransaction transaction)
    {
        if (owner_id == null)
        {
            throw new ArgumentNullException(nameof(owner_id), "Не определено значение owner_id.");
        }

        var repo = Services.Provider.GetService<IReportCardEmployeeRepository>();
        if (repo != null)
        {
            foreach (var item in created)
            {
                item.owner_id = owner_id;
                repo.Add(item, transaction);
            }

            foreach (var item in updated)
            {
                repo.Update(item, transaction);
            }

            foreach (var item in deleted)
            {
                repo.Wipe(item, transaction);
            }

            created.Clear();
            updated.Clear();
            deleted.Clear();
        }
    }

    #endregion

    private void BuildCalendar()
    {
        List<string> excludeColumns = new();

        gridContent.Columns.Clear();

        gridContent.Columns.Add(new GridTextColumn() 
        { 
            MappingName = "employee_name", 
            HeaderText = "Сотрудник" ,
            Width = 150,
            AllowEditing = false
        });

        gridContent.Columns.Add(new GridTextColumn()
        {
            MappingName = "summary",
            HeaderText = "Итого",
            Width = 100,
            AllowEditing = false
        });

        for (int i = 0; i < DateTime.DaysInMonth(Year, Month); i++)
        {
            var date = new DateTime(Year, Month, i + 1);

            var column = new GridTextColumn()
            {
                MappingName = $"info[{i}]",
                HeaderText = $"{i + 1}\r\n{date:ddd}",
                AllowHeaderTextWrapping = true,
                Width = 50
            };

            excludeColumns.Add(column.MappingName);

            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                column.HeaderStyle.TextColor = Color.Red;
            }
            else
            {
                column.HeaderStyle.TextColor = Color.Black;
            }

            column.HeaderStyle.Font.Bold = true;

            gridContent.Columns.Add(column);
        }

        autoFitOptions.ExcludeColumns = excludeColumns;
    }

    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        var repo = Services.Provider.GetService<IOurEmployeeRepository>();
        if (repo != null)
        {
            SelectDirectoryForm<OurEmployee> form = new(null, false, true);
            form.AddItems(repo.GetAllValid());
            if (form.ShowDialog() == DialogResult.OK && form.SelectedItem != null && list != null)
            {
                var emp = new ReportCardEmployee(form.SelectedItem, Year, Month);
                created.Add(emp);
                list.Add(emp);
            }
        }
    }

    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        var repo = Services.Provider.GetService<IOurEmployeeRepository>();
        if (repo != null && gridContent.SelectedItem is ReportCardEmployee emp)
        {
            SelectDirectoryForm<OurEmployee> form = new(null, false, true);
            form.AddItems(repo.GetAllValid());
            form.SelectedValue = emp.employee_id;
            if (form.ShowDialog() == DialogResult.OK && form.SelectedItem != null && list != null)
            {
                emp.SetEmployee(form.SelectedItem);
                if (emp.id != default)
                {
                    updated.Add(emp);
                }

                gridContent.Refresh();
            }
        }
    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (gridContent.SelectedItem is ReportCardEmployee emp && list != null)
        {
            if (emp.id == default)
            {
                created.Remove(emp);
            }
            else
            {
                if (updated.Contains(emp))
                {
                    updated.Remove(emp);
                }

                deleted.Add(emp);
            }

            list.Remove(emp);
        }
    }

    private void ButtonPopulate_Click(object sender, EventArgs e)
    {
        if (EditorPage == null)
        {
            return;
        }

        if (MessageBox.Show("Перед заполнением таблица будет очищена. Продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
            return;
        }

        if (owner_id == null || owner_id.Value == default)
        {
            if (MessageBox.Show("Документ не записан, для заполнения таблицы документ должен быть записан. Записать?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (!EditorPage.Save())
            {
                return;
            }
        }

        if (owner_id != null)
        {
            var repo = Services.Provider.GetService<IReportCardEmployeeRepository>();
            repo!.PopulateReportCard(owner_id.Value);

            RefreshDataSource();
        }
    }

    private void GridContent_CurrentCellValidated(object sender, CurrentCellValidatedEventArgs e)
    {
        if (e.RowData is ReportCardEmployee emp)
        {
            Match m = Regex.Match(e.Column.MappingName, @"^info\[(\d+)\]$");
            if (m.Success)
            {
                if (int.TryParse(m.Groups[1].Value, out int empIndex))
                {
                    emp.SetInfo(empIndex, e.NewValue.ToString() ?? string.Empty);
                    if (emp.id != 0 && !updated.Contains(emp))
                    {
                        updated.Add(emp);
                    }
                }
            }
        }
    }

    private void GridContent_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
    {
        if (e.ColumnIndex > 1)
        {
            e.Style.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }

    private void GridContent_QueryRowHeight(object sender, QueryRowHeightEventArgs e)
    {
        if (gridContent.AutoSizeController.GetAutoRowHeight(e.RowIndex, autoFitOptions, out int autoHeight))
        {
            if (autoHeight > gridContent.RowHeight)
            {
                e.Height = autoHeight;
                e.Handled = true;
            }
        }
    }

    private void GridContent_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs e)
    {
        gridContent.InvalidateRowHeight(e.DataRow.RowIndex);
        gridContent.TableControl.Invalidate();
    }
}
