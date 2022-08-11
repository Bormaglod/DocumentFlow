//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Editors;
using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Measurements;

public class MeasurementEditor : Editor<Measurement>, IMeasurementEditor
{
    private const int headerWidth = 140;

    public MeasurementEditor(IMeasurementRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        var code = new DfTextBox("code", "Код", headerWidth, 100) { DefaultAsNull = false };
        var name = new DfTextBox("item_name", "Наименование", headerWidth, 400);
        var abbreviation = new DfTextBox("abbreviation", "Сокр. наименование", headerWidth, 400);
        
        AddControls(new Control[]
        {
            code, 
            name, 
            abbreviation
        });
    }
}