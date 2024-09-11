// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RgbColor.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 8 July 2015 11:26:09 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System;

    public struct RgbColor : IColor
    {
        public static readonly RgbColor Default = new RgbColor(0, 0, 0);

        public static readonly RgbColor CornflowerBlue = new RgbColor(100, 149, 237);

        public RgbColor(double red, double green, double blue)
            : this()
        {
            this.Red = (int)(red.Clamp(0.0, 1.0) * 255.0);
            this.Green = (int)(green.Clamp(0.0, 1.0) * 255.0);
            this.Blue = (int)(blue.Clamp(0.0, 1.0) * 255.0);
        }

        public RgbColor(int red, int green, int blue)
            : this()
        {
            this.Red = red.Clamp(0, 255);
            this.Green = green.Clamp(0, 255);
            this.Blue = blue.Clamp(0, 255);
        }

        public int Red { get; private set; }

        public int Green { get; private set; }

        public int Blue { get; private set; }

        public static explicit operator RgbColor(HsvColor hsvColor)
        {
            var sextant = (int)Math.Floor(hsvColor.Hue / 60.0) % 6;
            var epsilon = (hsvColor.Hue / 60.0) - Math.Floor(hsvColor.Hue / 60.0);

            var alpha = hsvColor.Value * (1.0 - hsvColor.Saturation);
            var beta = hsvColor.Value * (1.0 - (epsilon * hsvColor.Saturation));
            var gamma = hsvColor.Value * (1.0 - ((1.0 - epsilon) * hsvColor.Saturation));

            var rgbColor = default(RgbColor);

            switch (sextant)
            {
                case 0:
                    rgbColor = new RgbColor(hsvColor.Value, gamma, alpha);
                    break;

                case 1:
                    rgbColor = new RgbColor(beta, hsvColor.Value, alpha);
                    break;

                case 2:
                    rgbColor = new RgbColor(alpha, hsvColor.Value, gamma);
                    break;

                case 3:
                    rgbColor = new RgbColor(alpha, beta, hsvColor.Value);
                    break;

                case 4:
                    rgbColor = new RgbColor(gamma, alpha, hsvColor.Value);
                    break;

                case 5:
                    rgbColor = new RgbColor(hsvColor.Value, alpha, beta);
                    break;

                default:
                    rgbColor = new RgbColor(0.0, 0.0, 0.0);
                    break;
            }

            return rgbColor;
        }

        public string ToUniqueKey()
        {
            return "RGB[RED={0:000};GRE={1:000};BLU={2:000}]".WithInvariantFormat(
                this.Red,
                this.Green,
                this.Blue);
        }
    }
}