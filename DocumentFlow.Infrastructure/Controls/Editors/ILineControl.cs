//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 18.05.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface ILineControl
{
    ILineControl SetDock(DockStyle dockStyle);
    ILineControl SetColor(Color color);
    ILineControl SetHeight(int height);
}
