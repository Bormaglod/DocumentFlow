﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.06.2023
//-----------------------------------------------------------------------

using Humanizer;

using System.ComponentModel;

namespace DocumentFlow.Tools;

public static class EnumExtension
{
    public static string Description(this Enum @enum)
    {
        Type type = @enum.GetType();
        var field = type.GetFields().FirstOrDefault(x => x.Name == @enum.ToString());
        if (field != null)
        {
            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            if (attr is DescriptionAttribute descr)
            {
                return descr.Description;
            }
            else
            {
                return field.Name.Humanize(LetterCasing.LowerCase);
            }
        }

        throw new NullReferenceException();
    }
}
