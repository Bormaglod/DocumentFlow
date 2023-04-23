//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//
// Версия 2023.4.8
//  - изменено наследование на IControlHeader
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls.Core;

namespace DocumentFlow.Infrastructure.Controls;

public interface ICheckBoxControl : IControl
{
    ICheckBoxControl ReadOnly();
    ICheckBoxControl AllowThreeState();
    ICheckBoxControl CheckChanged(ControlValueChanged<bool> action);
}
