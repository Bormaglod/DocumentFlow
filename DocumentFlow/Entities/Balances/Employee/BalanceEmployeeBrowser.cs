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

using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances;

public class BalanceEmployeeBrowser : BalanceBrowser<BalanceEmployee>, IBalanceEmployeeBrowser
{
    public BalanceEmployeeBrowser(IBalanceEmployeeRepository repository, IPageManager pageManager, IStandaloneSettings settings) : base(repository, pageManager, settings: settings) 
    {
        GridTextColumn id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.document_type_name, "Документ", hidden: false);
        GridDateTimeColumn doc_date = CreateDateTime(x => x.DocumentDate, "Дата", 150);
        GridNumericColumn doc_number = CreateNumeric(x => x.DocumentNumber, "Номер", 100);
        GridNumericColumn c_debt = CreateCurrency(x => x.employee_debt, "Долг сотрудника.", 150);
        GridNumericColumn o_debt = CreateCurrency(x => x.organization_debt, "Наш долг", 130);
        GridNumericColumn debt = CreateCurrency(x => x.debt, "Текущий долг", 130);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        c_debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        o_debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        debt.CellStyle.BackColor = ColorTranslator.FromHtml("#FFC873");
        debt.HeaderStyle.BackColor = ColorTranslator.FromHtml("#FFC873");

        AddColumns(new GridColumn[] { id, name, doc_date, doc_number, c_debt, o_debt, debt });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [doc_date] = ListSortDirection.Descending
        });

        AllowSorting = false;
    }

    protected override string HeaderText => "Расчёты с сотрудником";
}
