//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.04.2016
// Time: 10:02
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Mappings.Convetions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    using DocumentFlow.Data.Core;
    
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            if (typeof(EntityUID).IsAssignableFrom(instance.EntityType))
                instance.GeneratedBy.GuidNative();
            else
                instance.GeneratedBy.Native();

            instance.Column("id");
        }
    }
}
