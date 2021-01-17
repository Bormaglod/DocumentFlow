//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2020
// Time: 16:51
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace DocumentFlow.Core.Exceptions
{
    public class CompilerException : Exception
    {
        public CompilerException(IEnumerable<Diagnostic> failures) : base() => Failures = failures;
        public IEnumerable<Diagnostic> Failures { get; }
    }
}
