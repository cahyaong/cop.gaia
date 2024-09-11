// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AweTile.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 24 June 2015 1:58:41 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using nGratis.Cop.Core.Wpf;

    [TemplatePart(Name = "PART_Border", Type = typeof(Border))]
    public class AweTile : ContentControl
    {
        public static readonly DependencyProperty AccentColorProperty = DependencyProperty.Register(
            "AccentColor",
            typeof(Color),
            typeof(AweTile),
            new PropertyMetadata(Colors.CornflowerBlue));

        public static readonly DependencyProperty MeasurementProperty = DependencyProperty.Register(
            "Measurement",
            typeof(Measurement),
            typeof(AweTile),
            new PropertyMetadata(Measurement.M));

        public Color AccentColor
        {
            get { return (Color)this.GetValue(AccentColorProperty); }
            set { this.SetValue(AccentColorProperty, value); }
        }

        public Measurement Measurement
        {
            get { return (Measurement)this.GetValue(MeasurementProperty); }
            set { this.SetValue(MeasurementProperty, value); }
        }
    }
}