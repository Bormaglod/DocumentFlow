//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.11.2020
// Time: 16:59
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.Exceptions
{
    public enum PopulateMethod { Populate, AfterPopulate }

    public class PopulateControlException : Exception
    {
        public PopulateControlException(IControl control, PopulateMethod method, string message) : base(message) 
        {
            Control = control;
            Method = method;
        }

        public PopulateControlException(IControl control, PopulateMethod method, string message, Exception innerException) : base(message, innerException) 
        {
            Control = control;
            Method = method;
        }

        public IControl Control { get; }
        public PopulateMethod Method { get; }
    }
}
