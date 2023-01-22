//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Calculations;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Operations;

public class OperationUsageBrowser : Browser<OperationUsage>, IOperationUsageBrowser
{
    public OperationUsageBrowser(IOperationUsageRepository repository, IPageManager pageManager) : base(repository, pageManager)
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
            [calculation_code] = ListSortDirection.Ascending
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

    protected override string HeaderText => "Использование операции";
}
