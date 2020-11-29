//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2020
// Time: 21:03
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    public interface ICommandCollection : IEnumerable<ICommand>
    {
        ICommand Get(string name);
        ICommandAdded Add(CommandMethod method, string name, params string[] toolStripNames);
        void OpenDocument(Guid id);
        void OpenDiagram(Guid id);
    }
}
