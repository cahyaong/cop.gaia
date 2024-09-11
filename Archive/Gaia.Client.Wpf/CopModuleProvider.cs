// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopModuleProvider.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 12:36:23 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using nGratis.Cop.Core.Contract;

    [Export(typeof(IModuleProvider))]
    internal class CopModuleProvider : IModuleProvider
    {
        public IEnumerable<Assembly> FindModuleAssemblies()
        {
            var assemblies = Enumerable.Empty<Assembly>();
            var moduleFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Modules");

            if (Directory.Exists(moduleFolderPath))
            {
                assemblies = Directory
                    .GetFiles(moduleFolderPath, "nGratis.Cop.Gaia.Module.*.dll")
                    .Select(Assembly.LoadFile);
            }

            return assemblies;
        }

        public IEnumerable<Assembly> FindInternalAssemblies()
        {
            var assemblies = Directory
                 .GetFiles(Directory.GetCurrentDirectory(), "nGratis.Cop.*.dll")
                 .Where(file => !file.Contains("Module"))
                 .Select(Assembly.LoadFile);

            return assemblies;
        }
    }
}