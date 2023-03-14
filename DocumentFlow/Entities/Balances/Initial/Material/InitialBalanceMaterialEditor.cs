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

using DocumentFlow.Controls.Editors;
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
            new DfDirectorySelectBox<Material>("reference_id", "Материал", 100, 400) 
            {
                OpenAction = (t) => pageManager.ShowEditor<IMaterialEditor, Material>(t),
                DataSourceFunc = () => Services.Provider.GetService<IMaterialRepository>()?.GetAllValid()
            },
            new DfNumericTextBox("amount", "Количество", 100, 199) 
            { 
                NumberDecimalDigits = 3 
            },
            new DfCurrencyTextBox("operation_summa", "Сумма", 100, 100)
        });
    }
}
