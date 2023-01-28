//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Wages;

public class PayrollBrowser : Browser<Payroll>, IPayrollBrowser
{
    public PayrollBrowser(IPayrollRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var billing_range = CreateText(x => x.billing_range, "Расчётный период", width: 160);
        var emps = CreateText(x => x.employee_names_text, "Сотрудники");
        var wage = CreateCurrency(x => x.wage, "Сумма к выдаче", width: 150);

        emps.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, billing_range, emps, wage });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [billing_range] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Платёжная ведомость";
}
