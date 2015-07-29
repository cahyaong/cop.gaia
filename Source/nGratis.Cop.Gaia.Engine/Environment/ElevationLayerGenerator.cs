// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevationLayerGenerator.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 4 July 2015 12:34:37 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Noise;

    public class ElevationLayerGenerator : BaseLayerGenerator
    {
        private INoiseModule noiseModule;

        public override LayerMode LayerMode
        {
            get { return LayerMode.Elevation; }
        }

        public override void UpdateSeed(string seed)
        {
            Guard.AgainstNullArgument(() => seed);

            base.UpdateSeed(seed);

            this.noiseModule = new PerlinNoise(seed.ToStableSeed());
        }

        protected override void GenerateLayer(Region region)
        {
            // TODO: Use noise modifier instead of applying direct formula!

            var elevation = ((this.noiseModule.GetValue(region.Column, region.Row, 0.0) + 1.0) / 2.0).Clamp(0.0, 1.0) * WorldMap.Limits.Elevation.EndValue;
            region.Elevation = (int)elevation;
        }
    }
}