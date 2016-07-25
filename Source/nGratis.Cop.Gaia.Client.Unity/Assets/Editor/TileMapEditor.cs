// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileMapEditor.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 9 April 2016 5:15:20 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(TileMap))]
    public class TileMapEditor : Editor
    {
        private TileMap _tileMap;

        private int _numRows;

        private int _numColumns;

        private static bool _isMapSettingsShown;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            this.BuildMapSettingsView();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(this._tileMap);
            }
        }

        private void OnEnable()
        {
            this._tileMap = (TileMap)this.target;

            this._numRows = this._tileMap.NumRows;
            this._numColumns = this._tileMap.NumColumns;
        }

        private void OnSceneGUI()
        {
            this.DrawGrid();
        }

        private void BuildMapSettingsView()
        {
            TileMapEditor._isMapSettingsShown = EditorGUILayout.Foldout(
                TileMapEditor._isMapSettingsShown,
                "Map Settings");

            if (!TileMapEditor._isMapSettingsShown)
            {
                return;
            }

            this._numRows = EditorGUILayout.IntField(new GUIContent("Number of Rows"), this._numRows);
            this._numColumns = EditorGUILayout.IntField(new GUIContent("Number of Columns"), this._numColumns);

            var isRefreshRequired = false;

            if (this._numRows <= 0)
            {
                this._numRows = 8;
                isRefreshRequired = true;
            }

            if (this._numColumns <= 0)
            {
                this._numColumns = 8;
                isRefreshRequired = true;
            }

            if (isRefreshRequired || GUILayout.Button("Recreate Mesh"))
            {
                this.RefreshMap();
            }
        }

        private void RefreshMap()
        {
            this._tileMap.Resize(this._numRows, this._numColumns);

            if (Application.isPlaying)
            {
                this._tileMap.RebuildVisual();
            }

            SceneView.RepaintAll();
        }

        private void DrawGrid()
        {
            var startPoint =
                this._tileMap.gameObject.transform.position -
                new Vector3(this._numColumns / 2f, this._numRows / 2f);

            var endPoint = new Vector3(startPoint.x + this._numColumns, startPoint.y + this._numRows);

            Handles.color = this._numRows != this._tileMap.NumRows || this._numColumns != this._tileMap.NumColumns
                ? Constants.TileMap.ModifiedColor
                : Constants.TileMap.AppliedColor;

            Handles.DrawLine(new Vector3(startPoint.x, startPoint.y), new Vector3(endPoint.x, startPoint.y));
            Handles.DrawLine(new Vector3(endPoint.x, startPoint.y), new Vector3(endPoint.x, endPoint.y));
            Handles.DrawLine(new Vector3(endPoint.x, endPoint.y), new Vector3(startPoint.x, endPoint.y));
            Handles.DrawLine(new Vector3(startPoint.x, endPoint.y), new Vector3(startPoint.x, startPoint.y));

            for (var row = 0; row < this._numRows; row++)
            {
                var y = startPoint.y + row;

                Handles.DrawDottedLine(
                    new Vector3(startPoint.x, y),
                    new Vector3(endPoint.x, y),
                    Constants.TileMap.DashLength);
            }

            for (var column = 0; column < this._numColumns; column++)
            {
                var x = startPoint.x + column;

                Handles.DrawDottedLine(
                    new Vector3(x, startPoint.y),
                    new Vector3(x, endPoint.y),
                    Constants.TileMap.DashLength);
            }
        }
    }
}