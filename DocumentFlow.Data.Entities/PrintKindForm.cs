//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.05.2020
// Time: 18:21
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Base;

namespace DocumentFlow.Data.Entities
{
    public class PrintKindForm : Entity<long>
    {
        public Guid entity_kind_id { get; set; }
        public Guid print_form_id { get; set; }
        public bool default_form { get; set; }
        public EntityKind EntityKind { get; set; }
        public PrintForm PrintForm { get; set; }
    }
}
