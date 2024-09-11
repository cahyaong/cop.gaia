// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyBindingCollection.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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