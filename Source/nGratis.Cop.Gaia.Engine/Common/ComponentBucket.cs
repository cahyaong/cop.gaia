// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentBucket.cs" company="nGratis">
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