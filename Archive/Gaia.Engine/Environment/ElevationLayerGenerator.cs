// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElevationLayerGenerator.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 4 July 2015 12:34:37 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
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
            RapidGuard.AgainstNullArgument(seed);

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