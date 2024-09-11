// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 12:34:48 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
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