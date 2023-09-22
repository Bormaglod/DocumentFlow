//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

using Syncfusion.Data;

using System.Collections;
using System.ComponentModel;

namespace DocumentFlow.Controls.Tools;

public class DocumentInfoAggregate : ISummaryAggregate
{
    public int CountExceptDeleted { get; set; }
    public int CountOnlyAccepted { get; set; }
    public int CountLegalRows { get; set; }
    public decimal SumExceptDeleted { get; set; }
    public decimal SumOnlyAccepted { get; set; }
    public decimal SumLegalRows { get; set; }

    Action<IEnumerable, string, PropertyDescriptor> ISummaryAggregate.CalculateAggregateFunc()
    {
        return (items, property, pd) =>
        {
            if (items is IEnumerable<IAccountingDocument> docItems)
            {
                switch (pd.Name)
                {
                    case "CountExceptDeleted":
                        CountExceptDeleted = docItems.Count(r => !r.Deleted);
                        break;
                    case "CountOnlyAccepted":
                        CountOnlyAccepted = docItems.Count(r => r.CarriedOut);
                        break;
                    case "CountLegalRows":
                        CountLegalRows = docItems.Count(r => r.CarriedOut && !r.Deleted);
                        break;
                    case "SumExceptDeleted":
                        SumExceptDeleted = GetSumDocuments(docItems, property, r => !r.Deleted);
                        break;
                    case "SumOnlyAccepted":
                        SumOnlyAccepted = GetSumDocuments(docItems, property, r => r.CarriedOut);
                        break;
                    case "SumLegalRows":
                        SumLegalRows = GetSumDocuments(docItems, property, r => r.CarriedOut && !r.Deleted);
                        break;
                }
            }
            else if (items is IEnumerable<IDocumentInfo> enumerableItems)
            {
                switch (pd.Name)
                {
                    case "CountExceptDeleted":
                    case "CountLegalRows":
                        CountExceptDeleted = enumerableItems.Count(r => !r.Deleted);
                        CountLegalRows = CountExceptDeleted;
                        break;
                    case "CountOnlyAccepted":
                        CountOnlyAccepted = enumerableItems.Count();
                        break;
                    case "SumExceptDeleted":
                    case "SumLegalRows":
                        var first = enumerableItems.FirstOrDefault();
                        if (first == null)
                        {
                            SumExceptDeleted = 0;
                            SumLegalRows = 0;
                        }
                        else
                        {
                            var p = first.GetType().GetProperty(property);
                            SumExceptDeleted = p != null ? enumerableItems.Where(r => !r.Deleted).Sum(x => Convert.ToDecimal(p.GetValue(x))) : 0;
                            SumLegalRows = SumExceptDeleted;
                        }

                        break;
                }
            }
        };
    }

    private static decimal GetSumDocuments(IEnumerable<IAccountingDocument> documents, string property, Func<IAccountingDocument, bool> predicate)
    {
        var first = documents.FirstOrDefault();
        if (first == null)
        {
            return 0;
        }
        else
        {
            var p = first.GetType().GetProperty(property);
            return p != null ? documents.Where(predicate).Sum(x => Convert.ToDecimal(p.GetValue(x))) : 0;
        }
    }
}
