//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.05.2020
// Time: 18:21
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using DocumentFlow.Data.Core;

    public class PrintKindForm : EntityID
    {
        public virtual EntityKind EntityKind { get; set; }
        public virtual PrintForm PrintForm { get; set; }
        public virtual bool DefaultForm { get; set; }
    }
}
