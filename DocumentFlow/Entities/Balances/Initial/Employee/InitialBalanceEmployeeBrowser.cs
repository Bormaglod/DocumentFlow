//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceEmployeeBrowser : Browser<InitialBalanceEmployee>, IInitialBalanceEmployeeBrowser
{
    public InitialBalanceEmployeeBrowser(IInitialBalanceEmployeeRepository repository, IPageManager pageManager, IStandaloneSettings settings)
        : base(repository, pageManager, settings: settings)
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
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
