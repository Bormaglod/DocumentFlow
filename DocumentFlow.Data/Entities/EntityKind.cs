//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
// Time: 18:26
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using DocumentFlow.Data.Core;

    public class EntityKind : EntityUID
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual bool HasGroup { get; set; }
        public virtual Transition Transition { get; set; }
        public virtual string Prefix { get; set; }
        public virtual int NumberDigits { get; set; }

        public override string ToString()
        {
            return Code;
        }
    }
}
