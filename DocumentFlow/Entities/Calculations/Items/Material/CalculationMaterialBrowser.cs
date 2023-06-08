//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//
// Версия 2022.8.28
//  - из-за изменений в ConextMenu добавлен параметр в вызов Add
// Версия 2022.11.13
//  - добавлена кнопка для открытия окна редактирования материала
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.6.8
//  - добавлена колонка MethodPriceName
//  - добавлено контекстное меню "Тип цены" с соответствующими подпунктами
//    и их реализация
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DocumentFlow.Entities.Calculations;

public class CalculationMaterialBrowser : Browser<CalculationMaterial>, ICalculationMaterialBrowser
{
    public CalculationMaterialBrowser(ICalculationMaterialRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var material_name = CreateText(x => x.MaterialName, "Материал", hidden: false);
        var amount = CreateNumeric(x => x.Amount, "Количество", width: 150, decimalDigits: 3);
        var price = CreateCurrency(x => x.Price, "Цена", width: 150);
        var method = CreateText(x => x.MethodPriceName, "Тип цены", width: 150);
        var item_cost = CreateCurrency(x => x.ItemCost, "Стоимость", width: 150);
        var weight = CreateNumeric(x => x.Weight, "Вес, г", width: 100, decimalDigits: 3);
        var giving = CreateBoolean(x => x.IsGiving, "Давальческий", width: 150);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsCount(material_name, "Всего наименований: {?}")
            .AsSummary(amount)
            .AsSummary(item_cost, SummaryColumnFormat.Currency)
            .AsSummary(weight);

        material_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        weight.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, material_name, amount, price, method, item_cost, weight, giving });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [material_name] = ListSortDirection.Ascending
        });

        AllowSorting = false;

        Toolbar.Add("Материал", Resources.icons8_goods_16, Resources.icons8_goods_30, () => OpenMaterial(pageManager));
        Toolbar.AddSeparator();

        Toolbar.Add("Пересчитать количество", Resources.icons8_sigma_16, Resources.icons8_sigma_30, () =>
        {
            if (OwnerDocument != null)
            {
                repository.RecalculateCount(OwnerDocument.Value);
                RefreshView();
            }
        });

        Toolbar.Add("Пересчитать стоимость", Resources.icons8_calculate_16, Resources.icons8_calculate_30, () =>
        {
            if (OwnerDocument != null)
            {
                repository.RecalculatePrices(OwnerDocument.Value);
                RefreshView();
            }
        });

        Toolbar.Add("Очистить", Resources.icons8_broomstick_16, Resources.icons8_broomstick_30, () =>
        {
            if (OwnerDocument != null && MessageBox.Show("Записи помеченные на удаление будут безвозвратно удалены. Продолжить удаление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                repository.WipeAll(OwnerDocument.Value);
                RefreshView();
            }
        });

        ContextMenu.Add("Давальческий/собственный материал", Resources.icons8_sell_16, _ =>
        {
            if (CurrentDocument != null)
            {
                CurrentDocument.IsGiving = !CurrentDocument.IsGiving;
                repository.Update(CurrentDocument);
                RefreshRow(CurrentDocument);
            }
        });

        var menuPrice = ContextMenu.Add("Тип цены", addSeparator: false);

        ContextMenu.Add("Средняя цена",
                        _ => ChangePriceSettingMethod(repository, PriceSettingMethod.Average),
                        menuPrice);

        ContextMenu.Add("Цена из справочнка", 
                        _ => ChangePriceSettingMethod(repository, PriceSettingMethod.Dictionary),
                        menuPrice);

        ContextMenu.Add("Установлена вручную",
                        _ => ChangePriceSettingMethod(repository, PriceSettingMethod.Manual),
                        menuPrice);

        ContextMenu.Add("Используемый материал", Resources.icons8_goods_16, _ => OpenMaterial(pageManager));
    }

    protected override string HeaderText => "Материалы";

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

    private void OpenMaterial(IPageManager pageManager)
    {
        if (CurrentDocument != null && CurrentDocument.ItemId != null)
        {
            pageManager.ShowEditor<IMaterialEditor>(CurrentDocument.ItemId.Value);
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

            CurrentDocument.SetPriceSettingMethod(method);
            repository.Update(CurrentDocument);
            RefreshRow(CurrentDocument);
        }
    }
}
