//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Ед. изм.")]
public class Measurement : Directory
{
    private string? abbreviation;

    public string? Abbreviation 
    {
        get => abbreviation;
        set => SetProperty(ref abbreviation, value);
    }
}
