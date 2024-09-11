// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIdentityManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 6 August 2015 1:24:34 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public enum EntityKind
    {
        Generic = 0,
        Static,
        Dynamic
    }

    public interface IIdentityManager : IManager
    {
        uint RootId { get; }

        uint FindNextId(EntityKind entityKind);
    }
}