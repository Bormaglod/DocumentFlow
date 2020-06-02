//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.06.2019
// Time: 20:04
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System.Collections.Generic;

    public interface IContainer
    {
        IList<IEditorControl> Controls { get; }
        IList<IEditorControl> GetControls(bool withChilds);
    }
}
