//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Companies;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;

using System.Globalization;

namespace DocumentFlow.Entities.PaymentOrders;

public class DebtAdjustmentEditor : DocumentEditor<DebtAdjustment>, IDebtAdjustmentEditor
{
    public DebtAdjustmentEditor(IDebtAdjustmentRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        EditorControls
            .AddDirectorySelectBox<Contractor>(x => x.ContractorId, "Контрагент", select =>
                select
                    .SetDataSource(GetContractors)
                    .SetHeaderWidth(190)
                    .SetEditorWidth(400))
            .AddDocumentSelectBox<WaybillReceipt>(x => x.DocumentDebtId, "Документ (долг контрагента)", select =>
                select
                    .Required()
                    .CreateColumns(CreateDocumentColumns)
                    .EnableEditor<IWaybillReceiptEditor>(true)
                    .SetDataSource(GetWaybillReceiptsDebt, DataRefreshMethod.OnOpen)
                    .DocumentSelected(WaybillReceiptSelected)
                    .SetHeaderWidth(190)
                    .SetEditorWidth(400))
            .AddDocumentSelectBox<WaybillReceipt>(x => x.DocumentCreditId, "Документ (долг организации)", select =>
                select
                    .Required()
                    .CreateColumns(CreateDocumentColumns)
                    .EnableEditor<IWaybillReceiptEditor>(true)
                    .SetDataSource(GetWaybillReceiptsCredit, DataRefreshMethod.OnOpen)
                    .DocumentSelected(WaybillReceiptSelected)
                    .SetHeaderWidth(190)
                    .SetEditorWidth(400))
            .AddCurrencyTextBox(x => x.TransactionAmount, "Сумма", text =>
                text
                    .SetHeaderWidth(190)
                    .SetEditorWidth(200)
                    .DefaultAsValue());
    }

    private IEnumerable<WaybillReceipt> GetWaybillReceiptsDebt()
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>();
        var id = contractor.SelectedItem?.Id ?? Document.ContractorId;
        var repo = Services.Provider.GetService<IWaybillReceiptRepository>()!;
        return repo
            .GetByContractor(id)
            .Where(x => x.FullCost < x.Paid);
    }

    private IEnumerable<WaybillReceipt> GetWaybillReceiptsCredit()
    {
        var contractor = EditorControls.GetControl<IDirectorySelectBoxControl<Contractor>>();
        var id = contractor.SelectedItem?.Id ?? Document.ContractorId;
        var repo = Services.Provider.GetService<IWaybillReceiptRepository>()!;
        return repo
            .GetByContractor(id)
            .Where(x => x.FullCost > x.Paid);
    }

    private IEnumerable<Contractor> GetContractors() => Services.Provider.GetService<IContractorRepository>()!.GetList();

    private void WaybillReceiptSelected(WaybillReceipt? value)
    {
        var document_debt = EditorControls.GetControl<IDocumentSelectBoxControl<WaybillReceipt>>(x => x.DocumentDebtId);
        var document_credit = EditorControls.GetControl<IDocumentSelectBoxControl<WaybillReceipt>>(x => x.DocumentCreditId);
        if (document_debt.SelectedItem != null && document_credit.SelectedItem != null)
        {
            var debt = document_debt.SelectedItem.Paid - document_debt.SelectedItem.FullCost;
            var credit = document_credit.SelectedItem.FullCost - document_credit.SelectedItem.Paid;

            EditorControls.GetControl<ICurrencyTextBoxControl>(x => x.TransactionAmount).NumericValue = Math.Min(debt, credit);
        }
    }

    private static void CreateDocumentColumns(IList<GridColumn> columns)
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