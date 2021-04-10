//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2020
// Time: 21:17
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Data;

namespace DocumentFlow.Code
{
    public interface IBrowserOperation
    {
        IList Select(IDbConnection connection, IBrowserParameters parameters);
        object Select(IDbConnection connection, Guid id, IBrowserParameters parameters);
        int Delete(IDbConnection connection, IDbTransaction transaction, Guid id);
    }
}
