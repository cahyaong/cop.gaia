// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HsvColor.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 8 July 2015 11:33:06 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System;

    public struct HsvColor : IColor
    {
        public HsvColor(double hue, double saturation, double value)
            : this()
        {
            this.Hue = hue.Clamp(0.0, 360.0);
            this.Saturation = saturation.Clamp(0.0, 1.0);
            this.Value = value.Clamp(0.0, 1.0);
        }

        public double Saturation { get; private set; }

        public double Value { get; private set; }

        public double Hue { get; private set; }

        public static explicit operator HsvColor(RgbColor rgbColor)
        {
            var red = rgbColor.Red / 255.0;
            var green = rgbColor.Green / 255.0;
            var blue = rgbColor.Blue / 255.0;

            var min = AuxiliaryMath.Min(red, green, blue);
            var max = AuxiliaryMath.Max(red, green, blue);
            var chroma = max - min;

            var hue = 0.0;

            if (max.IsCloseTo(rgbColor.Red))
            {
                hue = (((rgbColor.Green - rgbColor.Blue) / chroma) % 6.0) * 60.0;
            }
            else if (max.IsCloseTo(rgbColor.Green))
            {
                hue = (((rgbColor.Blue - rgbColor.Red) / chroma) + 2.0) * 60.0;
            }
            else if (max.IsCloseTo(rgbColor.Blue))
            {
                hue = (((rgbColor.Red - rgbColor.Green) / chroma) + 4.0) * 60.0;
            }

            var saturation = max <= 0.0 ? 0.0 : chroma / max;
            var value = max;

            return new HsvColor(hue, saturation, value);
        }

        public string ToUniqueKey()
        {
            return "HSV[HUE={0:0.000};VAL={1:0.000};SAT={2:0.000}]".WithInvariantFormat(
                this.Hue,
                this.Saturation,
                this.Value);
        }
    }
}