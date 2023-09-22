//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

using System.ComponentModel;

namespace DocumentFlow.Data.Models;

public enum EmployeeRole 
{
    [Description("Не определена")]
    NotDefined,

    [Description("Директор")]
    Director,

    [Description("Гл. бухгалтер")]
    ChiefAccountant,

    [Description("Служащий")]
    Employee,

    [Description("Рабочий")]
    Worker 
}

[EntityName("Сотрудник")]
public class Employee : Directory
{
    private Guid? personId;
    private Guid? postId;
    private string? phone;
    private string? email;
    private string empRole = "not defined";

    public Guid? PersonId 
    { 
        get => personId; 
        set
        {
            if (personId != value) 
            { 
                personId = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid? PostId 
    { 
        get => postId; 
        set
        {
            if (postId != value)
            {
                postId = value;
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

    [Computed]
    public string? OwnerName { get; protected set; }

    [Computed]
    public string? PostName { get; protected set; }


    [EnumType("employee_role")]
    public string EmpRole 
    { 
        get => empRole; 
        set
        {
            if (empRole != value) 
            { 
                empRole = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public EmployeeRole EmployeeRole
    {
        get { return Enum.Parse<EmployeeRole>(EmpRole.Dehumanize()); }
        protected set { EmpRole = value.ToString().Humanize(LetterCasing.LowerCase); }
    }

    [Computed]
    public string EmployeeRoleName => EmployeeRole.Description();
}
