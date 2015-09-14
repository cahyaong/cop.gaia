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

            var movingEntities = this
                .RelatedEntities
                .Where(entity => brainBucket.FindComponent(entity).EntityAction == EntityAction.Explore);

            foreach (var entity in movingEntities)
            {
                var placementComponent = placementBucket.FindComponent(entity);

                var directionX = placementComponent.Direction.X;
                var directionY = placementComponent.Direction.Y;

                var displacementX = (float)(directionX * placementComponent.Speed * clock.ElapsedDuration.TotalSeconds);
                var displacementY = (float)(directionY * placementComponent.Speed * clock.ElapsedDuration.TotalSeconds);

                var positionX = placementComponent.Position.X + displacementX;
                var positionY = placementComponent.Position.Y + displacementY;

                if (positionX < 0)
                {
                    positionX = 0;
                    directionX = this.GameInfrastructure.ProbabilityManager.Roll(-1, 1);
                }
                else if (positionX > this.GameSpecification.MapSize.Width - 1)
                {
                    positionX = this.GameSpecification.MapSize.Width - 1;
                    directionX = this.GameInfrastructure.ProbabilityManager.Roll(-1, 1);
                }
                else
                {
                    directionX += this.GameInfrastructure.ProbabilityManager.Roll(-0.5F, 0.5F) / 10;
                    directionX = directionX.Clamp(-1, 1);
                }

                if (positionY < 0)
                {
                    positionY = 0;
                    directionY = this.GameInfrastructure.ProbabilityManager.Roll(-1, 1);
                }
                else if (positionY > this.GameSpecification.MapSize.Height - 1)
                {
                    positionY = this.GameSpecification.MapSize.Height - 1;
                    directionY = this.GameInfrastructure.ProbabilityManager.Roll(-1, 1);
                }
                else
                {
                    directionY += this.GameInfrastructure.ProbabilityManager.Roll(-0.5F, 0.5F) / 10;
                    directionY = directionY.Clamp(-1, 1);
                }

                if (this.GameInfrastructure.ProbabilityManager.Roll() >= 0.5)
                {
                    placementComponent.Speed += this.GameInfrastructure.ProbabilityManager.Roll() - 0.5F;
                    placementComponent.Speed = placementComponent.Speed.Clamp(0, 5);
                }

                placementComponent.Position = new Point(positionX, positionY);
                placementComponent.Direction = new Vector(directionX, directionY);
            }
        }
    }
}