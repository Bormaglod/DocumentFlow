//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2014
//
// Версия 2022.8.28
//  - Параметр propertyName в процедуре NotifyPropertyChanged отмечен
//    атрибутом CallerMemberName
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
// Версия 2023.3.17
//  - перенесено из DocumentFlow.Data.Core в DocumentFlow.Data
//  - удален метод ImageToByteArray
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Infrastructure.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DocumentFlow.Data;

public abstract class Identifier<T> : INotifyPropertyChanged, IIdentifier<T>
    where T : struct, IComparable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [Exclude]
    [Display(AutoGenerateField = false)]
    public T Id { get; set; }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
