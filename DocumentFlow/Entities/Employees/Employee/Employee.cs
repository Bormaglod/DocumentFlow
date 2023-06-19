//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.11.2021
//
// Версия 2023.1.21
//  - поле Roles заменено на свойство и поменяло тип на
//    IReadOnlyDictionary
// Версия 2023.6.19
//  - удалены свойства JRole и JobRole, а ткже перечисление JobRole
//  - добавлены свойства EmpRole и EmployeeRole, а также перечисление
//    EmployeeRole
//  - добавлено свойство EmployeeRoleName
//
//-----------------------------------------------------------------------

using DocumentFlow.Data;
using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Employees;

public enum EmployeeRole { NotDefined, Director, ChiefAccountant, Employee, Worker }

[Description("Сотрудник")]
public class Employee : Directory
{
    public static readonly Dictionary<EmployeeRole, string> roles = new()
    {
        [EmployeeRole.NotDefined] = "Не определена",
        [EmployeeRole.Director] = "Директор",
        [EmployeeRole.ChiefAccountant] = "Гл. бухгалтер",
        [EmployeeRole.Employee] = "Служащий",
        [EmployeeRole.Worker] = "Рабочий"
    };

    public string? OwnerName { get; protected set; }
    public Guid? PersonId { get; set; }
    public Guid? PostId { get; set; }
    public string? PostName { get; protected set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    [EnumType("employee_role")]
    public string EmpRole { get; set; } = "not defined";

    public EmployeeRole EmployeeRole
    {
        get { return Enum.Parse<EmployeeRole>(EmpRole.Dehumanize()); }
        protected set { EmpRole = value.ToString().Humanize(LetterCasing.LowerCase); }
    }

    public string EmployeeRoleName => roles[EmployeeRole];

    public static IReadOnlyDictionary<EmployeeRole, string> Roles => roles;
}
