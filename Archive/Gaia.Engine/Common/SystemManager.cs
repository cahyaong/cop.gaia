// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 1:15:38 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using nGratis.Cop.Core.Contract;

    [Export(typeof(IManager))]
    public class SystemManager : BaseManager, ISystemManager
    {
        private readonly IDictionary<Type, ISystem> systemLookup = new Dictionary<Type, ISystem>();

        public void AddSystem<TSystem>(TSystem system) where TSystem : class, ISystem
        {
            Guard.AgainstNullArgument(() => system);

            this.systemLookup.Add(system.GetType(), system);
        }

        public void RemoveSystem<TSystem>() where TSystem : class, ISystem
        {
            this.systemLookup.Remove(typeof(TSystem));
        }

        public void AddEntity(IEntity entity)
        {
            this
                .systemLookup
                .Values
                .ToList()
                .ForEach(system => system.AddEntity(entity));
        }

        public void RemoveEntity(IEntity entity)
        {
            this
                .systemLookup
                .Values
                .ToList()
                .ForEach(system => system.RemoveEnity(entity));
        }

        public void Update(Clock clock)
        {
            this
                .systemLookup
                .Values
                .ToList()
                .ForEach(system => system.Update(clock));
        }

        public void Render(Clock clock)
        {
            this
                .systemLookup
                .Values
                .ToList()
                .ForEach(system => system.Render(clock));
        }
    }
}