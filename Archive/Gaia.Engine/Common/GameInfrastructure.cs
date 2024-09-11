// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameInfrastructure.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 9 September 2015 12:37:06 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using nGratis.Cop.Gaia.Engine.Core;

    [Export(typeof(IGameInfrastructure))]
    internal class GameInfrastructure : IGameInfrastructure
    {
        private readonly IDictionary<Type, IManager> managerLookup;

        [ImportingConstructor]
        public GameInfrastructure([ImportMany] IEnumerable<IManager> managers)
        {
            if (managers != null)
            {
                this.managerLookup = managers.ToDictionary(
                    manager => manager.GetType().FindDirectInterface(),
                    manager => manager);

                this.managerLookup
                    .Values
                    .ForEach(value => value.Initialize(this));
            }
            else
            {
                this.managerLookup = new Dictionary<Type, IManager>();
            }

            this.TemplateManager = this.FindManager<ITemplateManager>();
            this.EntityManager = this.FindManager<IEntityManager>();
            this.SystemManager = this.FindManager<ISystemManager>();
            this.IdentityManager = this.FindManager<IIdentityManager>();
            this.ProbabilityManager = this.FindManager<IProbabilityManager>();
        }

        public ITemplateManager TemplateManager
        {
            get;
            private set;
        }

        public IEntityManager EntityManager
        {
            get;
            private set;
        }

        public ISystemManager SystemManager
        {
            get;
            private set;
        }

        public IIdentityManager IdentityManager
        {
            get;
            private set;
        }

        public IProbabilityManager ProbabilityManager
        {
            get;
            private set;
        }

        public TManager FindManager<TManager>() where TManager : IManager
        {
            RapidGuard.AgainstInvalidArgument(!managerLookup.ContainsKey(typeof(TManager)));

            return (TManager)this.managerLookup[typeof(TManager)];
        }
    }
}