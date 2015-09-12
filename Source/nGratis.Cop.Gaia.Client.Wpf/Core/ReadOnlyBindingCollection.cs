// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyBindingCollection.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 11 July 2015 12:56:35 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Windows;
    using nGratis.Cop.Core.Contract;

    public class ReadOnlyBindingCollection : FreezableCollection<ReadOnlyBinding>
    {
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.RegisterAttached(
            "AttachedItems",
            typeof(ReadOnlyBindingCollection),
            typeof(ReadOnlyBindingCollection),
            new PropertyMetadata(null));

        public ReadOnlyBindingCollection(object owner)
        {
            Guard.AgainstNullArgument(() => owner);

            this.Owner = owner;
            this.InitializeEventHandlers();
        }

        public object Owner { get; private set; }

        public static ReadOnlyBindingCollection GetItems(FrameworkElement frameworkElement)
        {
            Guard.AgainstNullArgument(() => frameworkElement);

            var items = (ReadOnlyBindingCollection)frameworkElement.GetValue(ItemsProperty);

            if (items == null)
            {
                items = new ReadOnlyBindingCollection(frameworkElement);
                frameworkElement.SetValue(ItemsProperty, items);
            }

            return items;
        }

        public static void SetItems(FrameworkElement frameworkElement, ReadOnlyBindingCollection items)
        {
            Guard.AgainstNullArgument(() => frameworkElement);

            frameworkElement.SetValue(ItemsProperty, items ?? new ReadOnlyBindingCollection(frameworkElement));
        }

        private void InitializeEventHandlers()
        {
            var whenCollectionChanged = Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                handler => ((INotifyCollectionChanged)this).CollectionChanged += handler,
                handler => ((INotifyCollectionChanged)this).CollectionChanged -= handler);

            whenCollectionChanged
                .Where(pattern => pattern.EventArgs.Action == NotifyCollectionChangedAction.Add)
                .Subscribe(pattern => pattern
                    .EventArgs
                    .NewItems
                    .Cast<ReadOnlyBinding>()
                    .ForEach(binding => binding.Establish(this.Owner)));

            whenCollectionChanged
                .Where(pattern => pattern.EventArgs.Action == NotifyCollectionChangedAction.Remove)
                .Subscribe(pattern => pattern
                    .EventArgs
                    .OldItems
                    .Cast<ReadOnlyBinding>()
                    .ForEach(binding => binding.Destroy()));
        }
    }
}