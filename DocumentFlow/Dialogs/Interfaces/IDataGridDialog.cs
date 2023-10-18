//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.10.2023
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs.Interfaces;

public interface IDataGridDialog : IDialog
{
    bool Create<T>([MaybeNullWhen(false)] out T row) where T : new();
    bool Edit<T>(T row);
}