// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplate.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 12:56:13 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections.Generic;

    public interface ITemplate
    {
        uint Id { get; }

        string Name { get; }

        ComponentKinds ComponentKinds { get; }

        IEnumerable<IComponent> Components { get; }
    }
}