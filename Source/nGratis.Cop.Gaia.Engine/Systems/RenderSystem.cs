// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderSystem.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 4 August 2015 12:02:42 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    public class RenderSystem : BaseSystem
    {
        private readonly IDrawingCanvas drawingCanvas;

        private readonly Size tileSize;

        public RenderSystem(IDrawingCanvas drawingCanvas, IEntityManager entityManager, ITemplateManager templateManager)
            : base(entityManager, templateManager, new ComponentKinds(ComponentKind.Placement))
        {
            RapidGuard.AgainstNullArgument(drawingCanvas);

            this.drawingCanvas = drawingCanvas;
            this.tileSize = new Size(10.0F, 10.0F);
        }

        protected override int UpdatingOrder
        {
            get { return SystemConstant.UpdatingOrders.Render; }
        }

        protected override void RenderCore(Clock clock)
        {
            var redPen = new Pen(new RgbColor(255, 0, 0), 1.0F, 1.0F);
            var greenPen = new Pen(new RgbColor(0, 255, 0), 1.0F, 1.0F);
            var orangePen = new Pen(new RgbColor(128, 128, 0), 1.0F, 1.0F);

            foreach (var entity in this.RelatedEntities)
            {
                var constitutionComponent = this.EntityManager.FindComponent<ConstitutionComponent>(entity);

                var pen = redPen;

                if (constitutionComponent.HitPoint > 65)
                {
                    pen = greenPen;
                }
                else if (constitutionComponent.HitPoint > 35)
                {
                    pen = orangePen;
                }

                var placementComponent = this.EntityManager.FindComponent<PlacementComponent>(entity);

                var startPoint = new Point(
                    placementComponent.Position.X * this.tileSize.Width,
                    placementComponent.Position.Y * this.tileSize.Height);

                var endPoint = new Point(
                    (placementComponent.Position.X + 1) * this.tileSize.Width,
                    (placementComponent.Position.Y + 1) * this.tileSize.Height);

                this.drawingCanvas.DrawLine(pen, startPoint, endPoint);

                startPoint = new Point(
                    (placementComponent.Position.X + 1) * this.tileSize.Width,
                    placementComponent.Position.Y * this.tileSize.Height);

                endPoint = new Point(
                    placementComponent.Position.X * this.tileSize.Width,
                    (placementComponent.Position.Y + 1) * this.tileSize.Height);

                this.drawingCanvas.DrawLine(pen, startPoint, endPoint);
            }
        }
    }
}