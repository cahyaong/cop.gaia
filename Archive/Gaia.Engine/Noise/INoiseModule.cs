// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INoiseModule.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 18 May 2015 2:08:05 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Noise
{
    public interface INoiseModule
    {
        double GetValue(double x, double y, double z);
    }
}