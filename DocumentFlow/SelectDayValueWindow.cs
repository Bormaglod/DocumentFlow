//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2020
// Time: 16:40
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Windows.Forms;
    using Syncfusion.WinForms.Input;
    using Syncfusion.Windows.Forms;
    using Syncfusion.WinForms.Input.Events;

    public partial class SelectDayValueWindow : MetroForm
    {
        public SelectDayValueWindow()
        {
            InitializeComponent();
        }

        public DateTime? SelectedDate => sfCalendar1.SelectedDate;

        private void sfCalendar1_SelectionChanging(SfCalendar sender, SelectionChangingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
