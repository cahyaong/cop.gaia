// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GrayscaleTileShader.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 2 June 2015 12:32:34 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

using nGratis.Cop.Gaia.Engine.Core;

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;

    internal class GrayscaleTileShader : ITileShader
    {
        private const int NumBuckets = 1 << 8;

        private static readonly Brush DefaultColor = new SolidColorBrush(Colors.Black);

        private static readonly IDictionary<int, SolidColorBrush> ColorLookup;

        private readonly int bucketSize;

        static GrayscaleTileShader()
        {
            ColorLookup = Enumerable
                .Range(0, NumBuckets)
                .Select(index => new { Index = index, Color = new RgbColor(index, index, index) })
                .ToDictionary(annon => annon.Index, annon => annon.Color.ToSolidColorBrush());
        }

        public GrayscaleTileShader(int maxValue)
        {
            Guard.AgainstDefaultArgument(() => maxValue);
            Guard.AgainstInvalidArgument(maxValue % NumBuckets != 0, () => maxValue);

            this.bucketSize = maxValue / NumBuckets;
        }

        public Brush FindBrush(int value)
        {
            var key = (value / this.bucketSize).Clamp(0, NumBuckets - 1);

            return ColorLookup.ContainsKey(key) ? ColorLookup[key] : DefaultColor;
        }
    }
}