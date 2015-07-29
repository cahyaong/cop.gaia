// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticBucket.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 25 June 2015 1:12:26 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
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