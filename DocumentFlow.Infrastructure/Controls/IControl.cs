//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface IControl
{
    string PropertyName { get; }
    string Tag { get; }
    IControl SetHeaderWidth(int width);
    IControl SetEditorWidth(int width);
    IControl Disable();
    IControl DefaultAsValue();
    IControl SetTag(string value);
    IControl SetVisible(bool visible);
    IControl SetEnabled(bool enabled);
}
