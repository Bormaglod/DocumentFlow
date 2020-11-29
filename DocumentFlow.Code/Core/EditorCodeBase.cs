//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.10.2020
// Time: 13:43
//-----------------------------------------------------------------------

using System;
using System.Data;
using Dapper;
using Inflector;

namespace DocumentFlow.Code.Core
{
    abstract public class EditorCodeBase<T>
    {
        virtual public object SelectById<TId>(IDbConnection connection, TId id, IBrowserParameters parameters)
        {
            return connection.QuerySingleOrDefault<T>(GetSelect(), new { id });
        }

        virtual public TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor)
        {
            return connection.QuerySingle<TId>(GetInsert(), transaction: transaction);
        }

        public int Update(IDbConnection connection, IDbTransaction transaction, IEditor editor)
        {
            return connection.Execute(GetUpdate((T)editor.Entity), editor.Entity, transaction);
        }

        public int Delete<TId>(IDbConnection connection, IDbTransaction transaction, TId id)
        {
            return connection.Execute(GetDelete(), new { id }, transaction);
        }

        virtual public bool GetEnabledValue(string field, string status_name)
        {
            return true;
        }

        virtual public bool GetVisibleValue(string field, string status_name)
        {
            return true;
        }

        abstract protected string GetSelect();
        virtual protected string GetInsert()
        {
            return $"insert into {typeof(T).Name.Underscore()} default values returning id";
        }

        abstract protected string GetUpdate(T entity);

        virtual protected string GetDelete()
        {
            return $"delete from {typeof(T).Name.Underscore()} where id = :id";
        }
    }
}
