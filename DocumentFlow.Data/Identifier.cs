//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2014
//-----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;

using DocumentFlow.Data.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace DocumentFlow.Data;

public abstract class Identifier<T> : ObservableObject, IIdentifier<T>
    where T : struct, IComparable
{
    private T id;

    [Key]
    [Display(AutoGenerateField = false)]
    public T Id 
    { 
        get => id;
        set => SetProperty(ref id, value);
    }
}
