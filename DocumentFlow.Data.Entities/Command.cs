//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.03.2019
// Time: 18:43
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Data.Entities
{
    public class Command : Entity<Guid>
    {
        public object Locker = new();
        private string scriptCode;

        public string code { get; set; }
        public string name { get; set; }
        public Guid? parent_id { get; set; }
        public Guid? picture_id { get; set; }
        public string note { get; set; }
        public Guid? entity_kind_id { get; set; }
        public string script 
        {
            get => scriptCode;
            set
            {
                if (scriptCode != value)
                {
                    scriptCode = value;
                    NotifyPropertyChanged(nameof(script));
                }
            }
        }

        public Picture Picture { get; set; }
        public EntityKind EntityKind { get; set; }
        public bool IsChanged { get; set; } = true;

        public override string ToString() => $"{name} ({code})";
    }
}
