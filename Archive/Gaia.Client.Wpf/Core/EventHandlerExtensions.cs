// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventHandlerExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 14 July 2015 1:36:38 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;

    public static class EventHandlerExtensions
    {
        public static void FireEvent(this EventHandler<EventArgs> handler, object sender, EventArgs args = null)
        {
            if (handler != null)
            {
                handler(sender, args ?? EventArgs.Empty);
            }
        }

        public static void FireEvent<TArgs>(this EventHandler<TArgs> handler, object sender, TArgs args)
            where TArgs : EventArgs
        {
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}