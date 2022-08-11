//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Accounts;

public class AccountEditor : BaseAccountEditor<Account>, IAccountEditor
{
    public AccountEditor(IAccountRepository repository, IPageManager pageManager) : base(repository, pageManager) { }
}