﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 26.12.2021
//
// Версия 2023.1.24
//  - добавлен атрибут AttributeUsage
//
//-----------------------------------------------------------------------

namespace DocumentFlow.Data.Core;

[AttributeUsage(AttributeTargets.Property)]
public class ExcludeAttribute : Attribute
{
}
