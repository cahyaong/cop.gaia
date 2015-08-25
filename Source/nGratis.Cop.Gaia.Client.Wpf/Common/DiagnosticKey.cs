// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticKey.cs" company="nGratis">
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
            "Render time (ms)",
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