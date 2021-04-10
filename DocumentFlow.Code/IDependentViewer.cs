//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 13.10.2020
// Time: 19:35
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IDependentViewer
    {
        void AddDependentViewer(string commandCode);
        void AddDependentViewers(string[] commandCodes);
    }
}
