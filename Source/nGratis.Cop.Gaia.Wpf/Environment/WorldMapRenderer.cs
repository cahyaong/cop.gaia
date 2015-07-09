// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldMapRenderer.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 30 May 2015 8:54:49 AM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System.ComponentModel.Composition;
    using System.Windows;
    using System.Windows.Media;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;

    [Export(typeof(IWorldMapRenderer))]
    public class WorldMapRenderer : TileMapRenderer, IWorldMapRenderer
    {
        private readonly ITileShader elevationShader;

        [ImportingConstructor]
        public WorldMapRenderer()
            : this(new TileMapViewport(), new Size(10.0, 10.0), Colors.CornflowerBlue)
        {
        }

        public WorldMapRenderer(ITileMapViewport tileMapViewport, Size tileSize, Color accentColor)
            : base(tileMapViewport, tileSize, accentColor)
        {
            this.elevationShader = new GrayscaleTileShader(1 << 14);
        }

        public override void RenderLayer(ICanvas canvas, TileMap tileMap)
        {
            Guard.AgainstNullArgument(() => canvas);
            Guard.AgainstNullArgument(() => tileMap);

            var world = tileMap as WorldMap;

            if (world == null)
            {
                base.RenderLayer(canvas, tileMap);
            }
            else
            {
                var rectangle = new Rect
                    {
                        Width = this.TileSize.Width,
                        Height = this.TileSize.Height
                    };

                for (var row = 0; row < this.TileMapViewport.NumRows; row++)
                {
                    for (var column = 0; column < this.TileMapViewport.NumColumns; column++)
                    {
                        var value = world[column + this.TileMapViewport.Column, row + this.TileMapViewport.Row].Elevation;

                        rectangle.X = column * this.TileSize.Width;
                        rectangle.Y = row * this.TileSize.Height;

                        canvas.DrawRectangle(null, this.elevationShader.FindBrush(value), rectangle);
                    }
                }
            }
        }
    }
}