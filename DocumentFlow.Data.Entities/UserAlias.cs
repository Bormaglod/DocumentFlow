//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2014
// Time: 10:38
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Data.Entities
{
    public class UserAlias : Entity<Guid>
    {
        public string name { get; set; }
        public string pg_name { get; set; }
        public string surname { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public bool is_system { get; set; }
        public override string ToString() => name;
    }
}
