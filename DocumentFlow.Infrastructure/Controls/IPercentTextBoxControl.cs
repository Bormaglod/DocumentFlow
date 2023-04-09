﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.04.2022
//-----------------------------------------------------------------------

namespace DocumentFlow.Infrastructure.Controls;

public interface IPercentTextBoxControl : IBaseNumericTextBoxControl<decimal>
{
    IPercentTextBoxControl SetPercentDecimalDigits(int digits);
}
