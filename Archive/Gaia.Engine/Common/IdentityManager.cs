// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 6 August 2015 1:29:43 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    [Export(typeof(IManager))]
    internal class IdentityManager : BaseManager, IIdentityManager
    {
        private readonly IDictionary<EntityKind, uint> nextIdLookup = new Dictionary<EntityKind, uint>()
            {
                { EntityKind.Generic, 1 },
                { EntityKind.Static, 10000000 },
                { EntityKind.Dynamic, 40000000 },
            };

        public uint RootId
        {
            get { return 0; }
        }

        public uint FindNextId(EntityKind entityKind)
        {
            return this.nextIdLookup[entityKind]++;
        }
    }
}