// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Template.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 12:37:17 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections.Generic;
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Core;

    internal class Template : ITemplate
    {
        public Template(uint id, string name, params IComponent[] components)
        {
            Guard.AgainstNullOrWhitespaceArgument(() => name);
            RapidGuard.AgainstNullArgument(components);

            this.Id = id;
            this.Name = name;
            this.ComponentKinds = components.ToComponentKinds();
            this.Components = components;
        }

        public uint Id
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public ComponentKinds ComponentKinds
        {
            get;
            private set;
        }

        public IEnumerable<IComponent> Components
        {
            get;
            private set;
        }
    }
}