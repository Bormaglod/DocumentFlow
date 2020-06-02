//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:22
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Windows.Forms;

    public interface IContainerPage
    {
        IPage Selected { get; set; }
        void Add(Control control);
        bool Contains(Guid id);
        IPage Get(Guid id);
    }
}
