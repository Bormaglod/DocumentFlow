//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.12.2020
// Time: 23:31
//-----------------------------------------------------------------------

using System.Linq;

namespace DocumentFlow.Code.Implementation
{
    public class ControlCollection<T> : IControlCollection<T>
    {
        private readonly IContainer container;

        public ControlCollection(IContainer container) => this.container = container;

        T IControlCollection<T>.this[string name] => (T)container.ControlsAll.OfType<IDataName>().First(x => x.Name == name);
    }
}
