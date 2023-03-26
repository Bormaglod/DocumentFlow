//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//
// Версия 2023.1.21
//  - поле Directions заменено на свойство и поменяло тип на
//    IReadOnlyDictionary
// Версия 2023.1.27
//  - добавлено свойство without_distrib
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.PaymentOrders;

public enum PaymentDirection { Income, Expense }

[Description("Банк/касса")]
public class PaymentOrder : AccountingDocument
{
    private readonly static Dictionary<PaymentDirection, string> directions = new()
    {
        [PaymentDirection.Income] = "Приход",
        [PaymentDirection.Expense] = "Расход"
    };

    public Guid? ContractorId { get; set; }
    public string? ContractorName { get; protected set; }
    public string? PaymentNumber { get; set; }
    public DateTime DateOperation { get; set; } = DateTime.Now;
    public decimal TransactionAmount { get; set; }
    public decimal? Expense { get; protected set; }
    public decimal? Income { get; protected set; }

    [EnumType("payment_direction")]
    public string Direction { get; set; } = "Expense";

    public bool WithoutDistrib { get; set; }

    public PaymentDirection PaymentDirection
    {
        get { return Enum.Parse<PaymentDirection>(Direction.Pascalize()); }
        protected set { Direction = value.ToString().Underscore(); }
    }

    public static IReadOnlyDictionary<PaymentDirection, string> Directions => directions;
}
