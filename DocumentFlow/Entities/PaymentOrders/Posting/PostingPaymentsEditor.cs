//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;

using System.Globalization;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class PostingPaymentsEditor : Editor<PostingPayments>, IPostingPaymentsEditor
{
    private readonly DfDocumentSelectBox<DebtDocument> document;

    public PostingPaymentsEditor(IPostingPaymentsRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        document = new DfDocumentSelectBox<DebtDocument>("document_id", "Документ", 150, 400)
        {
            Required = true,
            OpenAction = (t) => pageManager.ShowEditor(t.EditorType, t.id)
        };

        var contractor = new DfTextBox("contractor_name", "Контрагент", 150, 400) { Enabled = false };
        var amount = new DfCurrencyTextBox("transaction_amount", "Сумма операции", 150, 200) { DefaultAsNull = false };

        document.Columns += (sender, e) => CreateColumns(e.Columns);

        document.SetDataSource(() =>
        {
            if (OwnerId != null)
            {
                var payments = Services.Provider.GetService<IPaymentOrderRepository>();
                var p = payments!.GetById(OwnerId.Value);

                if (p.PaymentDirection == PaymentDirection.Expense)
                {
                    var prr = Services.Provider.GetService<IPurchaseRequestRepository>();
                    var pr = prr!.GetAllDefault(callback: q => q
                        .WhereTrue("purchase_request.carried_out")
                        .WhereFalse("purchase_request.deleted")
                        .Where("contractor_id", p.contractor_id))
                        .Select(d => new DebtDocument(d, "purchase", "Заявка на расход", typeof(IPurchaseRequestEditor)) 
                        {
                            contractor_name = d.contractor_name,
                            full_cost = d.full_cost,
                            paid = d.paid
                        });

                    var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
                    var wr = wrr!.GetAllDefault(callback: q => q
                        .WhereTrue("waybill_receipt.carried_out")
                        .WhereFalse("waybill_receipt.deleted")
                        .Where("contractor_id", p.contractor_id))
                        .Select(d => new DebtDocument(d, "receipt", "Поступление", typeof(IWaybillReceiptEditor))
                        {
                            contractor_name = d.contractor_name,
                            full_cost = d.full_cost,
                            paid = d.paid
                        });

                    return pr.Union(wr);
                }
                else
                {
                    
                }
            }

            return Array.Empty<DebtDocument>();
        });

        document.ValueChanged += (sender, e) =>
        {
            if (document.SelectedItem != null)
            {
                contractor.Value = document.SelectedItem.contractor_name ?? string.Empty;
            }
        };

        AddControls(new Control[]
        {
            document,
            contractor,
            amount
        });
    }

    protected override void DoBeforeSave()
    {
        base.DoBeforeSave();
        if (document.SelectedItem != null)
        {
            Document.table_name = document.SelectedItem.TableName;
        }
    }

    private static void CreateColumns(Columns columns)
    {
        var document_name = new GridTextColumn()
        {
            MappingName = "DocumentName",
            HeaderText = "Документ",
            Width = 200
        };

        var contractor_name = new GridTextColumn()
        {
            MappingName = "contractor_name",
            HeaderText = "Контрагент"
        };

        NumberFormatInfo numberFormat = (NumberFormatInfo)Application.CurrentCulture.NumberFormat.Clone();
        numberFormat.NumberDecimalDigits = 2;
        var payment_required = new GridNumericColumn()
        {
            MappingName = "full_cost",
            HeaderText = "Сумма документа",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency,
            NumberFormatInfo = numberFormat,
            Width = 120
        };

        var paid = new GridNumericColumn()
        {
            MappingName = "paid",
            HeaderText = "Оплачено",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency,
            NumberFormatInfo = numberFormat,
            Width = 120
        };

        columns.Add(document_name);
        columns.Add(contractor_name);
        columns.Add(payment_required);
        columns.Add(paid);

        contractor_name.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
    }
}