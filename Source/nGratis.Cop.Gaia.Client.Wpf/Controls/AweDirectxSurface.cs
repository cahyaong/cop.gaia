// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AweDirectxSurface.cs" company="nGratis">
//   The MIT License (MIT)
// 
//   Copyright (c) 2014 - 2015 Cahya Ong
// 
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
//   associated documentation files (the "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
//   following conditions:
// 
//   The above copyright notice and this permission notice shall be included in all copies or substantial
//   portions of the Software.
// 
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
//   LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO
//   EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR
//   THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 13 July 2015 11:56:23 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;
    using System.Windows.Media;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Client.Wpf.Framework;
    using nGratis.Cop.Gaia.Engine;
    using SharpDX.Direct3D9;

    public class AweDirectxSurface : ContentControl, IDisposable
    {
        public static readonly DependencyProperty RenderManagerProperty = DependencyProperty.Register(
            "RenderManager",
            typeof(IRenderManager),
            typeof(AweDirectxSurface),
            new PropertyMetadata(null));

        private readonly D3DImage directxImage;

        private SharpDX.Direct3D9.Texture sharpdxTexture;

        private GraphicsDeviceManager graphicsDeviceManager;

        private RenderTarget2D renderTarget;

        private IDrawingCanvas drawingCanvas;

        private bool shouldRedraw;

        private bool isDisposed;

        public AweDirectxSurface()
        {
            this.directxImage = new D3DImage();
            this.directxImage.IsFrontBufferAvailableChanged += this.OnFrontBufferAvailableChanged;

            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        ~AweDirectxSurface()
        {
            this.Dispose(false);
        }

        public IRenderManager RenderManager
        {
            get
            {
                return (IRenderManager)this.GetValue(RenderManagerProperty);
            }

            set
            {
                this.RenderManager.SetDrawingCanvas(null);

                value.SetDrawingCanvas(this.drawingCanvas);
                this.SetValue(RenderManagerProperty, value);

                this.InvalidateVisual();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                if (this.sharpdxTexture != null)
                {
                    this.sharpdxTexture.Dispose();
                    this.sharpdxTexture = null;
                }

                if (this.renderTarget != null)
                {
                    this.renderTarget.Dispose();
                    this.renderTarget = null;
                }

                if (this.graphicsDeviceManager != null)
                {
                    this.graphicsDeviceManager.Dispose();
                    this.graphicsDeviceManager = null;
                }

                this.directxImage.IsFrontBufferAvailableChanged -= this.OnFrontBufferAvailableChanged;
                this.Unloaded -= this.OnUnloaded;
                this.Loaded -= this.OnLoaded;
            }

            this.isDisposed = true;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            this.DestroyBackBuffer();
            this.shouldRedraw = true;

            base.OnRenderSizeChanged(sizeInfo);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Content = new Image { Source = this.directxImage, Stretch = Stretch.None };

            if (this.graphicsDeviceManager != null)
            {
                return;
            }

            this.graphicsDeviceManager = new GraphicsDeviceManager();
            this.graphicsDeviceManager.DeviceResetting += this.OnGraphicsDeviceDeviceResetting;
            this.graphicsDeviceManager.CreateDevice();

            this.EstablishBackBuffer();
            CompositionTarget.Rendering += this.OnCompositionTargetRendering;

            this.shouldRedraw = true;
        }

        private void OnUnloaded(object sender, RoutedEventArgs args)
        {
            if (this.graphicsDeviceManager != null)
            {
                this.DestroyBackBuffer();

                CompositionTarget.Rendering -= this.OnCompositionTargetRendering;

                this.graphicsDeviceManager.DeviceResetting -= this.OnGraphicsDeviceDeviceResetting;
                this.graphicsDeviceManager.Dispose();
                this.graphicsDeviceManager = null;
            }

            this.Content = null;
        }

        private void OnGraphicsDeviceDeviceResetting(object sender, EventArgs args)
        {
            this.DestroyBackBuffer();
            this.shouldRedraw = true;
        }

        private void EstablishBackBuffer()
        {
            if (this.renderTarget != null)
            {
                return;
            }

            this.renderTarget = new RenderTarget2D(
                this.graphicsDeviceManager.GraphicsDevice,
                (int)ActualWidth,
                (int)ActualHeight,
                false,
                SurfaceFormat.Bgra32,
                DepthFormat.Depth24Stencil8,
                1,
                RenderTargetUsage.PlatformContents,
                true);

            var handle = this.renderTarget.GetSharedHandle();
            Guard.AgainstInvalidOperation(handle == IntPtr.Zero);

            this.sharpdxTexture = new SharpDX.Direct3D9.Texture(
                this.graphicsDeviceManager.SharpdxDevice,
                this.renderTarget.Width,
                this.renderTarget.Height,
                1,
                Usage.RenderTarget,
                Format.A8R8G8B8,
                Pool.Default,
                ref handle);

            using (var surface = this.sharpdxTexture.GetSurfaceLevel(0))
            {
                this.directxImage.Lock();
                this.directxImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.NativePointer);
                this.directxImage.Unlock();
            }

            this.drawingCanvas = new DirectxDrawingCanvas(this.graphicsDeviceManager.GraphicsDevice);

            var renderManager = this.RenderManager;

            if (renderManager != null)
            {
                renderManager.SetDrawingCanvas(this.drawingCanvas);
            }
        }

        private void DestroyBackBuffer()
        {
            var renderManager = this.RenderManager;

            if (renderManager != null)
            {
                renderManager.SetDrawingCanvas(null);
            }

            if (this.renderTarget != null)
            {
                this.renderTarget.Dispose();
                this.renderTarget = null;
            }

            if (this.sharpdxTexture != null)
            {
                this.sharpdxTexture.Dispose();
                this.sharpdxTexture = null;
            }

            this.directxImage.Lock();
            this.directxImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
            this.directxImage.Unlock();
        }

        private void OnFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (this.directxImage.IsFrontBufferAvailable)
            {
                this.shouldRedraw = true;
            }
        }

        private void OnCompositionTargetRendering(object sender, EventArgs args)
        {
            if (!this.shouldRedraw || !this.CanDraw())
            {
                return;
            }

            this.shouldRedraw = false;

            this.EstablishBackBuffer();

            var actualWidth = (int)this.ActualWidth;
            var actualHeight = (int)this.ActualHeight;

            this.directxImage.Lock();

            this.graphicsDeviceManager.GraphicsDevice.SetRenderTarget(this.renderTarget);
            this.graphicsDeviceManager.GraphicsDevice.Viewport = new Viewport(
                0,
                0,
                Math.Max(1, actualWidth),
                Math.Max(1, actualHeight));

            this.RenderManager.Render();

            this.graphicsDeviceManager.GraphicsDevice.Flush();

            this.directxImage.AddDirtyRect(new Int32Rect(0, 0, actualWidth, actualHeight));
            this.directxImage.Unlock();

            this.graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
        }

        private bool CanDraw()
        {
            if (this.graphicsDeviceManager == null)
            {
                return false;
            }

            var isDeviceReady = true;

            switch (this.graphicsDeviceManager.GraphicsDevice.GraphicsDeviceStatus)
            {
                case GraphicsDeviceStatus.Lost:
                    {
                        isDeviceReady = false;
                        break;
                    }

                case GraphicsDeviceStatus.NotReset:
                    {
                        this.graphicsDeviceManager.ResetDevice((int)ActualWidth, (int)ActualHeight);
                        isDeviceReady = false;
                        break;
                    }
            }

            return this.directxImage.IsFrontBufferAvailable && isDeviceReady;
        }
    }
}