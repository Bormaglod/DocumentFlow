//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.06.2019
// Time: 10:54
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System;

    public interface IBindingEditorControl
    {
        event EventHandler ValueChanged;

        string DataField { get; set; }
        object Value { get; set; }
        object DefaultValue { get; }
        Type ValueType { get; }
    }
}
