//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using Syncfusion.Data;
using System.Collections;
using System.ComponentModel;

namespace DocumentFlow.Controls.Core;

public class DocumentInfoAggregate : ISummaryAggregate
{
    public int CountExceptDeleted { get; set; }
    public decimal SumExceptDeleted { get; set; }

    Action<IEnumerable, string, PropertyDescriptor> ISummaryAggregate.CalculateAggregateFunc()
    {
        return (items, property, pd) =>
        {
            if (items is IEnumerable<IDocumentInfo> enumerableItems)
            {
                switch (pd.Name)
                {
                    case "CountExceptDeleted":
                        CountExceptDeleted = enumerableItems.Count(r => !r.deleted);
                        break;
                    case "SumExceptDeleted":
                        var first = enumerableItems.FirstOrDefault();
                        if (first == null)
                        {
                            SumExceptDeleted = 0;
                        }
                        else
                        {
                            var p = first.GetType().GetProperty(property);
                            SumExceptDeleted = p != null ? enumerableItems.Where(r => !r.deleted).Sum(x => Convert.ToDecimal(p.GetValue(x))) : 0;
                        }

                        break;
                }
            }
        };
    }
}
