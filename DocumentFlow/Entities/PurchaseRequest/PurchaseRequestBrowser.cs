//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//
// Версия 2022.11.26
//  - добавлен столбец executed возвращающее флаг налиличия поставок по
//    заказу
// Версия 2022.12.11
//  - добавлен столбец state_name
//  - добавлена команда "Отменить заказ" в контекстное меню
//  - добавлен метод BrowserCellStyle
//  - добавлен метод DocumentIsReadOnly
//  - колонка "Оплачено" заменена на "Предоплата"
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data;
using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Infrastructure;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestBrowser : Browser<PurchaseRequest>, IPurchaseRequestBrowser
{
    public PurchaseRequestBrowser(IPurchaseRequestRepository repository, IPageManager pageManager, IDocumentFilter filter)
        : base(repository, pageManager, filter: filter)
    {
        AllowGrouping();

        var id = CreateText(x => x.id, "Id", width: 180, visible: false);
        var date = CreateDateTime(x => x.document_date, "Дата", hidden: false, width: 150);
        var number = CreateNumeric(x => x.document_number, "Номер", width: 100);
        var contractor = CreateText(x => x.contractor_name, "Контрагент");
        var contract = CreateText(x => x.contract_name, "Договор", width: 200, visible: false);
        var cost_order = CreateCurrency(x => x.cost_order, "Сумма", width: 120);
        var tax = CreateNumeric(x => x.tax, "НДС%", width: 80, mode: FormatMode.Percent);
        var tax_value = CreateCurrency(x => x.tax_value, "НДС", width: 120);
        var full_cost = CreateCurrency(x => x.full_cost, "Всего c НДС", width: 120);
        var paid = CreateCurrency(x => x.paid, "Предоплата", width: 120);
        var executed = CreateBoolean(x => x.executed, "Поставка", width: 100);
        var state_name = CreateText(x => x.state_name, "Состояние", width: 100);

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(cost_order, SummaryColumnFormat.Currency)
            .AsSummary(tax_value, SummaryColumnFormat.Currency)
            .AsSummary(full_cost, SummaryColumnFormat.Currency);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        tax.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, date, number, contractor, contract, cost_order, tax, tax_value, full_cost, paid, executed, state_name });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        ContextMenu.Add("Отменить заказ", (x) => SetStatePurchaseRequest(repository.Cancel));
        ContextMenu.Add("Завершить заказ", (x) => SetStatePurchaseRequest(repository.Complete), addSeparator: false);
    }

    protected override string HeaderText => "Заявка";

    protected override bool DocumentIsReadOnly(PurchaseRequest document) => document.PurchaseState == PurchaseState.Canceled || document.PurchaseState == PurchaseState.Completed;

    protected override void BrowserCellStyle(PurchaseRequest document, string column, CellStyleInfo style)
    {
        base.BrowserCellStyle(document, column, style);
        if (document.PurchaseState == PurchaseState.Canceled)
        {
            style.TextColor = Color.Red;
            style.Font.Strikeout = true;
        }
        else if (document.PurchaseState == PurchaseState.Completed && column == "state_name")
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
            }
            catch (RepositoryException e)
            {

                ExceptionHelper.MesssageBox(e);
            }

            RefreshGrid();
        }
    }
}
