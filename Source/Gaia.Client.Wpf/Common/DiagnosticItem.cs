// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticItem.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 25 June 2015 11:48:05 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using nGratis.Cop.Core.Contract;
    using ReactiveUI;

    public class DiagnosticItem : ReactiveObject
    {
        private DiagnosticKey key;

        private string name;

        private object value;

        private string formattedValue;

        public DiagnosticItem(DiagnosticKey key, object value)
        {
            Guard.AgainstNullArgument(() => key);
            Guard.AgainstInvalidArgument(key == DiagnosticKey.Unknown, () => key);

            this.Key = key;
            this.Value = value;
        }

        public DiagnosticKey Key
        {
            get
            {
                return this.key;
            }

            private set
            {
                this.RaiseAndSetIfChanged(ref this.key, value);
                this.Name = value.Name;
            }
        }

        public string Name
        {
            get { return this.name; }
            private set { this.RaiseAndSetIfChanged(ref this.name, value); }
        }

        public object Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.value, value);
                this.FormattedValue = this.Key.FormatValue(value);
            }
        }

        public string FormattedValue
        {
            get { return this.formattedValue; }
            private set { this.RaiseAndSetIfChanged(ref this.formattedValue, value); }
        }
    }
}