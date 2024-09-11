// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 25 June 2015 12:09:50 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    using System.Globalization;
    using System.Windows;

    internal static class ObjectExtensions
    {
        public static double ToDouble(this object instance)
        {
            return instance == null
                ? double.NaN
                : Convert.ToDouble(instance, CultureInfo.InvariantCulture);
        }

        public static int ToInt32(this object instance)
        {
            return instance == null
                ? 0
                : Convert.ToInt32(instance, CultureInfo.InvariantCulture);
        }

        public static Point ToPoint(this object instance)
        {
            return instance == null
                ? new Point(double.NaN, double.NaN)
                : (Point)instance;
        }
    }
}