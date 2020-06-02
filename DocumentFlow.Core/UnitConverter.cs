//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.05.2015
// Time: 10:15
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System.Drawing;

    public enum GraphicsUnit { Inch, Centimeter, Display, Point }

    public static class UnitConverter
    {
        public static float Convert(float metricValue, GraphicsUnit from, GraphicsUnit to)
        {
            switch (from)
            {
                case GraphicsUnit.Inch:
                    return ConvertFromInch(metricValue, to);
                case GraphicsUnit.Centimeter:
                    return ConvertFromCentimeter(metricValue, to);
                case GraphicsUnit.Display:
                    return ConvertFromDisplay(metricValue, to);
                case GraphicsUnit.Point:
                    return ConvertFromPoint(metricValue, to);
            }

            return metricValue;
        }

        public static float ConvertCentimeter(float metricValue, GraphicsUnit to) => Convert(metricValue, GraphicsUnit.Centimeter, to);

        public static SizeF Convert(SizeF metricValue, GraphicsUnit from, GraphicsUnit to)
        {
            return new SizeF(
                Convert(metricValue.Width, from, to),
                Convert(metricValue.Height, from, to));
        }

        private static float ConvertFromInch(float metricValue, GraphicsUnit to)
        {
            float[] metrics = { 1, 2.54f, 96, 72 };
            return metricValue * metrics[(int)to];
        }

        private static float ConvertFromCentimeter(float metricValue, GraphicsUnit to)
        {
            return ConvertFromInch(metricValue, to) / 2.54f;
        }

        private static float ConvertFromDisplay(float metricValue, GraphicsUnit to)
        {
            return ConvertFromInch(metricValue, to) / 96;
        }

        private static float ConvertFromPoint(float metricValue, GraphicsUnit to)
        {
            return ConvertFromInch(metricValue, to) / 72;
        }
    }
}
