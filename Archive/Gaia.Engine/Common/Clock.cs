// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Clock.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 4 August 2015 1:29:31 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using nGratis.Cop.Gaia.Engine.Core;

    public struct Clock
    {
        public Clock(TimeSpan totalDuration, TimeSpan elapsedDuration)
            : this((float)totalDuration.TotalSeconds, (float)elapsedDuration.TotalSeconds)
        {
        }

        public Clock(float totalDuration, float elapsedDuration)
            : this()
        {
            RapidGuard.AgainstInvalidArgument(totalDuration < 0);
            RapidGuard.AgainstInvalidArgument(elapsedDuration < 0);

            this.TotalDuration = totalDuration;
            this.ElapsedDuration = elapsedDuration;
        }

        public float TotalDuration { get; private set; }

        public float ElapsedDuration { get; private set; }
    }
}