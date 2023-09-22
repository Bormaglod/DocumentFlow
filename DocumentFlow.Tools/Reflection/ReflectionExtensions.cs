//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.03.2023
//-----------------------------------------------------------------------

using System.Linq.Expressions;
using System.Reflection;

namespace DocumentFlow.Tools.Reflection;

public static class ReflectionExtensions
{
    public static MemberInfo ToMember<TMapping, TReturn>(this Expression<Func<TMapping, TReturn?>> propertyExpression)
    {
        return ReflectionHelper.GetMember(propertyExpression) ?? throw new NullReferenceException("Параметр memberExpression должен содержать имя поля, но оно не найдено в классе."); ;
    }
}
