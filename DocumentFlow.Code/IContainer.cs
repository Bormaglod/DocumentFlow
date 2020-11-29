//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.06.2019
// Time: 20:04
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;

namespace DocumentFlow.Code
{
    public delegate bool DataFieldParameter(string dataField);

    public interface IContainer
    {
        IEnumerable<IControl> Controls { get; }
        IEnumerable<IControl> ControlsAll { get; }
        void Add(IControl control);
        void Add(IControl[] controls);
        void Populate(IDbConnection conn, object entity, DataFieldParameter getEnabled = null, DataFieldParameter getVisible = null);
        IControl AsControl();
    }
}
