﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//
// Версия 2022.8.29
//  - добавлено столбец state_name и метод BrowserCellStyle
//  - добавлено столбец execute_percent
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Productions.Lot;

public class ProductionLotNestedBrowser : BaseProductionLotBrowser, IProductionLotNestedBrowser
{
    public ProductionLotNestedBrowser(IProductionLotRepository repository, IPageManager pageManager, IStandaloneSettings settings)
        : base(repository, pageManager, settings: settings)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var goods = CreateText(x => x.goods_name, "Изделие", hidden: false);
        var calculation = CreateText(x => x.calculation_name, "Калькуляция", width: 150);
        var quantity = CreateNumeric(x => x.quantity, "Количество", width: 150, hidden: false, decimalDigits: 3);
        var state = CreateText(x => x.state_name, "Состояние", width: 150);
        var execute_percent = CreateProgress(x => x.execute_percent, "Выполнено", 100);

        goods.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        quantity.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        state.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsSummary(quantity);

        AddColumns(new GridColumn[] { id, date, number, goods, calculation, quantity, state, execute_percent });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });
    }
}