// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Coordinate.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 9 July 2015 1:17:22 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Data
{
    public struct Coordinate
    {
        public static Coordinate Origin
        {
            get { return new Coordinate { Row = 0, Column = 0 }; }
        }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}