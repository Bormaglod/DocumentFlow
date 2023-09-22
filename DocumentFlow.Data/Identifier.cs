//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2014
//-----------------------------------------------------------------------

using DocumentFlow.Data.Interfaces;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DocumentFlow.Data;

public abstract class Identifier<T> : INotifyPropertyChanged, IIdentifier<T>
    where T : struct, IComparable
{
    private T id;

    public event PropertyChangedEventHandler? PropertyChanged;

    [Key]
    [Display(AutoGenerateField = false)]
    public T Id 
    { 
        get => id; 
        set
        {
            if (id.CompareTo(value) != 0)
            {
                id = value;
                NotifyPropertyChanged();
            }
        }
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
