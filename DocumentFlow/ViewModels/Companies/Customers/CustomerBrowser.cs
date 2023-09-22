//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Properties;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class СustomerBrowser : BrowserPage<Customer>, IСustomerBrowser
{
    public СustomerBrowser(IServiceProvider services, IPageManager pageManager, ICustomerRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration) 
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Наименование", hidden: false);
        var doc_name = CreateText(x => x.DocName, "Наименование документа", width: 400);
        var doc_number = CreateText(x => x.DocNumber, "Номер документа", width: 150);
        var date_start = CreateDateTime(x => x.DateStart, "Начиная с...", width: 120, format: "dd.MM.yyyy");
        var price = CreateCurrency(x => x.Price, "Цена", width: 100);
        code.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, doc_name, doc_number, date_start, price });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });

        ToolBar.Add("Контрагент", Resources.icons8_contractor_16, Resources.icons8_contractor_30, () =>
        {
            if (CurrentDocument != null)
            {
                pageManager.ShowAssociateEditor<IContractorBrowser>(CurrentDocument.Id);
            }
        });

        ToolBar.Add("Договор", Resources.icons8_contract_16, Resources.icons8_contract_30, () =>
        {
            if (CurrentDocument != null && CurrentDocument.ContractId != null)
            {
                pageManager.ShowAssociateEditor<IContractBrowser>(CurrentDocument.ContractId.Value);
            }
        });

        ToolBar.Add("Приложение", Resources.icons8_contract_app_16, Resources.icons8_contract_app_30, () =>
        {
            if (CurrentDocument != null && CurrentDocument.ApplicationId != null)
            {
                pageManager.ShowAssociateEditor<IContractApplicationBrowser>(CurrentDocument.ApplicationId.Value);
            }
        });
    }
}
