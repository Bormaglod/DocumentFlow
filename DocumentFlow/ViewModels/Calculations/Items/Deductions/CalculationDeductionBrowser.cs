//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.Messaging;

using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Models;
using DocumentFlow.Messages;
using DocumentFlow.Properties;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class CalculationDeductionBrowser : BrowserPage<CalculationDeduction>, ICalculationDeductionBrowser
{
    public CalculationDeductionBrowser(IServiceProvider services, ICalculationDeductionRepository repository, IConfiguration configuration)
        : base(services, repository, configuration)
    {
        ToolBar.SmallIcons();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var deduction_name = CreateText(x => x.DeductionName, "Удержание", hidden: false);
        var calculation_base = CreateText(x => x.CalculationBaseText, "База удерж.", width: 200);
        var price = CreateCurrency(x => x.Price, "База, руб.", width: 150);
        var value = CreateNumeric(x => x.Value, "Процент", width: 150, mode: FormatMode.Percent, decimalDigits: 2);
        var item_cost = CreateCurrency(x => x.ItemCost, "Сумма", width: 150);

        CreateSummaryRow(VerticalPosition.Bottom)
            .AsCount(deduction_name, "Всего наименований: {?}")
            .AsSummary(item_cost, SummaryColumnFormat.Currency);

        deduction_name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        value.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        AddColumns(new GridColumn[] { id, deduction_name, calculation_base, price, value, item_cost });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [deduction_name] = ListSortDirection.Ascending
        });

        AllowSorting = false;

        ToolBar.Add("Удержание", Resources.icons8_discount_16, Resources.icons8_discount_30, OpenDeduction);
        ToolBar.AddSeparator();

        ToolBar.Add("Очистить", Resources.icons8_broomstick_16, Resources.icons8_broomstick_30, () =>
        {
            if (OwnerDocument != null && MessageBox.Show("Записи помеченные на удаление будут безвозвратно удалены. Продолжить удаление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                repository.WipeAll(OwnerDocument.Value);
                RefreshView();
            }
        });

        ContextMenu.Add("Удержание", Resources.icons8_discount_16, (s, e) => OpenDeduction());
    }

    private void OpenDeduction()
    {
        if (CurrentDocument != null && CurrentDocument.ItemId != null)
        {
            WeakReferenceMessenger.Default.Send(new EntityEditorOpenMessage(typeof(IDeductionEditor), CurrentDocument.ItemId.Value));
        }
    }
}
