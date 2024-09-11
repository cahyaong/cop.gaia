// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWorldGenerator.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 29 June 2015 12:32:24 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Threading.Tasks;
    using nGratis.Cop.Gaia.Engine;

    public interface IWorldGenerator
    {
        Task<WorldMap> GenerateMapAsync(string seed);
    }
}