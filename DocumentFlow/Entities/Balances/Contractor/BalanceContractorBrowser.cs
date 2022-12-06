//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.12.6
//  - в конструктор добавлен параметр filter
//  - добавлена колонка contract_name
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances;

public class BalanceContractorBrowser : BalanceBrowser<BalanceContractor>, IBalanceContractorBrowser
{
    public BalanceContractorBrowser(IBalanceContractorRepository repository, IPageManager pageManager, IBalanceContractorFilter filter) 
        : base(repository, pageManager, filter) 
    {
        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn name = CreateText(x => x.document_type_name, "Документ", hidden: false);
        GridDateTimeColumn doc_date = CreateDateTime(x => x.document_date, "Дата", 150);
        GridNumericColumn doc_number = CreateNumeric(x => x.document_number, "Номер", 100);
        GridTextColumn contract_name = CreateText(x => x.contract_header, "Договор", 200);
        GridNumericColumn c_debt = CreateCurrency(x => x.contractor_debt, "Долг контрагента.", 150);
        GridNumericColumn o_debt = CreateCurrency(x => x.organization_debt, "Наш долг", 130);
        GridNumericColumn debt = CreateCurrency(x => x.debt, "Текущий долг", 130);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        c_debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        o_debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        debt.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        debt.CellStyle.BackColor = ColorTranslator.FromHtml("#FFC873");
        debt.HeaderStyle.BackColor = ColorTranslator.FromHtml("#FFC873");

        AddColumns(new GridColumn[] { id, name, doc_date, doc_number, contract_name, c_debt, o_debt, debt });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [doc_date] = ListSortDirection.Descending
        });

        AllowSorting = false;
    }

    protected override string HeaderText => "Расчёты с контрагентом";
}
