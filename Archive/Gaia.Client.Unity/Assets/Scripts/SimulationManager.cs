// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimulationManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 7 May 2016 4:11:54 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Linq;
    using UnityEngine;

    public class SimulationManager : BaseManager
    {
        private TileMap _tileMap;

        public override void DrawDiagnosticVisual(IDrawingCanvas canvas)
        {
            var rowOffset = (int)this._tileMap.WorldBound.yMin;
            var columnOffset = (int)this._tileMap.WorldBound.xMin;

            Enumerable
                .Range(rowOffset, this._tileMap.NumRows + 1)
                .ToList()
                .ForEach(index => canvas.DrawLine(
                    Color.gray,
                    new Vector2(columnOffset, index),
                    new Vector2(this._tileMap.NumColumns + columnOffset, index)));

            Enumerable
                .Range(columnOffset, this._tileMap.NumColumns + 1)
                .ToList()
                .ForEach(index => canvas.DrawLine(
                    Color.gray,
                    new Vector2(index, rowOffset),
                    new Vector2(index, this._tileMap.NumRows + rowOffset)));
        }

        private void Start()
        {
            this._tileMap = ObjectFinder.FindExactlySingleObject<TileMap>();

            // TODO: Replace with a logic to load level from disk or world generator!
            if (this._tileMap.NumRows <= 0 || this._tileMap.NumColumns <= 0)
            {
                this._tileMap.Resize(8, 8);
                this._tileMap.RebuildVisual();
            }
        }
    }
}