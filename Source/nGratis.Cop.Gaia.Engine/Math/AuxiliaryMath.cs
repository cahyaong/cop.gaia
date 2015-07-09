// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="AuxiliaryMath.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 18 May 2015 1:39:29 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Linq;

    public static class AuxiliaryMath
    {
        public static double Max(params double[] values)
        {
            return values != null ? values.Max() : 0.0;
        }

        public static double Min(params double[] values)
        {
            return values != null ? values.Min() : 0.0;
        }

        public static double InterpolateCubic(double a, double b, double c, double d, double position)
        {
            var p = (d - c) - (a - b);
            var q = (a - b) - p;
            var r = c - a;
            var s = b;

            return (p * position * position * position) + (q * position * position) + (r * position) + s;
        }

        public static double InterpolateLinear(double a, double b, double position)
        {
            return ((1.0 - position) * a) + (position * b);
        }
    }
}