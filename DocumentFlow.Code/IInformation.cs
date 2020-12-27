//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.12.2020
// Time: 19:29
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code
{
    public interface IInformation
    {
        Guid Id { get; }
        DateTime Created { get; }
        DateTime Changed { get; }
        string Author { get; }
        string Editor { get; }
        string StatusCode { get; }
    }
}
