//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.08.2022
//
// Версия 2023.1.22
//  - DocumentFlow.Data.Infrastructure перемещено в DocumentFlow.Infrastructure.Data
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Data;
using DocumentFlow.Entities.Employees;
using DocumentFlow.Data;

namespace DocumentFlow.Entities.Wages;

public class ReportCardEmployee : Entity<long>, IEntityClonable, ICloneable
{
    public ReportCardEmployee() { }

    public ReportCardEmployee(OurEmployee emp, int year, int month)
    {
        SetEmployee(emp);

        var days = DateTime.DaysInMonth(year, month);

        Labels = new string[days];
        Hours = new int[days];
        Info = new string[days];

        for (int i = 0; i < days; i++)
        {
            var date = new DateTime(year, month, i + 1);
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                Labels[i] = "В";
                Info[i] = "В";
            }
            else
            {
                Labels[i] = "Я";
                Hours[i] = 8;
                Info[i] = "Я 8";
            }
        }

        UpdateSummary();
    }

    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; protected set; } = string.Empty;
    public string Summary { get; protected set; } = string.Empty;
    public string[]? Labels { get; set; }
    public int[]? Hours { get; set; }
    public string[]? Info { get; protected set; }

    public void SetEmployee(OurEmployee emp)
    {
        EmployeeId = emp.Id;
        EmployeeName = emp.ItemName ?? string.Empty;
    }

    public void SetInfo(int index, string empInfo)
    {
        if (Info != null && Labels != null && Hours != null)
        {
            var i = empInfo.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (i.Length > 0 && ReportCard.Labels.IsValidLabel(i[0]))
            {
                if (ReportCard.Labels.IsTimeLabel(i[0]))
                {
                    if (i.Length > 1 && int.TryParse(i[1], out int hrs))
                    {
                        Labels[index] = i[0];
                        Hours[index] = hrs;
                        Info[index] = $"{i[0]} {hrs}";
                    }
                }
                else
                {
                    Labels[index] = i[0];
                    Hours[index] = 0;
                    Info[index] = i[0];
                }
            }

            UpdateSummary();
        }
    }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((ReportCardEmployee)copy).Id = 0;

        return copy;
    }

    public override string ToString() => EmployeeName;

    public void UpdateSummary()
    {
        if (Labels != null && Hours != null)
        {
            Dictionary<string, (int Days, int Hours)> labelSummary = new();
            for (int i = 0; i < Labels.Length; i++)
            {
                string? label = Labels[i];
                if (label == null)
                {
                    continue;
                }
                
                if (!labelSummary.ContainsKey(label))
                {
                    labelSummary.Add(label, (0, 0));
                }

                int d = labelSummary[label].Days + 1;
                int h = labelSummary[label].Hours + Hours[i];
                labelSummary[label] = (d, h);
            }

            List<string> values = new();
            foreach (var item in labelSummary)
            {
                string hrs = item.Value.Hours > 0 ? $" {item.Value.Hours} ч." : string.Empty;
                string text = $"{item.Key} {item.Value.Days} дн.{hrs}";
                values.Add(text);
            }

            Summary = string.Join("\r\n", values);
        }
    }
}
