//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data;

public abstract class Entity<T> : Identifier<T>, IEntity<T>
    where T : struct, IComparable
{
    [Display(AutoGenerateField = false)]
    public Guid? OwnerId { get; set; }
}