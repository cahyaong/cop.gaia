// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pen.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 1 August 2015 12:15:46 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System;

    public struct Pen
    {
        public static Pen Null = new Pen(RgbColor.Default, 0.0F, 0.0F);

        public Pen(IColor color, float opacity, float thickness)
            : this()
        {
            this.Color = color ?? RgbColor.Default;
            this.Opacity = opacity.Clamp(0.0F, 1.0F);
            this.Thickness = Math.Max(thickness, 0.0F);
        }

        public IColor Color { get; private set; }

        public float Opacity { get; private set; }

        public float Thickness { get; private set; }

        public string ToUniqueKey()
        {
            return "PEN[{0};OPA={1:0.000};THI={2:0.000}]".WithInvariantFormat(
                this.Color.ToUniqueKey(),
                this.Opacity,
                this.Thickness);
        }
    }
}