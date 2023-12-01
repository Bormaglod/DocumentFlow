//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.11.2023
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

namespace DocumentFlow.Dialogs.Interfaces;

public interface IScannerDialog : IDialog
{
    bool Scan([MaybeNullWhen(false)] out IList<Image> images);
}
