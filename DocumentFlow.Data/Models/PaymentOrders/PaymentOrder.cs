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
        set => SetProperty(ref contractorId, value);
    }

    /// <summary>
    /// Возвращает или устанавливает номер платежного поручения или расходного/приходного ордера.
    /// </summary>
    public string? PaymentNumber 
    { 
        get => paymentNumber;
        set => SetProperty(ref paymentNumber, value);
    }

    /// <summary>
    /// Возвращает или устанавливает дату операции.
    /// </summary>
    public DateTime DateOperation 
    { 
        get => dateOperation;
        set => SetProperty(ref dateOperation, value);
    }

    /// <summary>
    /// Возвращает или устанавливает сумму операции.
    /// </summary>
    public decimal TransactionAmount 
    { 
        get => transactionAmount;
        set => SetProperty(ref transactionAmount, value);
    }

    /// <summary>
    /// Возвращает или устанавливает направление движения денег (приход или расход). Значение должно соответствовать 
    /// значению типа payment_direction (Postgresql).
    /// </summary>
    [EnumType("payment_direction")]
    public string Direction
    {
        get => direction;
        set => SetProperty(ref direction, value);
    }

    /// <summary>
    /// Возвращает или устанавливает флаг определяющий возможность проведения документа без распределения сумм по документам.
    /// </summary>
    public bool WithoutDistrib
    {
        get => withoutDistrib;
        set => SetProperty(ref withoutDistrib, value);
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
