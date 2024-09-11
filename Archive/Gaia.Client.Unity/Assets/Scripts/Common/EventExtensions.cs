// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 7 May 2016 4:52:52 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System;

    internal static class EventExtensions
    {
        public static void Raise(this EventHandler eventHandler)
        {
            eventHandler?.Invoke(null, EventArgs.Empty);
        }
    }
}