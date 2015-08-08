// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="World.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, 17 May 2015 1:05:29 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class World
{
    public int width = 128;

    public int height = 128;

    public void Render(GameObject parent)
    {
        var self = new GameObject();
        var mesh = new Mesh();

        var numHorizontalVertices = this.width + 1;
        var numVerticalVertices = this.height + 1;
        var numVertices = numVerticalVertices * numHorizontalVertices;

        var vertices = new Vector3[numVertices];
        var triangles = new int[6 * this.width * this.height];
        var normals = new Vector3[numVertices];
        var uv = new Vector2[numVertices];

        var vertexIndex = 0;
        var halfWidth = this.width / 2.0f;
        var halfHeight = this.height / 2.0f;

        var widthUnit = 1.0f / this.width;
        var heightUnit = 1.0f / this.height;

        for (var verticalIndex = 0; verticalIndex < numVerticalVertices; verticalIndex++)
        {
            for (var horizontalIndex = 0; horizontalIndex < numHorizontalVertices; horizontalIndex++)
            {
                vertices[vertexIndex] = new Vector3((horizontalIndex - halfWidth) * widthUnit, (verticalIndex - halfHeight) * heightUnit);
                normals[vertexIndex] = Vector3.up;
                uv[vertexIndex] = new Vector2(horizontalIndex / (float)numHorizontalVertices, verticalIndex / (float)numVerticalVertices);

                vertexIndex++;
            }
        }

        for (var y = 0; y < this.height; y++)
        {
            for (var x = 0; x < this.width; x++)
            {
                var triangleIndex = ((y * this.width) + x) * 6;
                var currentVerticalIndex = y * numHorizontalVertices;
                var adjacentVerticalIndex = (y + 1) * numHorizontalVertices;

                triangles[triangleIndex + 0] = currentVerticalIndex + x;
                triangles[triangleIndex + 1] = adjacentVerticalIndex + x;
                triangles[triangleIndex + 2] = adjacentVerticalIndex + x + 1;
                triangles[triangleIndex + 3] = currentVerticalIndex + x;
                triangles[triangleIndex + 4] = adjacentVerticalIndex + x + 1;
                triangles[triangleIndex + 5] = currentVerticalIndex + x + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        self.AddComponent<MeshFilter>().mesh = mesh;
        self.transform.parent = parent.transform;

        self.AddComponent<MeshRenderer>();
    }
}