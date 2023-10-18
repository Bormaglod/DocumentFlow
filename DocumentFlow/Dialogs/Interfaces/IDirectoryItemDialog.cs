//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 17.10.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;
using DocumentFlow.Data.Interfaces.Repository;

namespace DocumentFlow.Dialogs.Interfaces;

public interface IDirectoryItemDialog : IDialog
{
    T? Get<T>(IEnumerable<T> items, Guid? selectedItem = null, bool withColumns = true) 
        where T : class, IDirectory;

    T? Get<T, R>(Guid? selectedItem = null, bool withColumns = true)
        where T : class, IDirectory
        where R : IRepository<Guid, T>;
}