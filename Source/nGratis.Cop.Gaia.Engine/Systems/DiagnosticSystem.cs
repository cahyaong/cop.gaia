// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticSystem.cs" company="nGratis">
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
// <creation_timestamp>Wednesday, 12 August 2015 1:52:56 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Diagnostics;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    public class DiagnosticSystem : BaseSystem
    {
        private const string Font = "Fonts/Diagnostics";

        private readonly IDrawingCanvas drawingCanvas;

        private readonly Pen pen;

        private readonly PerformanceCounter globalCpuCounter;

        private readonly PerformanceCounter instanceCpuCounter;

        private TimeSpan elapsedPeriod;

        private uint numFrames;

        private uint fps;

        private float memory;

        private float cpu;

        private int numEntities;

        private int deltaEntities;

        public DiagnosticSystem(IDrawingCanvas drawingCanvas, IEntityManager entityManager, ITemplateManager templateManager)
            : base(entityManager, templateManager, ComponentKinds.Any)
        {
            Guard.AgainstNullArgument(() => drawingCanvas);

            var processName = Process.GetCurrentProcess().ProcessName;

            this.drawingCanvas = drawingCanvas;
            this.pen = new Pen(RgbColor.CornflowerBlue, 1.0F, 1.0F);
            this.globalCpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            this.instanceCpuCounter = new PerformanceCounter("Process", "% Processor Time", processName);
        }

        protected override int UpdatingOrder
        {
            get { return SystemConstant.UpdatingOrders.Diagnostic; }
        }

        protected override void UpdateCore(Clock clock)
        {
            this.elapsedPeriod += clock.ElapsedPeriod;

            if (this.elapsedPeriod.TotalSeconds < 1.0)
            {
                return;
            }

            this.elapsedPeriod -= TimeSpan.FromSeconds(1.0);

            this.fps = this.numFrames;
            this.numFrames = 0;

            this.memory = (float)Process.GetCurrentProcess().WorkingSet64 / (1 << 20);
            this.cpu = (this.globalCpuCounter.NextValue() / 100.0F) * this.instanceCpuCounter.NextValue();

            this.numEntities += this.deltaEntities;
            this.deltaEntities = 0;

            this.numEntities = this.RelatedEntities.Count;
        }

        protected override void RenderCore(Clock clock)
        {
            this.numFrames++;

            var position = new Point(20.0F, 20.0F);
            this.drawingCanvas.DrawText(this.pen, position, "-fps: {0}".WithInvariantFormat(this.fps), Font);

            position.Y += 20.0F;
            this.drawingCanvas.DrawText(this.pen, position, "-memory: {0:0.00} MB".WithInvariantFormat(this.memory), Font);

            position.Y += 20.0F;
            this.drawingCanvas.DrawText(this.pen, position, "-cpu: {0:0}%".WithInvariantFormat(this.cpu), Font);

            position.Y += 20.0F;
            this.drawingCanvas.DrawText(this.pen, position, "-num.entities: {0:N0}".WithInvariantFormat(this.numEntities), Font);
        }
    }
}