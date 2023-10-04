//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2022
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace DocumentFlow.Controls.Events;

public class ConfirmGeneratingColumnArgs : CancelEventArgs
{
    public ConfirmGeneratingColumnArgs(string mappingName, bool cancel) : base(cancel)
    {
        MappingName = mappingName;
    }

    public string MappingName { get; }
}
