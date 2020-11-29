//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.10.2020
// Time: 23:26
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Code
{
    public interface IEditorCode
    {
        void Initialize(IEditor editor, IDependentViewer dependentViewer = null);
        object SelectById<TId>(IDbConnection connection, TId id, IBrowserParameters parameters);
        TId Insert<TId>(IDbConnection connection, IDbTransaction transaction, IBrowserParameters parameters, IEditor editor);
        int Update(IDbConnection connection, IDbTransaction transaction, IEditor editor);
        int Delete<TId>(IDbConnection connection, IDbTransaction transaction, TId id);
        bool GetEnabledValue(string field, string status_name);
        bool GetVisibleValue(string field, string status_name);
    }
}
