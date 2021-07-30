// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorExtensions.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 9 July 2015 12:46:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    public static class ColorExtensions
    {
        // TODO: May need to handle concurrency when dealing with brush or pen lookup; for now assuming UI
        //       thread is the only one using them!

        private static readonly IDictionary<string, System.Windows.Media.Brush> BrushLookup;

        private static readonly IDictionary<string, System.Windows.Media.Pen> PenLookup;

        static ColorExtensions()
        {
            BrushLookup = new Dictionary<string, System.Windows.Media.Brush>();
            PenLookup = new Dictionary<string, System.Windows.Media.Pen>();
        }

        public static System.Windows.Media.Brush ToMediaBrush(this IColor color, double opacity)
        {
            if (color == null)
            {
                return null;
            }

            var uniqueKey = "{0};OPA={1:0.000}".WithInvariantFormat(color.ToUniqueKey(), opacity);

            var brush = default(System.Windows.Media.Brush);

            if (!BrushLookup.TryGetValue(uniqueKey, out brush))
            {
                brush = new SolidColorBrush(color.ToMediaColor(opacity));
                brush.Freeze();

                BrushLookup.Add(uniqueKey, brush);
            }

            return brush;
        }

        public static System.Windows.Media.Brush ToMediaBrush(this nGratis.Cop.Gaia.Engine.Core.Brush brush)
        {
            return brush.Color.ToMediaBrush(brush.Opacity);
        }

        public static System.Windows.Media.Pen ToMediaPen(this IColor color, double opacity, double thickness)
        {
            if (color == null)
            {
                return null;
            }

            var uniqueKey = "{0};OPA={1:0.000};THI={2:0.000}".WithInvariantFormat(
                color.ToUniqueKey(),
                opacity,
                thickness);

            var pen = default(System.Windows.Media.Pen);

            if (!PenLookup.TryGetValue(uniqueKey, out pen))
            {
                pen = new System.Windows.Media.Pen(color.ToMediaBrush(opacity), thickness);
                pen.Freeze();

                PenLookup.Add(uniqueKey, pen);
            }

            return pen;
        }

        public static System.Windows.Media.Pen ToMediaPen(this nGratis.Cop.Gaia.Engine.Core.Pen pen)
        {
            return pen.Color.ToMediaPen(pen.Opacity, pen.Thickness);
        }

        public static Color ToMediaColor(this IColor color, double opacity = 1.0)
        {
            if (color == null)
            {
                return Colors.Transparent;
            }

            var rgbColor = (RgbColor)color;

            return Color.FromArgb(
                Convert.ToByte((int)(opacity.Clamp(0.0, 1.0) * 255)),
                Convert.ToByte(rgbColor.Red),
                Convert.ToByte(rgbColor.Green),
                Convert.ToByte(rgbColor.Blue));
        }

        public static IColor ToCopColor(this System.Windows.Media.Color color)
        {
            return new RgbColor(color.R, color.G, color.B);
        }
    }
}