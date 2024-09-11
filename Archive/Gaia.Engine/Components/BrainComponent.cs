// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrainComponent.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 14 September 2015 1:46:09 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    [Component(ComponentKind.Brain)]
    internal class BrainComponent : BaseComponent
    {
        public EntityAction EntityAction { get; set; }

        public EntityState EntityState { get; set; }

        public override IComponent Clone()
        {
            return new BrainComponent()
            {
                EntityState = this.EntityState,
                EntityAction = this.EntityAction
            };
        }
    }
}