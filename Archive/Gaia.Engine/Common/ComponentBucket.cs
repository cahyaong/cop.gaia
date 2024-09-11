// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentBucket.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 1 September 2015 12:56:18 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections.Generic;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Core;

    internal class ComponentBucket<TComponent> : IComponentBucket<TComponent>
        where TComponent : IComponent
    {
        private readonly IDictionary<uint, TComponent> componentLookup = new Dictionary<uint, TComponent>();

        public ComponentBucket(ComponentKind componentKind)
        {
            Guard.AgainstDefaultArgument(() => componentKind);

            this.ComponentKind = componentKind;
        }

        public ComponentKind ComponentKind
        {
            get;
            private set;
        }

        void IComponentBucket.AddComponent(IEntity entity, IComponent component)
        {
            this.AddComponent(entity, (TComponent)component);
        }

        IComponent IComponentBucket.FindComponent(IEntity entity)
        {
            return this.FindComponent(entity);
        }

        public void AddComponent(IEntity entity, TComponent component)
        {
            RapidGuard.AgainstInvalidArgument(this.componentLookup.ContainsKey(entity.Id));

            this.componentLookup.Add(entity.Id, component);
        }

        public void RemoveComponent(IEntity entity)
        {
            RapidGuard.AgainstInvalidArgument(!this.componentLookup.ContainsKey(entity.Id));

            this.componentLookup.Remove(entity.Id);
        }

        public TComponent FindComponent(IEntity entity)
        {
            RapidGuard.AgainstInvalidArgument(!this.componentLookup.ContainsKey(entity.Id));

            return this.componentLookup[entity.Id];
        }
    }
}