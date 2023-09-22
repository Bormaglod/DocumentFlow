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
        set
        {
            if (address != value) 
            { 
                address = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string? Phone 
    { 
        get => phone; 
        set
        {
            if (phone != value)
            {
                phone = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string? Email 
    { 
        get => email; 
        set
        {
            if (email != value)
            {
                email = value;
                NotifyPropertyChanged();
            }
        }
    }

    public bool DefaultOrg 
    { 
        get => defaultOrg; 
        set
        {
            if (defaultOrg != value)
            {
                defaultOrg = value;
                NotifyPropertyChanged();
            }
        }
    }
}
