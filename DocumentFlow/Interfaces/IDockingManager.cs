//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 29.08.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;

namespace DocumentFlow.Interfaces;

public interface IDockingManager
{
    bool IsVisibility(IPage page);
    void Activate(IPage page);
    void Close(IPage page);
}
