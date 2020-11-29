//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.10.2020
// Time: 21:05
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Code.System;

namespace DocumentFlow.Code
{
    public interface ICommandAdded
    {
        event EventHandler<GettingParametersEventArgs> GettingParameters;
        event EventHandler<ExecuteEventArgs> Click;
        ICommandAdded SetIcon(string name);
    }
}
