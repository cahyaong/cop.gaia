// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 12:27:12 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Windows;
    using System.Windows.Media;
    using FirstFloor.ModernUI.Presentation;

    public partial class App
    {
        protected override void OnStartup(StartupEventArgs args)
        {
            AppearanceManager.Current.AccentColor = Colors.CornflowerBlue;

            base.OnStartup(args);
        }
    }
}