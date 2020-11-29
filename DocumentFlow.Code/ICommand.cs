//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2020
// Time: 19:18
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface ICommand
    {
        string Code { get; }
        ICommand SetEnabled(bool enabled);
        ICommand SetVisible(bool visible);
    }
}
