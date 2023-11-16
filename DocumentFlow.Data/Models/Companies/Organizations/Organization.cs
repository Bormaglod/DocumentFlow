//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Организация")]
public class Organization : Company
{
    private string? address;
    private string? phone;
    private string? email;
    private bool defaultOrg;

    public string? Address 
    { 
        get => address;
        set => SetProperty(ref address, value);
    }

    public string? Phone 
    { 
        get => phone;
        set => SetProperty(ref phone, value);
    }

    public string? Email 
    { 
        get => email;
        set => SetProperty(ref email, value);
    }

    public bool DefaultOrg 
    { 
        get => defaultOrg;
        set => SetProperty(ref defaultOrg, value);
    }
}
