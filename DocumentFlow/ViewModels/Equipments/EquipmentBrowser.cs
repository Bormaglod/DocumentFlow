//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class EquipmentBrowser : BrowserPage<Equipment>, IEquipmentBrowser
{
    public EquipmentBrowser(IServiceProvider services, IPageManager pageManager, IEquipmentRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration) 
    {
        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Код", width: 110);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var serial = CreateText(x => x.SerialNumber, "Серийный номер", width: 100);
        var is_tools = CreateBoolean(x => x.IsTools, "Инструмент", width: 160);
        var commissioning = CreateDateTime(x => x.Commissioning, "Ввод в экспл.", width: 150, format: "dd.MM.yyyy");
        var starting_hits = CreateNumeric(x => x.StartingHits, "Кол-во опрессовок", width: 100, visible: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, code, name, serial, commissioning, starting_hits, is_tools });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });
    }
}
