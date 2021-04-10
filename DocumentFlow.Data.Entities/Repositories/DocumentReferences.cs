//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.01.2021
// Time: 19:16
//-----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data;
using Dapper;
using DocumentFlow.Data.Entities;


namespace DocumentFlow.Data.Repositories
{
    public static class DocumentReferences
    {
        public static BindingList<DocumentRefs> Get(Guid doc_id)
        {
            using var conn = Db.OpenConnection();
            return Get(conn, doc_id);
        }

        public static BindingList<DocumentRefs> Get(IDbConnection connection, Guid doc_id)
        {
            return new BindingList<DocumentRefs>(
                connection.Query<DocumentRefs>("select * from document_refs where owner_id = :owner_id", new { owner_id = doc_id }).AsList()
                );
        }

        public static long Insert(this DocumentRefs refs, IDbTransaction transaction)
        {
            long id = Insert(transaction, refs);
            refs.id = id;
            return id;
        }

        public static int Update(this DocumentRefs refs, IDbTransaction transaction)
        {
            return Update(transaction, refs);
        }

        public static int Delete(this DocumentRefs refs, IDbTransaction transaction)
        {
            return Delete(transaction, refs);
        }

        private static long Insert(IDbTransaction transaction, DocumentRefs refs)
        {
            return transaction.Connection.QuerySingle<long>("insert into document_refs (owner_id, file_name, note, crc, length) values (:owner_id, :file_name, :note, :crc, :length) returning id", refs, transaction);
        }

        private static int Update(IDbTransaction transaction, DocumentRefs refs)
        {
            return transaction.Connection.Execute("update document_refs set file_name = :file_name, note = :note, crc = :crc, length = :length where id = :id", refs, transaction);
        }

        private static int Delete(IDbTransaction transaction, DocumentRefs refs)
        {
            return transaction.Connection.Execute("delete from document_refs where id = :id", new { refs.id }, transaction);
        }
    }
}
