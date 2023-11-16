//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Data.Tools;
using DocumentFlow.Tools;

using Humanizer;

namespace DocumentFlow.Data.Models;

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
        set => SetProperty(ref personId, value);
    }

    public Guid? PostId 
    { 
        get => postId;
        set => SetProperty(ref postId, value);
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

    [Computed]
    public string? OwnerName { get; set; }

    [Computed]
    public string? PostName { get; set; }


    [EnumType("employee_role")]
    public string EmpRole 
    { 
        get => empRole;
        set => SetProperty(ref empRole, value);
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
