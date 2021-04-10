//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 25.12.2020
// Time: 23:40
//-----------------------------------------------------------------------

using System;
using DocumentFlow.Data.Core;

namespace DocumentFlow.Code.Implementation
{
    public class Identifier : IIdentifier
    {
        private readonly object identifier;

        private Identifier(object id) => identifier = id;
        public static IIdentifier Get<T>(T id) => new Identifier(id);
        object IIdentifier.oid => identifier;
    }
}
