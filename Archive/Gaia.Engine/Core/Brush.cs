// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Brush.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 1 August 2015 12:15:54 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System;

    public struct Brush
    {
        public static Brush Null = new Brush(RgbColor.Default, 0.0F);

        public Brush(IColor color, float opacity = 1.0F)
            : this()
        {
            this.Color = color ?? RgbColor.Default;
            this.Opacity = opacity.Clamp(0.0F, 1.0F);
        }

        public IColor Color { get; private set; }

        public float Opacity { get; private set; }

        public string ToUniqueKey()
        {
            return "BRU[{0};OPA={1:0.000}]".WithInvariantFormat(
                this.Color.ToUniqueKey(),
                this.Opacity);
        }
    }
}