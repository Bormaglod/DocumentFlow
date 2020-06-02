//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:51
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using DocumentFlow.Data.Entities;

    public interface ICommandFactory
    {
        void Execute(Command command, params object[] parameters);
        void Execute(string command, params object[] parameters);
    }
}
