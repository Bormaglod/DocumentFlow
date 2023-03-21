//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Core;
using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;

using System.Globalization;

namespace DocumentFlow.Entities.PaymentOrders;

public class DebtAdjustmentEditor : DocumentEditor<DebtAdjustment>, IDebtAdjustmentEditor
{
    private readonly DfDocumentSelectBox<WaybillReceipt> document_debt;
    private readonly DfDocumentSelectBox<WaybillReceipt> document_credit;
    private readonly DfCurrencyTextBox amount;

    public DebtAdjustmentEditor(IDebtAdjustmentRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        var contractor = new DfDirectorySelectBox<Contractor>("contractor_id", "Контрагент", 190, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IContractorEditor, Contractor>(t)
        };

        document_debt = new DfDocumentSelectBox<WaybillReceipt>("document_debt_id", "Документ (долг контрагента)", 190, 400)
        {
            Required = true,
            OpenAction = (t) => pageManager.ShowEditor<IWaybillReceiptEditor>(t.Id),
            RefreshMethod = DataRefreshMethod.OnOpen
        };

        document_credit = new DfDocumentSelectBox<WaybillReceipt>("document_credit_id", "Документ (долг организации)", 190, 400)
        {
            Required = true,
            OpenAction = (t) => pageManager.ShowEditor<IWaybillReceiptEditor>(t.Id),
            RefreshMethod = DataRefreshMethod.OnOpen
        };

        amount = new DfCurrencyTextBox("transaction_amount", "Сумма", 190, 200) { DefaultAsNull = false };

        contractor.SetDataSource(() => Services.Provider.GetService<IContractorRepository>()!.GetAll());

        document_debt.SetDataSource(() =>
        {
            Guid? c = contractor.SelectedItem?.Id ?? Document.contractor_id;
            var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
            return wrr!.GetByContractor(c).Where(x => x.full_cost < x.paid);
        });

        document_credit.SetDataSource(() =>
        {
            Guid? c = contractor.SelectedItem?.Id ?? Document.contractor_id;
            var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
            return wrr!.GetByContractor(c).Where(x => x.full_cost > x.paid);
        });

        document_debt.Columns += (sender, e) => CreateColumns(e.Columns);
        document_credit.Columns += (sender, e) => CreateColumns(e.Columns);

        document_debt.ManualValueChange += Document_ManualValueChange;
        document_credit.ManualValueChange += Document_ManualValueChange;

        AddControls(new Control[]
        {
            contractor,
            document_debt,
            document_credit,
            amount
        });
    }

    private void Document_ManualValueChange(object? sender, ChangeDataEventArgs<WaybillReceipt> e)
    {
        if (document_debt.SelectedItem != null && document_credit.SelectedItem != null)
        {
            var debt = document_debt.SelectedItem.paid - document_debt.SelectedItem.full_cost;
            var credit = document_credit.SelectedItem.full_cost - document_credit.SelectedItem.paid;

            amount.NumericValue = Math.Min(debt, credit);
        }
    }

    private static void CreateColumns(Columns columns)
    {
        var document_contract = new GridTextColumn()
        {
            MappingName = "contract_name",
            HeaderText = "Договор"
        };

        var document_date = new GridDateTimeColumn()
        {
            MappingName = "document_date",
            HeaderText = "Дата",
            Width = 100
        };

        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = 0;
        var document_number = new GridNumericColumn()
        {
            MappingName = "document_number",
            HeaderText = "Номер",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Numeric,
            NumberFormatInfo = numberFormat,
            Width = 80
        };

        NumberFormatInfo currencyFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        currencyFormat.NumberDecimalDigits = 2;
        var payment_required = new GridNumericColumn()
        {
            MappingName = $"full_cost",
            HeaderText = "Сумма документа",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency,
            NumberFormatInfo = currencyFormat,
            Width = 140
        };

        var paid = new GridNumericColumn()
        {
            MappingName = $"paid",
            HeaderText = "Оплачено",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency,
            NumberFormatInfo = currencyFormat,
            Width = 140
        };

        columns.Add(document_contract);
        columns.Add(document_date);
        columns.Add(document_number);
        columns.Add(payment_required);
        columns.Add(paid);

        document_number.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
        document_contract.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
    }
}