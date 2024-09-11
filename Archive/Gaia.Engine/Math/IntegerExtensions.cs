// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 3 June 2015 12:51:09 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;

    public static class IntegerExtensions
    {
        public static int Clamp(this int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }

        public static uint Clamp(this uint value, uint min, uint max)
        {
            return Math.Min(Math.Max(value, min), max);
        }
    }
}