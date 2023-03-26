//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//
// Версия 2023.3.14
//  - GetAllMaterials заменен на GetAllValid
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Warehouse;

public class AdjustingBalancesEditor : DocumentEditor<AdjustingBalances>, IAdjustingBalancesEditor
{
    public AdjustingBalancesEditor(IAdjustingBalancesRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        AddControls(new Control[]
        {
            CreateDirectorySelectBox<Material, IMaterialEditor>(x => x.MaterialId, "Материал", 100, 400, data: GetMaterials),
            CreateNumericTextBox(x => x.Quantity, "Количество", 100, 200, digits: 3)
        });
    }

    private IEnumerable<Material> GetMaterials() => Services.Provider.GetService<IMaterialRepository>()!.GetAllValid();
}
