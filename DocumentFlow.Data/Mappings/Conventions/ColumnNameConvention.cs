//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.04.2016
// Time: 12:06
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Mappings.Convetions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    using Inflector;

    public class ColumnNameConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            instance.Column(instance.Name.Underscore());
        }
    }
}
