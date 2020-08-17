//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2020
// Time: 10:52
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;
    using DocumentFlow.DataSchema;

    public static class DateExtension
    {
        public static DateTime BeginningOfTime(this DateTime date)
        {
            return new DateTime(1900, 1, 1, 00, 00, 01);
        }

        public static DateTime EndOfTime(this DateTime date)
        {
            return new DateTime(2900, 12, 31, 23, 59, 59);
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 00, 00, 01);
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static DateTime FromDateRanges(this DateTime date, DateRanges ranges)
        {
            switch (ranges)
            {
                case DateRanges.FirstMonthDay:
                    return new DateTime(date.Year, date.Month, 1);
                case DateRanges.LastMonthDay:
                    return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                case DateRanges.FirstYearDay:
                    return new DateTime(date.Year, 1, 1);
                case DateRanges.LastYearDay:
                    return new DateTime(date.Year, 12, 31);
                case DateRanges.FirstQuarterDay:
                    return new DateTime(date.Year, (GetQuarter(date) - 1) * 3 + 1, 1);
                case DateRanges.LastQuarterDay:
                    int month = GetQuarter(date) * 3;
                    return new DateTime(date.Year, month, DateTime.DaysInMonth(date.Year, month));
            }

            return date;
        }

        private static int GetQuarter(DateTime date)
        {
            if (date.Month < 4)
                return 1;

            if (date.Month < 7)
                return 2;

            if (date.Month < 10)
                return 3;

            return 4;
        }
    }
}
