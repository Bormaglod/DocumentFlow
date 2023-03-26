//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2022.12.3
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Entities.Wages.Core;
using DocumentFlow.Entities.Wages.Dialogs;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.Entities.Wages;

public class BasePayrollEditor<T, P, E> : BillingDocumentEditor<T>
    where T : BasePayroll, new()
    where P : IWageEmployee, new()
    where E : IPayrollEmployeeRepository<P>
{
    public BasePayrollEditor(IDocumentRepository<T> repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var repo = Services.Provider.GetService<E>();
        EmployeeRows = new DfDataGrid<P>(repo!) { Dock = DockStyle.Fill };

        EmployeeRows.CreateTableSummaryRow(VerticalPosition.Bottom)
            .AsSummary("Wage", SummaryColumnFormat.Currency, SelectOptions.All);

        EmployeeRows.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "EmployeeName":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "IncomeItemName":
                    args.Column.Width = 250;
                    break;
                case "Wage":
                    if (args.Column is GridNumericColumn c)
                    {
                        c.FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency;
                    }

                    args.Column.Width = 150;
                    args.Column.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

                    break;
            }
        };

        EmployeeRows.DataCreate += (sender, args) =>
        {
            args.Cancel = FormEmployeePayroll.Create(args.CreatingData) == DialogResult.Cancel;
        };

        EmployeeRows.DataEdit += (sender, args) =>
        {
            args.Cancel = FormEmployeePayroll.Edit(args.EditingData) == DialogResult.Cancel;
        };

        EmployeeRows.DataCopy += (sender, args) =>
        {
            args.Cancel = FormEmployeePayroll.Edit(args.CopiedData) == DialogResult.Cancel;
        };

        AddControls(new Control[]
        {
            EmployeeRows
        });
    }

    protected DfDataGrid<P> EmployeeRows { get; }
}