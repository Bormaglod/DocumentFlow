//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2020
//-----------------------------------------------------------------------

using FluentDateTime;

namespace DocumentFlow.Dialogs;

public partial class SelectDateRangeForm : Form
{
    private int year;

    public SelectDateRangeForm()
    {
        InitializeComponent();

        year = DateTime.Today.Year;
        labelYear.Text = $"{year} год";
    }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    private void SelectMonth(object sender, EventArgs e)
    {
        int month = Convert.ToInt32(((Control)sender).Tag);
        DateTime date = new(year, month, 1);

        DateFrom = date;
        DateTo = date.EndOfMonth();

        DialogResult = DialogResult.OK;
    }

    private void SelectQuarter(object sender, EventArgs e)
    {
        int month = Convert.ToInt32(((Control)sender).Tag);
        DateTime date = new(year, month, 1);

        DateFrom = date;
        DateTo = date.EndOfQuarter();

        DialogResult = DialogResult.OK;
    }

    private void ButtonYear_Click(object sender, EventArgs e)
    {
        DateFrom = new DateTime(year, 1, 1);
        DateTo = new DateTime(year, 12, 31, 23, 59, 59);
        DialogResult = DialogResult.OK;
    }

    private void Button9Months_Click(object sender, EventArgs e)
    {
        DateFrom = new DateTime(year, 1, 1);
        DateTo = new DateTime(year, 9, 30, 23, 59, 59);
        DialogResult = DialogResult.OK;
    }

    private void ButtonHalfYear_Click(object sender, EventArgs e)
    {
        DateFrom = new DateTime(year, 1, 1);
        DateTo = new DateTime(year, 6, 30, 23, 59, 59);
        DialogResult = DialogResult.OK;
    }

    private void ButtonSelectDay_Click(object sender, EventArgs e)
    {
        SelectDayValueForm selectDay = new();
        if (selectDay.ShowDialog() == DialogResult.OK)
        {
            if (selectDay.SelectedDate.HasValue)
            {
                DateFrom = selectDay.SelectedDate.Value.BeginningOfDay();
                DateTo = selectDay.SelectedDate.Value.EndOfDay();
                DialogResult = DialogResult.OK;
            }
        }
    }

    private void ButtonPrevYear_Click(object sender, EventArgs e) => labelYear.Text = $"{--year} год";

    private void ButtonNextYear_Click(object sender, EventArgs e) => labelYear.Text = $"{++year} год";

    private void SelectDateRangeWindow_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
