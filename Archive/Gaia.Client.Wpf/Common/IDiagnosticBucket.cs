// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDiagnosticBucket.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 29 June 2015 12:37:55 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Collections.Generic;

    public interface IDiagnosticBucket
    {
        IEnumerable<DiagnosticItem> Items { get; }

        void AddOrUpdateItem(DiagnosticKey key, object value);
    }
}