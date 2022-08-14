//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.08.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Entities.Wages;

public class ReportCardLabel
{
    public ReportCardLabel(string label, string description, bool onlyDay = true) => (Label, Description, OnlyDay) = (label, description, onlyDay);
    public string Label { get; }
    public string Description { get; }
    public bool OnlyDay { get; }
}
