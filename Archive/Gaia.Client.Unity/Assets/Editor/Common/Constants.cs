// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 9 April 2016 7:23:04 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using UnityEngine;

    public class Constants
    {
        internal static class WpfColors
        {
            public static readonly Color PaleGreen = new Color32(0x98, 0xFB, 0x98, 0xFF);

            public static readonly Color PaleVioletRed = new Color32(0xDB, 0x70, 0x93, 0xFF);
        }

        public static class TileMap
        {
            public static readonly Color ModifiedColor = WpfColors.PaleVioletRed;

            public static readonly Color AppliedColor = WpfColors.PaleGreen;

            public const float DashLength = 1f;
        }
    }
}