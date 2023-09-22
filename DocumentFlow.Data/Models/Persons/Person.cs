//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 03.10.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

namespace DocumentFlow.Data.Models;

[EntityName("Физ. лицо")]
public class Person : Directory
{
    private string? surname;
    private string? firstName;
    private string? middleName;
    private string? phone;
    private string? email;

    public string? Surname
    {
        get => surname;
        set
        {
            if (surname != value)
            {
                surname = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string? FirstName
    {
        get => firstName;
        set
        {
            if (firstName != value) 
            { 
                firstName = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string? MiddleName
    {
        get => middleName;
        set 
        { 
            if (middleName != value)
            {
                middleName = value;
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
}
