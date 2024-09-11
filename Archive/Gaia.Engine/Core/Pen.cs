// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pen.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 1 August 2015 12:15:46 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System;

    public struct Pen
    {
        public static Pen Null = new Pen(RgbColor.Default, 0.0F, 0.0F);

        public Pen(IColor color, float opacity, float thickness)
            : this()
        {
            this.Color = color ?? RgbColor.Default;
            this.Opacity = opacity.Clamp(0.0F, 1.0F);
            this.Thickness = Math.Max(thickness, 0.0F);
        }

        public IColor Color { get; private set; }

        public float Opacity { get; private set; }

        public float Thickness { get; private set; }

        public string ToUniqueKey()
        {
            return "PEN[{0};OPA={1:0.000};THI={2:0.000}]".WithInvariantFormat(
                this.Color.ToUniqueKey(),
                this.Opacity,
                this.Thickness);
        }
    }
}