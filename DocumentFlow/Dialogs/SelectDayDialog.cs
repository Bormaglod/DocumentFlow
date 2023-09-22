//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2020
//-----------------------------------------------------------------------

using Syncfusion.WinForms.Input.Events;

namespace DocumentFlow.Dialogs;

public partial class SelectDayDialog : Form
{
    public SelectDayDialog()
    {
        InitializeComponent();
    }

    public DateTime? SelectedDate => calendar.SelectedDate;

    private void SelectDayValueWindow_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            DialogResult = DialogResult.Cancel;
        }
    }

    private void Calendar_CellClick(object sender, CalendarCellEventArgs e) => DialogResult = DialogResult.OK;
}
