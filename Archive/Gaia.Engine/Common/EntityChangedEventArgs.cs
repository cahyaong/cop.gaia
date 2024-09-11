// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityChangedEventArgs.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 2:09:29 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using nGratis.Cop.Gaia.Engine.Core;

    public class EntityChangedEventArgs : EventArgs
    {
        public EntityChangedEventArgs(IEntity entity)
        {
            RapidGuard.AgainstNullArgument(entity);

            this.Entity = entity;
        }

        public IEntity Entity { get; private set; }
    }
}