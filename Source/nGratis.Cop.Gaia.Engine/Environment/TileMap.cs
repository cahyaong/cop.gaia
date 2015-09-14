﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileMap.cs" company="nGratis">
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