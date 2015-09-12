// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseSystem.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 1 August 2015 1:49:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections.Generic;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Core;

    public abstract class BaseSystem : ISystem
    {
        private readonly ComponentKinds requiredComponentKinds;

        protected BaseSystem(ComponentKinds requiredComponentKinds)
        {
            Guard.AgainstNullArgument(() => requiredComponentKinds);

            this.requiredComponentKinds = requiredComponentKinds;

            this.RelatedEntities = new HashSet<IEntity>();
            this.IsEnabled = true;
        }

        public bool IsInitialized
        {
            get;
            private set;
        }

        public bool IsEnabled
        {
            get;
            set;
        }

        protected GameSpecification GameSpecification
        {
            get;
            private set;
        }

        protected IGameInfrastructure GameInfrastructure
        {
            get;
            private set;
        }

        protected IDrawingCanvas DrawingCanvas
        {
            get;
            private set;
        }

        protected HashSet<IEntity> RelatedEntities
        {
            get;
            private set;
        }

        protected abstract int UpdatingOrder
        {
            get;
        }

        public virtual void Initialize(
            GameSpecification gameSpecification,
            IGameInfrastructure gameInfrastructure,
            IDrawingCanvas drawingCanvas)
        {
            Guard.AgainstNullArgument(() => gameSpecification);
            Guard.AgainstNullArgument(() => gameInfrastructure);
            Guard.AgainstNullArgument(() => drawingCanvas);

            this.GameSpecification = gameSpecification;
            this.GameInfrastructure = gameInfrastructure;
            this.DrawingCanvas = drawingCanvas;

            this.InitializeCore();
            this.IsInitialized = true;
        }

        public virtual void AddEntity(IEntity entity)
        {
            RapidGuard.AgainstNullArgument(entity);

            var template = this.GameInfrastructure.TemplateManager.FindTemplate(entity.TemplateId);

            var isEntityRelated =
                template.ComponentKinds.HasFlags(this.requiredComponentKinds) &&
                !this.RelatedEntities.Contains(entity);

            if (isEntityRelated)
            {
                Guard.AgainstInvalidOperation(!this.RelatedEntities.Add(entity));
            }
        }

        public virtual void RemoveEnity(IEntity entity)
        {
            RapidGuard.AgainstNullArgument(entity);
            Guard.AgainstInvalidOperation(!this.RelatedEntities.Remove(entity));
        }

        public void Update(Clock clock)
        {
            if (this.IsInitialized && this.IsEnabled)
            {
                this.UpdateCore(clock);
            }
        }

        public void Render(Clock clock)
        {
            if (this.IsInitialized && this.IsEnabled)
            {
                this.RenderCore(clock);
            }
        }

        protected virtual void InitializeCore()
        {
        }

        protected virtual void UpdateCore(Clock clock)
        {
        }

        protected virtual void RenderCore(Clock clock)
        {
        }
    }
}