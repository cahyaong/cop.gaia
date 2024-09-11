// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 1:26:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using nGratis.Cop.Gaia.Engine.Core;

    [Export(typeof(IManager))]
    public class EntityManager : BaseManager, IEntityManager
    {
        private readonly IDictionary<uint, IEntity> entityLookup;

        private readonly IDictionary<ComponentKind, IComponentBucket> componentBucketLookup;

        [ImportingConstructor]
        public EntityManager()
        {
            this.entityLookup = new Dictionary<uint, IEntity>();
            this.componentBucketLookup = new Dictionary<ComponentKind, IComponentBucket>();
        }

        public event EventHandler<EntityChangedEventArgs> EntityCreated;

        public event EventHandler<EntityChangedEventArgs> EntityDestroyed;

        public void RegisterComponentType<TComponent>() where TComponent : IComponent
        {
            var componentKind = typeof(TComponent).ToComponentKind();
            RapidGuard.AgainstInvalidArgument(this.componentBucketLookup.ContainsKey(componentKind));

            this.componentBucketLookup.Add(componentKind, new ComponentBucket<TComponent>(componentKind));
        }

        public void UnregisterComponentType<TComponent>() where TComponent : IComponent
        {
            var componentKind = typeof(TComponent).ToComponentKind();
            RapidGuard.AgainstInvalidArgument(!this.componentBucketLookup.ContainsKey(componentKind));

            this.componentBucketLookup.Remove(componentKind);
        }

        public IEntity CreateEntity(ITemplate template)
        {
            RapidGuard.AgainstNullArgument(template);

            var entity = new Entity(
                this.GameInfrastructure.IdentityManager.FindNextId(EntityKind.Dynamic),
                this.GameInfrastructure.IdentityManager.RootId,
                template.Id);

            this.entityLookup.Add(entity.Id, entity);

            template
                .Components
                .Select(component => new
                {
                    Kind = component.GetType().ToComponentKind(),
                    Component = component.Clone()
                })
                .ToList()
                .ForEach(anon => this.componentBucketLookup[anon.Kind].AddComponent(entity, anon.Component));

            if (this.EntityCreated != null)
            {
                this.EntityCreated(this, new EntityChangedEventArgs(entity));
            }

            return entity;
        }

        public void DestroyEntity(IEntity entity)
        {
            RapidGuard.AgainstNullArgument(entity);

            if (this.EntityDestroyed != null)
            {
                this.EntityDestroyed(this, new EntityChangedEventArgs(entity));
            }
        }

        public IComponentBucket<TComponent> FindComponentBucket<TComponent>() where TComponent : IComponent
        {
            var componentKind = typeof(TComponent).ToComponentKind();
            RapidGuard.AgainstInvalidArgument(!this.componentBucketLookup.ContainsKey(componentKind));

            return (IComponentBucket<TComponent>)this.componentBucketLookup[componentKind];
        }
    }
}