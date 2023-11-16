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
        set => SetProperty(ref surname, value);
    }

    public string? FirstName
    {
        get => firstName;
        set => SetProperty(ref firstName, value);
    }

    public string? MiddleName
    {
        get => middleName;
        set => SetProperty(ref middleName, value);
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
}
