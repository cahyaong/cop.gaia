// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InteractionExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, 28 June 2015 1:12:31 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Windows.Input;

    internal static class InteractionExtensions
    {
        public static bool IsPanning(this Key key)
        {
            return key == Key.A || key == Key.D || key == Key.W || key == Key.S;
        }
    }
}