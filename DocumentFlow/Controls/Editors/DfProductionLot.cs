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
using DocumentFlow.Entities.Employees;
using DocumentFlow.Entities.Productions.Lot;
using DocumentFlow.Entities.Productions.Performed;
using DocumentFlow.Dialogs;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.Input.Enums;

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DocumentFlow.Controls.Editors;

public partial class DfProductionLot : BaseControl, IGridDataSource, IDataSourceControl
{
    private class EmpInfo
    {
        public EmpInfo(OurEmployee employee)
        {
            Employee = employee;
            Quantity = 0;
            Salary = 0;
        }

        public OurEmployee Employee { get; }
        public long Quantity { get; set; }
        public decimal Salary { get; set; }

        public override string ToString() => Employee?.item_name ?? string.Empty;
    }

    private class OperationInfo
    {
        public OperationInfo(ProductionLotOperation operation, IReadOnlyList<OurEmployee> employees)
        {
            Operation = operation;
            Employees = new List<EmpInfo>(employees.Count);
            foreach (var item in employees)
            {
                Employees.Add(new EmpInfo(item));
            }
        }

        public ProductionLotOperation Operation { get; }
        public IList<EmpInfo> Employees { get; }
        public long Quantity { get; set; }
        public decimal Salary { get; set; }

        public void SetEmployeeData(OperationsPerformed operation)
        {
            var emp = Employees.FirstOrDefault(x => x.Employee.id == operation.employee_id);
            if (emp != null)
            {
                emp.Quantity = operation.quantity;
                emp.Salary = operation.salary;
            }
        }

        public void UpdateSummaryValues()
        {
            Quantity = 0;
            Salary = 0;

            foreach (var emp in Employees)
            {
                Quantity += emp.Quantity;
                Salary += emp.Salary;
            }
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

            var operations = new List<OperationInfo>();

            // Список занятых в изготовлении партии
            var emps = repoPerf.GetWorkedEmployes(owner_id);

            // Список всех операций необходимых для изготовления изделия из партии
            var ops = repoLot.GetOperations(owner_id.Value);

            foreach (var item in ops)
            {
                var opInfo = new OperationInfo(item, emps);
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

            for (int i = 0; i < emps.Count; i++)
            {
                var qColName = $"Employees[{i}].Quantity";
                var sColName = $"Employees[{i}].Salary";

                gridContent.Columns.Add(new GridNumericColumn()
                {
                    MappingName = qColName,
                    HeaderText = "Кол-во",
                    NumberFormatInfo = GetNumberFormatInfo()
                });

                var sColumn = new GridNumericColumn()
                {
                    MappingName = sColName,
                    HeaderText = "Зарплата",
                    FormatMode = FormatMode.Currency
                };

                sColumn.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

                gridContent.Columns.Add(sColumn);

                srow.StackedColumns.Add(new StackedColumn()
                {
                    ChildColumns = $"{qColName},{sColName}",
                    HeaderText = emps[i].ToString()
                });

                var empSummaryColumn = new GridSummaryColumn
                {
                    SummaryType = SummaryType.Custom,
                    Format = "{ComplexSum:c}",
                    MappingName = sColName,
                    Name = sColName,
                    CustomAggregate = new ComplexSummaryAggregate()
                };

                tableSummaryRow.SummaryColumns.Add(empSummaryColumn);
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

            gridContent.DataSource = operations.Count > 0 ? operations.OrderBy(x => x.Operation.code) : (object?)null;
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

            if (form.ShowDialog() == DialogResult.OK)
            {
                var op = new OperationsPerformed()
                {
                    owner_id = owner_id,
                    employee_id = form.Employee!.id,
                    operation_id = form.Operation!.id,
                    replacing_material_id = form.ReplacingMaterial?.id,
                    quantity = form.Quantity
                };

                var repo = Services.Provider.GetService<IOperationsPerformedRepository>();
                if (repo != null)
                {
                    try
                    {
                        op = repo.Add(op);
                        repo.Accept(op);

                        CurrentApplicationContext.Context.App.SendNotify("operations_performed", op, Data.MessageAction.Add);
                    }
                    catch (RepositoryException e)
                    {
                        MessageBox.Show(e.Message, "Ощибка", MessageBoxButtons.OK);
                    }
                }
            }
        }
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
