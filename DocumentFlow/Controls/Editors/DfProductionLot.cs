//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Data.Core;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Productions.Performed;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.Input.Enums;

using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DocumentFlow.Controls.Editors;

public partial class DfProductionLot : BaseControl, IGridDataSource, IDataSourceControl
{
    private class BaseEmpInfo : INotifyPropertyChanged
    {
        private long quantity;
        private decimal salary;

        public event PropertyChangedEventHandler? PropertyChanged;

        public BaseEmpInfo(long quantity, decimal salary)
        {
            this.quantity = quantity;
            this.salary = salary;
        }

        public long Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                NotifyPropertyChanged();
            }
        }

        public decimal Salary
        {
            get => salary;

            set
            {
                salary = value;
                NotifyPropertyChanged();
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private class EmpInfo : BaseEmpInfo
    {
        public EmpInfo(OurEmployee employee) : base(0, 0)
        {
            Employee = employee;
        }

        public OurEmployee Employee { get; }
        public override string ToString() => Employee?.item_name ?? string.Empty;
    }

    private class OperationInfo : BaseEmpInfo
    {
        public OperationInfo(ProductionLotOperation operation, IReadOnlyList<OurEmployee> employees) : base(0, 0)
        {
            Operation = operation;
            Employees = new List<EmpInfo>();

            foreach (var item in employees)
            {
                AddEmployee(item);
            }
        }

        public ProductionLotOperation Operation { get; }
        public IList<EmpInfo> Employees { get; }

        public void AddEmployee(OurEmployee emp) => Employees.Add(new EmpInfo(emp));

        public void SetEmployeeData(OperationsPerformed operation)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].Employee.id == operation.employee_id)
                {
                    Employees[i].Quantity = operation.quantity;
                    Employees[i].Salary = operation.salary;
                    NotifyPropertyChanged($"Employees[{i}].Quantity");
                    break;
                }
            }
        }

        public void UpdateSummaryValues()
        {
            long quantity = 0;
            decimal salary = 0;

            foreach (var emp in Employees)
            {
                quantity += emp.Quantity;
                salary += emp.Salary;
            }

            Quantity = quantity;
            Salary = salary;
        }
    }

    private class ComplexSummaryAggregate : ISummaryAggregate
    {
        public decimal ComplexSum { get; set; }

        Action<IEnumerable, string, PropertyDescriptor> ISummaryAggregate.CalculateAggregateFunc()
        {
            return (items, property, pd) =>
            {
                if (items is IEnumerable<OperationInfo> enumerableItems && pd.Name == "ComplexSum")
                {
                    Match m = Regex.Match(property, @"^Employees\[(\d+)\].\w+$");
                    if (m.Success && int.TryParse(m.Groups[1].Value, out int empIndex))
                    {
                        ComplexSum = enumerableItems.Sum(x => empIndex < x.Employees.Count ? x.Employees[empIndex].Salary : 0);
                    }
                }
            };
        }
    }


    private Guid? owner_id;
    private List<OurEmployee> employees = new();

    public DfProductionLot() : base(string.Empty)
    {
        InitializeComponent();

        gridContent.CellRenderers.Remove("TableSummary");
        gridContent.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer());
    }

    #region IDataSourceControl interface

    public void RefreshDataSource()
    {
        if (owner_id != null)
        {

            gridContent.StackedHeaderRows.Clear();
            gridContent.TableSummaryRows.Clear();
            gridContent.Columns.Clear();

            var repoPerf = Services.Provider.GetService<IOperationsPerformedRepository>();
            if (repoPerf == null)
            {
                return;
            }

            var repoLot = Services.Provider.GetService<IProductionLotRepository>();
            if (repoLot == null)
            {
                return;
            }

            var operations = new ObservableCollection<OperationInfo>();

            // Список занятых в изготовлении партии
            employees = new List<OurEmployee>(repoPerf.GetWorkedEmployes(owner_id));

            // Список всех операций необходимых для изготовления изделия из партии
            var ops = repoLot.GetOperations(owner_id.Value);

            foreach (var item in ops.OrderBy(x => x.code))
            {
                var opInfo = new OperationInfo(item, employees);
                operations.Add(opInfo);
            }

            // Список произведенных работ сгруппированый пооперационно и по каждому сотруднику
            var summary = repoPerf.GetSummary(owner_id.Value);
            foreach (var item in summary)
            {
                var opInfo = operations.FirstOrDefault(x => x.Operation.id == item.operation_id);
                if (opInfo == null)
                {
                    continue;
                }

                opInfo.SetEmployeeData(item);
            }

            // Расчёт итоговых значений по операциям
            foreach (var item in operations)
            {
                item.UpdateSummaryValues();
            }

            gridContent.Columns.Add(new GridTextColumn() { MappingName = "Operation.code", HeaderText = "Код" });
            gridContent.Columns.Add(new GridTextColumn() { MappingName = "Operation.item_name", HeaderText = "Операция", Width = 400 });

            StackedHeaderRow srow = new();
            gridContent.StackedHeaderRows.Add(srow);

            var tableSummaryRow = new GridTableSummaryRow()
            {
                Name = "TableRowSummary",
                ShowSummaryInRow = false,
                Position = VerticalPosition.Bottom
            };

            gridContent.TableSummaryRows.Add(tableSummaryRow);

            for (int i = 0; i < employees.Count; i++)
            {
                CreateColumnEmployee(i, srow, tableSummaryRow);
            }

            gridContent.Columns.Add(new GridNumericColumn()
            {
                MappingName = "Operation.quantity_by_lot",
                HeaderText = "Всего на партию",
                AllowHeaderTextWrapping = true,
                NumberFormatInfo = GetNumberFormatInfo()
            });

            gridContent.Columns.Add(new GridNumericColumn()
            {
                MappingName = "Quantity",
                HeaderText = "Кол-во",
                NumberFormatInfo = GetNumberFormatInfo()
            });

            var sSummaryColumn = new GridNumericColumn()
            {
                MappingName = "Salary",
                HeaderText = "Зарплата",
                FormatMode = FormatMode.Currency
            };

            sSummaryColumn.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

            gridContent.Columns.Add(sSummaryColumn);

            srow.StackedColumns.Add(new StackedColumn() { ChildColumns = "Quantity,Salary", HeaderText = "Итого" });

            var summaryColumn = new GridSummaryColumn
            {
                SummaryType = SummaryType.DoubleAggregate,
                Format = "{Sum:c}",
                MappingName = "Salary",
                Name = "Salary"
            };

            tableSummaryRow.SummaryColumns.Add(summaryColumn);

            gridContent.DataSource = operations.Count > 0 ? operations : (object?)null;
        }
    }

    #endregion

    #region IGridDataSource interface

    public void SetOwner(Guid owner_id) => this.owner_id = owner_id;

    public void UpdateData(IDbTransaction transaction)
    {
        //throw new NotImplementedException();
    }

    #endregion

    private void AddCompleteOperations()
    {
        if (owner_id != null)
        {
            OurEmployee? emp = null;
            OperationInfo? oper = gridContent.SelectedItem as OperationInfo;

            if (oper != null)
            {
                var cell = gridContent.CurrentCell;
                Match m = Regex.Match(cell.Column.MappingName, @"^Employees\[(\d+)\].+$");
                if (m.Success)
                {
                    if (int.TryParse(m.Groups[1].Value, out int empIndex))
                    {
                        emp = oper.Employees[empIndex].Employee;
                    }
                }
            }

            OperationsPerformedForm form = new(owner_id.Value)
            {
                Operation = oper?.Operation,
                Employee = emp
            };

            if (form.ShowDialog() == DialogResult.OK && form.Employee != null && form.Operation != null)
            {
                var op = new OperationsPerformed()
                {
                    owner_id = owner_id,
                    employee_id = form.Employee.id,
                    operation_id = form.Operation.id,
                    replacing_material_id = form.ReplacingMaterial?.id,
                    quantity = form.Quantity
                };

                var repo = Services.Provider.GetService<IOperationsPerformedRepository>();
                if (repo != null)
                {
                    try
                    {
                        // добавим и проведем выполнение операции сотрудником
                        op = repo.AddAndAccept(op);

                        CurrentApplicationContext.Context.App.SendNotify("operations_performed", op, Data.MessageAction.Add);

                        // поучим сводную информацию - количество выполненных операций указанным сотрудником
                        var summary = repo.GetSummary(owner_id.Value, form.Operation, form.Employee);
                        if (summary != null && gridContent.DataSource is IList<OperationInfo> list)
                        {
                            // если сотрудник ещё ны был задействован для выполнения данной операции,
                            // то его добавляем в список работников, добавляем его во все записи операций и
                            // добавляем колонку с сотрудником в таблицу
                            if (employees.FirstOrDefault(x => x.id == op.employee_id) == null)
                            {
                                employees.Add(form.Employee);
                                foreach (var item in list)
                                {
                                    item.AddEmployee(form.Employee);
                                }

                                CreateColumnEmployee(employees.Count - 1, insertToEnd: true);
                            }

                            // обновим информацию о сотруднике
                            var o = list.FirstOrDefault(x => x.Operation.id == summary.operation_id);
                            if (o != null)
                            {
                                o.SetEmployeeData(summary);
                                o.UpdateSummaryValues();

                                // для обновления итоговой строки, необходимо сделать это...
                                gridContent.View.Refresh();
                            }
                        }
                    }
                    catch (RepositoryException e)
                    {
                        MessageBox.Show(e.Message, "Ощибка", MessageBoxButtons.OK);
                    }
                }
            }
        }
    }

    private void CreateColumnEmployee(int indexEmployee, StackedHeaderRow? srow = null, GridTableSummaryRow? tableSummaryRow = null, bool insertToEnd = false)
    {
        var qColName = $"Employees[{indexEmployee}].Quantity";
        var sColName = $"Employees[{indexEmployee}].Salary";

        var qColumn = new GridNumericColumn()
        {
            MappingName = qColName,
            HeaderText = "Кол-во",
            NumberFormatInfo = GetNumberFormatInfo()
        };

        var sColumn = new GridNumericColumn()
        {
            MappingName = sColName,
            HeaderText = "Зарплата",
            FormatMode = FormatMode.Currency
        };

        sColumn.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        if (insertToEnd)
        {
            gridContent.Columns.Insert((indexEmployee + 1) * 2, qColumn);
            gridContent.Columns.Insert((indexEmployee + 1) * 2 + 1, sColumn);
        }
        else
        {
            gridContent.Columns.Add(qColumn);
            gridContent.Columns.Add(sColumn);
        }

        if (srow == null)
        {
            srow = gridContent.StackedHeaderRows[0];
        }

        srow.StackedColumns.Add(new StackedColumn()
        {
            ChildColumns = $"{qColName},{sColName}",
            HeaderText = employees[indexEmployee].ToString()
        });

        var empSummaryColumn = new GridSummaryColumn
        {
            SummaryType = SummaryType.Custom,
            Format = "{ComplexSum:c}",
            MappingName = sColName,
            Name = sColName,
            CustomAggregate = new ComplexSummaryAggregate()
        };

        if (tableSummaryRow == null)
        {
            tableSummaryRow = gridContent.TableSummaryRows[0];
        }

        tableSummaryRow.SummaryColumns.Add(empSummaryColumn);
    }

    private static NumberFormatInfo GetNumberFormatInfo(int decimalDigits = 0)
    {
        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = decimalDigits;
        numberFormat.PercentDecimalDigits = decimalDigits;

        return numberFormat;
    }

    private void GridContent_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
    {
        if (e.DataRow.RowData is OperationInfo info)
        {
            Match m = Regex.Match(e.Column.MappingName, @"^Employees\[(\d+)\].(\w+)$");
            if (m.Success)
            {
                if (int.TryParse(m.Groups[1].Value, out int empIndex))
                {
                    decimal cellValue = m.Groups[2].Value switch
                    {
                        "Salary" => info.Employees[empIndex].Salary,
                        "Quantity" => info.Employees[empIndex].Quantity,
                        _ => 0
                    };

                    if (cellValue == 0)
                    {
                        e.Style.TextColor = Color.FromArgb(204, 204, 204);
                    }
                    else
                    {
                        e.Style.TextColor = SystemColors.ControlText;
                    }
                }
            }

            if (e.Column.MappingName == "Quantity")
            {
                if (info.Quantity < info.Operation.quantity_by_lot)
                {
                    e.Style.TextColor = Color.Red;
                }
                else
                {
                    e.Style.TextColor = Color.Green;
                }
            }
        }
    }

    private void GridContent_CellDoubleClick(object sender, CellClickEventArgs e) => AddCompleteOperations();

    private void MenuCreateOperation_Click(object sender, EventArgs e) => AddCompleteOperations();
}
