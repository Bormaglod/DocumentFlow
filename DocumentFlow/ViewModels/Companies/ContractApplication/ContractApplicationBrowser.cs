//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class ContractApplicationBrowser : BrowserPage<ContractApplication>, IContractApplicationBrowser
{
    public ContractApplicationBrowser(IServiceProvider services, IContractApplicationRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Номер", width: 100);
        var document_date = CreateDateTime(x => x.DocumentDate, "Дата", width: 150, format: "dd.MM.yyyy");
        var name = CreateText(x => x.ItemName, "Наименование");
        var date_start = CreateDateTime(x => x.DateStart, "Дата начала", width: 150, format: "dd.MM.yyyy");
        var date_end = CreateDateTime(x => x.DateEnd, "Дата окончания", width: 150, format: "dd.MM.yyyy");

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, document_date, name, date_start, date_end });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [document_date] = ListSortDirection.Ascending,
            [code] = ListSortDirection.Ascending
        });
    }
}
