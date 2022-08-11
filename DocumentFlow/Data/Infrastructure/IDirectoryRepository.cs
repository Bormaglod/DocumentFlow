//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using System.Data;

namespace DocumentFlow.Data.Infrastructure;

public interface IDirectoryRepository<T> : IOwnedRepository<Guid, T>
    where T : IIdentifier<Guid>
{
    Guid AddFolder(Guid? parent_id, string code, string folder_name);
    Guid? GetRoot(Guid object_id);
    IReadOnlyList<T> GetByParent(Guid? parent_id, IFilter? filter = null);
    IReadOnlyList<T> GetOnlyFolders();
    IReadOnlyList<T> GetParentFolders(Guid object_id);
    void WipeParented(Guid? owner, Guid? parent);
    void WipeParented(Guid? owner, Guid? parent, IDbTransaction transaction);
}
