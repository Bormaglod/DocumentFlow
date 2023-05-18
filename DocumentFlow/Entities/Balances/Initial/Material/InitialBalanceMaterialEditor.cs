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
        EditorControls
            .AddDirectorySelectBox<Material>(x => x.ReferenceId, "Материал", (select) =>
                select
                    .SetDataSource(GetMaterials)
                    .EnableEditor<IMaterialEditor>()
                    .SetEditorWidth(400))
            .AddNumericTextBox(x => x.Amount, "Количество", (text) =>
                text
                    .SetNumberDecimalDigits(3)
                    .SetEditorWidth(200))
            .AddCurrencyTextBox(x => x.OperationSumma, "Сумма");
    }

    private IEnumerable<Material> GetMaterials() => Services.Provider.GetService<IMaterialRepository>()!.GetListExisting();
}
