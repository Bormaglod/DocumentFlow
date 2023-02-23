//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//
// Версия 2022.11.15
//  - добавлен метод UpdateMaterialRemaind
// Версия 2023.1.24
//  - IDatabase перенесён из DocumentFlow.Data в DocumentFlow.Infrastructure.Data
// Версия 2023.2.23
//  - добавлена ссылка на DocumentFlow.Core.Exceptions
//
//-----------------------------------------------------------------------

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Balances;

public class BalanceMaterialRepository : BalanceProductRepository<BalanceMaterial>, IBalanceMaterialRepository
{
    public BalanceMaterialRepository(IDatabase database) : base(database) { }

    public void UpdateMaterialRemaind(BalanceMaterial balance)
    {
        var repo = Services.Provider.GetService<IMaterialRepository>();
        if (repo != null)
        {
            var avg_price = repo.GetAveragePrice(balance.reference_id, balance.document_date);

            using var conn = Database.OpenConnection();
            using var transaction = conn.BeginTransaction();
            try
            {
                balance.operation_summa = avg_price * Math.Abs(balance.amount);
                Update(balance);
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new RepositoryException(ExceptionHelper.Message(e), e);
            }
        }
    }
}
