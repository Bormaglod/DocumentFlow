//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2020
// Time: 23:31
//-----------------------------------------------------------------------

using System.Linq;

namespace DocumentFlow.Code.Implementation
{
    public class PopulateCollection : IPopulateCollection
    {
        private readonly IContainer container;

        public PopulateCollection(IContainer container) => this.container = container;

        IPopulate IPopulateCollection.this[string name] => container.ControlsAll.OfType<IDataName>().First(x => x.Name == name) as IPopulate;
    }
}
