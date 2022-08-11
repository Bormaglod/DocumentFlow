//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;
using DocumentFlow.Properties;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.Calculations;

public class CalculationDeductionBrowser : Browser<CalculationDeduction>, ICalculationDeductionBrowser
{
    public CalculationDeductionBrowser(ICalculationDeductionRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        Toolbar.IconSize = ButtonIconSize.Small;

        GridTextColumn id = CreateText(x => x.id, "Id", width: 180, visible: false);
        GridTextColumn deduction_name = CreateText(x => x.deduction_name, "Удержание", hidden: false);
        GridTextColumn calculation_base = CreateText(x => x.calculation_base_text, "База удерж.", width: 200);
        GridNumericColumn price = CreateCurrency(x => x.price, "База, руб.", width: 150);
        GridNumericColumn value = CreateNumeric(x => x.value, "Процент", width: 150, mode: FormatMode.Percent, decimalDigits: 2);
        GridNumericColumn item_cost = CreateCurrency(x => x.item_cost, "Сумма", width: 150);

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

        var clear = Toolbar.Add("Очистить", Resources.icons8_broomstick_16, Resources.icons8_broomstick_30, () =>
        {
            if (OwnerDocument != null && MessageBox.Show("Записи помеченные на удаление будут безвозвратно удалены. Продолжить удаление?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                repository.WipeAll(OwnerDocument.Value);
                RefreshView();
            }
        });
    }

    protected override string HeaderText => "Удержания";
}
