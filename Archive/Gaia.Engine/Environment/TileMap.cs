// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileMap.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Friday, 29 May 2015 1:41:37 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Core.Contract;

    public class TileMap<TTile> : TileMap
        where TTile : Tile
    {
        private readonly TTile[] tiles;

        protected TileMap(int numColumns, int numRows)
            : base(numColumns, numRows)
        {
            this.tiles = new TTile[this.NumColumns * this.NumRows];
        }

        public TTile this[int column, int row]
        {
            get { return this.tiles[(row * this.NumColumns) + column]; }
            protected set { this.tiles[(row * this.NumColumns) + column] = value; }
        }

        public TTile this[int index]
        {
            get { return this.tiles[index]; }
            protected set { this.tiles[index] = value; }
        }

        public override Tile GetTile(int column, int row)
        {
            return this[column, row];
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "By design.")]
    public abstract class TileMap
    {
        protected TileMap(int numColumns, int numRows)
        {
            Guard.AgainstInvalidArgument(numColumns <= 0, () => numColumns);
            Guard.AgainstInvalidArgument(numRows <= 0, () => numRows);

            this.NumColumns = numColumns;
            this.NumRows = numRows;
        }

        public int NumColumns { get; private set; }

        public int NumRows { get; private set; }

        public abstract Tile GetTile(int column, int row);
    }
}