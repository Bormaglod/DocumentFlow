//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.12.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Entities.PurchaseRequestLib;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Waybills;

public class WaybillReceiptCreationBased : ICreationBased
{
    public string DocumentName => "Поступление материалов";

    public Type DocumentEditorType => typeof(IWaybillReceiptEditor);

    public bool CanCreateDocument(Type documentType) => documentType == typeof(PurchaseRequest);

    private static Guid Create(PurchaseRequest document)
    {
        var db = Services.Provider.GetService<IDatabase>();

        if (db != null)
        {
            using var conn = db.OpenConnection();
            using var transaction = conn.BeginTransaction();

            WaybillReceipt waybill = new()
            {
                ContractorId = document.ContractorId,
                ContractId = document.ContractId
            };

            if (document.PurchaseState == PurchaseState.Active)
            {
                waybill.OwnerId = document.Id;
            }

            var wrr = Services.Provider.GetService<IWaybillReceiptRepository>();
            var prpr = Services.Provider.GetService<IPurchaseRequestPriceRepository>();
            var wrpr = Services.Provider.GetService<IWaybillReceiptPriceRepository>();
            if (wrr != null && prpr != null && wrpr != null)
            {
                try
                {
                    var wr = wrr.Add(waybill, transaction);
                    foreach (var item in prpr.GetByOwner(document.Id))
                    {
                        WaybillReceiptPrice row = new()
                        {
                            OwnerId = wr.Id,
                            ReferenceId = item.ReferenceId,
                            Amount = item.Amount,
                            Price = item.Price,
                            ProductCost = item.ProductCost,
                            Tax = item.Tax,
                            TaxValue = item.TaxValue,
                            FullCost = item.FullCost
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

        throw new Exception($"При попытке создать документ \"Поступление материалов\" возникли неустранимые ошибки.");
    }

    Guid ICreationBased.Create<T>(T document)
    {
        if (document is PurchaseRequest purchaseRequest)
        {
            return Create(purchaseRequest);
        }

        throw new NotImplementedException();
    }
}
