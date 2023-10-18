//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.08.2023
//-----------------------------------------------------------------------

using DocumentFlow.Dialogs.Interfaces;

namespace DocumentFlow.Controls.Events;

public class DialogParametersEventArgs : EventArgs
{
    public DialogParametersEventArgs(IDataGridDialog dialog) => Dialog = dialog;

    public IDataGridDialog Dialog { get; }
}