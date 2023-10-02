//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.09.2023
//-----------------------------------------------------------------------

using DocumentFlow.Controls;
using DocumentFlow.Controls.Enums;
using DocumentFlow.Controls.Events;
using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;

using System.Data;
using System.Globalization;

namespace DocumentFlow.ViewModels;

[Entity(typeof(PostingPayments), RepositoryType = typeof(IPostingPaymentsRepository))]
public partial class PostingPaymentsEditor : EditorPanel, IPostingPaymentsEditor, IDocumentEditor
{
    private readonly IServiceProvider services;
    private readonly IPageManager pageManager;

    public PostingPaymentsEditor(IServiceProvider services, IPageManager pageManager) : base(services)
    {
        InitializeComponent();

        this.services = services;
        this.pageManager = pageManager;

        Payment.OrganizationId = services.GetRequiredService<IOrganizationRepository>().GetMain().Id;
    }

    public Guid? OwnerId
    {
        get => Payment.OwnerId;
        set => Payment.OwnerId = value;
    }

    protected PostingPayments Payment { get; set; } = null!;

    protected override void AfterConstructData(ConstructDataMethod method)
    {
        textDocNumber.Enabled = Payment.Id != Guid.Empty;
    }

    protected override void DoBindingControls()
    {
        textDocNumber.DataBindings.Add(nameof(textDocNumber.IntegerValue), DataContext, nameof(PostingPayments.DocumentNumber), true, DataSourceUpdateMode.OnPropertyChanged, 0);
        dateDocument.DataBindings.Add(nameof(dateDocument.DateTimeValue), DataContext, nameof(PostingPayments.DocumentDate), true, DataSourceUpdateMode.OnPropertyChanged);
        comboOrg.DataBindings.Add(nameof(comboOrg.SelectedItem), DataContext, nameof(PostingPayments.OrganizationId), false, DataSourceUpdateMode.OnPropertyChanged);
        selectDocument.DataBindings.Add(nameof(selectDocument.SelectedItem), DataContext, nameof(PostingPayments.DocumentId), false, DataSourceUpdateMode.OnPropertyChanged);
        textContractor.DataBindings.Add(nameof(textContractor.TextValue), DataContext, nameof(PostingPayments.ContractorName), false, DataSourceUpdateMode.OnPropertyChanged);
        textSumma.DataBindings.Add(nameof(textSumma.DecimalValue), DataContext, nameof(PostingPayments.TransactionAmount), false, DataSourceUpdateMode.OnPropertyChanged);
    }

    protected override void CreateDataSources()
    {
        comboOrg.DataSource = services.GetRequiredService<IOrganizationRepository>().GetList();
        selectDocument.DataSource = GetDocuments();
    }

    private IEnumerable<IBaseDocument>? GetDocuments()
    {
        if (OwnerId == null)
        {
            return null;
        }

        var p = services
            .GetRequiredService<IPaymentOrderRepository>()
            .Get(OwnerId.Value);

        switch (p.PaymentDirection)
        {
            case PaymentDirection.Expense:
                var pr = services
                    .GetRequiredService<IPurchaseRequestRepository>()
                    .GetByContractor(p.ContractorId, PurchaseState.Active)
                    .Select(d => new DebtDocument(d, "purchase", "Заявка на расход", typeof(IPurchaseRequestEditor))
                    {
                        ContractorName = d.ContractorName,
                        FullCost = d.FullCost,
                        Paid = d.Prepayment
                    });

                var wrr = services
                    .GetRequiredService<IWaybillReceiptRepository>()
                    .GetByContractor(p.ContractorId)
                    .Select(d =>
                        new DebtDocument(d, "receipt", "Поступление", typeof(IWaybillReceiptEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.FullCost,
                            Paid = d.Paid
                        });
                var bcc = services
                    .GetRequiredService<IInitialBalanceContractorRepository>()
                    .GetByContractor(p.ContractorId, BalanceCategory.Credit)
                    .Select(d =>
                        new DebtDocument(d, "balance", "Нач. остаток", typeof(IInitialBalanceContractorEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.OperationSumma,
                            Paid = d.Paid
                        });

                return pr
                    .Union(wrr)
                    .Union(bcc)
                    .Where(x => x.FullCost != x.Paid || x.Id == Payment.DocumentId)
                    .OrderBy(x => x.DocumentDate)
                    .ThenBy(x => x.DocumentNumber);
            case PaymentDirection.Income:
                var wsr = services
                    .GetRequiredService<IWaybillSaleRepository>()
                    .GetByContractor(p.ContractorId)
                    .Select(d =>
                        new DebtDocument(d, "sale", "Реализация", typeof(IWaybillSaleEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.FullCost,
                            Paid = d.Paid
                        });
                var bcd = services
                    .GetRequiredService<IInitialBalanceContractorRepository>()
                    .GetByContractor(p.ContractorId, BalanceCategory.Debet)
                    .Select(d =>
                        new DebtDocument(d, "balance", "Нач. остаток", typeof(IInitialBalanceContractorEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.OperationSumma,
                            Paid = d.Paid
                        });

                return wsr
                    .Union(bcd)
                    .Where(x => x.FullCost != x.Paid || x.Id == Payment.DocumentId)
                    .OrderBy(x => x.DocumentDate)
                    .ThenBy(x => x.DocumentNumber);
            default:
                throw new NotImplementedException();
        }
    }

    private void SelectDocument_OpenButtonClick(object sender, DocumentSelectedEventArgs e)
    {
        pageManager.ShowEditor(((DebtDocument)e.Document).EditorType, e.Document.Id);
    }

    private void SelectDocument_DocumentDialogColumns(object sender, DocumentDialogColumnsEventArgs e)
    {
        var document_name = new GridTextColumn()
        {
            MappingName = "DocumentName",
            HeaderText = "Документ"
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
            Width = 120
        };

        var paid = new GridNumericColumn()
        {
            MappingName = "Paid",
            HeaderText = "Оплачено",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency,
            NumberFormatInfo = currencyFormat,
            Width = 100
        };

        e.Columns.Add(document_name);
        e.Columns.Add(document_date);
        e.Columns.Add(document_number);
        e.Columns.Add(payment_required);
        e.Columns.Add(paid);

        document_number.CellStyle.HorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
        document_name.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
    }

    private void SelectDocument_SelectedItemChanged(object sender, EventArgs e)
    {
        var debt = (DebtDocument)selectDocument.SelectedDocument;

        Payment.ContractorName = debt.ContractorName;
        Payment.Discriminator = debt.TableName;
    }

    private void SelectDocument_UserDocumentModified(object sender, DocumentChangedEventArgs e)
    {
        decimal balance = services
            .GetRequiredService<IPaymentOrderRepository>()
            .GetPaymentBalance(OwnerId);

        var debt = (DebtDocument)e.NewDocument;

        decimal newAmount = debt.FullCost - debt.Paid;
        if (newAmount > balance)
        {
            newAmount = balance;
        }

        Payment.TransactionAmount = newAmount;
    }
}
