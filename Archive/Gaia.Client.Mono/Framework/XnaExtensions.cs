// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XnaExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 11 August 2015 1:02:00 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using Microsoft.Xna.Framework;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    internal static class XnaExtensions
    {
        public static Vector2 ToXnaVector2(this Vector vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector2 ToXnaVector2(this nGratis.Cop.Gaia.Engine.Data.Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Color ToXnaColor(this IColor color)
        {
            var rgbColor = (RgbColor)color;

            return new Color
            {
                R = (byte)rgbColor.Red,
                G = (byte)rgbColor.Green,
                B = (byte)rgbColor.Blue
            };
        }
    }
}