//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2020
// Time: 18:07
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Code
{
    public interface IDataOperation
    {
        object Select(IDbConnection connection, IIdentifier id, IBrowserParameters parameters);
        object Insert(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor);
        int Update(IDbConnection connection, IDbTransaction transaction, IEditor editor);
        int Delete(IDbConnection connection, IDbTransaction transaction, IIdentifier id);
    }
}
