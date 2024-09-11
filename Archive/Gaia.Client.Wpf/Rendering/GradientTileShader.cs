// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GradientTileShader.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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
                    Color = (RgbColor)new HsvColor(
                        startColor.Hue + index * hueStep,
                        startColor.Saturation,
                        startColor.Value)
                })
                .ToDictionary(anon => anon.Index, anon => (IColor)anon.Color);

            this.bucketSize = (valueRange.EndValue - valueRange.StartValue) / numBuckets;
        }

        public IColor FindColor(int value)
        {
            var key = value / this.bucketSize;

            return this.colorLookup.ContainsKey(key) ? this.colorLookup[key] : RgbColor.Default;
        }
    }
}