// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimulationManager.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 7 May 2016 4:11:54 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Linq;
    using UnityEngine;

    public class SimulationManager : BaseManager
    {
        private readonly TileMap _tileMap = new TileMap();

        public override void DrawDiagnosticVisual(IDrawingCanvas canvas)
        {
            var rowOffset = (int)this._tileMap.VisualBound.yMin;
            var columnOffset = (int)this._tileMap.VisualBound.xMin;

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
    }
}