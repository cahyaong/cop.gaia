// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WpfDrawingCanvas.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 30 May 2015 8:31:30 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Data;
    using Brush = nGratis.Cop.Gaia.Engine.Core.Brush;
    using Pen = nGratis.Cop.Gaia.Engine.Core.Pen;

    internal class WpfDrawingCanvas : IDrawingCanvas
    {
        private readonly System.Windows.Media.DrawingContext drawingContext;

        public WpfDrawingCanvas(System.Windows.Media.DrawingContext drawingContext)
        {
            Guard.AgainstNullArgument(() => drawingContext);

            this.drawingContext = drawingContext;
        }

        ~WpfDrawingCanvas()
        {
            this.Dispose(false);
        }

        public void BeginBatch()
        {
            Throw.NotSupportedException("Batching is not available in WPF.");
        }

        public void EndBatch()
        {
            Throw.NotSupportedException("Batching is not available in WPF.");
        }

        public void Clear(IColor color)
        {
        }

        public void DrawRectangle(Pen pen, Brush brush, Rectangle rectangle)
        {
            this.drawingContext.DrawRectangle(
                brush.ToMediaBrush(),
                pen.ToMediaPen(),
                rectangle.ToWindowsRectangle());
        }

        public void DrawLine(Pen pen, Point start, Point end)
        {
            this.drawingContext.DrawLine(
                pen.ToMediaPen(),
                start.ToWindowsPoint(),
                end.ToWindowsPoint());
        }

        public void DrawCircle(Pen pen, Brush brush, Point center, float radius)
        {
            Throw.NotSupportedException("Drawing circle is not available in WPF.");
        }

        public void DrawText(Pen pen, Point position, string text, string font)
        {
            Throw.NotSupportedException("Drawing text is not available in WPF.");
        }

        public TContext GetDrawingContext<TContext>() where TContext : class
        {
            Guard.AgainstInvalidOperation(typeof(TContext) != typeof(System.Windows.Media.DrawingContext));

            return this.drawingContext as TContext;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
        }
    }
}