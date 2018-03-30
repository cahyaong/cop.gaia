// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityDrawingCanvas.cs" company="nGratis">
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
// <creation_timestamp>Wednesday, 11 May 2016 10:25:21 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Collections.Generic;
    using UnityEngine;

    public class UnityDrawingCanvas : MonoBehaviour, IDrawingCanvas
    {
        private readonly IDictionary<Color, Material> _lineMaterialCache = new Dictionary<Color, Material>();

        private Camera _camera;

        private Shader _lineShader;

        private void Start()
        {
            this._camera = this.GetComponent<Camera>();
            Guard.Operation.IsUnexpectedNull(this._camera);

            this._lineShader = Shader.Find("Self-Illumin/VertexLit");
            Guard.Operation.IsUnexpectedNull(this._lineShader);

            this._camera.orthographicSize = 16;
        }

        public void DrawLine(Color color, Vector2 start, Vector2 end)
        {
            UnityDrawingCanvas.DrawLine(this.FindLineMaterial(color), start, end);
        }

        public void DrawRectangle(Color color, Rect rectangle)
        {
            if (rectangle.width <= 0 || rectangle.height < 0)
            {
                return;
            }

            var material = this.FindLineMaterial(color);

            UnityDrawingCanvas.DrawLine(
                material,
                new Vector2(rectangle.xMin, rectangle.yMin),
                new Vector2(rectangle.xMax, rectangle.yMin));

            UnityDrawingCanvas.DrawLine(
                material,
                new Vector2(rectangle.xMax, rectangle.yMin),
                new Vector2(rectangle.xMax, rectangle.yMax));

            UnityDrawingCanvas.DrawLine(
                material,
                new Vector2(rectangle.xMax, rectangle.yMax),
                new Vector2(rectangle.xMin, rectangle.yMax));

            UnityDrawingCanvas.DrawLine(
                material,
                new Vector2(rectangle.xMin, rectangle.yMax),
                new Vector2(rectangle.xMin, rectangle.yMin));
        }

        public Vector2 ConvertToWorldPoint(Vector2 screenPoint)
        {
            return this._camera.ScreenToWorldPoint(screenPoint);
        }

        private static void DrawLine(Material material, Vector2 start, Vector2 end)
        {
            var angle = Mathf.Atan2(end.y - start.y, end.x - start.x);
            var length = (end - start).magnitude + Plane.LineHalfThickness;
            var matrix = Matrix4x4.identity;

            matrix.SetTRS(
                new Vector2(start.x - Plane.LineHalfThickness, start.y),
                Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward),
                new Vector2(length, 1));

            Graphics.DrawMesh(Plane.Line10.Mesh, matrix, material, 0);
        }

        private Material FindLineMaterial(Color color)
        {
            var material = default(Material);

            if (this._lineMaterialCache.TryGetValue(color, out material))
            {
                return material;
            }

            material = new Material(this._lineShader) { color = color };
            this._lineMaterialCache[color] = material;

            return material;
        }
    }
}