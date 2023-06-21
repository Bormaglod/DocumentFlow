//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//
// Версия 2023.6.11
//  - в перечисление DataOperation добавлен элемент None
//  - класс переименован из DataOperationAttribute в
//    AllowOperationAttribute
// Версия 2023.6.21
//  - перечисление DataOperation было некорректно объявлено. Исправлено
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

[Flags]
public enum DataOperation { 
    None    = 1, 
    Add     = 2, 
    Update  = 4, 
    Copy    = 8
}

[AttributeUsage(AttributeTargets.Property)]
public class AllowOperationAttribute : Attribute
{
    public AllowOperationAttribute(DataOperation dataOperation) => Operation = dataOperation;
    public DataOperation Operation { get; }
}
