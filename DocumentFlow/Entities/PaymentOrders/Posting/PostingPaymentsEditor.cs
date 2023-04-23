//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//
// Версия 2022.12.1
//  - исправлена ошибка '42702: column reference "contractor_id" is ambiguous'
//    при выборе списка документов на расход
//  - добавлена возможность выбора документов реализации
//  - в окно выбора документов добавлены колонки даты и номера документа
//  - из окна выбора документов удалена колонка с контрагентом
//  - добавлена сортировка документов по дате и номеру
// Версия 2022.12.17
//  - вызовы GetAllDefault заменены на GetByContractor
// Версия 2022.12.18
//  - добавлена возможность разнечения платежей на начальный остаток по
//    контрагенту
//  - класс стал наследоваться от DocumentEditor, а не Editor
// Версия 2022.12.21
//  - для IPurchaseRequestRepository вызов GetContractor(Guid?) заменен
//    на GetByContractor(Guid?, PurchaseState?)
//  - добавлена выборка документов у которых сумма документа не равна
//    уже оплаченной сумме, т.е. получим только те документы по которым
//    есть долг и можно разнести платёж
//  - добавлен авторасчёт суммы операции исходя из остатка платежа
//    по выбранному документу
// Версия 2022.12.24
//  - добавлена инициализация свойства DisableCurrentItem для document
// Версия 2022.12.29
//  - разнесение документов на начальный остаток работало некорректно - 
//    не учитывалься дебетовый остаток - исправлено
// Версия 2023.1.21
//  - авторасчёт суммы теперь учитывает остаток по платёжному документу
//  - выборка документов из версии 2022.12.21 работала только для расхода,
//    теперь работает и для прихода
// Версия 2023.3.27
//  - добавлен метод GetDocuments
//  - изменения связанные с тем, что параметр orderId в методе
//    GetPaymentBalance стал Nullable
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Balances.Initial;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Controls;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;

using System.Globalization;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class PostingPaymentsEditor : DocumentEditor<PostingPayments>, IPostingPaymentsEditor
{
    private readonly IPageManager pageManager;

    public PostingPaymentsEditor(IPostingPaymentsRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        this.pageManager = pageManager;

        EditorControls
            .AddDocumentSelectBox<DebtDocument>(x => x.DocumentId, "Документ", select =>
                select
                    .Required()
                    .SetDataSource(GetDocuments)
                    .EnableEditor(OpenDocument)
                    .CreateColumns(CreatePaymentsColumns)
                    .DocumentChanged(DocumentPaymentChanged)
                    .DocumentSelected(DocumentPaymentSelected)
                    .DisableCurrentItem()
                    .SetHeaderWidth(150)
                    .SetEditorWidth(400))
            .AddTextBox(x => x.ContractorName, "Контрагент", text =>
                text
                    .SetHeaderWidth(150)
                    .SetEditorWidth(400)
                    .Disable())
            .AddCurrencyTextBox(x => x.TransactionAmount, "Сумма операции", text =>
                text
                    .SetHeaderWidth(150)
                    .SetEditorWidth(200)
                    .DefaultAsValue());
    }

    protected override void DoBeforeSave()
    {
        base.DoBeforeSave();
        var document = EditorControls.GetControl<IDocumentSelectBoxControl<DebtDocument>>(x => x.DocumentId);
        if (document.SelectedItem != null)
        {
            Document.TableName = document.SelectedItem.TableName;
        }
    }

    private void DocumentPaymentChanged(DebtDocument? newValue)
    {
        var contractor = EditorControls.GetControl<ITextBoxControl>(x => x.ContractorName);
        if (newValue != null)
        {
            contractor.SetText(newValue.ContractorName ?? string.Empty);
        }
    }

    private void DocumentPaymentSelected(DebtDocument? newValue)
    {
        var amount = EditorControls.GetControl<ICurrencyTextBoxControl>(x => x.TransactionAmount);
        if (newValue != null)
        {
            var por = Services.Provider.GetService<IPaymentOrderRepository>();
            decimal balance = por!.GetPaymentBalance(OwnerId);

            decimal newAmount = newValue.FullCost - newValue.Paid;
            if (newAmount > balance)
            {
                newAmount = balance;
            }

            amount.NumericValue = newAmount;
        }
        else
        {
            amount.NumericValue = 0;
        }
    }

    private void OpenDocument(DebtDocument doc) => pageManager.ShowEditor(doc.EditorType, doc.Id);

    private IEnumerable<DebtDocument>? GetDocuments()
    {
        if (OwnerId != null)
        {
            var payments = Services.Provider.GetService<IPaymentOrderRepository>();
            var p = payments!.GetById(OwnerId.Value);

            if (p.PaymentDirection == PaymentDirection.Expense)
            {
                var prr = Services.Provider.GetService<IPurchaseRequestRepository>();
                var pr = prr!.GetByContractor(p.ContractorId, PurchaseState.Active)
                    .Select(d => new DebtDocument(d, "purchase", "Заявка на расход", typeof(IPurchaseRequestEditor))
                    {
                        ContractorName = d.ContractorName,
                        FullCost = d.FullCost,
                        Paid = d.Prepayment
                    });

                var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
                var wr = wrr!.GetByContractor(p.ContractorId)
                    .Select(d =>
                        new DebtDocument(d, "receipt", "Поступление", typeof(IWaybillReceiptEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.FullCost,
                            Paid = d.Paid
                        });
                var bcr = Services.Provider.GetService<IInitialBalanceContractorRepository>();
                var bc = bcr!.GetByContractor(p.ContractorId, BalanceCategory.Credit)
                    .Select(d =>
                        new DebtDocument(d, "balance", "Нач. остаток", typeof(IInitialBalanceContractorEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.OperationSumma,
                            Paid = d.Paid
                        });

                return pr
                    .Union(wr)
                    .Union(bc)
                    .Where(x => x.FullCost != x.Paid || x.Id == Document.DocumentId)
                    .OrderBy(x => x.DocumentDate)
                    .ThenBy(x => x.DocumentNumber);
            }
            else
            {
                var wsr = Services.Provider.GetService<IWaybillSaleRepository>();
                var wr = wsr!.GetByContractor(p.ContractorId)
                    .Select(d =>
                        new DebtDocument(d, "sale", "Реализация", typeof(IWaybillSaleEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.FullCost,
                            Paid = d.Paid
                        });
                var bcr = Services.Provider.GetService<IInitialBalanceContractorRepository>();
                var bc = bcr!.GetByContractor(p.ContractorId, BalanceCategory.Debet)
                    .Select(d =>
                        new DebtDocument(d, "balance", "Нач. остаток", typeof(IInitialBalanceContractorEditor))
                        {
                            ContractorName = d.ContractorName,
                            FullCost = d.OperationSumma,
                            Paid = d.Paid
                        });

                return wr
                    .Union(bc)
                    .Where(x => x.FullCost != x.Paid || x.Id == Document.DocumentId)
                    .OrderBy(x => x.DocumentDate)
                    .ThenBy(x => x.DocumentNumber);
            }
        }

        return null;
    }

    private static void CreatePaymentsColumns(IList<GridColumn> columns)
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

        columns.Add(document_name);
        columns.Add(document_date);
        columns.Add(document_number);
        columns.Add(payment_required);
        columns.Add(paid);

        document_number.CellStyle.HorizontalAlignment = HorizontalAlignment.Center;
        document_name.AutoSizeColumnsMode = Syncfusion.WinForms.DataGrid.Enums.AutoSizeColumnsMode.Fill;
    }
}