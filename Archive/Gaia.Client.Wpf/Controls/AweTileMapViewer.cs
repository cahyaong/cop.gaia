﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AweTileMapViewer.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 28 May 2015 11:33:49 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reactive.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using nGratis.Cop.Gaia.Engine;
    using ReactiveUI;

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
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnTileMapRendererChanged));

        public static readonly DependencyProperty SelectedTileProperty = DependencyProperty.Register(
            "SelectedTile",
            typeof(Tile),
            typeof(AweTileMapViewer),
            new PropertyMetadata(null));

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy",
            typeof(bool),
            typeof(AweTileMapViewer),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty DiagnosticBucketProperty = DependencyProperty.Register(
            "DiagnosticBucket",
            typeof(DiagnosticBucket),
            typeof(AweTileMapViewer),
            new PropertyMetadata(null));

        private readonly IList<IDisposable> subscriptions = new List<IDisposable>();

        private System.Windows.Point? draggedPoint;

        public AweTileMapViewer()
        {
            this.Focusable = true;
            this.FocusVisualStyle = null;

            this.HookEventHandlers();
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

        public Tile SelectedTile
        {
            get { return (Tile)this.GetValue(SelectedTileProperty); }
            private set { this.SetValue(SelectedTileProperty, value); }
        }

        public bool IsBusy
        {
            get { return (bool)this.GetValue(IsBusyProperty); }
            set { this.SetValue(IsBusyProperty, value); }
        }

        public DiagnosticBucket DiagnosticBucket
        {
            get { return (DiagnosticBucket)this.GetValue(DiagnosticBucketProperty); }
            set { this.SetValue(DiagnosticBucketProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Focusable = true;
            this.FocusVisualStyle = null;
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
            this.AdjustSelectedTile();

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

            var stopwatch = Stopwatch.StartNew();

            var tileMap = this.TileMap;
            var isBusy = this.IsBusy;

            var canvas = new WpfDrawingCanvas(drawingContext);

            drawingContext.DrawRectangle(
                new SolidColorBrush(Colors.Transparent),
                null,
                new Rect(0.0, 0.0, renderer.ViewportSize.Width, renderer.ViewportSize.Height));

            if (!isBusy && tileMap != null)
            {
                renderer.RenderLayer(canvas, tileMap);

                var selectedTile = this.SelectedTile;

                if (selectedTile != null)
                {
                    renderer.RenderTileSelection(canvas, selectedTile);
                }
            }

            renderer.RenderGridBorder(canvas);
            stopwatch.Stop();

            var diagnosticBucket = this.DiagnosticBucket;

            if (diagnosticBucket != null)
            {
                diagnosticBucket.AddOrUpdateItem(DiagnosticKey.RenderTime, stopwatch.ElapsedMilliseconds);
            }
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

        private static void OnTileMapRendererChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var viewer = (AweTileMapViewer)dependencyObject;

            if (viewer == null)
            {
                return;
            }

            viewer.UnhookEventHandlers();
            viewer.HookEventHandlers();
        }

        private void HookEventHandlers()
        {
            var subscription = default(IDisposable);

            subscription = Observable
                .FromEventPattern<MouseButtonEventArgs>(this, "MouseDown")
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(pattern => this.OnMouseDown(pattern.Sender, pattern.EventArgs));

            this.subscriptions.Add(subscription);

            subscription = Observable
                .FromEventPattern<MouseButtonEventArgs>(this, "MouseUp")
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(pattern => this.OnMouseUp(pattern.Sender, pattern.EventArgs));

            this.subscriptions.Add(subscription);

            subscription = Observable
                .FromEventPattern<MouseEventArgs>(this, "MouseMove")
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(pattern => this.OnMouseMove(pattern.Sender, pattern.EventArgs));

            this.subscriptions.Add(subscription);

            subscription = Observable
                .FromEventPattern<KeyEventArgs>(this, "KeyDown")
                .Where(pattern => pattern.EventArgs.Key.IsPanning())
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(pattern => this.PanDisplay(pattern.EventArgs.Key));

            this.subscriptions.Add(subscription);

            var renderer = this.TileMapRenderer;

            if (renderer != null)
            {
                subscription = renderer
                    .TileMapViewport
                    .WhenCoordinateUpdated
                    .Distinct()
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(pattern => this.InvalidateVisual());

                this.subscriptions.Add(subscription);
            }
        }

        private void UnhookEventHandlers()
        {
            this
                .subscriptions
                .ForEach(subscription => subscription.Dispose());

            this.subscriptions.Clear();
        }

        private void PanDisplay(Key key)
        {
            var renderer = this.TileMapRenderer;

            if (renderer == null)
            {
                return;
            }

            var tileMap = this.TileMap;

            if (tileMap == null)
            {
                return;
            }

            var deltaRows = 0;
            var deltaColumns = 0;

            switch (key)
            {
                case Key.A:
                    deltaColumns = -1;
                    break;

                case Key.D:
                    deltaColumns = 1;
                    break;

                case Key.W:
                    deltaRows = -1;
                    break;

                case Key.S:
                    deltaRows = 1;
                    break;
            }

            renderer.PanCamera(4 * deltaRows, 4 * deltaColumns, tileMap.NumRows, tileMap.NumColumns);

            this.AdjustSelectedTile();
        }

        private void PanDisplay(System.Windows.Point oldPoint, System.Windows.Point newPoint)
        {
            var renderer = this.TileMapRenderer;

            if (renderer == null)
            {
                return;
            }

            var tileMap = this.TileMap;

            if (tileMap == null)
            {
                return;
            }

            var deltaRows = (int)((oldPoint.Y / renderer.TileSize.Height) - (newPoint.Y / renderer.TileSize.Height));
            var deltaColumns = (int)((oldPoint.X / renderer.TileSize.Width) - (newPoint.X / renderer.TileSize.Width));

            renderer.PanCamera(deltaRows, deltaColumns, tileMap.NumRows, tileMap.NumColumns);

            this.AdjustSelectedTile();
        }

        private void PickTile(System.Windows.Point point)
        {
            var renderer = this.TileMapRenderer;

            if (renderer == null)
            {
                return;
            }

            var tileSize = renderer.TileSize;
            var tileMap = this.TileMap;

            if (tileMap == null)
            {
                return;
            }

            var row = Math.Min((int)(point.Y / tileSize.Height), renderer.TileMapViewport.NumRows - 1);
            var column = Math.Min((int)(point.X / tileSize.Width), renderer.TileMapViewport.NumColumns - 1);
            var selectedTile = tileMap.GetTile(renderer.TileMapViewport.Column + column, renderer.TileMapViewport.Row + row);

            this.SelectedTile = selectedTile;
            this.AdjustSelectedTile();
            this.InvalidateVisual();
        }

        private void AdjustSelectedTile()
        {
            var renderer = this.TileMapRenderer;

            if (renderer == null)
            {
                return;
            }

            if (!renderer.TileMapViewport.IsTileVisible(this.SelectedTile))
            {
                this.SelectedTile = null;
            }

            var diagnosticBucket = this.DiagnosticBucket;

            if (diagnosticBucket != null)
            {
                var selectedTile = this.SelectedTile;

                diagnosticBucket.AddOrUpdateItem(DiagnosticKey.SelectedCoordinate, selectedTile != null
                    ? selectedTile.Coordinate
                    : new nGratis.Cop.Gaia.Engine.Data.Coordinate?());
            }
        }

        private void ResetViewport()
        {
            this.draggedPoint = null;

            var renderer = this.TileMapRenderer;

            if (renderer != null)
            {
                renderer.TileMapViewport.Reset();
            }

            this.SelectedTile = null;
            this.AdjustSelectedTile();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs args)
        {
            if (args.ChangedButton == MouseButton.Left || args.ChangedButton == MouseButton.Right)
            {
                this.Focus();
                args.Handled = true;
            }

            if (args.ChangedButton == MouseButton.Right)
            {
                this.draggedPoint = args.GetPosition(this);
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs args)
        {
            if (args.ChangedButton == MouseButton.Left && args.ClickCount == 1)
            {
                this.PickTile(args.GetPosition(this));
                args.Handled = true;
            }
            else if (args.ChangedButton == MouseButton.Right)
            {
                this.draggedPoint = null;
                args.Handled = true;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (args.LeftButton == MouseButtonState.Pressed)
            {
                this.PickTile(args.GetPosition(this));
                args.Handled = true;
            }
            else if (args.RightButton == MouseButtonState.Pressed)
            {
                var selectedPoint = args.GetPosition(this);

                if (this.draggedPoint.HasValue)
                {
                    this.PanDisplay(this.draggedPoint.Value, selectedPoint);

                    var renderer = this.TileMapRenderer;
                    var tileSize = renderer.TileSize;

                    var isPositionUpdated =
                        Math.Abs(selectedPoint.X - this.draggedPoint.Value.X) >= tileSize.Width ||
                        Math.Abs(selectedPoint.Y - this.draggedPoint.Value.Y) >= tileSize.Height;

                    if (isPositionUpdated)
                    {
                        this.draggedPoint = selectedPoint;
                    }
                }

                args.Handled = true;
            }
        }
    }
}