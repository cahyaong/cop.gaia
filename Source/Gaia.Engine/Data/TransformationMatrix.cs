// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransformationMatrix.cs" company="nGratis">
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
// <creation_timestamp>Friday, 9 October 2015 11:45:25 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Data
{
    using nGratis.Cop.Gaia.Engine.Core;

    public struct TransformationMatrix
    {
        private readonly float[] data;

        public TransformationMatrix(params float[] data)
        {
            RapidGuard.AgainstNullArgument(data);
            RapidGuard.AgainstInvalidArgument(data.Length != 9);

            this.data = data;
        }

        public static TransformationMatrix Identity
        {
            get { return new TransformationMatrix(1, 0, 0, 0, 1, 0, 0, 0, 1); }
        }

        public static TransformationMatrix Zero
        {
            get { return new TransformationMatrix(0, 0, 0, 0, 0, 0, 0, 0, 0); }
        }

        public float this[int index]
        {
            get { return this.data[index]; }
            set { this.data[index] = value; }
        }

        public float this[int row, int column]
        {
            get { return this.data[row * 3 + column]; }
            set { this.data[row * 3 + column] = value; }
        }

        public static TransformationMatrix operator *(TransformationMatrix left, TransformationMatrix right)
        {
            var matrix = TransformationMatrix.Zero;

            for (var row = 0; row < 3; row++)
            {
                for (var column = 0; column < 3; column++)
                {
                    for (var index = 0; index < 3; index++)
                    {
                        matrix[row, column] += left[row, index] * right[index, column];
                    }
                }
            }

            return matrix;
        }

        public static Point operator *(TransformationMatrix matrix, Point point)
        {
            return new Point(
                (matrix[0, 0] * point.X) + (matrix[0, 1] * point.Y) + matrix[0, 2],
                (matrix[1, 0] * point.X) + (matrix[1, 1] * point.Y) + matrix[1, 2]);
        }

        public static TransformationMatrix Rotate(float angle)
        {
            var sin = (float)AuxiliaryMath.Sin(angle);
            var cos = (float)AuxiliaryMath.Cos(angle);

            return new TransformationMatrix(cos, sin, 0, -sin, cos, 0, 0, 0, 1);
        }

        public static TransformationMatrix Scale(float factorX, float factorY)
        {
            return new TransformationMatrix(factorX, 0, 0, 0, factorY, 0, 0, 0, 1);
        }

        public static TransformationMatrix Translate(float offsetX, float offsetY)
        {
            return new TransformationMatrix(1, 0, offsetX, 0, 1, offsetY, 0, 0, 1);
        }
    }
}