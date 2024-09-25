// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HeadUpDisplay.cs" company="nGratis">
//  The MIT License -- Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong -- cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, September 24, 2024 6:38:11 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

using Godot;
using nGratis.Cop.Gaia.Engine;

public partial class HeadUpDisplay : Node
{
    public IDiagnosticRenderer DiagnosticRenderer => this.GetNode<DiagnosticRenderer>("DiagnosticRenderer");
}