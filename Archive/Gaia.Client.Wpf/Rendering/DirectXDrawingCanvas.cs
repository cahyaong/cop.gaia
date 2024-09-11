// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectXDrawingCanvas.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 30 July 2015 1:42:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    internal class DirectXDrawingCanvas : IDrawingCanvas
    {
        private readonly GraphicsDevice graphicsDevice;

        public DirectXDrawingCanvas(GraphicsDevice graphicsDevice)
        {
            Guard.AgainstNullArgument(() => graphicsDevice);

            this.graphicsDevice = graphicsDevice;
        }

        ~DirectXDrawingCanvas()
        {
            this.Dispose(false);
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

        public void DrawLine(Pen pen, nGratis.Cop.Gaia.Engine.Data.Point start, nGratis.Cop.Gaia.Engine.Data.Point end)
        {
            Throw.NotSupportedException("Drawing line is not available in DirectX.");
        }

        public void DrawCircle(Pen pen, Brush brush, nGratis.Cop.Gaia.Engine.Data.Point center, float radius)
        {
            Throw.NotSupportedException("Drawing circle is not available in DirectX.");
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