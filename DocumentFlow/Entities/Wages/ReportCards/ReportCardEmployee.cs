//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;
using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Entities.Employees;

namespace DocumentFlow.Entities.Wages;

public class ReportCardEmployee : Entity<long>, IEntityClonable, ICloneable
{
    public ReportCardEmployee() { }

    public ReportCardEmployee(OurEmployee emp, int year, int month)
    {
        SetEmployee(emp);

        var days = DateTime.DaysInMonth(year, month);

        labels = new string[days];
        hours = new int[days];
        info = new string[days];

        for (int i = 0; i < days; i++)
        {
            var date = new DateTime(year, month, i + 1);
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                labels[i] = "В";
                info[i] = "В";
            }
            else
            {
                labels[i] = "Я";
                hours[i] = 8;
                info[i] = "Я 8";
            }
        }

        UpdateSummary();
    }

    public Guid employee_id { get; set; }
    public string employee_name { get; protected set; } = string.Empty;
    public string summary { get; protected set; } = string.Empty;
    public string[]? labels { get; set; }
    public int[]? hours { get; set; }
    public string[]? info { get; protected set; }

    public void SetEmployee(OurEmployee emp)
    {
        employee_id = emp.id;
        employee_name = emp.item_name ?? string.Empty;
    }

    public void SetInfo(int index, string empInfo)
    {
        if (info != null && labels != null && hours != null)
        {
            var i = empInfo.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (i.Length > 0 && ReportCard.Labels.IsValidLabel(i[0]))
            {
                if (ReportCard.Labels.IsTimeLabel(i[0]))
                {
                    if (i.Length > 1 && int.TryParse(i[1], out int hrs))
                    {
                        labels[index] = i[0];
                        hours[index] = hrs;
                        info[index] = $"{i[0]} {hrs}";
                    }
                }
                else
                {
                    labels[index] = i[0];
                    hours[index] = 0;
                    info[index] = i[0];
                }
            }

            UpdateSummary();
        }
    }

    public object Clone() => MemberwiseClone();

    public object Copy()
    {
        var copy = Clone();
        ((ReportCardEmployee)copy).id = 0;

        return copy;
    }

    public override string ToString() => employee_name;

    public void UpdateSummary()
    {
        if (labels != null && hours != null)
        {
            Dictionary<string, (int Days, int Hours)> labelSummary = new();
            for (int i = 0; i < labels.Length; i++)
            {
                string? label = labels[i];
                if (label == null)
                {
                    continue;
                }
                
                if (!labelSummary.ContainsKey(label))
                {
                    labelSummary.Add(label, (0, 0));
                }

                int d = labelSummary[label].Days + 1;
                int h = labelSummary[label].Hours + hours[i];
                labelSummary[label] = (d, h);
            }

            List<string> values = new();
            foreach (var item in labelSummary)
            {
                string hrs = item.Value.Hours > 0 ? $" {item.Value.Hours} ч." : string.Empty;
                string text = $"{item.Key} {item.Value.Days} дн.{hrs}";
                values.Add(text);
            }

            summary = string.Join("\r\n", values);
        }
    }
}
