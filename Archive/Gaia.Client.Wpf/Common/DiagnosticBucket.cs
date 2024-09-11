// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticBucket.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 25 June 2015 1:12:26 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using nGratis.Cop.Core.Contract;
    using ReactiveUI;

    [Export(typeof(IDiagnosticBucket))]
    public class DiagnosticBucket : ReactiveObject, IDiagnosticBucket
    {
        private IEnumerable<DiagnosticItem> items;

        [ImportingConstructor]
        public DiagnosticBucket()
        {
            this.Items = new ObservableCollection<DiagnosticItem>();
        }

        public IEnumerable<DiagnosticItem> Items
        {
            get { return this.items; }
            private set { this.RaiseAndSetIfChanged(ref this.items, value); }
        }

        public void AddOrUpdateItem(DiagnosticKey key, object value)
        {
            Guard.AgainstNullArgument(() => key);
            Guard.AgainstInvalidArgument(key == DiagnosticKey.Unknown, () => key);

            var matchedItem = this
                .Items
                .SingleOrDefault(item => item.Key == key);

            if (matchedItem == null)
            {
                this.Items = this.Items.Append(new DiagnosticItem(key, value));
            }
            else
            {
                matchedItem.Value = value;
            }
        }
    }
}