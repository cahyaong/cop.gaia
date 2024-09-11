// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITileMapRenderer.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 30 May 2015 8:47:10 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Windows;
    using nGratis.Cop.Gaia.Engine;

    public interface ITileMapRenderer
    {
        Size TileSize { get; }

        Size ViewportSize { get; }

        ITileMapViewport TileMapViewport { get; }

        Size DesiredViewportSize { get; }

        void RenderGridBorder(IDrawingCanvas drawingCanvas);

        void RenderGridLines(IDrawingCanvas drawingCanvas);

        void RenderLayer(IDrawingCanvas drawingCanvas, TileMap tileMap);

        void RenderTileSelection(IDrawingCanvas drawingCanvas, Tile tile);

        void MeasureViewport(Size availableSize);

        void ArrangeViewport(Size finalSize);

        void PanCamera(int deltaRows, int deltaColumns, int mostRows, int mostColumns);
    }
}