//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.12.2020
// Time: 21:34
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface ITool
    {
        bool Contains(ICommand command);
        void AddCommand(ICommand command);
    }
}
