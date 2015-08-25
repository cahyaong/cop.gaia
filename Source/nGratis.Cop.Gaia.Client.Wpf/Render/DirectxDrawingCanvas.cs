// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectxDrawingCanvas.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 30 July 2015 1:42:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    internal class DirectxDrawingCanvas : IDrawingCanvas
    {
        private readonly GraphicsDevice graphicsDevice;

        public DirectxDrawingCanvas(GraphicsDevice graphicsDevice)
        {
            Guard.AgainstNullArgument(() => graphicsDevice);

            this.graphicsDevice = graphicsDevice;
        }

        public void BeginBatch()
        {
            Throw.NotSupportedException("Batching is not available in DirectX.");
        }

        public void EndBatch()
        {
            Throw.NotSupportedException("Batching is not available in DirectX.");
        }

        public void Clear(IColor color)
        {
            var rgbColor = (RgbColor)color;

            this.graphicsDevice.Clear(new Color(rgbColor.Red, rgbColor.Green, rgbColor.Blue));
        }

        public void DrawRectangle(Pen pen, Brush brush, nGratis.Cop.Gaia.Engine.Data.Rectangle rectangle)
        {
            Throw.NotSupportedException("Drawing rectangle is not available in DirectX.");
        }

        public void DrawLine(Pen pen, nGratis.Cop.Gaia.Engine.Data.Point startPoint, nGratis.Cop.Gaia.Engine.Data.Point endPoint)
        {
            Throw.NotSupportedException("Drawing line is not available in DirectX.");
        }

        public void DrawText(Pen pen, nGratis.Cop.Gaia.Engine.Data.Point position, string text, string font)
        {
            Throw.NotSupportedException("Drawing text is not available in DirectX.");
        }

        public TContext GetDrawingContext<TContext>() where TContext : class
        {
            Guard.AgainstInvalidOperation(typeof(TContext) != typeof(GraphicsDevice));

            return this.graphicsDevice as TContext;
        }
    }
}