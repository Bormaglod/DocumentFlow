//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.04.2014
// Time: 10:38
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Entities
{
    using System;
    using DocumentFlow.Data.Core;

    public class UserAlias : EntityUID
    {
        public virtual string Name { get; set; }
        public virtual string PgName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual bool IsSystem { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
