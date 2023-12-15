//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Properties;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class OperationUsageBrowser : BrowserPage<OperationUsage>, IOperationUsageBrowser
{
    public OperationUsageBrowser(IServiceProvider services, IOperationUsageRepository repository, IConfiguration configuration) 
        : base(services, repository, configuration)
    {
        ToolBar.SmallIcons();

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
            [calculation_code] = ListSortDirection.Ascending
        });

        ToolBar.Add("Изделие", Resources.icons8_goods_16, Resources.icons8_goods_30, OpenGoods);
        ToolBar.Add("Калькуляция", Resources.icons8_calculation_16, Resources.icons8_calculation_30, OpenCalculation);
    }

    private void OpenGoods()
    {
        if (CurrentDocument != null)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IGoodsBrowser), CurrentDocument.GoodsId));
        }
    }

    private void OpenCalculation()
    {
        if (CurrentDocument != null)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(ICalculationBrowser), CurrentDocument.Id));
        }
    }
}
