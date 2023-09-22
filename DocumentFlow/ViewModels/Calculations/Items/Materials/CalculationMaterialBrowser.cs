﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Models;
using DocumentFlow.Dialogs;
using DocumentFlow.Interfaces;
using DocumentFlow.Properties;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Styles;

using System.ComponentModel;

using SyncEnums = Syncfusion.WinForms.DataGrid.Enums;
using WinEnums = System.Windows.Forms;

namespace DocumentFlow.ViewModels;

public class CalculationMaterialBrowser : BrowserPage<CalculationMaterial>, ICalculationMaterialBrowser
{
    private readonly IPageManager pageManager;

    public CalculationMaterialBrowser(IServiceProvider services, IPageManager pageManager, ICalculationMaterialRepository repository, IConfiguration configuration) 
        : base(services, pageManager, repository, configuration) 
    {
        this.pageManager = pageManager;

        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var material_name = CreateText(x => x.MaterialName, "Материал", hidden: false);
        var amount = CreateNumeric(x => x.Amount, "Количество", width: 150, decimalDigits: 3);
        var price = CreateCurrency(x => x.Price, "Цена", width: 150);
        var method = CreateText(x => x.MethodPriceName, "Тип цены", width: 150);
        var item_cost = CreateCurrency(x => x.ItemCost, "Стоимость", width: 150);
        var weight = CreateNumeric(x => x.Weight, "Вес, г", width: 100, decimalDigits: 3);
        var giving = CreateBoolean(x => x.IsGiving, "Давальческий", width: 150);

        CreateSummaryRow(SyncEnums.VerticalPosition.Bottom)
            .AsCount(material_name, "Всего наименований: {?}")
            .AsSummary(amount)
            .AsSummary(item_cost, SummaryColumnFormat.Currency)
            .AsSummary(weight);

        material_name.AutoSizeColumnsMode = SyncEnums.AutoSizeColumnsMode.Fill;
        amount.CellStyle.HorizontalAlignment = WinEnums.HorizontalAlignment.Right;
        weight.CellStyle.HorizontalAlignment = WinEnums.HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, material_name, amount, price, method, item_cost, weight, giving });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [material_name] = ListSortDirection.Ascending
        });

        AllowSorting = false;

        ToolBar.Add("Материал", Resources.icons8_goods_16, Resources.icons8_goods_30, OpenMaterial);
        ToolBar.AddSeparator();

        ToolBar.Add("Пересчитать количество", Resources.icons8_sigma_16, Resources.icons8_sigma_30, () =>
        {
            if (OwnerDocument != null)
            {
                repository.RecalculateCount(OwnerDocument.Value);
                RefreshView();
            }
        });

        ToolBar.Add("Пересчитать стоимость", Resources.icons8_calculate_16, Resources.icons8_calculate_30, () =>
        {
            if (OwnerDocument != null)
            {
                repository.RecalculatePrices(OwnerDocument.Value);
                RefreshView();
            }
        });

        ToolBar.Add("Очистить", Resources.icons8_broomstick_16, Resources.icons8_broomstick_30, () =>
        {
            if (OwnerDocument != null && MessageBox.Show("Записи помеченные на удаление будут безвозвратно удалены. Продолжить удаление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                repository.WipeAll(OwnerDocument.Value);
                RefreshView();
            }
        });

        ContextMenu.Add("Давальческий/собственный материал", Resources.icons8_sell_16, (s, e) =>
        {
            if (CurrentDocument != null)
            {
                CurrentDocument.IsGiving = !CurrentDocument.IsGiving;
                repository.Update(CurrentDocument);
                RefreshRow(CurrentDocument);
            }
        });

        var menuPrice = ContextMenu
            .AddSeparator()
            .Add("Тип цены");


        menuPrice.Add("Средняя цена", (s, e) => ChangePriceSettingMethod(repository, PriceSettingMethod.Average));

        menuPrice.Add("Цена из справочнка", (s, e) => ChangePriceSettingMethod(repository, PriceSettingMethod.Dictionary));

        menuPrice.Add("Установлена вручную", (s, e) => ChangePriceSettingMethod(repository, PriceSettingMethod.Manual));

        ContextMenu.Add("Используемый материал", Resources.icons8_goods_16, (s, e) => OpenMaterial());
    }

    protected override void BrowserCellStyle(CalculationMaterial document, string column, CellStyleInfo style)
    {
        base.BrowserCellStyle(document, column, style);
        if (column == "MethodPriceName")
        {
            style.TextColor = document.MethodPrice switch
            {
                PriceSettingMethod.Manual => Color.Red,
                PriceSettingMethod.Dictionary => Color.Blue,
                _ => Color.Black
            };
        }
    }

    private void OpenMaterial()
    {
        if (CurrentDocument != null && CurrentDocument.ItemId != null)
        {
            pageManager.ShowAssociateEditor<IMaterialBrowser>(CurrentDocument.ItemId.Value);
        }
    }

    private void ChangePriceSettingMethod(ICalculationMaterialRepository repository, PriceSettingMethod method)
    {
        if (CurrentDocument != null)
        {
            if (CurrentDocument.IsGiving) 
            {
                MessageBox.Show("Материал является давальческим - тип цены установить невозможно.");
                return;
            }
            if (method == PriceSettingMethod.Manual)
            {
                if (InputCurrencyDialog.ShowDialog(out decimal newPrice))
                {
                    CurrentDocument.Price = newPrice;
                }
            }

            CurrentDocument.MethodPrice = method;
            repository.Update(CurrentDocument);
            RefreshRow(CurrentDocument);
        }
    }
}