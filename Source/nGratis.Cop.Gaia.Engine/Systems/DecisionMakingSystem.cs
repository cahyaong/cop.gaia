// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DecisionMakingSystem.cs" company="nGratis">
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
// <creation_timestamp>Monday, 14 September 2015 1:51:11 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.ComponentModel.Composition;

    [Export(typeof(ISystem))]
    internal class DecisionMakingSystem : BaseSystem
    {
        public DecisionMakingSystem()
            : base(new ComponentKinds(ComponentKind.Brain))
        {
        }

        protected override int UpdatingOrder
        {
            get { return SystemConstant.UpdatingOrders.DecisionMaking; }
        }

        protected override float UpdatingInterval
        {
            get { return 1; }
        }

        protected override void UpdateCore(Clock clock)
        {
            var brainBucket = this.GameInfrastructure.EntityManager.FindComponentBucket<BrainComponent>();

            foreach (var entity in this.RelatedEntities)
            {
                var brainComponent = brainBucket.FindComponent(entity);

                switch (brainComponent.EntityAction)
                {
                    case EntityAction.DoNothing:
                        {
                            if (this.GameInfrastructure.ProbabilityManager.Roll(0, 1) < 0.50)
                            {
                                brainComponent.EntityAction = EntityAction.Explore;
                            }

                            break;
                        }
                    case EntityAction.Explore:
                        {
                            if (this.GameInfrastructure.ProbabilityManager.Roll(0, 1) < 0.25)
                            {
                                brainComponent.EntityAction = EntityAction.DoNothing;
                            }

                            break;
                        }
                }
            }
        }
    }
}