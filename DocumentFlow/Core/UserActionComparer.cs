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
    public class UserActionComparer : IEqualityComparer<IUserAction>
    {
        bool IEqualityComparer<IUserAction>.Equals(IUserAction x, IUserAction y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Code == y.Code;
        }

        int IEqualityComparer<IUserAction>.GetHashCode(IUserAction obj) => obj.Code.GetHashCode();
    }
}
