//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.08.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.Renderers;
using DocumentFlow.Data;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Dialogs.Interfaces;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.Input.Enums;

using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DocumentFlow.ViewModels;

[Entity(typeof(ProductionLot), RepositoryType = typeof(IProductionLotRepository))]
public partial class ProductionLotEditor : EditorPanel, IProductionLotEditor, IDocumentEditor
{
    [GeneratedRegex("^Employees\\[(\\d+)\\].+$")]
    private static partial Regex EmployeeRegex();

    [GeneratedRegex("^Employees\\[(\\d+)\\].(\\w+)$")]
    private static partial Regex EmpPropertiesRegex();

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
        public override string ToString() => Employee?.ItemName ?? string.Empty;
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
                if (Employees[i].Employee.Id == operation.EmployeeId)
                {
                    Employees[i].Quantity = operation.Quantity;
                    Employees[i].Salary = operation.Salary;
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
                    Match m = EmpPropertiesRegex().Match(property);
                    if (m.Success && int.TryParse(m.Groups[1].Value, out int empIndex))
                    {
                        ComplexSum = enumerableItems.Sum(x => empIndex < x.Employees.Count ? x.Employees[empIndex].Salary : 0);
                    }
                }
            };
        }
    }

    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;
    private List<OurEmployee> employees = new();

    public ProductionLotEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        gridContent.CellRenderers.Remove("TableSummary");
        gridContent.CellRenderers.Add("TableSummary", new CustomGridTableSummaryRenderer());

        Lot.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    public override void RegisterNestedBrowsers()
    {
        EditorPage.RegisterNestedBrowser<IOperationsPerformedNestedBrowser>();
        EditorPage.RegisterNestedBrowser<IFinishedGoodsNestedBrowser>();
    }

    public Guid? OwnerId => Lot.OwnerId;

    public void SetOwner(IDocumentInfo owner) => Lot.OwnerId = owner.Id;

    protected ProductionLot Lot { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Lot.Id != Guid.Empty;

        gridContent.StackedHeaderRows.Clear();
        gridContent.TableSummaryRows.Clear();
        gridContent.Columns.Clear();

        var repoPerf = services.GetRequiredService<IOperationsPerformedRepository>();
        if (repoPerf == null)
        {
            return;
        }

        var repoLot = services.GetRequiredService<IProductionLotRepository>();
        if (repoLot == null)
        {
            return;
        }

        var operations = new ObservableCollection<OperationInfo>();

        // Список занятых в изготовлении партии
        employees = new List<OurEmployee>(repoPerf.GetWorkedEmployes(Lot));

        // Список всех операций необходимых для изготовления изделия из партии
        var ops = repoLot.GetOperations(Lot);

        foreach (var item in ops.OrderBy(x => x.Code))
        {
            var opInfo = new OperationInfo(item, employees);
            operations.Add(opInfo);
        }

        // Список произведенных работ сгруппированый пооперационно и по каждому сотруднику
        var summary = repoPerf.GetSummary(Lot);
        foreach (var item in summary)
        {
            var opInfo = operations.FirstOrDefault(x => x.Operation.Id == item.OperationId);
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

        gridContent.Columns.Add(new GridTextColumn() { MappingName = $"{nameof(Operation)}.{nameof(Operation.Code)}", HeaderText = "Код" });
        gridContent.Columns.Add(new GridTextColumn() { MappingName = $"{nameof(Operation)}.{nameof(Operation.ItemName)}", HeaderText = "Операция", Width = 400 });

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
            MappingName = "Operation.QuantityByLot",
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

        sSummaryColumn.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;

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

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(ProductionLot.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(ProductionLot.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(ProductionLot.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectOrder.DataBindings.Add(nameof(selectOrder.SelectedItem), DataContext, nameof(ProductionLot.OwnerId), true, DataSourceUpdateMode.OnPropertyChanged, Guid.Empty);
        comboGoods.DataBindings.Add(nameof(comboGoods.SelectedItem), DataContext, nameof(ProductionLot.GoodsId), false, DataSourceUpdateMode.Never);
        comboCalc.DataBindings.Add(nameof(comboCalc.SelectedItem), DataContext, nameof(ProductionLot.CalculationId), false, DataSourceUpdateMode.OnPropertyChanged);
        textQuantity.DataBindings.Add(nameof(textQuantity.DecimalValue), DataContext, nameof(ProductionLot.Quantity), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectOrder.DataSource = services
            .GetRequiredService<IProductionOrderRepository>()
            .GetActiveOrders();
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

        sColumn.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Right;

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

        srow ??= gridContent.StackedHeaderRows[0];

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

        tableSummaryRow ??= gridContent.TableSummaryRows[0];
        tableSummaryRow.SummaryColumns.Add(empSummaryColumn);
    }

    private static NumberFormatInfo GetNumberFormatInfo(int decimalDigits = 0)
    {
        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = decimalDigits;
        numberFormat.PercentDecimalDigits = decimalDigits;

        return numberFormat;
    }

    private void AddCompleteOperations()
    {
        OurEmployee? emp = null;
        OperationInfo? oper = gridContent.SelectedItem as OperationInfo;

        if (oper != null)
        {
            var cell = gridContent.CurrentCell;
            Match m = EmployeeRegex().Match(cell.Column.MappingName);
            if (m.Success)
            {
                if (int.TryParse(m.Groups[1].Value, out int empIndex))
                {
                    emp = oper.Employees[empIndex].Employee;
                }
            }
        }

        var dialog = services.GetRequiredService<IOperationsPerformedDialog>();
        var res = dialog.Create(Lot, oper?.Operation, emp);

        if (res)
        {
            var op = dialog.Get();

            var repo = services.GetRequiredService<IOperationsPerformedRepository>();
            if (repo != null)
            {
                try
                {
                    // добавим и проведем выполнение операции сотрудником
                    op = repo.AddAndAccept(op);

                    var app = services.GetRequiredService<IHostApp>();
                    app.SendNotify("operations_performed", op, MessageAction.Add);

                    // получим сводную информацию - количество выполненных операций указанным сотрудником
                    var summary = repo.GetSummary(Lot, dialog.Operation, dialog.Employee);
                    if (summary != null && gridContent.DataSource is IList<OperationInfo> list)
                    {
                        // если сотрудник ещё ны был задействован для выполнения данной операции,
                        // то его добавляем в список работников, добавляем его во все записи операций и
                        // добавляем колонку с сотрудником в таблицу
                        if (employees.FirstOrDefault(x => x.Id == op.EmployeeId) == null)
                        {
                            employees.Add(dialog.Employee);
                            foreach (var item in list)
                            {
                                item.AddEmployee(dialog.Employee);
                            }

                            CreateColumnEmployee(employees.Count - 1, insertToEnd: true);
                        }

                        // обновим информацию о сотруднике
                        var o = list.FirstOrDefault(x => x.Operation.Id == summary.OperationId);
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
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK);
                }
            }
        }
    }

    private void SelectOrder_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        ModelsHelper.CreateProductinOrderColumns(e.Columns);
    }

    private void SelectOrder_SelectedItemChanged(object sender, EventArgs e)
    {
        var order = (ProductionOrder)selectOrder.SelectedDocument;
        comboGoods.DataSource = services
            .GetRequiredService<IProductionOrderRepository>()
            .GetGoods(order);
        comboGoods.UpdateValue();
    }

    private void ComboGoods_SelectedItemChanged(object sender, EventArgs e)
    {
        comboCalc.DataSource = services
            .GetRequiredService<ICalculationRepository>()
            .GetApproved(comboGoods.SelectedItem);
        comboCalc.UpdateValue();

        if (comboGoods.SelectedItem != Guid.Empty)
        {
            var goods = services
                .GetRequiredService<IGoodsRepository>()
                .Get(comboGoods.SelectedItem, userDefindedQuery: false, ignoreAdjustedQuery: true);
            if (goods.MeasurementId != null)
            {
                var meas = services
                    .GetRequiredService<IMeasurementRepository>()
                    .Get(goods.MeasurementId.Value);

                textQuantity.Suffix = meas.Abbreviation ?? meas.ItemName ?? meas.Code;
                textQuantity.ShowSuffix = true;

                return;
            }
        }

        textQuantity.ShowSuffix = false;
    }

    private void SelectOrder_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IProductionOrderBrowser>(e.Document);
    }

    private void ComboGoods_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<IGoodsBrowser>(e.Document);
    }

    private void ComboCalc_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowAssociateEditor<ICalculationBrowser>(e.Document);
    }

    private void GridContent_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
    {
        if (e.DataRow.RowData is OperationInfo info)
        {
            Match m = EmpPropertiesRegex().Match(e.Column.MappingName);
            if (m.Success)
            {
                if (int.TryParse(m.Groups[1].Value, out int empIndex))
                {
                    decimal cellValue = m.Groups[2].Value switch
                    {
                        nameof(BaseEmpInfo.Salary) => info.Employees[empIndex].Salary,
                        nameof(BaseEmpInfo.Quantity) => info.Employees[empIndex].Quantity,
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

            if (e.Column.MappingName == nameof(BaseEmpInfo.Quantity))
            {
                if (info.Quantity < info.Operation.QuantityByLot)
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
