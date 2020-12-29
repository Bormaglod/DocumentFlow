//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.06.2018
// Time: 22:09
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Entities
{
    public class Catalog : Entity<Guid>
    {
        public string code { get; set; }
        public string name { get; set; }
        public Guid? parent_id { get; set; }
        public int order_index { get; set; }
        public Guid? command_id { get; set; }
        public Command Command { get; set; }

        public override string ToString() => $"[{code}] {name}";
    }
}
