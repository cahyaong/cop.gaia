// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticKey.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 25 June 2015 1:02:10 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Globalization;
    using nGratis.Cop.Gaia.Engine.Data;

    public sealed partial class DiagnosticKey
    {
        public static readonly DiagnosticKey Unknown = new DiagnosticKey("[__UNKNOWN__]");

        public static readonly DiagnosticKey RenderTime = new DiagnosticKey(
            "Rendering time (ms)",
            value => value.ToDouble().ToString("N0", CultureInfo.InvariantCulture));

        public static readonly DiagnosticKey FramesPerSecond = new DiagnosticKey(
            "Frames per second",
            value => value.ToDouble().ToString("F0", CultureInfo.InvariantCulture));

        public static readonly DiagnosticKey GenerationTime = new DiagnosticKey(
            "Generation time (ms)",
            value => value.ToDouble().ToString("N0", CultureInfo.InvariantCulture));

        public static readonly DiagnosticKey SelectedCoordinate = new DiagnosticKey(
            "Selected coordinate",
            value =>
            {
                var coordinate = (Coordinate?)value;

                return coordinate.HasValue
                    ? "({0:G0}, {1:G0})".WithInvariantFormat(coordinate.Value.Column, coordinate.Value.Row)
                    : "<NULL>";
            });

        private DiagnosticKey(string name = null, Func<object, string> formatValue = null)
        {
            this.Name = name ?? "[__UNDEFINED__]";
            this.FormatValue = formatValue ?? (value => value.ToString());
        }

        public string Name { get; private set; }

        public Func<object, string> FormatValue { get; private set; }
    }
}