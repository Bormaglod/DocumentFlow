//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.01.2021
// Time: 20:07
//-----------------------------------------------------------------------

using System.Collections.Generic;
using DocumentFlow.Code;

namespace DocumentFlow
{
    public class CommandComparer : IEqualityComparer<ICommand>
    {
        bool IEqualityComparer<ICommand>.Equals(ICommand x, ICommand y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Code == y.Code;
        }

        int IEqualityComparer<ICommand>.GetHashCode(ICommand obj)
        {
            return obj.Code.GetHashCode();
        }
    }
}
