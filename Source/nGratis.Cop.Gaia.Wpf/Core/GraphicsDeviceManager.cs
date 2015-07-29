// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphicsDeviceManager.cs" company="nGratis">
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
// <creation_timestamp>Monday, 13 July 2015 11:56:05 AM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows;
    using System.Windows.Interop;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using nGratis.Cop.Core.Contract;
    using SharpDX.Direct3D9;

    // TODO: Consider making this class as a singleton to improve performance?

    [Export(typeof(IGraphicsDeviceManager))]
    public sealed class GraphicsDeviceManager : IGraphicsDeviceManager, IGraphicsDeviceService, IDisposable
    {
        private PresentationParameters graphicsParameters;

        private bool isDisposed;

        ~GraphicsDeviceManager()
        {
            this.Dispose(false);
        }

        public event EventHandler<EventArgs> DeviceCreated;

        public event EventHandler<EventArgs> DeviceDisposing;

        public event EventHandler<EventArgs> DeviceReset;

        public event EventHandler<EventArgs> DeviceResetting;

        public Direct3DEx SharpdxDirect3D { get; private set; }

        public DeviceEx SharpdxDevice { get; private set; }

        public GraphicsDevice GraphicsDevice { get; private set; }

        public void CreateDevice()
        {
            if (this.SharpdxDevice == null)
            {
                this.SharpdxDirect3D = new Direct3DEx();

                var sharpdxParameters = new PresentParameters
                    {
                        Windowed = true,
                        SwapEffect = SwapEffect.Discard,
                        DeviceWindowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle,
                        PresentationInterval = SharpDX.Direct3D9.PresentInterval.Default
                    };

                this.SharpdxDevice = new DeviceEx(
                    this.SharpdxDirect3D,
                    0,
                    DeviceType.Hardware,
                    IntPtr.Zero,
                    CreateFlags.HardwareVertexProcessing | CreateFlags.Multithreaded | CreateFlags.FpuPreserve,
                    sharpdxParameters);
            }

            if (this.GraphicsDevice == null)
            {
                this.graphicsParameters = new PresentationParameters
                {
                    BackBufferWidth = 1,
                    BackBufferHeight = 1,
                    BackBufferFormat = SurfaceFormat.Color,
                    DepthStencilFormat = DepthFormat.Depth24,
                    DeviceWindowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle,
                    PresentationInterval = Microsoft.Xna.Framework.Graphics.PresentInterval.Immediate,
                    IsFullScreen = false
                };

                this.GraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, this.graphicsParameters);

                this.DeviceCreated.FireEvent(this);
            }
        }

        public bool BeginDraw()
        {
            Throw.NotSupportedException("Drawing operation is not available.");
            return false;
        }

        public void EndDraw()
        {
            Throw.NotSupportedException("Drawing operation is not available.");
        }

        public void ResetDevice(int width, int height)
        {
            if (width <= this.graphicsParameters.BackBufferWidth && height <= this.graphicsParameters.BackBufferHeight)
            {
                return;
            }

            this.DeviceResetting.FireEvent(this);

            this.graphicsParameters.BackBufferWidth = width;
            this.graphicsParameters.BackBufferHeight = height;

            this.DeviceReset.FireEvent(this);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                if (this.SharpdxDevice != null)
                {
                    this.SharpdxDevice.Dispose();
                    this.SharpdxDevice = null;
                }

                if (this.SharpdxDirect3D != null)
                {
                    this.SharpdxDirect3D.Dispose();
                    this.SharpdxDirect3D = null;
                }

                if (this.GraphicsDevice != null)
                {
                    this.DeviceDisposing.FireEvent(this);
                    this.GraphicsDevice.Dispose();
                    this.GraphicsDevice = null;
                }
            }

            this.isDisposed = true;
        }
    }
}