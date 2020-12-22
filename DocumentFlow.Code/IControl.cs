//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 18:36
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentFlow.Code
{
    public interface IControl
    {
        string ControlName { get; }
        int Left { get; set; }
        int Top { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        bool Enabled { get; set; }
        bool Visible { get; set; }
        DockStyle Dock { get; set; }
        IList<int> Margin { get; set; }
        IList<int> Padding { get; set; }
        bool FitToSize { get; set; }

        IControl SetControlName(string name);
        IControl SetLeft(int left);
        IControl SetTop(int top);
        IControl SetWidth(int width);
        IControl SetHeight(int height);
        IControl SetLocation(Point location);
        IControl SetSize(Size size);
        IControl SetEnabled(bool enabled);
        IControl SetVisible(bool visible);
        IControl SetDock(DockStyle dock);
        IControl SetMargin(int left = 0, int top = 0, int right = 0, int bottom = 0);
        IControl SetMargin(int all);
        IControl SetPadding(int left = 0, int top = 0, int right = 0, int bottom = 0);
        IControl SetPadding(int all);
        IControl SetFitToSize(bool fitToSize);
    }
}
