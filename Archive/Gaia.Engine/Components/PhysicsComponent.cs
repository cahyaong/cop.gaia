// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhysicsComponent.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 19 September 2015 12:40:07 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    [Component(ComponentKind.Physics)]
    public class PhysicsComponent : BaseComponent
    {
        public float Radius { get; set; }

        public float Speed { get; set; }

        public float DirectionAngle { get; set; }

        public override IComponent Clone()
        {
            return new PhysicsComponent()
            {
                Radius = Radius,
                Speed = this.Speed,
                DirectionAngle = this.DirectionAngle
            };
        }
    }
}