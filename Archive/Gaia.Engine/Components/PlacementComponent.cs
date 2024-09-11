// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlacementComponent.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 12:06:22 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Gaia.Engine.Data;

    [Component(ComponentKind.Placement)]
    public class PlacementComponent : BaseComponent
    {
        public Point Center { get; set; }

        public override IComponent Clone()
        {
            return new PlacementComponent()
            {
                Center = this.Center
            };
        }
    }
}