//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.02.2022
//
// Версия 2022.08.17
//  - удалено свойство UseGetId
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Infrastructure;

public interface IDiscriminator
{
    string TableName { get; set; }
}
