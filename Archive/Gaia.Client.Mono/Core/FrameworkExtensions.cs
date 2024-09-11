// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 1:40:37 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono.Core
{
    using Microsoft.Xna.Framework;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Core;

    internal static class FrameworkExtensions
    {
        public static Clock ToCopClock(this GameTime gameTime)
        {
            RapidGuard.AgainstNullArgument(gameTime);

            return new Clock(gameTime.TotalGameTime, gameTime.ElapsedGameTime);
        }
    }
}