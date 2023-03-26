//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.12.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.PageContents;
using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Measurements;

public class MeasurementEditor : Editor<Measurement>, IMeasurementEditor
{
    private const int headerWidth = 140;

    public MeasurementEditor(IMeasurementRepository repository, IPageManager pageManager) : base(repository, pageManager) 
    {
        AddControls(new Control[]
        {
            CreateTextBox(x => x.Code, "Код", headerWidth, 100, defaultAsNull: false),
            CreateTextBox(x => x.ItemName, "Наименование", headerWidth, 400),
            CreateTextBox(x => x.Abbreviation, "Сокр. наименование", headerWidth, 400)
        });
    }
}