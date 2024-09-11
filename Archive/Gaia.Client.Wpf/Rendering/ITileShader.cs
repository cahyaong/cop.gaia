// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITileShader.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 2 June 2015 12:27:30 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using nGratis.Cop.Gaia.Engine;

    public interface ITileShader
    {
        IColor FindColor(int value);
    }
}