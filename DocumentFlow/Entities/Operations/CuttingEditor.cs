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

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure;
using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Entities.Operations;

public class CuttingEditor : Editor<Cutting>, ICuttingEditor
{
    private ICuttingRepository repository;
    private const int headerWidth = 190;

    public CuttingEditor(ICuttingRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        this.repository = repository;

        AddControls(new Control[]
        {
            CreateTextBox(x => x.Code, "Код", headerWidth, 100, defaultAsNull: false),
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400),
            CreateDirectorySelectBox<Cutting>(x => x.ParentId, "Группа", headerWidth, 400, showOnlyFolder: true, data: repository.GetOnlyFolders),
            CreateChoice(x => x.ProgramNumber, "Программа", headerWidth, 150, data: GetChoices),
            CreateIntegerTextBox<int>(x => x.SegmentLength, "Длина провода", headerWidth, 100, defaultAsNull: false),
            new DfWireStripping("Зачистка слева", StrippingPlace.Left, headerWidth),
            new DfWireStripping("Зачистка справа", StrippingPlace.Right, headerWidth),
            CreateIntegerTextBox<int>(x => x.Produced, "Выработка", headerWidth, 100, defaultAsNull: false, enabled: false),
            CreateIntegerTextBox<int>(x => x.ProdTime, "Время выработки, сек.", headerWidth, 100, defaultAsNull: false, enabled: false),
            CreateIntegerTextBox<int>(x => x.ProductionRate, "Норма выработка, ед./час", headerWidth, 100, defaultAsNull: false, enabled: false),
            CreateDateTimePicker(x => x.DateNorm, "Дата нормирования", headerWidth, 150, required: false),
            CreateNumericTextBox(x => x.Salary, "Зарплата, руб.", headerWidth, 100, defaultAsNull: false, enabled: false)
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOperationUsageBrowser, OperationUsage>();
    }

    private IEnumerable<IChoice<int>> GetChoices() => repository.GetAvailableProgram(Document.ProgramNumber)
                                                                .Select(x => new Choice<int>(x));
}