//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.10.2020
// Time: 13:43
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Inflector;

namespace DocumentFlow.Code.Core
{
    abstract public class BrowserCodeBase<T>
    {
        public IList Select(IDbConnection connection, IBrowserParameters parameters)
        {
            return SelectAll(connection, parameters).AsList();
        }

        virtual public IEnumerable<T> SelectAll(IDbConnection connection, IBrowserParameters parameters)
        {
            return connection.Query<T>(GetSelect());
        }

        public object SelectById(IDbConnection connection, Guid id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<T>(GetSelectById(), new { id });
        }

        public int Delete(IDbConnection connection, IDbTransaction transaction, Guid id)
        {
            return connection.Execute(GetDelete(), new { id }, transaction);
        }

        abstract protected string GetSelect();
        abstract protected string GetSelectById();
        virtual protected string GetDelete()
        {
            return $"delete from {typeof(T).Name.Underscore()} where id = :id";
        }
    }
}
