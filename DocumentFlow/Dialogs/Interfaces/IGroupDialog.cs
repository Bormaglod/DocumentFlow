//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.10.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

namespace DocumentFlow.Dialogs.Interfaces;

public delegate bool GroupOperation(string code, string name);

public interface IGroupDialog : IDialog
{
    bool Create(GroupOperation funcCreate);
    bool Edit(IDirectory directory, GroupOperation funcEdit);
}