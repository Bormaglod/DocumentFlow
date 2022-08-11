//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Infrastructure;

public interface IBindingControl
{
    string Header { get; }
    string PropertyName { get; }
    bool AllowSaving { get; set; }
    object? Value { get; set; }
    void ClearValue();
}