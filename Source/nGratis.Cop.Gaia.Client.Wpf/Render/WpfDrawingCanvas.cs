// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WpfDrawingCanvas.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 30 May 2015 8:31:30 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
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

        public void DrawLine(Pen pen, Point startPoint, Point endPoint)
        {
            this.drawingContext.DrawLine(
                pen.ToMediaPen(),
                startPoint.ToWindowsPoint(),
                endPoint.ToWindowsPoint());
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
    }
}