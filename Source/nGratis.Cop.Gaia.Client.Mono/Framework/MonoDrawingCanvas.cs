// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MonoDrawingCanvas.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 11 August 2015 12:40:24 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    internal class MonoDrawingCanvas : IDrawingCanvas
    {
        private readonly GraphicsDevice graphicsDevice;

        private readonly IFontManager fontManager;

        private readonly SpriteBatch spriteBatch;

        private readonly Texture2D pixelTexture;

        public MonoDrawingCanvas(GraphicsDevice graphicsDevice, IFontManager fontManager)
        {
            RapidGuard.AgainstNullArgument(graphicsDevice);
            RapidGuard.AgainstNullArgument(fontManager);

            this.graphicsDevice = graphicsDevice;
            this.fontManager = fontManager;

            this.spriteBatch = new SpriteBatch(graphicsDevice);
            this.pixelTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            this.pixelTexture.SetData(new[] { Color.Gray });
        }

        public void BeginBatch()
        {
            this.spriteBatch.Begin();
        }

        public void EndBatch()
        {
            this.spriteBatch.End();
        }

        public void Clear(IColor color)
        {
            this.graphicsDevice.Clear(color.ToXnaColor());
        }

        public void DrawRectangle(Pen pen, Brush brush, nGratis.Cop.Gaia.Engine.Data.Rectangle rectangle)
        {
            Throw.NotSupportedException("Drawing rectangle is not supported in Mono.");
        }

        public void DrawLine(Pen pen, nGratis.Cop.Gaia.Engine.Data.Point startPoint, nGratis.Cop.Gaia.Engine.Data.Point endPoint)
        {
            var distance = Vector2.Distance(startPoint.ToXnaVector2(), endPoint.ToXnaVector2());
            var angle = (float)Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);

            this.spriteBatch.Draw(
                this.pixelTexture,
                startPoint.ToXnaVector2(),
                null,
                pen.Color.ToXnaColor(),
                angle,
                Vector2.Zero,
                new Vector2(distance, pen.Thickness),
                SpriteEffects.None,
                0);
        }

        public void DrawText(Pen pen, nGratis.Cop.Gaia.Engine.Data.Point position, string text, string font)
        {
            this.spriteBatch.DrawString(
                this.fontManager.GetSpriteFont(font),
                text,
                position.ToXnaVector2(),
                pen.Color.ToXnaColor());
        }

        public TContext GetDrawingContext<TContext>() where TContext : class
        {
            Throw.NotSupportedException("Getting drawing context is not supported in Mono.");

            return default(TContext);
        }
    }
}