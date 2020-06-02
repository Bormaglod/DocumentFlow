//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.03.2019
// Time: 19:24
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using DocumentFlow.Data.Core;

    public class ChangingStatus : EntityUID
    {
        public virtual string Name { get; set; }
        public virtual Transition Transition { get; set; }
        public virtual Status FromStatus { get; set; }
        public virtual Status ToStatus { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual int OrderIndex { get; set; }
        public virtual bool IsSystem { get; set; }
    }
}
