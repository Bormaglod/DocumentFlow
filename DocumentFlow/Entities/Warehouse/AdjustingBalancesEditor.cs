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
        EditorControls
            .AddDirectorySelectBox<Material>(x => x.MaterialId, "Материал", select =>
                select
                    .SetDataSource(GetMaterials)
                    .SetEditorWidth(400))
            .AddNumericTextBox(x => x.Quantity, "Количество", text =>
                text
                    .SetNumberDecimalDigits(3)
                    .SetEditorWidth(200));
    }

    private IEnumerable<Material> GetMaterials() => Services.Provider.GetService<IMaterialRepository>()!.GetListExisting();
}
