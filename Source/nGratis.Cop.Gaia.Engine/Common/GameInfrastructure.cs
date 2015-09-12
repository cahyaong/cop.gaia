// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameInfrastructure.cs" company="nGratis">
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