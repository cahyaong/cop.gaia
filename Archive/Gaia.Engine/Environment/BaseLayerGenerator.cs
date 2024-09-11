// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseLayerGenerator.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 29 June 2015 12:31:48 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Core;

    public abstract class BaseLayerGenerator : ILayerGenerator
    {
        public string Seed { get; private set; }

        public abstract LayerMode LayerMode { get; }

        public virtual void UpdateSeed(string seed)
        {
            RapidGuard.AgainstNullArgument(seed);

            this.Seed = seed;
        }

        public void GenerateLayer(WorldMap worldMap, int startIndex, int endIndex)
        {
            RapidGuard.AgainstNullArgument(worldMap);
            Guard.AgainstInvalidArgument(startIndex < 0, () => startIndex);
            Guard.AgainstInvalidArgument(startIndex >= endIndex, () => startIndex);

            for (var index = startIndex; index <= endIndex; index++)
            {
                this.GenerateLayer(worldMap[index]);
            }
        }

        protected abstract void GenerateLayer(Region region);
    }
}