//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.08.2022
//-----------------------------------------------------------------------

using Syncfusion.WinForms.DataGrid;

namespace DocumentFlow.Controls.Editors;

public partial class DfReportCard : UserControl
{
    private int year;
    private int month;

    public DfReportCard()
    {
        InitializeComponent();

        year = DateTime.Now.Year;
        month = DateTime.Now.Month;

        BuildCalendar();
    }

    public int Year
    {
        get => year;
        set
        {
            if (year != value)
            {
                year = value;
                BuildCalendar();
            }
        }
    }

    public int Month
    {
        get => month;
        set
        {
            if (month != value)
            {
                month = value;
                BuildCalendar();
            }
        }
    }

    private void BuildCalendar()
    {
        gridContent.Columns.Clear();

        gridContent.Columns.Add(new GridTextColumn() 
        { 
            MappingName = "employee_name", 
            HeaderText = "Сотрудник" ,
            Width = 150
        });

        for (int i = 0; i < DateTime.DaysInMonth(Year, Month); i++)
        {
            var date = new DateTime(Year, Month, i + 1);

            var column = new GridTextColumn()
            {
                MappingName = $"info[{i}]",
                HeaderText = $"{i + 1}\r\n{date:ddd}",
                AllowHeaderTextWrapping = true,
                Width = 50
            };

            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                column.HeaderStyle.TextColor = Color.Red;
            }
            else
            {
                column.HeaderStyle.TextColor = Color.Black;
            }

            column.HeaderStyle.Font.Bold = true;

            gridContent.Columns.Add(column);
        }
    }

    private void ButtonAdd_Click(object sender, EventArgs e)
    {

    }

    private void ButtonEdit_Click(object sender, EventArgs e)
    {

    }

    private void ButtonDelete_Click(object sender, EventArgs e)
    {

    }

    private void ButtonPopulate_Click(object sender, EventArgs e)
    {

    }
}
