// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlendingModifier.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 4 July 2015 1:09:10 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Noise
{
    using nGratis.Cop.Gaia.Engine.Core;

    public class BlendingModifier : INoiseModule
    {
        private readonly INoiseModule firstModule;

        private readonly INoiseModule secondModule;

        private readonly INoiseModule weightingModule;

        public BlendingModifier(INoiseModule firstModule, INoiseModule secondModule, INoiseModule weightingModule)
        {
            RapidGuard.AgainstNullArgument(firstModule);
            RapidGuard.AgainstNullArgument(secondModule);
            RapidGuard.AgainstNullArgument(weightingModule);

            this.firstModule = firstModule;
            this.secondModule = secondModule;
            this.weightingModule = weightingModule;
        }

        public double GetValue(double x, double y, double z)
        {
            return AuxiliaryMath.InterpolateLinear(
                this.firstModule.GetValue(x, y, z),
                this.secondModule.GetValue(x, y, z),
                this.weightingModule.GetValue(x, y, z));
        }
    }
}