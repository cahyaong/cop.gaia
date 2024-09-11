// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GrayscaleTileShader.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 2 June 2015 12:32:34 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Collections.Generic;
    using System.Linq;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    internal class GrayscaleTileShader : ITileShader
    {
        private const int NumBuckets = 1 << 8;

        private static readonly IDictionary<int, IColor> ColorLookup;

        private readonly int bucketSize;

        static GrayscaleTileShader()
        {
            ColorLookup = Enumerable
                .Range(0, NumBuckets)
                .Select(index => new
                {
                    Index = index,
                    Color = new RgbColor(index, index, index)
                })
                .ToDictionary(anon => anon.Index, anon => (IColor)anon.Color);
        }

        public GrayscaleTileShader(int maxValue)
        {
            Guard.AgainstDefaultArgument(() => maxValue);
            Guard.AgainstInvalidArgument(maxValue % NumBuckets != 0, () => maxValue);

            this.bucketSize = maxValue / NumBuckets;
        }

        public IColor FindColor(int value)
        {
            var key = (value / this.bucketSize).Clamp(0, NumBuckets - 1);

            return ColorLookup.ContainsKey(key) ? ColorLookup[key] : RgbColor.Default;
        }
    }
}