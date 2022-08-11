//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.06.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

namespace DocumentFlow.Entities.Products;

[Description("Тип провода")]
public class Wire : Directory
{
    public decimal? wsize { get; set; }
}
