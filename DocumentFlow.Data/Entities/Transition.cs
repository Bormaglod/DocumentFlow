//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.03.2019
// Time: 15:24
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using DocumentFlow.Data.Core;

    public class Transition : EntityUID
    {
        public virtual string Name { get; set; }
        public virtual Status Starting { get; set; }
        public virtual Status Finishing { get; set; }
        public virtual byte[] DiagramModel { get; set; }
    }
}
