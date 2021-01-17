//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.10.2020
// Time: 13:43
//-----------------------------------------------------------------------

using System.Collections;

namespace DocumentFlow.Code.Core
{
    public class GettingParametersEventArgs : ExecuteEventArgs
    {
        private readonly Hashtable parameters = new Hashtable();

        public GettingParametersEventArgs(IBrowser browser) : base(browser) { }
        public GettingParametersEventArgs(IEditor editor) : base(editor) { }

        public Hashtable Parameters => parameters;
    }
}
