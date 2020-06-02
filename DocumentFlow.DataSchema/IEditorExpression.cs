//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.04.2020
// Time: 23:20
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;

    public interface IEditorExpression : IControlExpression
    {
        IList<IDataExpresssion> Expressions { get; }
    }
}
