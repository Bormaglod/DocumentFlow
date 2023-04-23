//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Infrastructure.Controls;

public interface ITextBoxControl : IControl
{
    string? Text { get; }
    ITextBoxControl TextChanged(ControlValueChanged<string?> action);
    ITextBoxControl ReadOnly();
    ITextBoxControl Multiline(int height = 75);
    ITextBoxControl SetText(string? text);
}
