// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldMap.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 1:05:48 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Gaia.Engine.Data;

    public class WorldMap : TileMap<Region>
    {
        public WorldMap(int numColumns, int numRows)
            : base(numColumns, numRows)
        {
            var index = 0;

            for (var row = 0; row < this.NumRows; row++)
            {
                for (var column = 0; column < this.NumColumns; column++)
                {
                    this[index++] = new Region(row, column);
                }
            }
        }

        public static class Limits
        {
            public static readonly Range<int> Elevation = new Range<int>(0, (1 << 14) - 1);
        }
    }
}