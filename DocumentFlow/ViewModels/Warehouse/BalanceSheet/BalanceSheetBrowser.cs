//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

public class BalanceSheetBrowser : BrowserPage<BalanceSheet>, IBalanceSheetBrowser
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
    private readonly IPageManager pageManager;
    private readonly StackColumnInfo[] columns;

    public BalanceSheetBrowser(IServiceProvider services, IPageManager pageManager, IBalanceSheetRepository repository, IConfiguration configuration, IBalanceSheetFilter filter)
        : base(services, pageManager, repository, configuration, filter: filter) 
    {
        this.filter = filter;
        this.pageManager = pageManager;

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var name = CreateText(x => x.ProductName, "Наименование", hidden: false);
        var code = CreateText(x => x.ProductCode, "Артикул", width: 150);
        var group_name = CreateText(x => x.GroupName, "Группа", width: 200, visible: false);
        var opening_balance_amount = CreateNumeric(x => x.OpeningBalanceAmount, "Остаток на начало", width: 150, decimalDigits: 3);
        var opening_balance_summa = CreateCurrency(x => x.OpeningBalanceSumma, "Остаток на начало", width: 150);
        var income_amount = CreateNumeric(x => x.IncomeAmount, "Приход", width: 150, decimalDigits: 3);
        var income_summa = CreateCurrency(x => x.IncomeSumma, "Приход", width: 150);
        var expense_amount = CreateNumeric(x => x.ExpenseAmount, "Расход", width: 150, decimalDigits: 3);
        var expense_summa = CreateCurrency(x => x.ExpenseSumma, "Расход", width: 150);
        var closing_balance_amount = CreateNumeric(x => x.ClosingBalanceAmount, "Остаток на конец", width: 150, decimalDigits: 3);
        var closing_balance_summa = CreateCurrency(x => x.ClosingBalanceSumma, "Остаток на конец", width: 150);

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

        CreateGrouping()
            .Add(group_name);

        ToolBar.Add("Открыть", Properties.Resources.icons8_open_document_16, Properties.Resources.icons8_open_document_30, OpenCurrentDocument);

        ContextMenu.AddSeparator();
        ContextMenu.Add("Открыть", Properties.Resources.icons8_open_document_16, (s, e) => OpenCurrentDocument());
    }

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

    private void OpenCurrentDocument()
    {
        if (CurrentDocument == null)
        {
            return;
        }

        switch (filter.Content)
        {
            case Data.Enums.BalanceSheetContent.Material:
                pageManager.ShowEditor(typeof(IMaterialEditor), CurrentDocument.Id);
                break;
            case Data.Enums.BalanceSheetContent.Goods:
                pageManager.ShowEditor(typeof(IGoodsEditor), CurrentDocument.Id);
                break;
            default:
                break;
        }
    }
}
