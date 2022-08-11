//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Productions.Performed;

public class OperationsPerformedEditor : BaseOperationsPerformedEditor, IOperationsPerformedEditor
{
    public OperationsPerformedEditor(IOperationsPerformedRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, false) { }
}
