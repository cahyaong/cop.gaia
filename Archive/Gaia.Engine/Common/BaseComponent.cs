// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseComponent.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 1 August 2015 1:49:20 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public abstract class BaseComponent : IComponent
    {
        public abstract IComponent Clone();
    }
}