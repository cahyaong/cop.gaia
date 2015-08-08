// --------------------------------------------------------------------------------------------------------------------
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
    using nGratis.Cop.Gaia.Engine.Core;

    public class EntityManager : IEntityManager
    {
        private readonly IIdentityManager identityManager;

        private readonly ITemplateManager templateManager;

        private readonly IDictionary<uint, IEntity> entityLookup = new Dictionary<uint, IEntity>();

        public EntityManager()
            : this(new IdentityManager(), new TemplateManager())
        {
        }

        public EntityManager(IIdentityManager identityManager, ITemplateManager templateManager)
        {
            Guard.AgainstNullArgument(() => identityManager);
            Guard.AgainstNullArgument(() => templateManager);

            this.identityManager = identityManager;

            this.templateManager = templateManager;
            this.templateManager.InitializeCreatureTemplates();
        }

        public event EventHandler<EntityChangedEventArgs> EntityCreated;

        public event EventHandler<EntityChangedEventArgs> EntityDestroyed;

        public void RegisterComponentType<TComponent>() where TComponent : IComponent
        {
            throw new System.NotImplementedException();
        }

        public void UnregisterComponentType<TComponent>() where TComponent : IComponent
        {
            throw new System.NotImplementedException();
        }

        public IEntity CreteEntity(string templateName, EntityKind entityKind)
        {
            var entity = this
                .templateManager
                .FindTemplate(templateName)
                .CreateEntity(this.identityManager.FindNextId(entityKind));

            if (this.EntityCreated != null)
            {
                this.EntityCreated(this, new EntityChangedEventArgs(entity));
            }

            return entity;
        }

        public void DestroyEntity(IEntity entity)
        {
            Guard.AgainstNullArgument(() => entity);

            if (this.EntityDestroyed != null)
            {
                this.EntityDestroyed(this, new EntityChangedEventArgs(entity));
            }
        }
    }
}