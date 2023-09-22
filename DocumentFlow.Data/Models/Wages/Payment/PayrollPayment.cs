//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 28.01.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;


[EntityName("Выплата заработной платы")]
public class PayrollPayment : AccountingDocument, IBilling
{
    private DateTime dateOperation = DateTime.Now;
    private decimal transactionAmount;
    private string? paymentNumber;

    public PayrollPayment() => BillingRange = new(this);
    
    /// <summary>
    /// Возвращает или устанавливает дату операции.
    /// </summary>
    public DateTime DateOperation 
    { 
        get => dateOperation; 
        set
        {
            if (dateOperation != value) 
            {
                dateOperation = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает сумма платежа.
    /// </summary>
    public decimal TransactionAmount 
    { 
        get => transactionAmount; 
        set
        {
            if (transactionAmount != value) 
            { 
                transactionAmount = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает номер платежного поручения или расходного/приходного ордера.
    /// </summary>
    public string? PaymentNumber 
    { 
        get => paymentNumber; 
        set
        {
            if (paymentNumber != value)
            {
                paymentNumber = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int PayrollNumber { get; protected set; }
    public DateTime PayrollDate { get; protected set; }
    public int BillingYear { get; protected set; }
    public short BillingMonth { get; protected set; }
    public BillingRange BillingRange { get; }
}
