// ------------------------------------------------------------------------------------------------------------------------------------------------------------
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
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Core.Contract;

    public abstract class GeometricPrimitive : IDisposable
    {
        private readonly IList<VertexPositionNormal> vertices = new List<VertexPositionNormal>();

        private readonly IList<ushort> indices = new List<ushort>();

        private VertexBuffer vertexBuffer;

        private IndexBuffer indexBuffer;

        private BasicEffect defaultEffect;

        private bool isDisposed;

        ~GeometricPrimitive()
        {
            this.Dispose(false);
        }

        public void Initialize(GraphicsDevice graphicsDevice)
        {
            Guard.AgainstNullArgument(() => graphicsDevice);

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

            this.defaultEffect = new BasicEffect(graphicsDevice);
            this.defaultEffect.EnableDefaultLighting();
        }

        public void Draw(Effect effect)
        {
            Guard.AgainstNullArgument(() => effect);

            var graphicsDevice = effect.GraphicsDevice;
            graphicsDevice.SetVertexBuffer(this.vertexBuffer);
            graphicsDevice.Indices = this.indexBuffer;

            var numTriangles = this.indices.Count / 3;

            foreach (var pass in effect.CurrentTechnique.Passes)
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

        public void Draw(Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, Color diffuseColor)
        {
            this.defaultEffect.World = worldMatrix;
            this.defaultEffect.View = viewMatrix;
            this.defaultEffect.Projection = projectionMatrix;
            this.defaultEffect.DiffuseColor = diffuseColor.ToVector3();
            this.defaultEffect.Alpha = diffuseColor.A / 255.0F;

            var graphicsDevice = this.defaultEffect.GraphicsDevice;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphicsDevice.BlendState = diffuseColor.A < 255.0F ? BlendState.AlphaBlend : BlendState.Opaque;

            this.Draw(this.defaultEffect);
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

                if (this.defaultEffect != null)
                {
                    this.defaultEffect.Dispose();
                    this.defaultEffect = null;
                }
            }

            this.isDisposed = true;
        }
    }
}