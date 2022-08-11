//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 14.07.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Dialogs;

using FluentDateTime;

namespace DocumentFlow.Controls;

public partial class DateRangeControl : UserControl
{
    public DateRangeControl()
    {
        InitializeComponent();

        dateTimePickerFrom.Checked = true;
        dateTimePickerTo.Checked = true;
        dateTimePickerFrom.Value = DateTime.Today.BeginningOfYear();
        dateTimePickerTo.Value = DateTime.Today.EndOfYear();
    }

    public bool FromEnabled
    {
        get => dateTimePickerFrom.Checked;
        set => dateTimePickerFrom.Checked = value;
    }
    public bool ToEnabled
    {
        get => dateTimePickerTo.Checked;
        set => dateTimePickerTo.Checked = value;
    }

    public DateTime? From
    {
        get => dateTimePickerFrom.Checked ? dateTimePickerFrom.Value.BeginningOfDay() : null;
        set
        {
            if (value == null)
            {
                dateTimePickerFrom.Checked = false;
                dateTimePickerFrom.Value = DateTime.MinValue;
            }
            else
            {
                dateTimePickerFrom.Value = value.Value;
            }
        }
    }

    public DateTime? To
    {
        get => dateTimePickerTo.Checked ? dateTimePickerTo.Value.EndOfDay() : null;
        set
        {
            if (value == null)
            {
                dateTimePickerTo.Checked = false;
                dateTimePickerTo.Value = DateTime.MaxValue;
            }
            else
            {
                dateTimePickerTo.Value = value.Value;
            }
        }
    }

    public void SetRange(DateRange range)
    {
        switch (range)
        {
            case DateRange.CurrentDay:
                (From, To) = (DateTime.Today.BeginningOfDay(), DateTime.Today.EndOfDay());
                break;
            case DateRange.CurrentMonth:
                (From, To) = (DateTime.Today.BeginningOfMonth(), DateTime.Today.EndOfMonth());
                break;
            case DateRange.CurrentQuarter:
                (From, To) = (DateTime.Today.BeginningOfQuarter(), DateTime.Today.EndOfQuarter());
                break;
            case DateRange.CurrentYear:
                (From, To) = (DateTime.Today.BeginningOfYear(), DateTime.Today.EndOfYear());
                break;
            default:
                break;
        }
    }

    private void ButtonSelectDateRange_Click(object sender, EventArgs e)
    {
        var win = new SelectDateRangeForm();
        if (win.ShowDialog() == DialogResult.OK)
        {
            dateTimePickerFrom.Value = win.DateFrom;
            dateTimePickerTo.Value = win.DateTo;
        }
    }
}
