// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 9 September 2015 12:34:01 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public interface IManager
    {
        bool IsInitialized { get; }

        void Initialize(IGameInfrastructure gameInfrastructure);
    }
}