//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(Account), RepositoryType = typeof(IAccountRepository))]
public class AccountEditor : BaseAccountEditor, IAccountEditor
{
    public AccountEditor(IServiceProvider services) : base(services)
    {
    }

    protected Account Account { get; set; } = null!;
}
