// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuxiliaryMath.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 18 May 2015 1:39:29 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Linq;

    public static class AuxiliaryMath
    {
        private const double DegreeToRadian = Math.PI / 180.0;

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

        public static double Sin(double angle)
        {
            return Math.Sin(angle * DegreeToRadian);
        }

        public static double Cos(double angle)
        {
            return Math.Cos(angle * DegreeToRadian);
        }

        public static double Tan(double angle)
        {
            return Math.Tan(angle * DegreeToRadian);
        }
    }
}