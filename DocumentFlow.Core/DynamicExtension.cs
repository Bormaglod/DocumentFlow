//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.06.2019
// Time: 19:31
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System.Collections;
    using System.Collections.Generic;

    public static class DynamicExtension
    {
        // https://stackoverrun.com/ru/q/4986563
        public static object With(this IDictionary<string, object> obj, IDictionary additionalProperties)
        {
            if (additionalProperties != null)
            {
                foreach (string name in additionalProperties.Keys)
                {
                    obj[name] = additionalProperties[name];
                }
            }

            return obj;
        }

        public static object With(this IDictionary obj, IDictionary additionalProperties)
        {
            if (additionalProperties != null)
            {
                foreach (string name in additionalProperties.Keys)
                {
                    obj[name] = additionalProperties[name];
                }
            }

            return obj;
        }
    }
}
