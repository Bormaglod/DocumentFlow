//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Enums;
using DocumentFlow.Dialogs;

using FluentDateTime;

namespace DocumentFlow.Controls;

public partial class DateRangeControl : UserControl
{
    private bool dateFromEnabled = true;
    private bool dateToEnabled = true;
    private DateTime dateFrom = DateTime.Today.BeginningOfYear();
    private DateTime dateTo = DateTime.Today.EndOfYear();

    public event EventHandler? DateFromEnabledChanged;
    public event EventHandler? DateToEnabledChanged;
    public event EventHandler? DateFromChanged;
    public event EventHandler? DateToChanged;

    public DateRangeControl()
    {
        InitializeComponent();
    }

    public bool DateFromEnabled
    {
        get => dateFromEnabled;
        set
        {
            if (dateFromEnabled != value)
            {
                dateFromEnabled = value;
                dateTimePickerFrom.Checked = value;
                DateFromEnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public bool DateToEnabled
    {
        get => dateToEnabled;
        set
        {
            if (dateToEnabled != value)
            {
                dateToEnabled = value;
                dateTimePickerTo.Checked = value;
                DateToEnabledChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public DateTime DateFrom
    {
        get => dateFrom;
        set
        {
            var valueDate = value.BeginningOfDay();
            if (dateFrom != valueDate)
            {
                dateFrom = valueDate;
                dateTimePickerFrom.Value = valueDate;
                DateFromChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public DateTime DateTo
    {
        get => dateTo;
        set
        {
            var valueDate = value.EndOfDay();
            if (dateTo != valueDate)
            {
                dateTo = valueDate;
                dateTimePickerTo.Value = valueDate;
                DateToChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void SetRange(DateRange range)
    {
        switch (range)
        {
            case DateRange.CurrentDay:
                (DateFrom, DateTo) = (DateTime.Today.BeginningOfDay(), DateTime.Today.EndOfDay());
                break;
            case DateRange.CurrentMonth:
                (DateFrom, DateTo) = (DateTime.Today.BeginningOfMonth(), DateTime.Today.EndOfMonth());
                break;
            case DateRange.CurrentQuarter:
                (DateFrom, DateTo) = (DateTime.Today.BeginningOfQuarter(), DateTime.Today.EndOfQuarter());
                break;
            case DateRange.CurrentYear:
                (DateFrom, DateTo) = (DateTime.Today.BeginningOfYear(), DateTime.Today.EndOfYear());
                break;
            default:
                break;
        }

        DateFromEnabled = true;
        DateToEnabled = true;
    }

    private void ButtonSelectDateRange_Click(object sender, EventArgs e)
    {
        var win = new DateRangeDialog();
        if (win.ShowDialog() == DialogResult.OK)
        {
            DateFrom = win.DateFrom;
            DateTo = win.DateTo;
        }
    }

    private void DateTimePickerFrom_CheckBoxCheckedChanged(object sender, EventArgs e)
    {
        DateFromEnabled = dateTimePickerFrom.Checked;
    }

    private void DateTimePickerTo_CheckBoxCheckedChanged(object sender, EventArgs e)
    {
        DateToEnabled = dateTimePickerTo.Checked;
    }

    private void DateTimePickerFrom_ValueChanged(object sender, EventArgs e)
    {
        DateFrom = dateTimePickerFrom.Value.BeginningOfDay();
    }

    private void DateTimePickerTo_ValueChanged(object sender, EventArgs e)
    {
        DateTo = dateTimePickerTo.Value.EndOfDay();
    }
}
