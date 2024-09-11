// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComponentBucket.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 1 September 2015 12:54:06 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public interface IComponentBucket<TComponent> : IComponentBucket
        where TComponent : IComponent
    {
        void AddComponent(IEntity entity, TComponent component);

        new TComponent FindComponent(IEntity entity);
    }

    public interface IComponentBucket
    {
        ComponentKind ComponentKind { get; }

        void AddComponent(IEntity entity, IComponent component);

        void RemoveComponent(IEntity entity);

        IComponent FindComponent(IEntity entity);
    }
}