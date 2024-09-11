// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatisticComponent.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 11:25:56 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    [Component(ComponentKind.Statistic)]
    public class StatisticComponent : BaseComponent
    {
        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Constitution { get; set; }

        public int Intelligence { get; set; }

        public int Wisdom { get; set; }

        public int Charisma { get; set; }

        public override IComponent Clone()
        {
            return new StatisticComponent()
            {
                Strength = this.Strength,
                Dexterity = this.Dexterity,
                Constitution = this.Constitution,
                Intelligence = this.Intelligence,
                Wisdom = this.Wisdom,
                Charisma = this.Charisma
            };
        }
    }
}