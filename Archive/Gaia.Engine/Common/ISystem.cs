// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystem.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 12:57:31 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;

    public interface ISystem : IDisposable
    {
        bool IsInitialized { get; }

        bool IsEnabled { get; set; }

        void Initialize(
            GameSpecification gameSpecification,
            IGameInfrastructure gameInfrastructure,
            IDrawingCanvas drawingCanvas);

        void AddEntity(IEntity entity);

        void RemoveEnity(IEntity entity);

        void Update(Clock clock);

        void Render(Clock clock);
    }
}