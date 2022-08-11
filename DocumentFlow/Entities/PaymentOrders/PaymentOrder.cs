//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.PaymentOrders;

public enum PaymentDirection { Income, Expense }

[Description("Банк/касса")]
public class PaymentOrder : AccountingDocument
{
    public Guid? contractor_id { get; set; }
    public string? contractor_name { get; protected set; }
    public string? payment_number { get; set; }
    public DateTime date_operation { get; set; }
    public decimal transaction_amount { get; set; }
    public decimal? expense { get; protected set; }
    public decimal? income { get; protected set; }

    [EnumType("payment_direction")]
    public string? direction { get; set; }

    public PaymentDirection PaymentDirection
    {
        get { return Enum.Parse<PaymentDirection>(direction.Pascalize()); }
        protected set { direction = value.ToString().Underscore(); }
    }

    public static Dictionary<PaymentDirection, string> Directions = new()
    {
        [PaymentDirection.Income] = "Приход",
        [PaymentDirection.Expense] = "Расход"
    };
}
