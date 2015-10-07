// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GradientTileShader.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 7 July 2015 1:18:32 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Collections.Generic;
    using System.Linq;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    internal class GradientTileShader : ITileShader
    {
        private readonly IDictionary<int, IColor> colorLookup;

        private readonly int bucketSize;

        public GradientTileShader(Range<int> valueRange, Range<IColor> colorRange, int numBuckets)
        {
            Guard.AgainstNullArgument(() => valueRange);
            Guard.AgainstInvalidArgument(valueRange.StartValue < 0 || valueRange.EndValue < 0, () => valueRange);
            Guard.AgainstNullArgument(() => colorRange);
            Guard.AgainstInvalidArgument(numBuckets <= 0, () => numBuckets);

            var startColor = (HsvColor)colorRange.StartValue;
            var endColor = (HsvColor)colorRange.EndValue;
            var hueStep = (endColor.Hue - startColor.Hue) / numBuckets;

            this.colorLookup = Enumerable
                .Range(0, numBuckets)
                .Select(index => new
                {
                    Index = index,
                    Color = (RgbColor)new HsvColor(startColor.Hue + (index * hueStep), startColor.Saturation, startColor.Value)
                })
                .ToDictionary(annon => annon.Index, annon => (IColor)annon.Color);

            this.bucketSize = (valueRange.EndValue - valueRange.StartValue) / numBuckets;
        }

        public IColor FindColor(int value)
        {
            var key = value / this.bucketSize;

            return this.colorLookup.ContainsKey(key) ? this.colorLookup[key] : RgbColor.Default;
        }
    }
}