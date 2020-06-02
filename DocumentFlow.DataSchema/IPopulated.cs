//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.08.2019
// Time: 19:47
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using NHibernate;

    public interface IPopulated
    {
        void Populate(ISession session, IDictionary row, IDictionary<string, Type> types, int status);
    }
}
