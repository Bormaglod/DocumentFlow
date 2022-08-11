//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.12.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Core;

public static class ObjectExtension
{
    public static object? GetPropertyValue(this object? obj, string propertyName)
    {
        var properties = propertyName.Split('.');

        for (var i = 0; i < properties.Length; i++)
        {
            if (obj != null)
            {
                var prop = obj.GetType().GetProperty(properties[i]);
                if (prop != null)
                {
                    obj = prop.GetValue(obj);
                }
                else
                {
                    return null;
                }
            }
        }

        return obj;
    }
}
