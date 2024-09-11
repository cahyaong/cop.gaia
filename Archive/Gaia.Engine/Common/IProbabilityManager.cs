// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProbabilityManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 7 September 2015 12:25:14 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public interface IProbabilityManager : IManager
    {
        float Roll();

        float Roll(float min, float max);
    }
}