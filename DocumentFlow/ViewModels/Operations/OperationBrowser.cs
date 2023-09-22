//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class OperationBrowser : BrowserPage<Operation>, IOperationBrowser
{
    public OperationBrowser(IServiceProvider services, IPageManager pageManager, IOperationRepository repository, IConfiguration configuration, IBreadcrumb navigator) 
        : base(services, pageManager, repository, configuration, navigator: navigator) 
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.Code, "Код", width: 150);
        var name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var produced = CreateNumeric(x => x.Produced, "Выработка", width: 100);
        var prod_time = CreateNumeric(x => x.ProdTime, "Время выработки, сек.", width: 100);
        var production_rate = CreateNumeric(x => x.ProductionRate, "Норма выработки, ед./час", width: 100);
        var type_name = CreateText(x => x.TypeName, "Тип операции", width: 250);
        var salary = CreateNumeric(x => x.Salary, "Зар. плата, руб.", width: 100, decimalDigits: 4);
        var operation_using = CreateBoolean(x => x.OperationUsing, "Используется", 120);
        var date_norm = CreateDateTime(x => x.DateNorm, "Дата нормир.", 100, format: "dd.MM.yyyy", visible: false);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        produced.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        prod_time.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        production_rate.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        salary.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, code, name, produced, prod_time, production_rate, type_name, salary, operation_using, date_norm });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }
}
