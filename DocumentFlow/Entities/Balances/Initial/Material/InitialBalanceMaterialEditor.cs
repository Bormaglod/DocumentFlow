//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.06.2022
//
// Версия 2023.3.14
//  - GetAllMaterials заменен на GetAllValid
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Balances.Initial;

internal class InitialBalanceMaterialEditor : DocumentEditor<InitialBalanceMaterial>, IInitialBalanceMaterialEditor
{
    public InitialBalanceMaterialEditor(IInitialBalanceMaterialRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) 
    {
        AddControls(new Control[]
        {
            CreateDirectorySelectBox<Material, IMaterialEditor>(x => x.ReferenceId, "Материал", 100, 400, data: GetMaterials),
            CreateNumericTextBox(x => x.Amount, "Количество", 100, 200, digits: 3),
            CreateCurrencyTextBox(x => x.OperationSumma, "Сумма", 100, 100)
        });
    }

    private IEnumerable<Material> GetMaterials() => Services.Provider.GetService<IMaterialRepository>()!.GetAllValid();
}
