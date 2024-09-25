// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticRenderer.cs" company="nGratis">
//  The MIT License -- Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong -- cahya.ong@gmail.com</author>
// <creation_timestamp>Sunday, September 15, 2024 3:16:10 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Godot;
using nGratis.Cop.Gaia.Engine;

public partial class DiagnosticRenderer : CanvasLayer, IDiagnosticRenderer
{
    private static class Default
    {
        public const string StatisticLabel = "<DR.STATISTIC>";
    }

    private readonly IDictionary<string, string> _statisticValueByKeyLookup;

    private RichTextLabel _statisticLabel;

    public DiagnosticRenderer()
    {
        this._statisticValueByKeyLookup = new Dictionary<string, string>();
    }

    public override void _Ready()
    {
        this._statisticLabel = this.GetNode<RichTextLabel>("StatisticLabel");
    }

    public override void _Process(double _)
    {
        var formattedStatisticChunks = this
            ._statisticValueByKeyLookup
            .Select(pair => $"[b]{pair.Key}:[/b] {pair.Value}")
            .ToImmutableArray();

        this._statisticLabel.Text = formattedStatisticChunks.Any()
            ? string.Join(System.Environment.NewLine, formattedStatisticChunks)
            : Default.StatisticLabel;
    }

    public void UpdateStatistic(string key, string value)
    {
        this._statisticValueByKeyLookup[key] = value;
    }
}