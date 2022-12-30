//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
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
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Balances.Initial;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Entities.Waybills;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Syncfusion.WinForms.DataGrid;

using System.Globalization;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public class PostingPaymentsEditor : DocumentEditor<PostingPayments>, IPostingPaymentsEditor
{
    private readonly DfDocumentSelectBox<DebtDocument> document;

    public PostingPaymentsEditor(IPostingPaymentsRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        document = new DfDocumentSelectBox<DebtDocument>("document_id", "Документ", 150, 400)
        {
            Required = true,
            OpenAction = (t) => pageManager.ShowEditor(t.EditorType, t.id),
            DisableCurrentItem = true
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
                    var pr = prr!.GetByContractor(p.contractor_id, PurchaseState.Active)
                        .Select(d => new DebtDocument(d, "purchase", "Заявка на расход", typeof(IPurchaseRequestEditor)) 
                        {
                            contractor_name = d.contractor_name,
                            full_cost = d.full_cost,
                            paid = d.prepayment
                        });

                    var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
                    var wr = wrr!.GetByContractor(p.contractor_id)
                        .Select(d => 
                            new DebtDocument(d, "receipt", "Поступление", typeof(IWaybillReceiptEditor))
                            {
                                contractor_name = d.contractor_name,
                                full_cost = d.full_cost,
                                paid = d.paid
                            });
                    var bcr = Services.Provider.GetService<IInitialBalanceContractorRepository>();
                    var bc = bcr!.GetByContractor(p.contractor_id, BalanceCategory.Credit)
                        .Select(d =>
                            new DebtDocument(d, "balance", "Нач. остаток", typeof(IInitialBalanceContractorEditor))
                            {
                                contractor_name = d.contractor_name,
                                full_cost = d.operation_summa,
                                paid = d.paid
                            });

                    return pr
                        .Union(wr)
                        .Union(bc)
                        .Where(x => x.full_cost != x.paid || x.id == Document.document_id)
                        .OrderBy(x => x.document_date)
                        .ThenBy(x => x.document_number);
                }
                else
                {
                    var wsr = Services.Provider.GetService<IWaybillSaleRepository>();
                    var wr = wsr!.GetByContractor(p.contractor_id)
                        .Select(d =>
                            new DebtDocument(d, "sale", "Реализация", typeof(IWaybillSaleEditor))
                            {
                                contractor_name = d.contractor_name,
                                full_cost = d.full_cost,
                                paid = d.paid
                            });
                    var bcr = Services.Provider.GetService<IInitialBalanceContractorRepository>();
                    var bc = bcr!.GetByContractor(p.contractor_id, BalanceCategory.Debet)
                        .Select(d =>
                            new DebtDocument(d, "balance", "Нач. остаток", typeof(IInitialBalanceContractorEditor))
                            {
                                contractor_name = d.contractor_name,
                                full_cost = d.operation_summa,
                                paid = d.paid
                            });

                    return wr
                        .Union(bc)
                        .OrderBy(x => x.document_date)
                        .ThenBy(x => x.document_number);
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

        document.ManualValueChange += (sender, e) =>
        {
            if (e.NewValue != null) 
            {
                amount.NumericValue = e.NewValue.full_cost - e.NewValue.paid;
            }
            else
            {
                amount.NumericValue = 0;
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
            HeaderText = "Документ"
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
            MappingName = "full_cost",
            HeaderText = "Сумма документа",
            FormatMode = Syncfusion.WinForms.Input.Enums.FormatMode.Currency,
            NumberFormatInfo = currencyFormat,
            Width = 120
        };

        var paid = new GridNumericColumn()
        {
            MappingName = "paid",
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