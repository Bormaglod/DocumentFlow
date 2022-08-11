//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceContractorBrowser : Browser<InitialBalanceContractor>, IInitialBalanceContractorBrowser
{
    public InitialBalanceContractorBrowser(IInitialBalanceContractorRepository repository, IPageManager pageManager)
        : base(repository, pageManager)
    {
        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var contractor_name = CreateText(x => x.contractor_name, "Контрагент");
        var contract_date = CreateDateTime(x => x.contract_date, "Дата", width: 150, format: "dd.MM.yyyy");
        var contract_number = CreateText(x => x.contract_number, "Номер", width: 100);
        var contract_name = CreateText(x => x.contract_name, "Наименование", width: 250);
        var our_debt = CreateCurrency(x => x.OurDebt, "Наш", width: 120);
        var contractor_debt = CreateCurrency(x => x.ContractorDebt, "Контрагента", width: 120);

        contractor_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        CreateStackedColumns("Договор", new GridColumn[] { contract_date, contract_number, contract_name });
        CreateStackedColumns("Долг", new GridColumn[] { our_debt, contractor_debt });

        AddColumns(new GridColumn[] { id, date, number, contractor_name, contract_date, contract_number, contract_name, our_debt, contractor_debt });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Нач. остатки";
}
