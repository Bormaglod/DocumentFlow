//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Interfaces;

public interface IDiscriminator
{
    string? Discriminator { get; set; }
}
