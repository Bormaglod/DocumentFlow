//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.10.2020
// Time: 13:43
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DocumentFlow.Code.System
{
    public class GettingParametersEventArgs : ExecuteEventArgs
    {
        private Dictionary<string, object> parameters = new Dictionary<string, object>();

        public GettingParametersEventArgs(IBrowser browser) : base(browser) { }

        public IDictionary<string, object> Parameters => parameters;
    }
}
