//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Products;

public class MaterialUsageBrowser : Browser<MaterialUsage>, IMaterialUsageBrowser
{
    public MaterialUsageBrowser(IMaterialUsageRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        var id = CreateText(x => x.id, "Идентификатор", 180, visible: false);
        var goods_id = CreateText(x => x.goods_id, "Идентификатор изделия", 180, visible: false);
        var code = CreateText(x => x.goods_code, "Артикул", 140, visible: false);
        var goods_name = CreateText(x => x.goods_name, "Изделие", hidden: false);
        var calculation_name = CreateText(x => x.calculation_name, "Наименование калькуляции", 300, visible: false);
        var calculation_code = CreateText(x => x.calculation_code, "Калькуляция", 150);
        var amount = CreateNumeric(x => x.amount, "Количество", 120, decimalDigits: 3);

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
                pageManager.ShowEditor<IGoodsEditor>(CurrentDocument.goods_id);
            }
        });

        Toolbar.Add("Калькуляция", Resources.icons8_calculation_16, Resources.icons8_calculation_30, () =>
        {
            if (CurrentDocument != null)
            {
                pageManager.ShowEditor<ICalculationEditor>(CurrentDocument.id);
            }
        });
    }

    protected override string HeaderText => "Использование материала";
}
