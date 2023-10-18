//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 16.10.2023
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs.Interfaces;

public interface ICodeGeneratorDialog : IDialog
{
    bool Get(string source, [MaybeNullWhen(false)] out string code);
}
