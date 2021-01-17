//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.01.2021
// Time: 12:52
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.Core
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public class DatabaseScriptAttribute : Attribute
    {
    }
}
