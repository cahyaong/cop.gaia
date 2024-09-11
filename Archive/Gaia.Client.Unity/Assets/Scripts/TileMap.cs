// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileMap.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 9 April 2016 5:14:40 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    // TODO: Move rendering component out from this class!

    [Serializable]
    public class TileMap : MonoBehaviour
    {
        [SerializeField]
        [HideInInspector]
        private int _numRows;

        [SerializeField]
        [HideInInspector]
        private int _numColumns;

        [NonSerialized]
        private Tile[,] _tiles;

        public int NumRows
        {
            get { return this._numRows; }
            private set { this._numRows = value; }
        }

        public int NumColumns
        {
            get { return this._numColumns; }
            private set { this._numColumns = value; }
        }

        public Rect WorldBound
        {
            get;
            private set;
        }

        public void Resize(int numRows, int numColumns)
        {
            Guard.Argument.IsZeroOrNegative(numRows);
            Guard.Argument.IsZeroOrNegative(numColumns);

            if (this.NumRows == numRows && this.NumColumns == numColumns)
            {
                return;
            }

            this.NumRows = numRows;
            this.NumColumns = numColumns;

            this.CalculateBound();
            this.GenerateTiles();
        }

        public void RebuildVisual()
        {
            this.GenerateMesh();
            this.GenerateTexture();
        }

        public Vector2 ConvertToGridPoint(Vector2 worldPoint)
        {
            return this.WorldBound.Contains(worldPoint)
                ? new Vector2((int)(worldPoint.x - this.WorldBound.xMin), (int)(worldPoint.y - this.WorldBound.yMin))
                : new Vector2(-1, -1);
        }

        private void Start()
        {
            this.CalculateBound();
            this.GenerateMesh();
            this.GenerateTexture();
        }

        private void CalculateBound()
        {
            this.WorldBound = new Rect(
                new Vector2(-this.NumColumns / 2f, -this.NumRows / 2f),
                new Vector2(this.NumColumns, this.NumRows));
        }

        private void GenerateTiles()
        {
            this._tiles = new Tile[this.NumRows, this.NumColumns];

            for (var row = 0; row < this.NumRows; row++)
            {
                for (var column = 0; column < this.NumColumns; column++)
                {
                    this._tiles[row, column] = new Tile();
                }
            }
        }

        private void GenerateTexture()
        {
            var texture = new Texture2D(this.NumColumns, this.NumRows);

            for (var row = 0; row < this.NumRows; row++)
            {
                for (var column = 0; column < this.NumColumns; column++)
                {
                    var value = Random.value;
                    var color = new Color(value, value, value);
                    texture.SetPixel(column, row, color);
                }
            }

            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.Apply();

            var meshRenderer = this.gameObject.GetComponent<MeshRenderer>();

            if (!meshRenderer)
            {
                meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
            }

            var material = new Material(Shader.Find("Diffuse")) { mainTexture = texture };

            meshRenderer.sharedMaterial = material;
        }

        private void GenerateMesh()
        {
            var mesh = new Mesh { name = "TileMap" };

            var numHorizontalVertices = this.NumColumns + 1;
            var numVerticalVertices = this.NumRows + 1;
            var numVertices = numVerticalVertices * numHorizontalVertices;

            var vertices = new Vector3[numVertices];
            var triangles = new int[6 * this.NumColumns * this.NumRows];
            var normals = new Vector3[numVertices];
            var uv = new Vector2[numVertices];

            var vertexIndex = 0;
            var halfNumColumns = this.NumColumns / 2.0f;
            var halfNumRows = this.NumRows / 2.0f;

            for (var verticalIndex = 0; verticalIndex < numVerticalVertices; verticalIndex++)
            {
                for (var horizontalIndex = 0; horizontalIndex < numHorizontalVertices; horizontalIndex++)
                {
                    normals[vertexIndex] = Vector3.forward;

                    vertices[vertexIndex] = new Vector3(
                        (horizontalIndex - halfNumColumns),
                        (verticalIndex - halfNumRows),
                        0);

                    uv[vertexIndex] = new Vector2(
                        horizontalIndex / (float)this.NumColumns,
                        verticalIndex / (float)this.NumRows);

                    vertexIndex++;
                }
            }

            for (var row = 0; row < this.NumRows; row++)
            {
                for (var column = 0; column < this.NumColumns; column++)
                {
                    var triangleIndex = ((row * this.NumColumns) + column) * 6;
                    var currentVerticalIndex = row * numHorizontalVertices;
                    var adjacentVerticalIndex = (row + 1) * numHorizontalVertices;

                    triangles[triangleIndex + 0] = currentVerticalIndex + column;
                    triangles[triangleIndex + 1] = adjacentVerticalIndex + column;
                    triangles[triangleIndex + 2] = adjacentVerticalIndex + column + 1;
                    triangles[triangleIndex + 3] = currentVerticalIndex + column;
                    triangles[triangleIndex + 4] = adjacentVerticalIndex + column + 1;
                    triangles[triangleIndex + 5] = currentVerticalIndex + column + 1;
                }
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.uv = uv;

            var meshFilter = this.GetComponent<MeshFilter>();

            if (!meshFilter)
            {
                meshFilter = this.gameObject.AddComponent<MeshFilter>();
            }

            meshFilter.mesh = mesh;
        }
    }
}