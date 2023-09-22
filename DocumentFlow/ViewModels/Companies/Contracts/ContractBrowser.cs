//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class ContractBrowser : BrowserPage<Contract>, IContractBrowser
{
    public ContractBrowser(IServiceProvider services, IPageManager pageManager, IContractRepository repository, IConfiguration configuration)
        : base(services, pageManager, repository, configuration)
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Номер", width: 180);
        var document_date = CreateDateTime(x => x.DocumentDate, "Дата", width: 150, format: "dd.MM.yyyy");
        var name = CreateText(x => x.ItemName, "Наименование");
        var tax_payer = CreateBoolean(x => x.TaxPayer, "Плательщик НДС", width: 150);
        var c_type = CreateText(x => x.CTypeText, "Вид договора", width: 150);
        var date_start = CreateDateTime(x => x.DateStart, "Дата начала", width: 150, format: "dd.MM.yyyy");
        var date_end = CreateDateTime(x => x.DateEnd, "Дата окончания", width: 150, format: "dd.MM.yyyy");
        var period = CreateNumeric(x => x.PaymentPeriod, "Срок оплаты, дней", width: 150, visible: false);
        var is_default = CreateBoolean(x => x.IsDefault, "Основной", width: 150);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, document_date, name, tax_payer, c_type, date_start, date_end, period, is_default });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [document_date] = ListSortDirection.Ascending,
            [code] = ListSortDirection.Ascending
        });
    }
}
