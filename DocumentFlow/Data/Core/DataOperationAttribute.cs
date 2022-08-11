//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

[Flags]
public enum DataOperation { Add, Update, Copy }

[AttributeUsage(AttributeTargets.Property)]
public class DataOperationAttribute : Attribute
{
    public DataOperationAttribute(DataOperation dataOperation) => Operation = dataOperation;
    public DataOperation Operation { get; }
}
