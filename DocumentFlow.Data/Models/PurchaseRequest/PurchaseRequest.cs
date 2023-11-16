//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

namespace DocumentFlow.Data.Models;

[EntityName("Заявка")]
public class PurchaseRequest : ShipmentDocument
{
    private string? note;
    private string state = "not active";

    public string? Note 
    { 
        get => note;
        set => SetProperty(ref note, value);
    }

    [DenyCopying]
    [EnumType("purchase_state")]
    public string State 
    { 
        get => state;
        set => SetProperty(ref state, value);
    }

    public decimal CostOrder { get; protected set; }
    public bool TaxPayer { get; protected set; }
    public int Tax { get; protected set; }
    public decimal TaxValue { get; protected set; }
    public decimal FullCost { get; protected set; }
    public decimal Prepayment { get; protected set; }
    public decimal ReceiptPayment { get; protected set; }
    public decimal DeliveryAmount { get; protected set; }
    public bool Executed { get => DeliveryAmount > 0; }
    public bool? Paid 
    { 
        get
        {
            if (Prepayment + ReceiptPayment == 0)
            {
                return false;
            }

            if (Prepayment + ReceiptPayment == DeliveryAmount)
            {
                return true;
            }

            return null;
        }
    }

    [Write(false)]
    public PurchaseState PurchaseState
    {
        get { return Enum.Parse<PurchaseState>(State.Dehumanize()); }
        set { State = StateFromValue(value); }
    }

    public string StateName => PurchaseState.Description();

    [WritableCollection]
    public IList<PurchaseRequestPrice> Prices { get; protected set; } = null!;

    public static string StateFromValue(PurchaseState state) => state.ToString().Humanize(LetterCasing.LowerCase);
}
