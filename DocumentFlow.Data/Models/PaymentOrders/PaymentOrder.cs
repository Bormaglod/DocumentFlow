//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

namespace DocumentFlow.Data.Models;

[EntityName("Банк/касса")]
public class PaymentOrder : AccountingDocument
{
    private Guid? contractorId;
    private string? paymentNumber;
    private DateTime dateOperation = DateTime.Now;
    private decimal transactionAmount;
    private string direction = "expense";
    private bool withoutDistrib;

    /// <summary>
    /// Возвращает или устанавливает идентификатор контрагента.
    /// </summary>
    public Guid? ContractorId 
    { 
        get => contractorId; 
        set
        {
            if (contractorId != value) 
            { 
                contractorId = value;
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
    /// Возвращает или устанавливает сумму операции.
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
    /// Возвращает или устанавливает направление движения денег (приход или расход). Значение должно соответствовать 
    /// значению типа payment_direction (Postgresql).
    /// </summary>
    [EnumType("payment_direction")]
    public string Direction
    {
        get => direction;
        set
        {
            if (direction != value)
            {
                direction = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает или устанавливает флаг определяющий возможность проведения документа без распределения сумм по документам.
    /// </summary>
    public bool WithoutDistrib
    {
        get => withoutDistrib;
        set
        {
            if (withoutDistrib != value)
            {
                withoutDistrib = value;
                NotifyPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Возвращает наименование контрагента.
    /// </summary>
    public string? ContractorName { get; protected set; }

    /// <summary>
    /// Возвращает сумму расхода (или null, если Direction = "income").
    /// </summary>
    public decimal? Expense { get; protected set; }

    /// <summary>
    /// Возвращает сумму прихода (или null, если Direction = "expense").
    /// </summary>
    public decimal? Income { get; protected set; }

    [Write(false)]
    public PaymentDirection PaymentDirection
    {
        get { return Enum.Parse<PaymentDirection>(Direction.Pascalize()); }
        set { Direction = value.ToString().Underscore(); }
    }

    public string PaymentDirectionName => PaymentDirection.Description();
}
