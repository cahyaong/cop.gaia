// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MefExtensions.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace System.ComponentModel.Composition
// ReSharper restore CheckNamespace
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;

    public static class MefExtensions
    {
        public static void AddExport<TKey>(this CompositionBatch compositionBatch, Func<object> createInstance)
        {
            var typeName = typeof(TKey).FullName;

            var export = new Export(
                new ExportDefinition(typeName, new Dictionary<string, object> { { "ExportTypeIdentity", typeName } }),
                createInstance);

            compositionBatch.AddExport(export);
        }
    }
}