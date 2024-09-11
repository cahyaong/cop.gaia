// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Region.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 4 June 2015 12:05:30 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public class Region : Tile
    {
        public Region(int row, int column)
            : base(row, column)
        {
        }

        public int Elevation { get; set; }
    }
}