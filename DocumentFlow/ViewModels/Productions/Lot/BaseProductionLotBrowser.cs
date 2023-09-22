//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid.Styles;

namespace DocumentFlow.ViewModels;

public abstract class BaseProductionLotBrowser : BrowserPage<ProductionLot>
{
    private readonly IProductionLotRepository repository;
    private readonly Dictionary<IContextMenuItem, LotState> menu = new();

    public BaseProductionLotBrowser(IServiceProvider services, IPageManager pageManager, IProductionLotRepository repository, IConfiguration configuration, IDocumentFilter? filter = null) 
        : base(services, pageManager, repository, configuration, filter: filter)
    {
        this.repository = repository;

        var menuState = ContextMenu.Add("Состояние");

        foreach (LotState item in Enum.GetValues(typeof(LotState)))
        {
            var menuItem = ContextMenu.CreateItem(item.Description(), LotChangeState);
            menu.Add(menuItem, item);
        }

        ContextMenu.AddItems(menu.Keys.ToArray());
    }

    protected override void BrowserCellStyle(ProductionLot document, string column, CellStyleInfo style)
    {
        base.BrowserCellStyle(document, column, style);

        if (column == "StateName")
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

    private void LotChangeState(object? sender, EventArgs args)
    {
        if (CurrentDocument != null && sender is IContextMenuItem item)
        {
            if (menu.TryGetValue(item, out var state)) 
            {
                repository.SetState(CurrentDocument, state);
                RefreshGrid();
            }
        }
    }
}
