// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="AweTileMapViewer.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 28 May 2015 11:33:49 AM</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using nGratis.Cop.Gaia.Engine;

    public class AweTileMapViewer : Canvas
    {
        public static readonly DependencyProperty TileMapProperty = DependencyProperty.Register(
            "TileMap",
            typeof(TileMap),
            typeof(AweTileMapViewer),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnTileMapChanged));

        public static readonly DependencyProperty TileMapRendererProperty = DependencyProperty.Register(
            "TileMapRenderer",
            typeof(ITileMapRenderer),
            typeof(AweTileMapViewer),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy",
            typeof(bool),
            typeof(AweTileMapViewer),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        private Point? selectedPoint;

        private Point? draggedPoint;

        public AweTileMapViewer()
        {
            this.Focusable = true;

            WeakEventManager<AweTileMapViewer, MouseButtonEventArgs>.AddHandler(this, "MouseDown", this.OnMouseDown);
            WeakEventManager<AweTileMapViewer, MouseButtonEventArgs>.AddHandler(this, "MouseUp", this.OnMouseUp);
            WeakEventManager<AweTileMapViewer, MouseEventArgs>.AddHandler(this, "MouseMove", this.OnMouseMove);
        }

        public TileMap TileMap
        {
            get { return (TileMap)this.GetValue(TileMapProperty); }
            set { this.SetValue(TileMapProperty, value); }
        }

        public ITileMapRenderer TileMapRenderer
        {
            get { return (ITileMapRenderer)this.GetValue(TileMapRendererProperty); }
            set { this.SetValue(TileMapRendererProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool)this.GetValue(IsBusyProperty); }
            set { this.SetValue(IsBusyProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var renderer = this.TileMapRenderer;

            if (renderer == null)
            {
                return Size.Empty;
            }

            renderer.MeasureViewport(availableSize);

            return renderer.DesiredViewportSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var renderer = this.TileMapRenderer;

            if (renderer == null)
            {
                return this.DesiredSize;
            }

            renderer.ArrangeViewport(finalSize);

            // Add extra one tile length to avoid jitter when resizing.
            return new Size(
                renderer.ViewportSize.Width + renderer.TileSize.Width,
                renderer.ViewportSize.Height + renderer.TileSize.Height);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            var renderer = this.TileMapRenderer;

            if (renderer == null)
            {
                return;
            }

            var tileMap = this.TileMap;
            var isBusy = this.IsBusy;

            var tileSize = renderer.TileSize;
            var canvas = new WpfDrawingCanvas(drawingContext);

            drawingContext.DrawRectangle(
                new SolidColorBrush(Colors.Transparent),
                null,
                new Rect(0.0, 0.0, renderer.ViewportSize.Width, renderer.ViewportSize.Height));

            if (!isBusy)
            {
                if (tileMap != null)
                {
                    renderer.RenderLayer(canvas, tileMap);
                }

                renderer.RenderGridLines(canvas);

                var viewportBoundary = new Rect(
                    new Point(0.0, 0.0),
                    new Size(renderer.ViewportSize.Width, renderer.ViewportSize.Height));

                if (this.selectedPoint.HasValue && viewportBoundary.Contains(this.selectedPoint.Value))
                {
                    var row = Math.Min((int)(this.selectedPoint.Value.Y / tileSize.Height), renderer.TileMapViewport.NumRows - 1);
                    var column = Math.Min((int)(this.selectedPoint.Value.X / tileSize.Width), renderer.TileMapViewport.NumColumns - 1);

                    renderer.RenderTileSelection(canvas, row, column);
                }
            }

            renderer.RenderGridBorder(canvas);
        }

        private static void OnTileMapChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var viewer = (AweTileMapViewer)dependencyObject;

            if (viewer == null)
            {
                return;
            }

            viewer.ResetViewport();
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs args)
        {
            if (args.ChangedButton == MouseButton.Left && args.ClickCount == 1)
            {
                this.selectedPoint = args.GetPosition(this);
                args.Handled = true;

                this.InvalidateVisual();
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs args)
        {
            this.Focus();
        }

        private void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                this.selectedPoint = args.GetPosition(this);
                args.Handled = true;

                this.InvalidateVisual();
            }
            else if (args.RightButton == MouseButtonState.Pressed)
            {
                var currentPoint = args.GetPosition(this);

                if (this.draggedPoint.HasValue)
                {
                    var tileMap = this.TileMap;
                    var renderer = this.TileMapRenderer;

                    var column =
                        renderer.TileMapViewport.Column +
                        ((int)(this.draggedPoint.Value.X / renderer.TileSize.Width) - (int)(currentPoint.X / renderer.TileSize.Width));

                    var row =
                        renderer.TileMapViewport.Row +
                        ((int)(this.draggedPoint.Value.Y / renderer.TileSize.Height) - (int)(currentPoint.Y / renderer.TileSize.Height));

                    renderer.TileMapViewport.Column = column.Clamp(0, tileMap.NumColumns - renderer.TileMapViewport.NumColumns - 1);
                    renderer.TileMapViewport.Row = row.Clamp(0, tileMap.NumRows - renderer.TileMapViewport.NumRows - 1);

                    this.InvalidateVisual();
                }

                this.draggedPoint = currentPoint;
                args.Handled = true;
            }
            else if (args.RightButton == MouseButtonState.Released)
            {
                this.draggedPoint = null;
                args.Handled = true;
            }
        }

        private void ResetViewport()
        {
            this.selectedPoint = null;
            this.draggedPoint = null;

            var renderer = this.TileMapRenderer;

            if (renderer != null)
            {
                renderer.TileMapViewport.Row = 0;
                renderer.TileMapViewport.Column = 0;
            }

            this.InvalidateVisual();
        }
    }
}