// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticItem.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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