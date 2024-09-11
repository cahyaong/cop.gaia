// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vector.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 30 July 2015 11:09:50 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Data
{
    public struct Vector
    {
        public Vector(float x, float y, float z = 0.0F)
            : this()
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector operator +(Vector vector, float factor)
        {
            return new Vector(vector.X + factor, vector.Y + factor, vector.Z + factor);
        }

        public static Vector operator -(Vector vector, float factor)
        {
            return new Vector(vector.X - factor, vector.Y - factor, vector.Z - factor);
        }

        public static Vector operator *(Vector vector, float factor)
        {
            return new Vector(vector.X * factor, vector.Y * factor, vector.Z * factor);
        }

        public static Vector operator /(Vector vector, float factor)
        {
            return new Vector(vector.X / factor, vector.Y / factor, vector.Z / factor);
        }
    }
}