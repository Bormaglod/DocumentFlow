//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.6.3
//  - добавлена колонка PaymentPeriod
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Companies;

public class ContractBrowser : Browser<Contract>, IContractBrowser
{
    public ContractBrowser(IContractRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

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

    protected override string HeaderText => "Договоры";
}
