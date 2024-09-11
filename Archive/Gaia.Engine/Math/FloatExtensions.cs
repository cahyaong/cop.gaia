// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 15 August 2015 1:13:50 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;

    public static class FloatExtensions
    {
        public static float Clamp(this float value, float min, float max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static bool IsCloseTo(this float left, float right, float tolerance = 0.00001F)
        {
            return Math.Abs(left - right) <= tolerance;
        }
    }
}