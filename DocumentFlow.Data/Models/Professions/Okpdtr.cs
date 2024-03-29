﻿//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("ОКПДТР")]
public class Okpdtr : Directory
{
    private string? signatoryName;

    public string? SignatoryName
    {
        get => signatoryName;
        set => SetProperty(ref signatoryName, value);
    }
}
