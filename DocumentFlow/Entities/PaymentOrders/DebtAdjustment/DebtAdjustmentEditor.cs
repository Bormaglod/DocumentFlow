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
    private readonly IPageManager pageManager;
    private readonly DfDocumentSelectBox<WaybillReceipt> document_debt;
    private readonly DfDocumentSelectBox<WaybillReceipt> document_credit;
    private readonly DfCurrencyTextBox amount;

    public DebtAdjustmentEditor(IDebtAdjustmentRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        this.pageManager = pageManager;

        var contractor = CreateDirectorySelectBox<Contractor, IContractorEditor>(x => x.ContractorId, "Контрагент", 190, 400, data: GetContractors);

        document_debt = CreateDocumentSelectBox<WaybillReceipt, IWaybillReceiptEditor>(x => x.DocumentDebtId, "Документ (долг контрагента)", 190, 400, required: true, refreshMethod: DataRefreshMethod.OnOpen, openById: true);
        document_credit = CreateDocumentSelectBox<WaybillReceipt, IWaybillReceiptEditor>(x => x.DocumentCreditId, "Документ (долг организации)", 190, 400, required: true, refreshMethod: DataRefreshMethod.OnOpen, openById: true);
        amount = CreateCurrencyTextBox(x => x.TransactionAmount, "Сумма", 190, 200, defaultAsNull: false);

        document_debt.SetDataSource(() =>
        {
            Guid? c = contractor.SelectedItem?.Id ?? Document.ContractorId;
            var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
            return wrr!.GetByContractor(c).Where(x => x.FullCost < x.Paid);
        });

        document_credit.SetDataSource(() =>
        {
            Guid? c = contractor.SelectedItem?.Id ?? Document.ContractorId;
            var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
            return wrr!.GetByContractor(c).Where(x => x.FullCost > x.Paid);
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

    private IEnumerable<Contractor> GetContractors() => Services.Provider.GetService<IContractorRepository>()!.GetAll();

    private void Document_ManualValueChange(object? sender, ChangeDataEventArgs<WaybillReceipt> e)
    {
        if (document_debt.SelectedItem != null && document_credit.SelectedItem != null)
        {
            var debt = document_debt.SelectedItem.Paid - document_debt.SelectedItem.FullCost;
            var credit = document_credit.SelectedItem.FullCost - document_credit.SelectedItem.Paid;

            amount.NumericValue = Math.Min(debt, credit);
        }
    }

    private static void CreateColumns(Columns columns)
    {
        var document_contract = new GridTextColumn()
        {
            MappingName = "ContractName",
            HeaderText = "Договор"
        };

        var document_date = new GridDateTimeColumn()
        {
            MappingName = "DocumentDate",
            HeaderText = "Дата",
            Width = 100
        };

        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = 0;
        var document_number = new GridNumericColumn()
        {
            MappingName = "DocumentNumber",
            HeaderText = "Номер",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Numeric,
            NumberFormatInfo = numberFormat,
            Width = 80
        };

        NumberFormatInfo currencyFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        currencyFormat.NumberDecimalDigits = 2;
        var payment_required = new GridNumericColumn()
        {
            MappingName = "FullCost",
            HeaderText = "Сумма документа",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency,
            NumberFormatInfo = currencyFormat,
            Width = 140
        };

        var paid = new GridNumericColumn()
        {
            MappingName = "Paid",
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