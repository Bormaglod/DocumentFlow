//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Operations;

public class CuttingEditor : Editor<Cutting>, ICuttingEditor
{
    private const int headerWidth = 190;

    public CuttingEditor(ICuttingRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var code = new DfTextBox("code", "Код", headerWidth, 100) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var parent = new DfDirectorySelectBox<Cutting>("parent_id", "Группа", headerWidth, 400) { ShowOnlyFolder = true };
        var program_number = new DfChoice<int>("program_number", "Программа", headerWidth, 150);
        var segment_length = new DfIntegerTextBox<int>("segment_length", "Длина провода", headerWidth, 100) { DefaultAsNull = false };
        var strip_left = new DfWireStripping("Зачистка слева", StrippingPlace.Left, headerWidth);
        var strip_right = new DfWireStripping("Зачистка справа", StrippingPlace.Right, headerWidth);
        var produced = new DfIntegerTextBox<int>("produced", "Выработка", headerWidth, 100) { DefaultAsNull = false, Enabled = false };
        var prod_time = new DfIntegerTextBox<int>("prod_time", "Время выработки, сек.", headerWidth, 100) { DefaultAsNull = false, Enabled = false };
        var production_rate = new DfIntegerTextBox<int>("production_rate", "Норма выработка, ед./час", headerWidth, 100) { DefaultAsNull = false, Enabled = false };
        var salary = new DfNumericTextBox("salary", "Зарплата, руб.", headerWidth, 100) { DefaultAsNull = false, Enabled = false, NumberDecimalDigits = 4 };

        parent.SetDataSource(() => repository.GetOnlyFolders());
        program_number.SetDataSource(() =>
        {
            return repository
                .GetAvailableProgram(Document.program_number)
                .Select(x => new Choice<int>(x));
        });

        AddControls(new Control[]
        {
            code,
            name,
            parent,
            program_number,
            segment_length,
            strip_left,
            strip_right,
            produced,
            prod_time,
            production_rate,
            salary
        });
    }

    protected override void RegisterNestedBrowsers()
    {
        base.RegisterNestedBrowsers();
        RegisterNestedBrowser<IOperationUsageBrowser, OperationUsage>();
    }
}