//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2020
// Time: 22:51
//-----------------------------------------------------------------------

using System.Collections.Generic;
using DocumentFlow.Data.Entities;

namespace DocumentFlow
{
    public interface ICommandFactory
    {
        Command this[string code] { get; }
        IEnumerable<Command> Commands { get; }
        void Execute(Command command, params object[] parameters);
        void Execute(string command, params object[] parameters);
    }
}
