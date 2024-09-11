// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectxViewModel.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 27 July 2015 2:09:25 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf.Sdk
{
    using System.ComponentModel.Composition;
    using nGratis.Cop.Gaia.Client.Wpf.Framework;
    using ReactiveUI;

    [Export]
    public class DirectxViewModel : ReactiveObject
    {
        private IRenderingManager renderingManager;

        [ImportingConstructor]
        public DirectxViewModel()
        {
            this.RenderingManager = new SdkRenderingManager();
        }

        public IRenderingManager RenderingManager
        {
            get { return this.renderingManager; }
            private set { this.RaiseAndSetIfChanged(ref this.renderingManager, value); }
        }
    }
}