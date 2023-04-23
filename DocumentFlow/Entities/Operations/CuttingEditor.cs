//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//
// Версия 2023.2.4
//  - добавлен поле "Дата нормирования"
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Operations;

public class CuttingEditor : Editor<Cutting>, ICuttingEditor
{
    private readonly ICuttingRepository repository;
    private const int headerWidth = 190;

    public CuttingEditor(ICuttingRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        this.repository = repository;

        EditorControls
            .AddTextBox(x => x.Code, "Код", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .AddTextBox(x => x.ItemName, "Наименование", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddDirectorySelectBox<Cutting>(x => x.ParentId, "Группа", select =>
                select
                    .ShowOnlyFolder()
                    .SetDataSource(repository.GetOnlyFolders)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(400))
            .AddChoice<int>(x => x.ProgramNumber, "Программа", choice =>
                choice
                    .SetDataSource(GetChoices)
                    .SetHeaderWidth(headerWidth)
                    .SetEditorWidth(150))
            .AddIntergerTextBox<int>(x => x.SegmentLength, "Длина провода", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue())
            .AddWireStripping(x => x.Left, "Зачистка слева", strip =>
                strip
                    .SetHeaderWidth(headerWidth))
            .AddWireStripping(x => x.Right, "Зачистка справа", strip =>
                strip
                    .SetHeaderWidth(headerWidth))
            .AddIntergerTextBox<int>(x => x.Produced, "Выработка", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Disable())
            .AddIntergerTextBox<int>(x => x.ProdTime, "Время выработки, сек.", text =>
                text
                    .SetHeaderWidth(headerWidth)
                    .DefaultAsValue()
                    .Disable())
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
                    .Disable());
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOperationUsageBrowser, OperationUsage>();
    }

    private IEnumerable<IChoice<int>> GetChoices() => repository.GetAvailableProgram(Document.ProgramNumber)
                                                                .Select(x => new Choice<int>(x));
}