//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//
// Версия 2022.9.2
//  - удалён метод ColumnVisible
//  - IsAllowVisibilityColumn теперь всегда равен false
// Версия 2022.9.3
//  - удалены методы IsColumnVisible, IsAllowVisibilityColumn и IsVisible
// Версия 2022.12.3
//  - добавлено колонка product_code
//  - в функции AsSummary заменён параметр includeDeleted имеющий значение
//    true на options равный SelectOptions.All
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Settings;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;
using DocumentFlow.Settings.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;
using System.Text.Json;

namespace DocumentFlow.Entities.Warehouse;

public class BalanceSheetBrowser : Browser<BalanceSheet>, IBalanceSheetBrowser
{
    private class StackColumnInfo
    {
        public StackColumnInfo(string name, GridColumn amount, GridColumn summa)
        {
            Name = name;
            Amount = amount;
            Summa = summa;
        }

        public string Name { get; set; }
        public GridColumn Amount { get; set; }
        public GridColumn Summa { get; set; }
    }

    private readonly IBalanceSheetFilter filter;
    private readonly StackColumnInfo[] columns;
    private readonly ISingletoneSettings settings;

    public BalanceSheetBrowser(IBalanceSheetRepository repository, IPageManager pageManager, IBalanceSheetFilter filter, ISingletoneSettings settings) 
        : base(repository, pageManager, filter: filter) 
    {
        AllowGrouping();

        this.filter = filter;
        this.settings = settings;

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.product_name, "Наименование", hidden: false);
        var code = CreateText(x => x.product_code, "Артикул", width: 150);
        var group_name = CreateText(x => x.group_name, "Группа", width: 200, visible: false);
        var opening_balance_amount = CreateNumeric(x => x.opening_balance_amount, "Остаток на начало", width: 150, decimalDigits: 3);
        var opening_balance_summa = CreateCurrency(x => x.opening_balance_summa, "Остаток на начало", width: 150);
        var income_amount = CreateNumeric(x => x.income_amount, "Приход", width: 150, decimalDigits: 3);
        var income_summa = CreateCurrency(x => x.income_summa, "Приход", width: 150);
        var expense_amount = CreateNumeric(x => x.expense_amount, "Расход", width: 150, decimalDigits: 3);
        var expense_summa = CreateCurrency(x => x.expense_summa, "Расход", width: 150);
        var closing_balance_amount = CreateNumeric(x => x.closing_balance_amount, "Остаток на конец", width: 150, decimalDigits: 3);
        var closing_balance_summa = CreateCurrency(x => x.closing_balance_summa, "Остаток на конец", width: 150);

        name.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        opening_balance_amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        income_amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        expense_amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
        closing_balance_amount.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;

        columns = new StackColumnInfo[]
        {
            new StackColumnInfo("Остаток на начало", opening_balance_amount, opening_balance_summa),
            new StackColumnInfo("Приход", income_amount, income_summa),
            new StackColumnInfo("Расход", expense_amount, expense_summa),
            new StackColumnInfo("Остаток на конец", closing_balance_amount, closing_balance_summa),
        };

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(opening_balance_amount, options: SelectOptions.All)
            .AsSummary(opening_balance_summa, SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary(income_amount, options: SelectOptions.All)
            .AsSummary(income_summa, SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary(expense_amount, options: SelectOptions.All)
            .AsSummary(expense_summa, SummaryColumnFormat.Currency, SelectOptions.All)
            .AsSummary(closing_balance_amount, options: SelectOptions.All)
            .AsSummary(closing_balance_summa, SummaryColumnFormat.Currency, SelectOptions.All);

        AddColumns(new GridColumn[] { 
            id, 
            name, 
            code,
            group_name, 
            opening_balance_amount, 
            opening_balance_summa, 
            income_amount, 
            income_summa, 
            expense_amount, 
            expense_summa, 
            closing_balance_amount, 
            closing_balance_summa 
        });

        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [name] = ListSortDirection.Ascending
        });

        CreateGroups()
            .Add(group_name);
    }

    protected override string HeaderText => "Материальный отчёт";

    protected override void ConfigureColumns()
    {
        base.ConfigureColumns();

        ClearStackedRows();

        foreach (var column in columns)
        {
            column.Amount.Visible = filter.AmountVisible;
            column.Summa.Visible = filter.SummaVisible;
        }
        
        if (filter.AmountVisible && filter.SummaVisible)
        {
            foreach (var item in columns)
            {
                CreateStackedColumns(item.Name, new GridColumn[] { item.Amount, item.Summa });
                item.Amount.HeaderText = "Кол-во";
                item.Summa.HeaderText = "Сумма";
            }
        }
        else
        {
            foreach (var item in columns)
            {
                item.Amount.HeaderText = item.Name;
                item.Summa.HeaderText = item.Name;
            }
        }
    }

    protected override void ApplySettings(out bool canceledSettings)
    {
        filter?.Configure(settings);

        canceledSettings = true;
    }

    protected override void WriteSettings(out bool canceledSettings)
    {
        filter?.WriteConfigure(settings);

        canceledSettings = true;
    }
}
