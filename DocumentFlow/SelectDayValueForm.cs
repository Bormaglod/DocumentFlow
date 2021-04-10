//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2020
// Time: 16:40
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Syncfusion.WinForms.Input;
using Syncfusion.WinForms.Input.Events;

namespace DocumentFlow
{
    public partial class SelectDayValueForm : Form
    {
        public SelectDayValueForm()
        {
            InitializeComponent();
        }

        public DateTime? SelectedDate => sfCalendar1.SelectedDate;

        private void sfCalendar1_SelectionChanging(SfCalendar sender, SelectionChangingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void SelectDayValueWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
