﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityManager.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 1:26:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using nGratis.Cop.Gaia.Engine.Core;

    public class EntityManager : IEntityManager
    {
        private readonly IIdentityManager identityManager;

        private readonly IDictionary<uint, IEntity> entityLookup;

        private readonly IDictionary<ComponentKind, IDictionary<uint, IComponent>> componentLookup;

        public EntityManager()
            : this(new IdentityManager())
        {
        }

        public EntityManager(IIdentityManager identityManager)
        {
            RapidGuard.AgainstNullArgument(identityManager);

            this.entityLookup = new Dictionary<uint, IEntity>();
            this.componentLookup = new Dictionary<ComponentKind, IDictionary<uint, IComponent>>();

            this.identityManager = identityManager;
        }

        public event EventHandler<EntityChangedEventArgs> EntityCreated;

        public event EventHandler<EntityChangedEventArgs> EntityDestroyed;

        public void RegisterComponentType<TComponent>() where TComponent : IComponent
        {
            var type = typeof(TComponent);
            this.componentLookup.Add(type.ToComponentKind(), new Dictionary<uint, IComponent>());
        }

        public void UnregisterComponentType<TComponent>() where TComponent : IComponent
        {
            var type = typeof(TComponent);
            this.componentLookup.Remove(type.ToComponentKind());
        }

        public IEntity CreateEntity(ITemplate template)
        {
            RapidGuard.AgainstNullArgument(template);

            var entity = new Entity(
                this.identityManager.FindNextId(EntityKind.Dynamic),
                this.identityManager.RootId,
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
                .ForEach(annon => this.componentLookup[annon.Kind].Add(entity.Id, annon.Component));

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

        public TComponent FindComponent<TComponent>(IEntity entity) where TComponent : IComponent
        {
            RapidGuard.AgainstNullArgument(entity);

            return (TComponent)this.componentLookup[typeof(TComponent).ToComponentKind()][entity.Id];
        }
    }
}