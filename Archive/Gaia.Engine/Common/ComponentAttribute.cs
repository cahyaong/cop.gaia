// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentAttribute.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 18 August 2015 1:14:08 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using nGratis.Cop.Core.Contract;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ComponentAttribute : Attribute
    {
        public ComponentAttribute(ComponentKind componentKind)
        {
            Guard.AgainstInvalidArgument(componentKind == ComponentKind.None, () => componentKind);

            this.ComponentKind = componentKind;
        }

        public ComponentKind ComponentKind { get; private set; }
    }
}