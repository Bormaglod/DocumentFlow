//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.07.2022
//
// Версия 2023.1.5
//  - добавлен вызов MoveToEnd для перемещения в конец таблицы
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
// Версия 2023.5.11
//  - корректировка ширины некоторых колонок
//  - изменен формат колонки OrderDate
//  - не формировался заголовок "Заказ" для колонок OrderDate и
//    OrderNumber. Исправлено
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Returns;

public class ReturnMaterialsBrowser : Browser<ReturnMaterials>, IReturnMaterialsBrowser
{
    public ReturnMaterialsBrowser(IReturnMaterialsRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var order_date = CreateDateTime(x => x.OrderDate, "Дата", width: 80, format: "dd.MM.yyyy");
        var order_number = CreateNumeric(x => x.OrderNumber, "Номер", width: 80);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var contract = CreateText(x => x.ContractName, "Договор", width: 200, visible: false);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, order_date, order_number, contractor, contract });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Заказ", new GridColumn[] { order_date, order_number });

        MoveToEnd();
    }

    protected override string HeaderText => "Возврат материалов заказчику";
}
