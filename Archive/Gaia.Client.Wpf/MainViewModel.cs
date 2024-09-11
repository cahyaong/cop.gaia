// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 12:34:02 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.ComponentModel.Composition;
    using ReactiveUI;

    [Export]
    public class MainViewModel : ReactiveObject
    {
        public MainViewModel()
        {
        }
    }
}