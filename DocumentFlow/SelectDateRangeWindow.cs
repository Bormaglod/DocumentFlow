//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2020
// Time: 15:30
//-----------------------------------------------------------------------

namespace DocumentFlow
{
    using System;
    using System.Windows.Forms;
    using Syncfusion.Windows.Forms;
    using DocumentFlow.Core;
    using DocumentFlow.DataSchema;

    public partial class SelectDateRangeWindow : MetroForm
    {
        private int year;

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public SelectDateRangeWindow()
        {
            InitializeComponent();
            
            year = DateTime.Today.Year;
            labelYear.Text = $"{year} год";
        }

        private void SelectMonth(object sender, EventArgs e)
        {
            int month = Convert.ToInt32((sender as Control).Tag);
            DateTime date = new DateTime(year, month, 1);

            DateFrom = date;
            DateTo = date.FromDateRanges(DateRanges.LastMonthDay);

            DialogResult = DialogResult.OK;
        }

        private void SelectQuart(object sender, EventArgs e)
        {
            int quart = Convert.ToInt32((sender as Control).Tag) - 1;
            int month = quart * 3 + 1;
            DateTime date = new DateTime(year, month, 1);

            DateFrom = date;
            DateTo = date.FromDateRanges(DateRanges.LastQuarterDay);

            DialogResult = DialogResult.OK;
        }

        private void buttonYear_Click(object sender, EventArgs e)
        {
            DateFrom = new DateTime(year, 1, 1);
            DateTo = new DateTime(year, 12, 31);
            DialogResult = DialogResult.OK;
        }

        private void button9Months_Click(object sender, EventArgs e)
        {
            DateFrom = new DateTime(year, 1, 1);
            DateTo = new DateTime(year, 9, 30);
            DialogResult = DialogResult.OK;
        }

        private void buttonHalfYear_Click(object sender, EventArgs e)
        {
            DateFrom = new DateTime(year, 1, 1);
            DateTo = new DateTime(year, 6, 30);
            DialogResult = DialogResult.OK;
        }

        private void buttonSelectDay_Click(object sender, EventArgs e)
        {
            SelectDayValueWindow selectDay = new SelectDayValueWindow();
            if (selectDay.ShowDialog() == DialogResult.OK)
            {
                if (selectDay.SelectedDate.HasValue)
                {
                    DateFrom = selectDay.SelectedDate.Value;
                    DateTo = selectDay.SelectedDate.Value;
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void buttonPrevYear_Click(object sender, EventArgs e)
        {
            labelYear.Text = $"{--year} год";
        }

        private void buttonNextYear_Click(object sender, EventArgs e)
        {
            labelYear.Text = $"{++year} год";
        }
    }
}
