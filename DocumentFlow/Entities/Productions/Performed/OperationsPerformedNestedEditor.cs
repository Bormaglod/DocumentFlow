//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Productions.Performed;

public class OperationsPerformedNestedEditor : BaseOperationsPerformedEditor, IOperationsPerformedNestedEditor
{
    public OperationsPerformedNestedEditor(IOperationsPerformedRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) { }
}
