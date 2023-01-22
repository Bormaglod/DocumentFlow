//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//
// Версия 2022.8.28
//  - время выработки заменено с мин на сек
// Версия 2022.9.3
//  - удалены методы IsColumnVisible и IsAllowVisibilityColumn
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Operations;

public class OperationBrowser : Browser<Operation>, IOperationBrowser
{
    public OperationBrowser(IOperationRepository repository, IPageManager pageManager, IBreadcrumb navigator, IStandaloneSettings settings) 
        : base(repository, pageManager, navigator: navigator, settings: settings) 
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var code = CreateText(x => x.code, "Код", width: 150);
        var name = CreateText(x => x.item_name, "Наименование", hidden: false);
        var produced = CreateNumeric(x => x.produced, "Выработка", width: 100);
        var prod_time = CreateNumeric(x => x.prod_time, "Время выработки, сек.", width: 100);
        var production_rate = CreateNumeric(x => x.production_rate, "Норма выработки, ед./час", width: 100);
        var type_name = CreateText(x => x.type_name, "Тип операции", width: 250);
        var salary = CreateNumeric(x => x.salary, "Зар. плата, руб.", width: 100, decimalDigits: 4);
        var operation_using = CreateBoolean(x => x.operation_using, "Используется", 120);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        produced.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        prod_time.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        production_rate.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        salary.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, code, name, produced, prod_time, production_rate, type_name, salary, operation_using });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });
    }

    protected override string HeaderText => "Произв. операции";
}
