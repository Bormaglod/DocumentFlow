//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.11.2021
//-----------------------------------------------------------------------

namespace DocumentFlow.Controls.Editors;

public partial class DfLine : UserControl
{
    public DfLine()
    {
        InitializeComponent();
        Height = 12;
        Dock = DockStyle.Top;
    }

    public Color LineColor
    {
        get => panelColored.BackColor;
        set => panelColored.BackColor = value;
    }

    public int LineHeight
    {
        get => panelColored.Height;
        set => panelColored.Height = value > Height ? Height : value;
    }
}
