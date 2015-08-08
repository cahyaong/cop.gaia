// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometricPrimitive.cs" company="nGratis">
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
// <creation_timestamp>Monday, 27 July 2015 2:26:46 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine;

    public abstract class GeometricPrimitive : IRenderPrimitive, IDisposable
    {
        private readonly IList<VertexPositionNormal> vertices = new List<VertexPositionNormal>();

        private readonly IList<ushort> indices = new List<ushort>();

        private VertexBuffer vertexBuffer;

        private IndexBuffer indexBuffer;

        private BasicEffect effect;

        private bool isDisposed;

        ~GeometricPrimitive()
        {
            this.Dispose(false);
        }

        public void Initialize(IDrawingCanvas drawingCanvas)
        {
            Guard.AgainstNullArgument(() => drawingCanvas);

            var graphicsDevice = drawingCanvas.GetDrawingContext<GraphicsDevice>();

            this.vertexBuffer = new VertexBuffer(
                graphicsDevice,
                typeof(VertexPositionNormal),
                this.vertices.Count,
                BufferUsage.None);

            this.vertexBuffer.SetData(this.vertices.ToArray());

            this.indexBuffer = new IndexBuffer(
                graphicsDevice,
                typeof(ushort),
                this.indices.Count,
                BufferUsage.None);

            this.indexBuffer.SetData(this.indices.ToArray());

            this.effect = new BasicEffect(graphicsDevice);
            this.effect.EnableDefaultLighting();
        }

        public void Render()
        {
            this.effect.World = Matrix.CreateFromYawPitchRoll(0.5F, 0.5F, 0) * Matrix.CreateTranslation(new Vector3());
            this.effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 2.5F), Vector3.Zero, Vector3.Up);
            this.effect.DiffuseColor = Color.CornflowerBlue.ToVector3();
            this.effect.Alpha = 1.0F;

            var graphicsDevice = this.effect.GraphicsDevice;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.SetVertexBuffer(this.vertexBuffer);
            graphicsDevice.Indices = this.indexBuffer;

            this.effect.Projection = Matrix.CreatePerspectiveFieldOfView(1, graphicsDevice.Viewport.AspectRatio, 1, 10);

            var numTriangles = this.indices.Count / 3;

            foreach (var pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    0,
                    0,
                    this.vertices.Count,
                    0,
                    numTriangles);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void AddVertex(Vector3 position, Vector3 normal)
        {
            this.vertices.Add(new VertexPositionNormal(position, normal));
        }

        protected void AddIndex(int index)
        {
            Guard.AgainstInvalidArgument(index > ushort.MaxValue, () => index);

            this.indices.Add((ushort)index);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                if (this.vertexBuffer != null)
                {
                    this.vertexBuffer.Dispose();
                    this.vertexBuffer = null;
                }

                if (this.indexBuffer != null)
                {
                    this.indexBuffer.Dispose();
                    this.indexBuffer = null;
                }

                if (this.effect != null)
                {
                    this.effect.Dispose();
                    this.effect = null;
                }
            }

            this.isDisposed = true;
        }
    }
}