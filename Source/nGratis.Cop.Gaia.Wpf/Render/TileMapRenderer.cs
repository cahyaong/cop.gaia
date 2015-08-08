// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileMapRenderer.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 30 May 2015 9:15:11 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    public class TileMapRenderer : ITileMapRenderer
    {
        private readonly Pen gridBorderPen;

        private readonly Pen gridLinePen;

        private readonly Pen cellSelectionPen;

        private readonly Brush cellSelectionBrush;

        public TileMapRenderer(ITileMapViewport tileMapViewport, System.Windows.Size tileSize, IColor accentColor)
        {
            Guard.AgainstNullArgument(() => tileMapViewport);
            Guard.AgainstInvalidArgument(tileSize.Width <= 0 || tileSize.Height <= 0, () => tileSize);
            Guard.AgainstNullArgument(() => accentColor);

            this.gridBorderPen = new Pen(accentColor, 1.0, 1.0);
            this.gridLinePen = new Pen(accentColor, 1.0, 0.1);
            this.cellSelectionPen = new Pen(accentColor, 1.0, 1.0);
            this.cellSelectionBrush = new Brush(accentColor, 0.25);

            this.TileSize = tileSize;
            this.TileMapViewport = tileMapViewport;

            this.ViewportSize = System.Windows.Size.Empty;

            this.DesiredViewportSize = new System.Windows.Size(
                this.TileMapViewport.NumColumns * this.TileSize.Width,
                this.TileMapViewport.NumRows * this.TileSize.Height);
        }

        public System.Windows.Size TileSize { get; private set; }

        public System.Windows.Size DesiredViewportSize { get; private set; }

        public System.Windows.Size ViewportSize { get; private set; }

        public ITileMapViewport TileMapViewport { get; private set; }

        public void RenderGridBorder(IDrawingCanvas drawingCanvas)
        {
            Guard.AgainstNullArgument(() => drawingCanvas);

            drawingCanvas.DrawRectangle(
                this.gridBorderPen,
                null,
                new Rectangle(this.ViewportSize.Width, this.ViewportSize.Height));
        }

        public virtual void RenderGridLines(IDrawingCanvas drawingCanvas)
        {
            Guard.AgainstNullArgument(() => drawingCanvas);

            if (this.TileMapViewport.NumRows <= 0 || this.TileMapViewport.NumColumns <= 0)
            {
                return;
            }

            var startPoint = new Point();
            var endPoint = new Point();

            for (var row = 1; row < this.TileMapViewport.NumRows; row++)
            {
                startPoint.X = 0.0F;
                startPoint.Y = (float)(row * this.TileSize.Height);
                endPoint.X = (float)this.ViewportSize.Width;
                endPoint.Y = (float)(row * this.TileSize.Height);

                drawingCanvas.DrawLine(this.gridLinePen, startPoint, endPoint);
            }

            for (var column = 1; column < this.TileMapViewport.NumColumns; column++)
            {
                startPoint.X = (float)(column * this.TileSize.Width);
                startPoint.Y = 0.0F;
                endPoint.X = (float)(column * this.TileSize.Width);
                endPoint.Y = (float)this.ViewportSize.Height;

                drawingCanvas.DrawLine(this.gridLinePen, startPoint, endPoint);
            }
        }

        public virtual void RenderLayer(IDrawingCanvas drawingCanvas, TileMap tileMap)
        {
        }

        public virtual void RenderTileSelection(IDrawingCanvas drawingCanvas, Tile tile)
        {
            Guard.AgainstNullArgument(() => drawingCanvas);
            Guard.AgainstNullArgument(() => tile);

            var rectangle = new Rectangle(
                (tile.Column - this.TileMapViewport.Column) * this.TileSize.Width,
                (tile.Row - this.TileMapViewport.Row) * this.TileSize.Height,
                this.TileSize.Width,
                this.TileSize.Height);

            drawingCanvas.DrawRectangle(this.cellSelectionPen, this.cellSelectionBrush, rectangle);
        }

        public void MeasureViewport(System.Windows.Size availableSize)
        {
            this.DesiredViewportSize = new System.Windows.Size(
                (int)(availableSize.Width / this.TileSize.Width).Clamp(0, this.TileMapViewport.MostRows) * this.TileSize.Width,
                (int)(availableSize.Height / this.TileSize.Height).Clamp(0, this.TileMapViewport.MostColumns) * this.TileSize.Height);
        }

        public void ArrangeViewport(System.Windows.Size finalSize)
        {
            this.TileMapViewport.Resize(
                (int)(finalSize.Height / this.TileSize.Height),
                (int)(finalSize.Width / this.TileSize.Width));

            this.ViewportSize = new System.Windows.Size(
                this.TileMapViewport.NumColumns * this.TileSize.Width,
                this.TileMapViewport.NumRows * this.TileSize.Height);
        }

        public void PanCamera(int deltaRows, int deltaColumns, int mostRows, int mostColumns)
        {
            Guard.AgainstInvalidArgument(mostRows <= 0, () => mostRows);
            Guard.AgainstInvalidArgument(mostColumns <= 0, () => mostColumns);

            this.TileMapViewport.Pan(
                deltaRows.Clamp(-this.TileMapViewport.Row, mostRows - this.TileMapViewport.Row - this.TileMapViewport.NumRows),
                deltaColumns.Clamp(-this.TileMapViewport.Column, mostColumns - this.TileMapViewport.Column - this.TileMapViewport.NumColumns));
        }
    }
}