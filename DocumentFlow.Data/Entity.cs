//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data;

public abstract class Entity<T> : Identifier<T>, IEntity<T>
    where T : struct, IComparable
{

    [Display(AutoGenerateField = false)]
    public Guid? OwnerId { get; set; }
}