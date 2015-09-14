// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainGame.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 30 July 2015 10:36:23 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Reflection;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Client.Mono.Core;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    internal class MainGame : Game
    {
        private readonly IColor backgroundColor = new RgbColor(37, 37, 38);

        public bool IsInitialized
        {
            get;
            private set;
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

        public IDrawingCanvas DrawingCanvas
        {
            get;
            private set;
        }

        protected IList<ISystem> Systems
        {
            get;
            private set;
        }

        public void Initialize(
            GameSpecification gameSpecification,
            IGameInfrastructure gameInfrastructure,
            IReadOnlyList<ISystem> systems)
        {
            Guard.AgainstNullArgument(() => gameSpecification);
            Guard.AgainstNullArgument(() => gameInfrastructure);
            Guard.AgainstNullArgument(() => systems);
            Guard.AgainstInvalidArgument(!systems.Any(), () => systems);

            this.GameSpecification = gameSpecification;
            this.GameInfrastructure = gameInfrastructure;
            this.Systems = systems.ToList();

            var graphicsDeviceManager = new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = (int)gameSpecification.ScreenSize.Width,
                    PreferredBackBufferHeight = (int)gameSpecification.ScreenSize.Height
                };

            graphicsDeviceManager.ApplyChanges();

            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            this.IsInitialized = true;
        }

        protected override void LoadContent()
        {
            Guard.AgainstInvalidOperation(!this.IsInitialized);

            var fontManager = this.GameInfrastructure.FindManager<IFontManager>();
            fontManager.Initialize(this.Content);

            this.DrawingCanvas = new MonoDrawingCanvas(this.GraphicsDevice, fontManager);
            base.LoadContent();

            this.LoadTemplateManager();
            this.LoadEntityManager();
            this.LoadSystemManager();
            this.LoadGame();
        }

        protected override void BeginRun()
        {
            Guard.AgainstInvalidOperation(!this.IsInitialized);

            base.BeginRun();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (!this.IsInitialized)
            {
                return;
            }

            this.GameInfrastructure.SystemManager.Update(gameTime.ToCopClock());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!this.IsInitialized)
            {
                return;
            }

            this.DrawingCanvas.BeginBatch();
            this.DrawingCanvas.Clear(this.backgroundColor);
            this.GameInfrastructure.SystemManager.Render(gameTime.ToCopClock());
            this.DrawingCanvas.EndBatch();

            base.Draw(gameTime);
        }

        private void LoadTemplateManager()
        {
            this.GameInfrastructure.TemplateManager.InitializeCreatureTemplates();
        }

        private void LoadEntityManager()
        {
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetCustomAttributes<ComponentAttribute>().Any())
                .ForEach(type =>
                    {
                        typeof(IEntityManager)
                            .GetMethod("RegisterComponentType")
                            .MakeGenericMethod(type)
                            .Invoke(this.GameInfrastructure.EntityManager, null);
                    });

            Observable
                .FromEventPattern<EntityChangedEventArgs>(this.GameInfrastructure.EntityManager, "EntityCreated")
                .Subscribe(pattern => this.GameInfrastructure.SystemManager.AddEntity(pattern.EventArgs.Entity));

            Observable
                .FromEventPattern<EntityChangedEventArgs>(this.GameInfrastructure.EntityManager, "EntityDestroyed")
                .Subscribe(pattern => this.GameInfrastructure.SystemManager.RemoveEntity(pattern.EventArgs.Entity));
        }

        private void LoadSystemManager()
        {
#if DEBUG
            foreach (var system in this.Systems)
#else
            foreach (var system in this.Systems.Where(system => !(system is DiagnosticSystem)))
#endif
            {
                system.Initialize(this.GameSpecification, this.GameInfrastructure, this.DrawingCanvas);
                this.GameInfrastructure.SystemManager.AddSystem(system);
            }
        }

        private void LoadGame()
        {
            var template = this.GameInfrastructure.TemplateManager.FindTemplate("Character");

            var entities = Enumerable
                .Range(0, 250)
                .Select(_ => this.GameInfrastructure.EntityManager.CreateEntity(template));

            var constitutionBucket = this
                .GameInfrastructure
                .EntityManager
                .FindComponentBucket<ConstitutionComponent>();

            var placementBucket = this
                .GameInfrastructure
                .EntityManager
                .FindComponentBucket<PlacementComponent>();

            foreach (var entity in entities)
            {
                var constitutionComponent = constitutionBucket.FindComponent(entity);
                constitutionComponent.HitPoint = this.GameInfrastructure.ProbabilityManager.Roll(0, 100);

                var placementComponent = placementBucket.FindComponent(entity);

                placementComponent.Position = new nGratis.Cop.Gaia.Engine.Data.Point(
                    this.GameInfrastructure.ProbabilityManager.Roll(0, this.GameSpecification.MapSize.Width),
                    this.GameInfrastructure.ProbabilityManager.Roll(0, this.GameSpecification.MapSize.Height));

                placementComponent.Direction = new Vector(
                    this.GameInfrastructure.ProbabilityManager.Roll(-1, 1),
                    this.GameInfrastructure.ProbabilityManager.Roll(-1, 1));

                placementComponent.Speed = this.GameInfrastructure.ProbabilityManager.Roll(0, 3);
            }
        }
    }
}