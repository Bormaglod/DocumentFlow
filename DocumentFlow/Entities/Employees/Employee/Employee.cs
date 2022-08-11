//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 02.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Employees;

public enum JobRole { NotDefined, Director, ChiefAccountant, Employee, Worker }

[Description("Сотрудник")]
public class Employee : Directory
{
    public string? owner_name { get; protected set; }
    public Guid? person_id { get; set; }
    public Guid? post_id { get; set; }
    public string? post_name { get; protected set; }
    public string? phone { get; set; }
    public string? email { get; set; }

    [EnumType("job_role")]
    public string j_role { get; set; } = "not defined";

    public JobRole JobRole
    {
        get { return Enum.Parse<JobRole>(j_role.Pascalize()); }
        protected set { j_role = value.ToString().Underscore(); }
    }

    public static Dictionary<JobRole, string> Roles = new()
    {
        [JobRole.NotDefined] = "Не определена",
        [JobRole.Director] = "Директор",
        [JobRole.ChiefAccountant] = "Гл. бухгалтер",
        [JobRole.Employee] = "Служащий",
        [JobRole.Worker] = "Рабочий"
    };
}
