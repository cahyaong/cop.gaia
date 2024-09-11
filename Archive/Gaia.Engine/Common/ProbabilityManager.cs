// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProbabilityManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 7 September 2015 12:28:03 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.ComponentModel.Composition;

    [Export(typeof(IManager))]
    public class ProbabilityManager : BaseManager, IProbabilityManager
    {
        private readonly Random random;

        [ImportingConstructor]
        public ProbabilityManager()
            : this(Environment.TickCount)
        {
        }

        public ProbabilityManager(int seed)
        {
            this.random = new Random(seed);
        }

        public float Roll()
        {
            return (float)this.random.NextDouble();
        }

        public float Roll(float min, float max)
        {
            return (float)((this.random.NextDouble() * (max - min)) + min);
        }
    }
}