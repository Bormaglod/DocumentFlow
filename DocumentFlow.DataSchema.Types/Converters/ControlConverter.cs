//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.03.2019
// Time: 21:42
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json.Linq;
    using DocumentFlow.DataSchema.Types.Core;

    public class ControlConverter : JsonCreationConverter<IEditorControl>
    {
        private readonly Dictionary<string, Type> fields = new Dictionary<string, Type>();

        public ControlConverter() : base()
        {
            IEnumerable<TypeInfo> info = Assembly.GetAssembly(GetType()).DefinedTypes;
            foreach (TypeInfo i in info)
            {
                if (i.GetCustomAttribute(typeof(TagAttribute)) is TagAttribute a)
                {
                    fields.Add(a.Name, i.AsType());
                }
            }
        }

        protected override IEditorControl Create(Type objectType, JObject jObject)
        {
            string type = jObject["type"]?.Value<string>();
            if (type != null && fields.ContainsKey(type))
            {
                return Activator.CreateInstance(fields[type]) as IEditorControl;
            }

            throw new UnknownTypeException(type);
        }
    }
}
