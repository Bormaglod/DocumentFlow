//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2022.12.3
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Wages.Core;
using DocumentFlow.Entities.Wages.Dialogs;
using DocumentFlow.Infrastructure;

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
            .AsSummary("wage", SummaryColumnFormat.Currency, SelectOptions.All);

        EmployeeRows.AutoGeneratingColumn += (sender, args) =>
        {
            switch (args.Column.MappingName)
            {
                case "employee_name":
                    args.Column.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
                    break;
                case "income_item_name":
                    args.Column.Width = 250;
                    break;
                case "wage":
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