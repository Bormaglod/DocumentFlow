//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
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
// Версия 2022.12.17
//  - наименование поля paid заменено на prepayment (колонка "Предоплата")
//  - колонка "Поставка" переименована в "Выполнена"
//  - добавлена колонка "Оплачена"
//  - IDocumentFilter заменен на IPurchaseRequestFilter
// Версия 2022.12.21
//  - в конструктор добавлен параметр IEnumerable<ICreationBased>? creations
// Версия 2023.1.5
//  - добавлена установка диапазона дат для фильтра IDocumentFilter
//  - добавлен вызов MoveToEnd для перемещения в конец таблицы
// Версия 2023.1.8
//  - в конструктор добавлен параметр settings
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//  - DocumentFlow.Settings.Infrastructure перемещено в DocumentFlow.Infrastructure.Settings
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;
using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Infrastructure.Settings;

using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.Input.Enums;

using System.ComponentModel;

namespace DocumentFlow.Entities.PurchaseRequestLib;

public class PurchaseRequestBrowser : Browser<PurchaseRequest>, IPurchaseRequestBrowser
{
    public PurchaseRequestBrowser(IPurchaseRequestRepository repository, IPageManager pageManager, IPurchaseRequestFilter filter, IEnumerable<ICreationBased>? creations, IStandaloneSettings settings)
        : base(repository, pageManager, filter: filter, creations: creations, settings: settings)
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

        CreateSummaryRow(VerticalPosition.Bottom, true)
            .AsSummary(cost_order, SummaryColumnFormat.Currency)
            .AsSummary(tax_value, SummaryColumnFormat.Currency)
            .AsSummary(full_cost, SummaryColumnFormat.Currency);

        contractor.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;
        tax.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;

        AddColumns(new GridColumn[] { id, date, number, contractor, contract, cost_order, tax, tax_value, full_cost, prepayment, executed, paid, state_name });
        AddSortColumns(new Dictionary<GridColumn, ListSortDirection>()
        {
            [date] = ListSortDirection.Ascending,
            [number] = ListSortDirection.Ascending
        });

        CreateStackedColumns("Поставка", new GridColumn[] { executed, paid });

        ContextMenu.Add("Отменить заказ", (x) => SetStatePurchaseRequest(repository.Cancel));
        ContextMenu.Add("Завершить заказ", (x) => SetStatePurchaseRequest(repository.Complete), addSeparator: false);

        filter?.SetDateRange(DateRange.CurrentYear);

        MoveToEnd();
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
