// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISystemManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 1:15:19 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public interface ISystemManager : IManager
    {
        void AddSystem<TSystem>(TSystem system) where TSystem : class, ISystem;

        void RemoveSystem<TSystem>() where TSystem : class, ISystem;

        void AddEntity(IEntity entity);

        void RemoveEntity(IEntity entity);

        void Update(Clock clock);

        void Render(Clock clock);
    }
}