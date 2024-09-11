// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGameInfrastructure.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 9 September 2015 12:17:41 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public interface IGameInfrastructure
    {
        ITemplateManager TemplateManager { get; }

        IEntityManager EntityManager { get; }

        ISystemManager SystemManager { get; }

        IIdentityManager IdentityManager { get; }

        IProbabilityManager ProbabilityManager { get; }

        TManager FindManager<TManager>() where TManager : IManager;
    }
}