//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2021
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

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

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn code = CreateText(x => x.code, "Номер", width: 180);
        GridDateTimeColumn document_date = CreateDateTime(x => x.document_date, "Дата", width: 150, format: "dd.MM.yyyy");
        GridTextColumn name = CreateText(x => x.item_name, "Наименование");
        GridCheckBoxColumn tax_payer = CreateBoolean(x => x.tax_payer, "Плательщик НДС", width: 150);
        GridTextColumn c_type = CreateText(x => x.c_type_text, "Вид договора", width: 150);
        GridDateTimeColumn date_start = CreateDateTime(x => x.date_start, "Дата начала", width: 150, format: "dd.MM.yyyy");
        GridDateTimeColumn date_end = CreateDateTime(x => x.date_end, "Дата окончания", width: 150, format: "dd.MM.yyyy");
        GridCheckBoxColumn is_default = CreateBoolean(x => x.is_default, "Основной", width: 150);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, document_date, name, tax_payer, c_type, date_start, date_end, is_default });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [document_date] = ListSortDirection.Ascending,
            [code] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Договоры";
}
