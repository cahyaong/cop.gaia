// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopModuleProvider.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 - 2015 Cahya Ong
//
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
//   associated documentation files (the "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
//   following conditions:
//
//   The above copyright notice and this permission notice shall be included in all copies or substantial
//   portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
//   LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO
//   EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR
//   THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
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