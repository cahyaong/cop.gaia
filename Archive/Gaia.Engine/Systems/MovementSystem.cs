// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovementSystem.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 4 August 2015 11:52:14 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.ComponentModel.Composition;
    using System.Linq;
    using nGratis.Cop.Gaia.Engine.Data;

    [Export(typeof(ISystem))]
    public class MovementSystem : BaseSystem
    {
        [ImportingConstructor]
        public MovementSystem()
            : base(new ComponentKinds(ComponentKind.Brain, ComponentKind.Placement))
        {
        }

        protected override int UpdatingOrder
        {
            get { return SystemConstant.UpdatingOrders.Movement; }
        }

        protected override void UpdateCore(Clock clock)
        {
            var placementBucket = this.GameInfrastructure.EntityManager.FindComponentBucket<PlacementComponent>();
            var brainBucket = this.GameInfrastructure.EntityManager.FindComponentBucket<BrainComponent>();
            var physicsBucket = this.GameInfrastructure.EntityManager.FindComponentBucket<PhysicsComponent>();

            var movingEntities = this
                .RelatedEntities
                .Where(entity => brainBucket.FindComponent(entity).EntityAction == EntityAction.Wander);

            foreach (var entity in movingEntities)
            {
                var placementComponent = placementBucket.FindComponent(entity);
                var physicsComponent = physicsBucket.FindComponent(entity);

                var distance = physicsComponent.Speed * clock.ElapsedDuration;
                var distanceX = (float)(distance * AuxiliaryMath.Cos(physicsComponent.DirectionAngle));
                var distanceY = (float)(distance * AuxiliaryMath.Sin(physicsComponent.DirectionAngle));

                var positionX = placementComponent.Center.X + distanceX;
                var positionY = placementComponent.Center.Y + distanceY;

                if (positionX - physicsComponent.Radius <= 0)
                {
                    positionX = physicsComponent.Radius;
                    physicsComponent.Speed = this.GameInfrastructure.ProbabilityManager.Roll(0, 3);
                    physicsComponent.DirectionAngle = this.GameInfrastructure.ProbabilityManager.Roll(-45, 45);
                }
                else if (positionX + physicsComponent.Radius >= this.GameSpecification.MapSize.Width)
                {
                    positionX = this.GameSpecification.MapSize.Width - physicsComponent.Radius;
                    physicsComponent.Speed = this.GameInfrastructure.ProbabilityManager.Roll(0, 3);
                    physicsComponent.DirectionAngle = this.GameInfrastructure.ProbabilityManager.Roll(135, 225);
                }
                else
                {
                    physicsComponent.Speed += this.GameInfrastructure.ProbabilityManager.Roll(-1, 1) / 10;
                    physicsComponent.Speed = physicsComponent.Speed.Clamp(0, 5);
                }

                if (positionY - physicsComponent.Radius <= 0)
                {
                    positionY = physicsComponent.Radius;
                    physicsComponent.Speed = this.GameInfrastructure.ProbabilityManager.Roll(0, 3);
                    physicsComponent.DirectionAngle = this.GameInfrastructure.ProbabilityManager.Roll(45, 135);
                }
                else if (positionY + physicsComponent.Radius >= this.GameSpecification.MapSize.Height)
                {
                    positionY = this.GameSpecification.MapSize.Height - physicsComponent.Radius;
                    physicsComponent.Speed = this.GameInfrastructure.ProbabilityManager.Roll(0, 3);
                    physicsComponent.DirectionAngle = this.GameInfrastructure.ProbabilityManager.Roll(225, 315);
                }
                else
                {
                    physicsComponent.Speed += this.GameInfrastructure.ProbabilityManager.Roll(-1, 1) / 10;
                    physicsComponent.Speed = physicsComponent.Speed.Clamp(0, 5);
                }

                if (this.GameInfrastructure.ProbabilityManager.Roll() >= 0.5)
                {
                    physicsComponent.Speed += this.GameInfrastructure.ProbabilityManager.Roll(-1, 1);
                    physicsComponent.Speed.Clamp(0, 5);
                }

                placementComponent.Center = new Point(positionX, positionY);
            }
        }
    }
}