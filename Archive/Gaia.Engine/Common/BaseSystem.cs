// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseSystem.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 1 August 2015 1:49:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Core;

    public abstract class BaseSystem : ISystem
    {
        private readonly ComponentKinds requiredComponentKinds;

        private float skippedUpdatingDuration;

        protected BaseSystem(ComponentKinds requiredComponentKinds)
        {
            Guard.AgainstNullArgument(() => requiredComponentKinds);

            this.requiredComponentKinds = requiredComponentKinds;

            this.RelatedEntities = new HashSet<IEntity>();
            this.IsEnabled = true;
        }

        ~BaseSystem()
        {
            this.Dispose(false);
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

        protected virtual float UpdatingInterval
        {
            get { return 0; }
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
            if (this.UpdatingInterval > 0)
            {
                this.skippedUpdatingDuration += clock.ElapsedDuration;

                if (this.skippedUpdatingDuration < this.UpdatingInterval)
                {
                    return;
                }

                this.skippedUpdatingDuration -= this.UpdatingInterval;
            }

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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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

        protected virtual void Dispose(bool isDisposing)
        {
        }
    }
}