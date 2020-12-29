//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.03.2019
// Time: 19:24
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Entities
{
    public class ChangingStatus : Entity<Guid>
    {
        public string name { get; set; }
        public Guid transition_id { get; set; }
        public int from_status_id { get; set; }
        public int to_status_id { get; set; }
        public Guid? picture_id { get; set; }
        public int order_index { get; set; }
        public bool is_system { get; set; }
        public Status FromStatus { get; set; }
        public Status ToStatus { get; set; }
        public Picture Picture { get; set; }
        public override string ToString() => name;
    }
}
