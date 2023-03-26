//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Settings;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Products;

public class MaterialUsageBrowser : Browser<MaterialUsage>, IMaterialUsageBrowser
{
    public MaterialUsageBrowser(IMaterialUsageRepository repository, IPageManager pageManager, IStandaloneSettings settings) 
        : base(repository, pageManager, settings: settings)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.Id, "Идентификатор", 180, visible: false);
        var goods_id = CreateText(x => x.GoodsId, "Идентификатор изделия", 180, visible: false);
        var code = CreateText(x => x.GoodsCode, "Артикул", 140, visible: false);
        var goods_name = CreateText(x => x.GoodsName, "Изделие", hidden: false);
        var calculation_name = CreateText(x => x.CalculationName, "Наименование калькуляции", 300, visible: false);
        var calculation_code = CreateText(x => x.CalculationCode, "Калькуляция", 150);
        var amount = CreateNumeric(x => x.Amount, "Количество", 120, decimalDigits: 3);

        goods_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

        AddColumns(new GridColumn[] { id, goods_id, code, goods_name, calculation_name, calculation_code, amount });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [goods_name] = ListSortDirection.Ascending,
            [calculation_name] = ListSortDirection.Ascending
        });

        Toolbar.Add("Изделие", Resources.icons8_goods_16, Resources.icons8_goods_30, () =>
        {
            if (CurrentDocument != null)
            {
                pageManager.ShowEditor<IGoodsEditor>(CurrentDocument.GoodsId);
            }
        });

        Toolbar.Add("Калькуляция", Resources.icons8_calculation_16, Resources.icons8_calculation_30, () =>
        {
            if (CurrentDocument != null)
            {
                pageManager.ShowEditor<ICalculationEditor>(CurrentDocument.Id);
            }
        });
    }

    protected override string HeaderText => "Использование материала";
}
