//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.02.2023
//-----------------------------------------------------------------------

using DocumentFlow.Entities.Productions.Order;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Waybills;

public class WaybilSlaleCreationBased : ICreationBased
{
    public string DocumentName => "Реализация";

    public Type DocumentEditorType => typeof(IWaybillSaleEditor);

    public bool CanCreateDocument(Type documentType) => documentType == typeof(ProductionOrder);

    private static Guid Create(ProductionOrder document)
    {
        var db = Services.Provider.GetService<IDatabase>();

        if (db != null)
        {
            using var conn = db.OpenConnection();
            using var transaction = conn.BeginTransaction();

            WaybillSale waybill = new()
            {
                ContractorId = document.ContractorId,
                ContractId = document.ContractId,
                OwnerId = document.Id
            };

            var wrr = Services.Provider.GetService<IWaybillSaleRepository>();
            var prpr = Services.Provider.GetService<IProductionOrderPriceRepository>();
            var wrpr = Services.Provider.GetService<IWaybillSalePriceRepository>();
            if (wrr != null && prpr != null && wrpr != null)
            {
                try
                {
                    var wr = wrr.Add(waybill, transaction);
                    foreach (var item in prpr.GetByOwner(document.Id))
                    {
                        WaybillSalePrice row = new()
                        {
                            OwnerId = wr.Id,
                            ReferenceId = item.ReferenceId,
                            Amount = item.Amount,
                            Price = item.Price,
                            ProductCost = item.ProductCost,
                            Tax = item.Tax,
                            TaxValue = item.TaxValue,
                            FullCost = item.FullCost,
                            TableName = "goods"
                        };

                        wrpr.Add(row, transaction);
                    }

                    transaction.Commit();

                    return wr.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        throw new Exception($"При попытке создать документ \"Реализация\" возникли неустранимые ошибки.");
    }

    Guid ICreationBased.Create<T>(T document)
    {
        if (document is ProductionOrder order)
        {
            return Create(order);
        }

        throw new NotImplementedException();
    }
}
