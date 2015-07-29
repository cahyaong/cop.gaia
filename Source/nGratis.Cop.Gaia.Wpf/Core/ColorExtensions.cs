// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorExtensions.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 9 July 2015 12:46:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using System.Windows.Media;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Contract;
    using nGratis.Cop.Gaia.Engine.Core;

    public static class ColorExtensions
    {
        public static SolidColorBrush ToSolidColorBrush(this IColor color, double opacity = 1.0)
        {
            Guard.AgainstNullArgument(() => color);

            var brush = new SolidColorBrush(color.ToMediaColor(opacity));
            brush.Freeze();

            return brush;
        }

        public static Color ToMediaColor(this IColor color, double opacity = 1.0)
        {
            Guard.AgainstNullArgument(() => color);
            Guard.AgainstInvalidArgument(opacity < 0.0 || opacity > 1.0, () => opacity);

            var rgbColor = (RgbColor)color;

            return Color.FromArgb(
                Convert.ToByte((int)(opacity.Clamp(0.0, 1.0) * 255)),
                Convert.ToByte(rgbColor.Red),
                Convert.ToByte(rgbColor.Green),
                Convert.ToByte(rgbColor.Blue));
        }
    }
}