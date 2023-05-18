//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.11.2021
//
// Версия 2023.5.18
//  - добавлено наследование от ILineControl
//  - удалены свойства LineColor и LineHeight
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.Editors;

public partial class DfLine : UserControl, ILineControl
{
    public DfLine()
    {
        InitializeComponent();
        Height = 12;
        Dock = DockStyle.Top;
    }

    ILineControl ILineControl.SetColor(Color color)
    {
        panelColored.BackColor = color;
        return this;
    }

    ILineControl ILineControl.SetDock(DockStyle dockStyle)
    {
        Dock = dockStyle;
        return this;
    }

    ILineControl ILineControl.SetHeight(int height)
    {
        panelColored.Height = height > Height ? Height : height;
        return this;
    }
}
