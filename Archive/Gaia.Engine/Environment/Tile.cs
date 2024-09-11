// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tile.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Friday, 29 May 2015 1:15:46 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Gaia.Engine.Data;

    public class Tile
    {
        public Tile(Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

        public Tile(int row, int column)
        {
            this.Coordinate = new Coordinate { Row = row, Column = column };
        }

        public int Row
        {
            get { return this.Coordinate.Row; }
        }

        public int Column
        {
            get { return this.Coordinate.Column; }
        }

        public Coordinate Coordinate { get; private set; }
    }
}