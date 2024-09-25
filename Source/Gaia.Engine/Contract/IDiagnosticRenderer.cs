// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDiagnosticRenderer.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, September 17, 2024 3:19:42 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine;

public interface IDiagnosticRenderer
{
    void UpdateStatistic(string key, string value);
}