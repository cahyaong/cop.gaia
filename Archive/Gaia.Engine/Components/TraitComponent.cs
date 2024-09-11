// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TraitComponent.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 12:54:09 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    [Component(ComponentKind.Trait)]
    public class TraitComponent : BaseComponent
    {
        public string Race { get; set; }

        public string Class { get; set; }

        public override IComponent Clone()
        {
            return new TraitComponent()
            {
                Race = this.Race,
                Class = this.Class
            };
        }
    }
}