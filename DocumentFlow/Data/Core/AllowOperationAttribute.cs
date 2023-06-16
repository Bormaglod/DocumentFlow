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
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

[Flags]
public enum DataOperation { None, Add, Update, Copy }

[AttributeUsage(AttributeTargets.Property)]
public class AllowOperationAttribute : Attribute
{
    public AllowOperationAttribute(DataOperation dataOperation) => Operation = dataOperation;
    public DataOperation Operation { get; }
}
