//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data.Core;

public abstract class Entity<T> : Identifier<T>, IEntity<T>
    where T : struct, IComparable
{
    [Display(AutoGenerateField = false)]
    public Guid? owner_id { get; set; }
}