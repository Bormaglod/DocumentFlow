//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.02.2020
// Time: 14:01
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;
    using System.Reflection;

    public static class ObjectExtension
    {
        public static object GetValue(this object obj, string propertyName, char delimiter = '.')
        {
            Type cur = obj.GetType();
            object result = obj;

            foreach (string part in propertyName.Split(delimiter))
            {
                PropertyInfo prop = cur.GetProperty(part);
                if (prop == null)
                {
                    LogHelper.Logger.Error($"В {obj.GetType().Name} не найдено поле {propertyName}");
                    return null;
                }

                result = prop.GetValue(result);
                if (result == null)
                    break;

                cur = prop.PropertyType;
            }

            return result;
        }
    }
}
