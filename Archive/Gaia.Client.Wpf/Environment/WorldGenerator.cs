// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldGenerator.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 29 June 2015 12:31:48 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    [Export(typeof(IWorldGenerator))]
    public class WorldGenerator : IWorldGenerator
    {
        private const int ChunkSize = 32 * 1024;

        private readonly IList<ILayerGenerator> layerGenerators;

        [ImportingConstructor]
        public WorldGenerator()
        {
            this.layerGenerators = new List<ILayerGenerator>()
                {
                    new ElevationLayerGenerator()
                };
        }

        public async Task<WorldMap> GenerateMapAsync(string seed)
        {
            this.layerGenerators
                .ForEach(generator => generator.UpdateSeed(seed));

            var elevationGenerator = this
                .layerGenerators
                .Single(generator => generator.LayerMode == LayerMode.Elevation);

            var worldMap = new WorldMap(1024, 1024);

            var tasks = AuxiliaryEnumerable
                .Step(0, worldMap.NumRows * worldMap.NumColumns, ChunkSize)
                .Select(value => new
                {
                    StartIndex = value,
                    EndIndex = (value + ChunkSize) - 1
                })
                .Select(chunk => Task
                    .Factory
                    .StartNew(() => elevationGenerator.GenerateLayer(worldMap, chunk.StartIndex, chunk.EndIndex)));

            await Task.WhenAll(tasks);

            return worldMap;
        }
    }
}