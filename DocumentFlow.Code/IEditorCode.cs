//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.10.2020
// Time: 23:26
//-----------------------------------------------------------------------

namespace DocumentFlow.Code
{
    public interface IEditorCode
    {
        void Initialize(IEditor editor, IDatabase database, IDependentViewer dependentViewer = null);
    }
}
