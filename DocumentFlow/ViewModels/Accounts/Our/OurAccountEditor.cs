//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 21.06.2023
//-----------------------------------------------------------------------

using DocumentFlow.Data.Models;
using DocumentFlow.Interfaces;
using DocumentFlow.Tools;

namespace DocumentFlow.ViewModels;

[Entity(typeof(OurAccount), RepositoryType = typeof(IOurAccountRepository))]
public class OurAccountEditor : BaseAccountEditor, IOurAccountEditor
{
    public OurAccountEditor(IServiceProvider services, IPageManager pageManager) : base(services, pageManager)
    {
    }

    protected OurAccount OurAccount { get; set; } = null!;
}
