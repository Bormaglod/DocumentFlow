﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.01.2022
//
// Версия 2022.11.13
//  - добавлены кнопки для открытия окон редактирования операции и
//    материала
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Calculations;

public class CalculationOperationBrowser : Browser<CalculationOperation>, ICalculationOperationBrowser
{
    public CalculationOperationBrowser(ICalculationOperationRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var equipment_name = CreateText(x => x.equipment_name, "Оборудование", width: 250, visible: false);
        var tools_name = CreateText(x => x.tools_name, "Инструмент", width: 250, visible: false);
        var operation_name = CreateText(x => x.operation_name, "Производственная операция", width: 300, visible: false);

        var code = CreateText(x => x.code, "Код", width: 100);
        var item_name = CreateText(x => x.item_name, "Наименование", hidden: false);
        var material_name = CreateText(x => x.material_name, "Материал", width: 140);
        var previous_operation = CreateText(x => x.previous_operation_value, "Пред. операция", width: 150);
        var using_operations = CreateText(x => x.using_operations_value, "Используется в...", width: 150);
        var material_amount = CreateNumeric(x => x.material_amount, "Количество", width: 120, decimalDigits: 3);
        var price = CreateNumeric(x => x.price, "Расценка", width: 100, decimalDigits: 4);
        var stimul_cost = CreateCurrency(x => x.stimul_cost, "Стимул. выпл.", width: 100);
        var total_material = CreateNumeric(x => x.total_material, "Количество", width: 120, decimalDigits: 3);
        var produced_time = CreateNumeric(x => x.produced_time, "Время, с", width: 100, decimalDigits: 1);
        var item_cost = CreateCurrency(x => x.item_cost, "Стоимость", width: 110);
        var repeats = CreateNumeric(x => x.repeats, "Кол-во повторов", width: 100);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsCount(item_name, "Всего наименований: {?}")
            .AsSummary(material_amount)
            .AsSummary(total_material)
            .AsSummary(produced_time)
            .AsSummary(item_cost, SummaryColumnFormat.Currency);

        item_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        material_amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        total_material.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        produced_time.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        repeats.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        price.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        repeats.AllowHeaderTextWrapping = true;
        stimul_cost.AllowHeaderTextWrapping = true;

        AddColumns(new GridColumn[] { id, code, item_name, operation_name, previous_operation, using_operations, material_name, material_amount, price, repeats, stimul_cost, produced_time, total_material, item_cost, equipment_name, tools_name });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Использование материала", new GridColumn[] { material_name, material_amount });
        CreateStackedColumns("С учётом повторов за ед. изм.", new GridColumn[] { stimul_cost, produced_time, total_material, item_cost });

        AllowSorting = false;

        ShowToolTip = (d) => d?.note ?? string.Empty;

        Toolbar.Add("Производственная операция", Resources.icons8_robot_16, Resources.icons8_robot_30, () => OpenOperation(pageManager));
        Toolbar.Add("Используемый материал", Resources.icons8_goods_16, Resources.icons8_goods_30, () => OpenMaterial(pageManager));
        Toolbar.AddSeparator();

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

        ContextMenu.Add("Производственная операция", Resources.icons8_robot_16, (_) => OpenOperation(pageManager));
        ContextMenu.Add("Используемый материал", Resources.icons8_goods_16, (_) => OpenMaterial(pageManager), false);
    }

    protected override string HeaderText => "Операция";

    private void OpenOperation(IPageManager pageManager)
    {
        if (CurrentDocument != null && CurrentDocument.material_id != null)
        {
            pageManager.ShowEditor<IMaterialEditor>(CurrentDocument.material_id.Value);
        }
    }

    private void OpenMaterial(IPageManager pageManager)
    {
        if (CurrentDocument != null && CurrentDocument.material_id != null)
        {
            pageManager.ShowEditor<IMaterialEditor>(CurrentDocument.material_id.Value);
        }
    }
}
