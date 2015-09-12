// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyBinding.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 11 July 2015 12:12:52 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Windows;
    using System.Windows.Data;
    using nGratis.Cop.Core.Contract;

    public class ReadOnlyBinding : Freezable
    {
        public static readonly DependencyProperty SourcePathProperty = DependencyProperty.Register(
            "SourcePath",
            typeof(string),
            typeof(ReadOnlyBinding),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SourceValueProperty = DependencyProperty.Register(
            "SourceValue",
            typeof(object),
            typeof(ReadOnlyBinding),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TargetPathProperty = DependencyProperty.Register(
            "TargetPath",
            typeof(string),
            typeof(ReadOnlyBinding),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TargetValueProperty = DependencyProperty.Register(
            "TargetValue",
            typeof(object),
            typeof(ReadOnlyBinding),
            new PropertyMetadata(null, OnTargetValueChanged));

        public string SourcePath
        {
            get { return (string)this.GetValue(SourcePathProperty); }
            set { this.SetValue(SourcePathProperty, value); }
        }

        public object SourceValue
        {
            get { return this.GetValue(SourceValueProperty); }
            private set { this.SetValue(SourceValueProperty, value); }
        }

        public string TargetPath
        {
            get { return (string)this.GetValue(TargetPathProperty); }
            set { this.SetValue(TargetPathProperty, value); }
        }

        public object TargetValue
        {
            get { return this.GetValue(TargetValueProperty); }
            private set { this.SetValue(TargetValueProperty, value); }
        }

        public void Establish(object target)
        {
            Guard.AgainstNullArgument(() => target);

            var sourcePath = this.SourcePath;
            var targetPath = this.TargetPath;

            if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(targetPath))
            {
                // TODO: Add logging when source or target path is not valid!
                return;
            }

            var sourceBinding = new Binding
                {
                    Path = new PropertyPath(sourcePath),
                    Mode = BindingMode.OneWayToSource
                };

            BindingOperations.SetBinding(this, SourceValueProperty, sourceBinding);

            var targetBinding = new Binding
                {
                    Source = target,
                    Path = new PropertyPath(targetPath),
                    Mode = BindingMode.OneWay
                };

            BindingOperations.SetBinding(this, TargetValueProperty, targetBinding);

            this.SourceValue = this.TargetValue;
        }

        public void Destroy()
        {
            BindingOperations.ClearBinding(this, SourceValueProperty);
            BindingOperations.ClearBinding(this, TargetValueProperty);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new ReadOnlyBinding();
        }

        private static void OnTargetValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var binding = dependencyObject as ReadOnlyBinding;

            if (binding != null)
            {
                binding.SourceValue = binding.TargetValue;
            }
        }
    }
}