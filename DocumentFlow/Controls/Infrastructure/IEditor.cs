//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

namespace DocumentFlow.Controls.Infrastructure;

public interface IEditor<T, B> : IEditorPage
    where T : class, IIdentifier<Guid>
    where B : IBrowser<T>
{
    IToolBar Toolbar { get; }
}
