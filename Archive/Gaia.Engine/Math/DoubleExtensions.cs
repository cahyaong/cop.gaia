// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 30 May 2015 1:08:00 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;

    public static class DoubleExtensions
    {
        public static double ToInt32(this double value)
        {
            if (value >= 1073741824.0)
            {
                return (2.0 * Math.IEEERemainder(value, 1073741824.0)) - 1073741824.0;
            }

            if (value <= -1073741824.0)
            {
                return (2.0 * Math.IEEERemainder(value, 1073741824.0)) + 1073741824.0;
            }

            return value;
        }

        public static double ToCubicSCurve(this double value)
        {
            return value * value * (3.0 - (2.0 * value));
        }

        public static double ToQuinticSCurve(this double value)
        {
            var cubic = value * value * value;
            var quatric = cubic * value;
            var quintic = quatric * value;

            return (6.0 * quintic) - (15.0 * quatric) + (10.0 * cubic);
        }

        public static double Clamp(this double value, double min, double max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static bool IsCloseTo(this double left, double right, double tolerance = 0.00001)
        {
            return Math.Abs(left - right) <= tolerance;
        }
    }
}