//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//
// Версия 2023.2.4
//  - добавлен поле "Дата нормирования"
// Версия 2023.2.6
//  - добавлено поле goods
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Dialogs.Infrastructure;
using DocumentFlow.Entities.OperationTypes;
using DocumentFlow.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace DocumentFlow.Entities.Operations;

public class OperationEditor : Editor<Operation>, IOperationEditor
{
    private const int headerWidth = 180;

    public OperationEditor(IOperationRepository repository, IPageManager pageManager) : base(repository, pageManager)
    {
        EditorControls
            .AddTextBox(x => x.Code, "Код", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddComboBox<OperationType>(x => x.TypeId, "Тип операции", combo =>
                combo
                    .SetDataSource(GetTypes)
                    .EnableEditor<IOperationTypeEditor>()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(250))
            .AddDirectorySelectBox<Operation>(x => x.ParentId, "Группа", select =>
                select
                    .SetDataSource(repository.GetOnlyFolders)
                    .ShowOnlyFolder()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddIntergerTextBox<int>(x => x.Produced, "Выработка", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .AddIntergerTextBox<int>(x => x.ProdTime, "Время выработки, сек.", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .AddIntergerTextBox<int>(x => x.ProductionRate, "Норма выработка, ед./час", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Disable())
            .AddDateTimePicker(x => x.DateNorm, "Дата нормирования", date =>
                date
                    .NotRequired()
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddNumericTextBox(x => x.Salary, "Зарплата, руб.", text =>
                text
                    .SetNumberDecimalDigits(4)
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Disable())
            .AddDataGrid<OperationGoods>(grid =>
                grid
                    .SetRepository<IOperationGoodsRepository>()
                    .SetHeader("Операция будет использоваться только при производстве этих изделий")
                    .Dialog<IGoodsSelectDialog>()
                    .SetDock(DockStyle.Fill));
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOperationUsageBrowser, OperationUsage>();
    }

    private IEnumerable<OperationType> GetTypes() => Services.Provider.GetService<IOperationTypeRepository>()!.GetAllValid(callback: q => q.OrderBy("item_name"));
}