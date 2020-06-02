//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.06.2019
// Time: 17:17
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using DocumentFlow.Data.Core;

    public class PrintForm : EntityUID
    {
        public virtual string Name { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual string FormText { get; set; }
        public virtual string Properties { get; set; }
    }
}
