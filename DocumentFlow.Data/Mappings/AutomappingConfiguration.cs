//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.04.2016
// Time: 9:44
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Mappings
{
    using System;
    using System.Collections.Generic;
    using FluentNHibernate;
    using FluentNHibernate.Automapping;
    
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        private static readonly IList<string> IgnoredMembers = new List<string> { "ImageSmall" };

        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "DocumentFlow.Data.Entities";
        }

        public override bool ShouldMap(Member member)
        {
            return base.ShouldMap(member) && !IgnoredMembers.Contains(member.Name);
        }
    }
}
