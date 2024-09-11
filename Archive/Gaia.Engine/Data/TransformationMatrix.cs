// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransformationMatrix.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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