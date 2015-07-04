// ------------------------------------------------------------------------------------------------------------------------------------------------------------
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
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    public class TileMapRenderer : ITileMapRenderer
    {
        private readonly Pen gridBorderPen;

        private readonly Pen gridLinePen;

        private readonly Pen cellSelectionPen;

        private readonly Brush cellSelectionBrush;

        public TileMapRenderer(ITileMapViewport tileMapViewport, Size tileSize, Color accentColor)
        {
            Guard.AgainstNullArgument(() => tileMapViewport);
            Guard.AgainstInvalidArgument(tileSize.Width <= 0 || tileSize.Height <= 0, () => tileSize);

            this.gridBorderPen = new Pen(new SolidColorBrush(accentColor), 1.0);
            this.gridLinePen = new Pen(new SolidColorBrush(accentColor), 0.1);
            this.cellSelectionPen = new Pen(new SolidColorBrush(accentColor), 1.0);
            this.cellSelectionBrush = new SolidColorBrush(accentColor) { Opacity = 0.25 };

            this.gridBorderPen.Freeze();
            this.gridLinePen.Freeze();
            this.cellSelectionPen.Freeze();
            this.cellSelectionBrush.Freeze();

            this.TileSize = tileSize;
            this.TileMapViewport = tileMapViewport;
            this.DesiredViewportSize = new Size(this.TileMapViewport.NumColumns * this.TileSize.Width, this.TileMapViewport.NumRows * this.TileSize.Height);
            this.ViewportSize = Size.Empty;
        }

        public Size TileSize { get; private set; }

        public Size DesiredViewportSize { get; private set; }

        public Size ViewportSize { get; private set; }

        public ITileMapViewport TileMapViewport { get; private set; }

        public void RenderGridBorder(ICanvas canvas)
        {
            Guard.AgainstNullArgument(() => canvas);

            canvas.DrawRectangle(this.gridBorderPen, null, new Rect(0.0, 0.0, this.ViewportSize.Width, this.ViewportSize.Height));
        }

        public virtual void RenderGridLines(ICanvas canvas)
        {
            Guard.AgainstNullArgument(() => canvas);

            if (this.TileMapViewport.NumRows <= 0 || this.TileMapViewport.NumColumns <= 0)
            {
                return;
            }

            var startPoint = new Point();
            var endPoint = new Point();

            for (var row = 1; row < this.TileMapViewport.NumRows; row++)
            {
                startPoint.X = 0.0;
                startPoint.Y = row * this.TileSize.Height;
                endPoint.X = this.ViewportSize.Width;
                endPoint.Y = row * this.TileSize.Height;

                canvas.DrawLine(this.gridLinePen, startPoint, endPoint);
            }

            for (var column = 1; column < this.TileMapViewport.NumColumns; column++)
            {
                startPoint.X = column * this.TileSize.Width;
                startPoint.Y = 0.0;
                endPoint.X = column * this.TileSize.Width;
                endPoint.Y = this.ViewportSize.Height;

                canvas.DrawLine(this.gridLinePen, startPoint, endPoint);
            }
        }

        public virtual void RenderLayer(ICanvas canvas, TileMap tileMap)
        {
        }

        public virtual void RenderTileSelection(ICanvas canvas, uint row, uint column)
        {
            Guard.AgainstNullArgument(() => canvas);

            canvas.DrawRectangle(
                this.cellSelectionPen,
                this.cellSelectionBrush,
                new Rect(column * this.TileSize.Width, row * this.TileSize.Height, this.TileSize.Width, this.TileSize.Height));
        }

        public void MeasureViewport(Size availableSize)
        {
            this.DesiredViewportSize = new Size(
                (int)(availableSize.Width / this.TileSize.Width).Clamp(0, this.TileMapViewport.MaxNumRows) * this.TileSize.Width,
                (int)(availableSize.Height / this.TileSize.Height).Clamp(0, this.TileMapViewport.MaxNumColumns) * this.TileSize.Height);
        }

        public void ArrangeViewport(Size finalSize)
        {
            this.TileMapViewport.Resize((uint)(finalSize.Height / this.TileSize.Height), (uint)(finalSize.Width / this.TileSize.Width));
            this.ViewportSize = new Size(this.TileMapViewport.NumColumns * this.TileSize.Width, this.TileMapViewport.NumRows * this.TileSize.Height);
        }

        public void PanCamera(int deltaRows, int deltaColumns, uint maxNumRows, uint maxNumColumns)
        {
            this.TileMapViewport.Pan(
                (this.TileMapViewport.Row + this.TileMapViewport.NumRows + deltaRows) > maxNumRows ? 0 : deltaRows,
                (this.TileMapViewport.Column + this.TileMapViewport.NumColumns + deltaColumns) > maxNumColumns ? 0 : deltaColumns);
        }
    }
}