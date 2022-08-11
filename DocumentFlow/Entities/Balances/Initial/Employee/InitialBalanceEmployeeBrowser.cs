//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceEmployeeBrowser : Browser<InitialBalanceEmployee>, IInitialBalanceEmployeeBrowser
{
    public InitialBalanceEmployeeBrowser(IInitialBalanceEmployeeRepository repository, IPageManager pageManager)
        : base(repository, pageManager)
    {
        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var contractor_name = CreateText(x => x.employee_name, "Сотрудник");
        var our_debt = CreateCurrency(x => x.OurDebt, "Наш", width: 120);
        var employee_debt = CreateCurrency(x => x.EmployeeDebt, "Сотрудника", width: 120);

        contractor_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        CreateStackedColumns("Долг", new GridColumn[] { our_debt, employee_debt });

        AddColumns(new GridColumn[] { id, date, number, contractor_name, our_debt, employee_debt });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Нач. остатки";
}
