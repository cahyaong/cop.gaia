// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingSystem.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 4 August 2015 12:02:42 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.ComponentModel.Composition;
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    [Export(typeof(ISystem))]
    public class RenderingSystem : BaseSystem
    {
        [ImportingConstructor]
        public RenderingSystem()
            : base(new ComponentKinds(ComponentKind.Placement, ComponentKind.Physics))
        {
        }

        protected override int UpdatingOrder
        {
            get { return SystemConstant.UpdatingOrders.Rendering; }
        }

        protected override void RenderCore(Clock clock)
        {
            var redPen = new Pen(new RgbColor(255, 0, 0), 1, 1);
            var greenPen = new Pen(new RgbColor(0, 255, 0), 1, 1);
            var orangePen = new Pen(new RgbColor(128, 128, 0), 1, 1);

            var constitutionBucket = this
                .GameInfrastructure
                .EntityManager
                .FindComponentBucket<ConstitutionComponent>();

            var placementBucket = this
                .GameInfrastructure
                .EntityManager
                .FindComponentBucket<PlacementComponent>();

            var physicsBucket = this
                .GameInfrastructure
                .EntityManager
                .FindComponentBucket<PhysicsComponent>();

            foreach (var entity in this.RelatedEntities)
            {
                var constitutionComponent = constitutionBucket.FindComponent(entity);
                var placementComponent = placementBucket.FindComponent(entity);
                var physicsComponent = physicsBucket.FindComponent(entity);

                var pen = redPen;

                if (constitutionComponent.HitPoint > 65)
                {
                    pen = greenPen;
                }
                else if (constitutionComponent.HitPoint > 35)
                {
                    pen = orangePen;
                }

                var radius = physicsComponent.Radius * this.GameSpecification.TileLength;

                var center = new Point(
                    placementComponent.Center.X * this.GameSpecification.TileLength,
                    placementComponent.Center.Y * this.GameSpecification.TileLength);

                this.DrawingCanvas.DrawCircle(pen, Brush.Null, center, radius);

                var point = new Point(
                    center.X + (float)AuxiliaryMath.Cos(physicsComponent.DirectionAngle) * radius,
                    center.Y + (float)AuxiliaryMath.Sin(physicsComponent.DirectionAngle) * radius);

                this.DrawingCanvas.DrawLine(pen, center, point);
            }
        }
    }
}