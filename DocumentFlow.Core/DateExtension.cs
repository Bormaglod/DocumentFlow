//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 11.01.2020
// Time: 10:52
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Core
{
    public enum DateRanges { None, FirstMonthDay, LastMonthDay, FirstQuarterDay, LastQuarterDay, FirstYearDay, LastYearDay, CurrentDay }

    public static class DateExtension
    {
        public static DateTime BeginningOfTime(this DateTime _) => new(1900, 1, 1, 00, 00, 01);

        public static DateTime EndOfTime(this DateTime _) => new(2900, 12, 31, 23, 59, 59);

        public static DateTime StartOfDay(this DateTime date) => new(date.Year, date.Month, date.Day, 00, 00, 01);

        public static DateTime EndOfDay(this DateTime date) => new(date.Year, date.Month, date.Day, 23, 59, 59);

        public static DateTime FromDateRanges(this DateTime date, DateRanges ranges)
        {
            return ranges switch
            {
                DateRanges.FirstMonthDay => new DateTime(date.Year, date.Month, 1),
                DateRanges.LastMonthDay => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59),
                DateRanges.FirstYearDay => new DateTime(date.Year, 1, 1),
                DateRanges.LastYearDay => new DateTime(date.Year, 12, 31, 23, 59, 59),
                DateRanges.FirstQuarterDay => new DateTime(date.Year, (GetQuarter(date) - 1) * 3 + 1, 1),
                DateRanges.LastQuarterDay => GetLastQuarterDay(date),
                _ => date
            };
        }

        private static DateTime GetLastQuarterDay(DateTime date)
        {
            int month = GetQuarter(date) * 3;
            return new DateTime(date.Year, month, DateTime.DaysInMonth(date.Year, month), 23, 59, 59);
        }

        private static int GetQuarter(DateTime date)
        {
            return date.Month switch
            {
                < 4 => 1,
                < 7 => 2,
                < 10 => 3,
                _ => 4
            };
        }
    }
}
