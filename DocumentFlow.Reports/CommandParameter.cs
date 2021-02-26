//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.01.2021
// Time: 23:36
//-----------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;

namespace DocumentFlow.Reports
{
    public class CommandParameter
    {
        private bool hasValue = false;
        private object parameterValue;

        public string Name { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }

        [JsonIgnore]
        public object Value
        {
            get => parameterValue;
            set
            {
                if (parameterValue != null)
                {
                    if (value.GetType().Name != DataType)
                    {
                        throw new Exception($"Тип параметра {Name} - {DataType}, а не {value.GetType().Name}.");
                    }
                }

                hasValue = true;
                parameterValue = value;
            }
        }

        public bool HasValue => hasValue;
    }
}
