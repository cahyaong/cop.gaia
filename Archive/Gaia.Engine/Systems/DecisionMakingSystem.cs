// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DecisionMakingSystem.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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

                switch (brainComponent.EntityState)
                {
                    case EntityState.Idle:
                        {
                            if (this.GameInfrastructure.ProbabilityManager.Roll(0, 1) < 0.75)
                            {
                                brainComponent.EntityState = EntityState.Exploration;
                                brainComponent.EntityAction = EntityAction.Wander;
                            }

                            break;
                        }
                    case EntityState.Exploration:
                        {
                            if (this.GameInfrastructure.ProbabilityManager.Roll(0, 1) < 0.25)
                            {
                                brainComponent.EntityState = EntityState.Idle;
                                brainComponent.EntityAction = EntityAction.DoNothing;
                            }

                            break;
                        }
                }
            }
        }
    }
}