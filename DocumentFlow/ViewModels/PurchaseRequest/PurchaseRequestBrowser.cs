//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces.Filters;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;

using Microsoft.Extensions.Configuration;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

using SyncEnums = Syncfusion.WinForms.DataGrid.Enums;

namespace DocumentFlow.ViewModels;

public class PurchaseRequestBrowser : BrowserPage<PurchaseRequest>, IPurchaseRequestBrowser
{
    public PurchaseRequestBrowser(IServiceProvider services, IPageManager pageManager, IPurchaseRequestRepository repository, IConfiguration configuration, IPurchaseRequestFilter filter)
        : base(services, pageManager, repository, configuration, filter: filter)
    {
        AllowGrouping();

        var id = CreateText(x => x.Id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.DocumentDate, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.DocumentNumber, "Номер", width: 100);
        var contractor = CreateText(x => x.ContractorName, "Контрагент");
        var contract = CreateText(x => x.ContractName, "Договор", width: 200, visible: false);
        var cost_order = CreateCurrency(x => x.CostOrder, "Сумма", width: 120);
        var tax = CreateNumeric(x => x.Tax, "НДС%", width: 80, mode: FormatMode.Percent);
        var tax_value = CreateCurrency(x => x.TaxValue, "НДС", width: 120);
        var full_cost = CreateCurrency(x => x.FullCost, "Всего c НДС", width: 120);
        var prepayment = CreateCurrency(x => x.Prepayment, "Предоплата", width: 120);
        var executed = CreateBoolean(x => x.Executed, "Выполнена", width: 100);
        var paid = CreateBoolean(x => x.Paid, "Оплачена", width: 100);
        var state_name = CreateText(x => x.StateName, "Состояние", width: 100);

        CreateSummaryRow(SyncEnums.VerticalPosition.Bottom, true)
            .AsSummary(cost_order, SummaryColumnFormat.Currency)
            .AsSummary(tax_value, SummaryColumnFormat.Currency)
            .AsSummary(full_cost, SummaryColumnFormat.Currency);

        contractor.AutoSizeColumnsMode = SyncEnums.AutoSizeColumnsMode.Fill;
        tax.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, date, number, contractor, contract, cost_order, tax, tax_value, full_cost, prepayment, executed, paid, state_name });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Поставка", new GridColumn[] { executed, paid });

        ContextMenu.AddItems(new IContextMenuItem[]
        {
            ContextMenu.CreateItem("Отменить заказ", (s, e) => SetStatePurchaseRequest(repository.Cancel)),
            ContextMenu.CreateItem("Завершить заказ", (s, e) => SetStatePurchaseRequest(repository.Complete)),
        });

        MoveToEnd();
    }

    protected override bool DocumentIsReadOnly(PurchaseRequest document) => document.PurchaseState == PurchaseState.Canceled || document.PurchaseState == PurchaseState.Completed;

    protected override void BrowserCellStyle(PurchaseRequest document, string column, CellStyleInfo style)
    {
        base.BrowserCellStyle(document, column, style);
        if (document.PurchaseState == PurchaseState.Canceled)
        {
            style.TextColor = Color.Red;
            style.Font.Strikeout = true;
        }
        else if (document.PurchaseState == PurchaseState.Completed && column == nameof(PurchaseRequest.StateName))
        {
            style.TextColor = Color.Green;
        }
    }

    private void SetStatePurchaseRequest(Action<PurchaseRequest> action)
    {
        if (CurrentDocument != null)
        {
            try
            {
                action(CurrentDocument);
                RefreshRow(CurrentDocument);
            }
            catch (RepositoryException e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshGrid();
        }
    }
}
