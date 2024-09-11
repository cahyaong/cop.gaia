// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SdkRenderingManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 29 July 2015 12:54:01 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf.Sdk
{
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Gaia.Client.Wpf.Framework;
    using nGratis.Cop.Gaia.Engine;

    internal class SdkRenderingManager : IRenderingManager
    {
        private readonly CubePrimitive cube;

        private GraphicsDevice graphicsDevice;

        public SdkRenderingManager()
        {
            this.cube = new CubePrimitive();
        }

        public void Render()
        {
            if (this.graphicsDevice == null)
            {
                return;
            }

            this.graphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);

            this.cube.Render();
        }

        public void SetDrawingCanvas(IDrawingCanvas drawingCanvas)
        {
            if (drawingCanvas == null)
            {
                this.graphicsDevice = null;
                return;
            }

            this.graphicsDevice = drawingCanvas.GetDrawingContext<GraphicsDevice>();

            this.cube.Initialize(drawingCanvas);
        }
    }
}