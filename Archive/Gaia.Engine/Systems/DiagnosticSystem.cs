﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticSystem.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 12 August 2015 1:52:56 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    [Export(typeof(ISystem))]
    public class DiagnosticSystem : BaseSystem
    {
        private const string Font = "Fonts/Diagnostics";

        private readonly Pen pen;

        private readonly PerformanceCounter globalCpuCounter;

        private readonly PerformanceCounter instanceCpuCounter;

        private float elapsedDuration;

        private uint numFrames;

        private uint fps;

        private float memory;

        private float cpu;

        private int numEntities;

        private int deltaEntities;

        private bool isDisposed;

        [ImportingConstructor]
        public DiagnosticSystem()
            : base(ComponentKinds.Any)
        {
            var processName = Process.GetCurrentProcess().ProcessName;

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
            this.elapsedDuration += clock.ElapsedDuration;

            if (this.elapsedDuration < 1)
            {
                return;
            }

            this.elapsedDuration--;

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

            this.DrawingCanvas.DrawText(
                this.pen,
                position,
                "-fps: {0}".WithInvariantFormat(this.fps),
                Font);

            position.Y += 20.0F;

            this.DrawingCanvas.DrawText(
                this.pen,
                position,
                "-memory: {0:0.00} MB".WithInvariantFormat(this.memory),
                Font);

            position.Y += 20.0F;

            this.DrawingCanvas.DrawText(
                this.pen,
                position,
                "-cpu: {0:0}%".WithInvariantFormat(this.cpu),
                Font);

            position.Y += 20.0F;

            this.DrawingCanvas.DrawText(
                this.pen,
                position,
                "-num.entities: {0:N0}".WithInvariantFormat(this.numEntities),
                Font);
        }

        protected override void Dispose(bool isDisposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                this.globalCpuCounter.Dispose();
                this.instanceCpuCounter.Dispose();
            }

            base.Dispose(isDisposing);
            this.isDisposed = true;
        }
    }
}