//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Entities.Products;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Warehouse;

public class AdjustingBalancesEditor : DocumentEditor<AdjustingBalances>, IAdjustingBalancesEditor
{
    public AdjustingBalancesEditor(IAdjustingBalancesRepository repository, IPageManager pageManager) : base(repository, pageManager, true) 
    {
        var material = new DfDirectorySelectBox<Material>("material_id", "Материал", 100, 400)
        {
            OpenAction = (t) => pageManager.ShowEditor<IMaterialEditor, Material>(t)
        };

        var quantity = new DfNumericTextBox("quantity", "Количество", 100, 200) { NumberDecimalDigits = 3 }; 

        material.SetDataSource(() => Services.Provider.GetService<IMaterialRepository>()?.GetAllMaterials());


        AddControls(new Control[]
        {
            material,
            quantity
        });
    }
}
