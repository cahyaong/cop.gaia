// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleExtensions.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 - 2015 Cahya Ong
//
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
//   associated documentation files (the "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
//   following conditions:
//
//   The above copyright notice and this permission notice shall be included in all copies or substantial
//   portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
//   LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO
//   EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR
//   THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
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