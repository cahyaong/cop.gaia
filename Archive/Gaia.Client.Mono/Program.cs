// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 10 August 2015 9:30:08 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Mono
{
    using System;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Data;

    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            var mefContainer = new CompositionContainer(
                new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new DirectoryCatalog(Directory.GetCurrentDirectory(), "nGratis.Cop.*.dll")),
                CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);

            var gameInfrastructure = mefContainer.GetExportedValue<IGameInfrastructure>();
            var gameSpecification = new GameSpecification(new Size(64, 36), 20);

            using (var mainGame = new MainGame())
            {
                mainGame.Initialize(
                    gameSpecification,
                    gameInfrastructure,
                    mefContainer.GetExportedValues<ISystem>().ToList());

                mainGame.Run();
            }
        }
    }
}