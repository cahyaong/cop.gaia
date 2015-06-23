// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 12:34:48 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using Caliburn.Micro;
    using nGratis.Cop.Core;
    using nGratis.Cop.Core.Contract;

    internal class AppBootstrapper : BootstrapperBase
    {
        private readonly IModuleProvider moduleProvider = new CopModuleProvider();

        private CompositionContainer mefContainer;

        public AppBootstrapper()
        {
            this.Initialize();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return Enumerable
                .Empty<Assembly>()
                .Prepend(Assembly.GetExecutingAssembly())
                .Union(this.moduleProvider.FindInternalAssemblies())
                .Union(this.moduleProvider.FindModuleAssemblies());
        }

        protected override void Configure()
        {
            var catalogs = this
                .SelectAssemblies()
                .Select(assembly => new AssemblyCatalog(assembly));

            this.mefContainer = new CompositionContainer(
                new AggregateCatalog(catalogs),
                CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);

            var caliburnBatch = new CompositionBatch();
            caliburnBatch.AddExport<IWindowManager>(() => new WindowManager());
            caliburnBatch.AddExport<IInfrastructureManager>(() => InfrastructureManager.Instance);

            this.mefContainer.Compose(caliburnBatch);
        }

        protected override object GetInstance(Type type, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(type) : key;
            var exports = this.mefContainer.GetExportedValues<object>(contract);

            return exports.Single();
        }

        protected override IEnumerable<object> GetAllInstances(Type type)
        {
            var contract = AttributedModelServices.GetContractName(type);

            return this.mefContainer.GetExportedValues<object>(contract);
        }

        protected override void BuildUp(object instance)
        {
            this.mefContainer.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs args)
        {
            this.DisplayRootViewFor<MainViewModel>();
        }
    }
}