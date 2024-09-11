// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstitutionComponent.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 17 August 2015 12:46:09 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    [Component(ComponentKind.Constitution)]
    public class ConstitutionComponent : BaseComponent
    {
        public float HitPoint { get; set; }

        public override IComponent Clone()
        {
            return new ConstitutionComponent()
            {
                HitPoint = this.HitPoint
            };
        }
    }
}