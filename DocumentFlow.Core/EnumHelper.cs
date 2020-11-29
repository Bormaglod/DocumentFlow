//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.09.2020
// Time: 19:36
//-----------------------------------------------------------------------

using System;
using System.Linq;

namespace DocumentFlow.Core
{
    public static class EnumHelper
    {
        public static TOut TransformEnum<TOut, TIn>(TIn enumValue)
        {
            Type type = typeof(TIn);
            if (!type.IsEnum)
            {
                throw new Exception("TIn должно быть перечисления");
            }

            type = typeof(TOut);
            if (!type.IsEnum)
            {
                throw new Exception("TOut должно быть перечисления");
            }

            return type.GetEnumValues().Cast<TOut>().Where(x => x.ToString() == enumValue.ToString()).Single();
        }
    }
}
