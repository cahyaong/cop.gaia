// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameManager.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 30 July 2015 10:36:23 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using System;
    using System.Reactive.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using nGratis.Cop.Gaia.Client.Mono.Core;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Common;
    using nGratis.Cop.Gaia.Engine.Core;

    internal class GameManager : Game, IGameManager
    {
        private readonly IColor backgroundColor = new RgbColor(37, 37, 38);

        private readonly ITemplateManager templateManager;

        private readonly IEntityManager entityManager;

        private readonly ISystemManager systemManager;

        private IDrawingCanvas drawingCanvas;

        private IFontManager fontManager;

        public GameManager()
            : this(new TemplateManager(), new EntityManager(), new SystemManager())
        {
        }

        public GameManager(
            ITemplateManager templateManager,
            IEntityManager entityManager,
            ISystemManager systemManager)
        {
            RapidGuard.AgainstNullArgument(templateManager);
            RapidGuard.AgainstNullArgument(entityManager);
            RapidGuard.AgainstNullArgument(systemManager);

            this.templateManager = templateManager;
            this.entityManager = entityManager;
            this.systemManager = systemManager;

            var graphicsDeviceManager = new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1280,
                    PreferredBackBufferHeight = 720
                };

            graphicsDeviceManager.ApplyChanges();

            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.fontManager = new FontManager(this.Content);
            this.drawingCanvas = new MonoDrawingCanvas(this.GraphicsDevice, this.fontManager);

            this.InitializeTemplateManager();
            this.InitializeEntityManager();
            this.InitializeSystemManager();

            this.InitializeGame();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            this.systemManager.Update(gameTime.ToCopClock());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.drawingCanvas.BeginBatch();
            this.drawingCanvas.Clear(this.backgroundColor);
            this.systemManager.Render(gameTime.ToCopClock());
            this.drawingCanvas.EndBatch();

            base.Draw(gameTime);
        }

        private void InitializeTemplateManager()
        {
            this.templateManager.InitializeCreatureTemplates();
        }

        private void InitializeEntityManager()
        {
            this.entityManager.RegisterComponentType<StatisticComponent>();
            this.entityManager.RegisterComponentType<ConstitutionComponent>();
            this.entityManager.RegisterComponentType<TraitComponent>();
            this.entityManager.RegisterComponentType<PlacementComponent>();

            Observable
                .FromEventPattern<EntityChangedEventArgs>(this.entityManager, "EntityCreated")
                .Subscribe(pattern => this.systemManager.AddEntity(pattern.EventArgs.Entity));

            Observable
                .FromEventPattern<EntityChangedEventArgs>(this.entityManager, "EntityDestroyed")
                .Subscribe(pattern => this.systemManager.RemoveEntity(pattern.EventArgs.Entity));
        }

        private void InitializeSystemManager()
        {
            this.systemManager.AddSystem(new CombatSystem(this.entityManager, this.templateManager));
            this.systemManager.AddSystem(new RenderSystem(this.drawingCanvas, this.entityManager, this.templateManager));

#if DEBUG
            this.systemManager.AddSystem(new DiagnosticSystem(this.drawingCanvas, this.entityManager, this.templateManager));
#endif
        }

        private void InitializeGame()
        {
            var random = new Random(Environment.TickCount);
            var template = this.templateManager.FindTemplate("Character");

            for (var counter = 0; counter < 250; counter++)
            {
                var entity = this.entityManager.CreateEntity(template);

                var constitutionComponent = this.entityManager.FindComponent<ConstitutionComponent>(entity);
                constitutionComponent.HitPoint = random.Next(0, 100);

                var placementComponent = this.entityManager.FindComponent<PlacementComponent>(entity);
                placementComponent.Position = new nGratis.Cop.Gaia.Engine.Data.Point(random.Next(128), random.Next(72));
            }
        }
    }
}