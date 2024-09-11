// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticKey.Region.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 9 July 2015 1:49:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Globalization;

    public sealed partial class DiagnosticKey
    {
        public static class Region
        {
            public static readonly DiagnosticKey Elevation = new DiagnosticKey(
                "Region: Altitude (m)", value => value.ToInt32().ToString("N0", CultureInfo.InvariantCulture));
        }
    }
}