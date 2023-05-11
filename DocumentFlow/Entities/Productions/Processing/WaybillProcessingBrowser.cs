//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.07.2022
//
// Версия 2022.11.26
//  - изменен метод GetColumnsAfter
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.15
//  - изменено наследование с WaybillBrowser на Browser
//  - удалены колонки с ценой
// Версия 2023.1.17
//  - колонка "Счёт-фактура" переименована в "Накладная"
//  - удалены колонки invoice_date и invoice_number
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
// Версия 2023.5.11
//  - поля WaybillDate и WaybillNumber по умолчанию теперь видно
//  - корректировка ширины некоторых колонок
//  - изменен формат колонки OrderDate
//  - не формировался заголовок "Заказ" для колонок OrderDate и
//    OrderNumber. Исправлено
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Processing;

public class WaybillProcessingBrowser : Browser<WaybillProcessing>, IWaybillProcessingBrowser
{
    public WaybillProcessingBrowser(IWaybillProcessingRepository repository, IPageManager pageManager, IDocumentFilter filter, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, settings: settings)
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var waybill_date = CreateDateTime(x => x.WaybillDate, "Дата", width: 80, format: "dd.MM.yyyy");
        var waybill_number = CreateNumeric(x => x.WaybillNumber, "Номер", width: 80);
        var order_date = CreateDateTime(x => x.OrderDate, "Дата", width: 80, format: "dd.MM.yyyy");
        var order_number = CreateNumeric(x => x.OrderNumber, "Номер", width: 80);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var contract = CreateText(x => x.ContractName, "Договор", width: 200, visible: false);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, date, number, waybill_date, waybill_number, order_date, order_number, contractor, contract });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Накладная", new GridColumn[] { waybill_date, waybill_number });
        CreateStackedColumns("Заказ", new GridColumn[] { order_date, order_number });

        MoveToEnd();
    }

    protected override string HeaderText => "Поступление в переработку";
}
