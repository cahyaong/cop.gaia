// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plane.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 - 2016 Cahya Ong
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
// <creation_timestamp>Tuesday, 24 May 2016 9:45:10 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using UnityEngine;

    public class Plane
    {
        public static readonly Plane Square10 = new Plane(1, 1);

        public static readonly Plane Line10 = new Plane(1, 0.05f, 0);

        private readonly float _width;

        private readonly float _height;

        private readonly float _offset;

        public Plane(float width, float height, float offset = 0.5f)
        {
            Guard.Argument.IsZeroOrNegative(width);
            Guard.Argument.IsZeroOrNegative(height);
            Guard.Argument.IsNegative(offset);

            this._width = width;
            this._height = height;
            this._offset = offset;

            this.ConstructMesh();
        }

        public Mesh Mesh { get; private set; }

        private void ConstructMesh()
        {
            var rectangle = this._offset > 0
                ? new Rect(-this._offset, -this._offset, this._width, this._height)
                : new Rect(0f, -this._height / 2f, this._width, this._height);

            var vertices = new[]
                {
                    new Vector3(rectangle.xMin, rectangle.yMin, 0),
                    new Vector3(rectangle.xMin, rectangle.yMax, 0),
                    new Vector3(rectangle.xMax, rectangle.yMax, 0),
                    new Vector3(rectangle.xMax, rectangle.yMin, 0)
                };

            var uv = new[]
                {
                    new Vector2(0, 0),
                    new Vector2(0, 1),
                    new Vector2(1, 1),
                    new Vector2(1, 0)
                };

            var triangles = new[] { 0, 1, 2, 0, 2, 3 };

            var mesh = new Mesh
            {
                vertices = vertices,
                uv = uv
            };

            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            this.Mesh = mesh;
        }
    }
}