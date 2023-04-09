//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.04.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface IMaskedTextBoxControl<T> : IBaseNumericTextBoxControl<T>
    where T : struct, IComparable<T>
{
    IMaskedTextBoxControl<T> SetMask(string mask);
}
