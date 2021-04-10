//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.10.2020
// Time: 23:26
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Code
{
    public interface IValueEditor
    {
        IIdentifier Entity { get; }
        IControlCollection<IPopulate> Populates { get; }
        IControlCollection<IValueControl> Values { get; }
    }
}
