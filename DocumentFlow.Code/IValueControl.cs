//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2021
// Time: 18:30
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IValueControl : IControl
    {
        object Value { get; set; }
    }
}
