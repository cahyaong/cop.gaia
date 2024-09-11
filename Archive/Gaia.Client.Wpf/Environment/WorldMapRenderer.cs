// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldMapRenderer.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 30 May 2015 8:54:49 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.ComponentModel.Composition;
    using System.Windows;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    [Export(typeof(IWorldMapRenderer))]
    public class WorldMapRenderer : TileMapRenderer, IWorldMapRenderer
    {
        private readonly ITileShader elevationShader;

        [ImportingConstructor]
        public WorldMapRenderer()
            : this(new TileMapViewport(), new Size(10.0, 10.0), System.Windows.Media.Colors.CornflowerBlue.ToCopColor())
        {
        }

        public WorldMapRenderer(ITileMapViewport tileMapViewport, Size tileSize, IColor accentColor)
            : base(tileMapViewport, tileSize, accentColor)
        {
            this.elevationShader = new GrayscaleTileShader(1 << 14);
        }

        public override void RenderLayer(IDrawingCanvas drawingCanvas, TileMap tileMap)
        {
            Guard.AgainstNullArgument(() => drawingCanvas);
            Guard.AgainstNullArgument(() => tileMap);

            var world = tileMap as WorldMap;

            if (world == null)
            {
                base.RenderLayer(drawingCanvas, tileMap);
            }
            else
            {
                var rectangle = new nGratis.Cop.Gaia.Engine.Data.Rectangle((float)this.TileSize.Width, (float)this.TileSize.Height);

                for (var row = 0; row < this.TileMapViewport.NumRows; row++)
                {
                    for (var column = 0; column < this.TileMapViewport.NumColumns; column++)
                    {
                        var value = world[column + this.TileMapViewport.Column, row + this.TileMapViewport.Row].Elevation;

                        rectangle.X = (float)(column * this.TileSize.Width);
                        rectangle.Y = (float)(row * this.TileSize.Height);

                        var brush = new Brush(this.elevationShader.FindColor(value));

                        drawingCanvas.DrawRectangle(Pen.Null, brush, rectangle);
                    }
                }
            }
        }
    }
}