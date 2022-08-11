//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Calculations;

public class CalculationCuttingEditor : BaseCalculationOperationEditor<CalculationCutting>, ICalculationCuttingEditor
{
    public CalculationCuttingEditor(ICalculationCuttingRepository repository, IPageManager pageManager) : base(repository, pageManager) { }

    protected override Guid? RootId => new Guid("0525748e-e98c-4296-bd0e-dcacee7224f3");

    protected override bool IsCuttingOperation => true;
}