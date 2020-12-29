//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
// Time: 18:26
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Entities
{
    public class EntityKind : Entity<Guid>
    {
        public string code { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public bool has_group { get; set; }
        public Guid transition_id { get; set; }
        public override string ToString() => code;
    }
}
