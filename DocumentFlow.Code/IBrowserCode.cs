//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 13:34
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Data;

namespace DocumentFlow.Code
{
    public interface IBrowserCode
    {
        void Initialize(IBrowser browser);
        IList Select(IDbConnection connection, IBrowserParameters parameters);
        object SelectById(IDbConnection connection, Guid id, IBrowserParameters parameters);
        int Delete(IDbConnection connection, IDbTransaction transaction, Guid id);
        IEditorCode CreateEditor();
    }
}
