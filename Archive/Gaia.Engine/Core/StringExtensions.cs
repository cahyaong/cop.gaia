// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, 21 June 2015 2:17:36 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    using System.Linq;

    public static class StringExtensions
    {
        public static int ToStableSeed(this string value)
        {
            return string.IsNullOrEmpty(value)
                ? 0
                : value.Aggregate(42, (hash, letter) => (hash * 31) + (int)letter);
        }
    }
}