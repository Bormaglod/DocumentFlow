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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Calculations;

public class CalculationMaterialBrowser : Browser<CalculationMaterial>, ICalculationMaterialBrowser
{
    public CalculationMaterialBrowser(ICalculationMaterialRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        GridTextColumn id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        GridTextColumn material_name = CreateText(x => x.material_name, "Материал", hidden: false);
        GridNumericColumn amount = CreateNumeric(x => x.amount, "Количество", width: 150, decimalDigits: 3);
        GridNumericColumn price = CreateCurrency(x => x.price, "Цена", width: 150);
        GridNumericColumn item_cost = CreateCurrency(x => x.item_cost, "Стоимость", width: 150);
        GridNumericColumn weight = CreateNumeric(x => x.weight, "Вес, г", width: 100, decimalDigits: 3);
        GridCheckBoxColumn giving = CreateBoolean(x => x.is_giving, "Давальческий", width: 150);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsCount(material_name, "Всего наименований: {?}")
            .AsSummary(amount)
            .AsSummary(item_cost, SummaryColumnFormat.Currency)
            .AsSummary(weight);

        material_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        weight.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, material_name, amount, price, item_cost, weight, giving });
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

        ContextMenu.Add("Давальческий/собственный материал", Resources.icons8_sell_16, (t) =>
        {
            if (CurrentDocument != null)
            {
                CurrentDocument.is_giving = !CurrentDocument.is_giving;
                repository.Update(CurrentDocument);
                RefreshRow(CurrentDocument);
            }
        });

        ContextMenu.Add("Используемый материал", Resources.icons8_goods_16, (_) => OpenMaterial(pageManager));
    }

    protected override string HeaderText => "Материалы";

    private void OpenMaterial(IPageManager pageManager)
    {
        if (CurrentDocument != null && CurrentDocument.item_id != null)
        {
            pageManager.ShowEditor<IMaterialEditor>(CurrentDocument.item_id.Value);
        }
    }
}
