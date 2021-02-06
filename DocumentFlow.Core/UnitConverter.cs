//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 27.05.2015
// Time: 10:15
//-----------------------------------------------------------------------

using System;
using System.Drawing;

namespace DocumentFlow.Core
{
    public enum GraphicsUnit { Inch, Centimeter, Millimeter, Display, Point, Dpi100, Dpi200, Dpi300 }

    public class Length
    {
        private float value;
        private GraphicsUnit unit;

        public Length(float value, GraphicsUnit unit) => (this.value, this.unit) = (value, unit);

        public Length ToInch() => new Length(Convert(value, unit, GraphicsUnit.Inch), GraphicsUnit.Inch);

        public Length ToPoint() => new Length(Convert(value, unit, GraphicsUnit.Point), GraphicsUnit.Point);

        public Length ToDpi200() => new Length(Convert(value, unit, GraphicsUnit.Dpi200), GraphicsUnit.Dpi200);

        public static Length FromInch(float value) => new Length(value, GraphicsUnit.Inch);
        
        public static Length FromMillimeter(float value) => new Length(value, GraphicsUnit.Millimeter);

        public static implicit operator float(Length length) => length.value;

        private static float Convert(float metricValue, GraphicsUnit from, GraphicsUnit to)
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
                case GraphicsUnit.Millimeter:
                    return ConvertFromMillimeter(metricValue, to);
                case GraphicsUnit.Dpi100:
                    return ConvertFromDpi100(metricValue, to);
                case GraphicsUnit.Dpi200:
                    return ConvertFromDpi200(metricValue, to);
                case GraphicsUnit.Dpi300:
                    return ConvertFromDpi300(metricValue, to);
            }

            return metricValue;
        }

        private static float ConvertFromInch(float metricValue, GraphicsUnit to)
        {
            float[] metrics = { 1, 2.54f, 25.4f, 96, 72, 100, 200, 300 };
            return metricValue * metrics[(int)to];
        }

        private static float ConvertFromCentimeter(float metricValue, GraphicsUnit to) => ConvertFromInch(metricValue, to) / 2.54f;

        private static float ConvertFromDisplay(float metricValue, GraphicsUnit to) => ConvertFromInch(metricValue, to) / 96;

        private static float ConvertFromPoint(float metricValue, GraphicsUnit to) => ConvertFromInch(metricValue, to) / 72;

        private static float ConvertFromMillimeter(float metricValue, GraphicsUnit to) => ConvertFromInch(metricValue, to) / 25.4f;

        private static float ConvertFromDpi100(float metricValue, GraphicsUnit to) => ConvertFromInch(metricValue, to) / 100f;

        private static float ConvertFromDpi200(float metricValue, GraphicsUnit to) => ConvertFromInch(metricValue, to) / 200f;

        private static float ConvertFromDpi300(float metricValue, GraphicsUnit to) => ConvertFromInch(metricValue, to) / 300f;
    }

    public static class LengthExt
    {
        public static Length AsInch(this float value) => new Length(value, GraphicsUnit.Inch);

        public static Length AsMillimeter(this float value) => new Length(value, GraphicsUnit.Millimeter);

        public static int ToInt(this float value) => Convert.ToInt32(value);
    }
}
