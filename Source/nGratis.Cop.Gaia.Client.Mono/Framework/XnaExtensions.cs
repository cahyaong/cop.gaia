// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XnaExtensions.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 11 August 2015 1:02:00 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using Microsoft.Xna.Framework;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    internal static class XnaExtensions
    {
        public static Vector2 ToXnaVector2(this Vector vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector2 ToXnaVector2(this nGratis.Cop.Gaia.Engine.Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Color ToXnaColor(this IColor color)
        {
            RapidGuard.AgainstNullArgument(color);

            var rgbColor = (RgbColor)color;

            return new Color()
                {
                    R = (byte)rgbColor.Red,
                    G = (byte)rgbColor.Green,
                    B = (byte)rgbColor.Blue
                };
        }
    }
}