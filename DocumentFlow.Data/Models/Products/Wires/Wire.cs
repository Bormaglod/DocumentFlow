//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Тип провода")]
public class Wire : Directory
{
    private decimal wsize;

    public decimal Wsize
    {
        get => wsize;
        set => SetProperty(ref wsize, value);
    }
}
