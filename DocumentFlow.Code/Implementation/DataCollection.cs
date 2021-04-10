//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2020
// Time: 23:15
//-----------------------------------------------------------------------

using System.Linq;

namespace DocumentFlow.Code.Implementation
{
    public class DataCollection : IDataCollection
    {
        private readonly IContainer container;

        public DataCollection(IContainer container) => this.container = container;

        public object this[string name] 
        { 
            get
            {
                var c = container.ControlsAll.OfType<IDataName>().First(x => x.Name == name) as IBindingControl;
                return c.Value;
            }

            set
            {
                var c = container.ControlsAll.OfType<IDataName>().First(x => x.Name == name) as IBindingControl;
                c.Value = value;
            }
        }
    }
}
