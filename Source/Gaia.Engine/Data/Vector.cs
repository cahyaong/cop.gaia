﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vector.cs" company="nGratis">
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