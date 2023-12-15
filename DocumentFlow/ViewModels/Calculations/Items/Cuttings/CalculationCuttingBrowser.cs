//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Properties;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class CalculationCuttingBrowser : BrowserPage<CalculationCutting>, ICalculationCuttingBrowser
{
    public CalculationCuttingBrowser(IServiceProvider services, ICalculationCuttingRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration) 
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var operation_name = CreateText(x => x.OperationName, "Производственная операция", width: 400, visible: false);
        var equipment_name = CreateText(x => x.EquipmentName, "Оборудование", width: 250, visible: false);
        var tools_name = CreateText(x => x.ToolsName, "Инструмент", width: 250, visible: false);

        var code = CreateText(x => x.Code, "Код", width: 100);
        var item_name = CreateText(x => x.ItemName, "Наименование", hidden: false);
        var using_operations = CreateText(x => x.UsingOperationsValue, "Используется в...", width: 150);
        var material_name = CreateText(x => x.MaterialName, "Материал", width: 150);
        var material_amount = CreateNumeric(x => x.MaterialAmount, "Количество", width: 130, decimalDigits: 3);
        var price = CreateNumeric(x => x.Price, "Расценка", width: 100, decimalDigits: 4);
        var stimul_cost = CreateCurrency(x => x.StimulCost, "Стимул. выпл.", width: 100);
        var repeats = CreateNumeric(x => x.Repeats, "Кол-во повторов", width: 100);
        var produced_time = CreateNumeric(x => x.ProducedTime, "Время, с", width: 100, decimalDigits: 1);
        var total_material = CreateNumeric(x => x.TotalMaterial, "Количество", width: 130, decimalDigits: 3);
        var item_cost = CreateCurrency(x => x.ItemCost, "Стоимость", width: 120);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsCount(item_name, "Всего наименований: {?}")
            .AsSummary(material_amount)
            .AsSummary(total_material)
            .AsSummary(item_cost, SummaryColumnFormat.Currency)
            .AsSummary(produced_time);

        item_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        material_amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        total_material.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        produced_time.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        repeats.AllowHeaderTextWrapping = true;
        stimul_cost.AllowHeaderTextWrapping = true;

        AddColumns(new GridColumn[] { id, code, item_name, operation_name, using_operations, material_name, material_amount, price, repeats, stimul_cost, produced_time, total_material, item_cost, equipment_name, tools_name });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [code] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Использование материала", new GridColumn[] { material_name, material_amount });
        CreateStackedColumns("С учётом повторов за ед. изм.", new GridColumn[] { stimul_cost, produced_time, total_material, item_cost });

        AllowSorting = false;

        ShowToolTip = (d) => d?.Note ?? string.Empty;

        ToolBar.Add("Производственная операция", Resources.icons8_robot_16, Resources.icons8_robot_30, OpenOperation);
        ToolBar.Add("Используемый материал", Resources.icons8_goods_16, Resources.icons8_goods_30, OpenMaterial);
        ToolBar.AddSeparator();

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

        ContextMenu.AddItems(new IContextMenuItem[]
        {
            ContextMenu.CreateItem("Производственная операция", Resources.icons8_robot_16, (s, e) => OpenOperation()),
            ContextMenu.CreateItem("Используемый материал", Resources.icons8_goods_16, (s, e) => OpenMaterial())
        });
    }

    private void OpenOperation()
    {
        if (CurrentDocument != null && CurrentDocument.ItemId != null)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(ICuttingEditor), CurrentDocument.ItemId.Value));
        }
    }

    private void OpenMaterial()
    {
        if (CurrentDocument != null && CurrentDocument.MaterialId != null)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IMaterialEditor), CurrentDocument.MaterialId.Value));
        }
    }
}
