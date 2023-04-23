//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;

namespace DocumentFlow.Infrastructure.Controls;

public interface IControl
{
    string PropertyName { get; }
    bool IsRaised { get; }
    int GetRaisedGroup();
    IControl SetHeaderTextAlign(ContentAlignment alignment);
    IControl SetHeaderWidth(int width);
    IControl SetEditorWidth(int width);
    IControl SetDock(DockStyle dockStyle);
    IControl SetPadding(int left, int top, int right, int bottom);
    IControl SetWidth(int width);
    IControl Disable();
    IControl DefaultAsValue();
    IControl SetVisible(bool visible);
    IControl SetEnabled(bool enabled);
    IControl EditorFitToSize();
    IControl Raise();
    IControl Raise(int group);
    IControl If(bool condition, Action<IControl> trueAction, Action<IControl>? falseAction = null);
}
