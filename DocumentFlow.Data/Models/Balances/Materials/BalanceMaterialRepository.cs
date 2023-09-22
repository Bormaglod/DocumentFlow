//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Exceptions;
using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

public class BalanceMaterialRepository : BalanceProductRepository<BalanceMaterial>, IBalanceMaterialRepository
{
    private readonly IMaterialRepository materials;

    public BalanceMaterialRepository(IDatabase database, IMaterialRepository materials) : base(database)
    {
        this.materials = materials;
    }

    public void UpdateMaterialRemaind(BalanceMaterial balance)
    {
        var avg_price = materials.GetAveragePrice(balance.ReferenceId, balance.DocumentDate);

        using var conn = GetConnection();
        using var transaction = conn.BeginTransaction();
        try
        {
            balance.OperationSumma = avg_price * Math.Abs(balance.Amount);
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
