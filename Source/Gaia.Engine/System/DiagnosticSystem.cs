// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticSystem.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, September 17, 2024 3:18:49 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine;

public class DiagnosticSystem : ISystem
{
    private readonly IDiagnosticRenderer _diagnosticRenderer;

    public DiagnosticSystem(IDiagnosticRenderer diagnosticRenderer)
    {
        this._diagnosticRenderer = diagnosticRenderer;
    }

    public void Process(uint tick)
    {
        this._diagnosticRenderer.UpdateStatistic("Tick", tick.ToString());
    }
}