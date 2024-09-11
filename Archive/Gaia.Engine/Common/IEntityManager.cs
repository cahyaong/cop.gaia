// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 1:27:26 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;

    public interface IEntityManager : IManager
    {
        event EventHandler<EntityChangedEventArgs> EntityCreated;

        event EventHandler<EntityChangedEventArgs> EntityDestroyed;

        void RegisterComponentType<TComponent>() where TComponent : IComponent;

        void UnregisterComponentType<TComponent>() where TComponent : IComponent;

        IEntity CreateEntity(ITemplate template);

        void DestroyEntity(IEntity entity);

        IComponentBucket<TComponent> FindComponentBucket<TComponent>() where TComponent : IComponent;
    }
}