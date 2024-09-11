// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plane.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 24 May 2016 9:45:10 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using UnityEngine;

    public class Plane
    {
        public const float LineHalfThickness = 0.025f;

        public static readonly Plane Square10 = new Plane(1, 1);

        public static readonly Plane Line10 = new Plane(1, 2 * Plane.LineHalfThickness, 0);

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