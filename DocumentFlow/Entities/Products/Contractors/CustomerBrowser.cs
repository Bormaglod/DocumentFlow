//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Products;

public class СustomerBrowser : Browser<Сustomer>, IСustomerBrowser
{
    public СustomerBrowser(IСustomerRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.code, "Наименование", hidden: false);
        var doc_name = CreateText(x => x.doc_name, "Наименование документа", width: 400);
        var doc_number = CreateText(x => x.doc_number, "Номер документа", width: 150);
        var date_start = CreateDateTime(x => x.date_start, "Начиная с...", width: 120, format: "dd.MM.yyyy");
        var price = CreateCurrency(x => x.price, "Цена", width: 100);
        code.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, doc_name, doc_number, date_start, price });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });

        Toolbar.Add("Контрагент", Resources.icons8_contractor_16, Resources.icons8_contractor_30, () =>
        {
            if (CurrentDocument != null)
            {
                pageManager.ShowEditor<IContractorEditor>(CurrentDocument.Id);
            }
        });

        Toolbar.Add("Договор", Resources.icons8_contract_16, Resources.icons8_contract_30, () =>
        {
            if (CurrentDocument != null && CurrentDocument.contract_id != null)
            {
                pageManager.ShowEditor<IContractEditor>(CurrentDocument.contract_id.Value);
            }
        });

        Toolbar.Add("Приложение", Resources.icons8_contract_app_16, Resources.icons8_contract_app_30, () =>
        {
            if (CurrentDocument != null && CurrentDocument.application_id != null)
            {
                pageManager.ShowEditor<IContractApplicationEditor>(CurrentDocument.application_id.Value);
            }
        });
    }

    protected override string HeaderText => "Контрагенты";
}
