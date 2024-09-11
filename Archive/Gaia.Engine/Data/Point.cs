// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Point.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 30 July 2015 12:52:23 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Data
{
    public struct Point
    {
        public Point(float x, float y, float z = 0.0F)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public static Vector operator +(Point left, Point right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector operator -(Point left, Point right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Point operator +(Point point, Vector displacement)
        {
            return new Point(point.X + displacement.X, point.Y + displacement.Y, point.Z + displacement.Z);
        }

        public static Point operator -(Point point, Vector displacement)
        {
            return new Point(point.X - displacement.X, point.Y - displacement.Y, point.Z - displacement.Z);
        }
    }
}