//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces.Filters;

namespace DocumentFlow.Data.Interfaces.Repository;

public interface IDirectoryRepository<T> : IOwnedRepository<Guid, T>
    where T : IIdentifier<Guid>
{
    Guid GetRoot(Guid objectId);
    Guid AddFolder(Guid? parent, string code, string folderName);
    IReadOnlyList<T> GetByParent(Guid? parent, IFilter? filter = null);
    IReadOnlyList<T> GetOnlyFolders();
    void WipeParented(Guid? owner, Guid? parent);
}
