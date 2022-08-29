//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//
// Версия 2022.8.29
//  - добавлен метод BrowserCellStyle
//  - добавлен меню для установки состояния партии
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid.Styles;

namespace DocumentFlow.Entities.Productions.Lot;

public class BaseProductionLotBrowser : Browser<ProductionLot>
{
    private readonly IProductionLotRepository repository;

    public BaseProductionLotBrowser(IProductionLotRepository repository, IPageManager pageManager, IDocumentFilter? filter = null) 
        : base(repository, pageManager, filter: filter)
    {
        this.repository = repository;

        var menuState = ContextMenu.Add("Состояние");

        foreach (var item in Enum.GetValues(typeof(LotState)))
        {
            var menu = ContextMenu.Add(ProductionLot.StateNameFromValue((LotState)item), LotChangeState, menuState);
            menu.Tag = item;
        }
    }

    protected override string HeaderText => "Партия";

    protected override void BrowserCellStyle(ProductionLot document, string column, CellStyleInfo style)
    {
        base.BrowserCellStyle(document, column, style);

        if (column == "state_name")
        {
            style.TextColor = document.LotState switch
            {
                LotState.Created => Color.Black,
                LotState.Production => Color.Green,
                LotState.Completed => Color.Red,
                _ => Color.Orange
            };
        }
    }

    private void LotChangeState(object? data)
    {
        if (data is LotState state && CurrentDocument != null)
        {
            repository.SetState(CurrentDocument, state);
            RefreshGrid();
        }
    }
}
