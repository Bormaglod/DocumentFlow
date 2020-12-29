//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.03.2019
// Time: 15:24
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Entities
{
    public class Transition : Entity<Guid>
    {
        public string name { get; set; }
        public int starting_id { get; set; }
        public int? finishing_id { get; set; }
        public byte[] diagram_model { get; set; }
        public Status Starting { get; set; }
        public Status Finishing { get; set; }
        public override string ToString() => name;
    }
}
