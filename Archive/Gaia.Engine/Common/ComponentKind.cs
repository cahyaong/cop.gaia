// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentKind.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 18 August 2015 12:34:26 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System.Collections;
    using System.Linq;
    using nGratis.Cop.Core.Contract;

    public enum ComponentKind
    {
        None = 0,
        Brain,
        Statistic = 10,
        Constitution,
        Trait,
        Placement = 20,
        Physics
    }

    public sealed class ComponentKinds
    {
        public static readonly ComponentKinds None = new ComponentKinds();

        public static readonly ComponentKinds Any = new ComponentKinds();

        private readonly BitArray bits;

        public ComponentKinds(params ComponentKind[] kinds)
        {
            Guard.AgainstInvalidArgument(kinds.Any(kind => kind == ComponentKind.None), () => kinds);

            this.bits = new BitArray(128);

            kinds
                .Distinct()
                .ToList()
                .ForEach(kind => this.bits.Set((int)kind, true));
        }

        public bool HasFlag(ComponentKind kind)
        {
            Guard.AgainstInvalidArgument(kind == ComponentKind.None, () => kind);

            return this.bits.Get((int)kind);
        }

        public bool HasFlags(ComponentKinds kinds)
        {
            if (kinds == Any)
            {
                return true;
            }

            return kinds
                .bits
                .OfType<bool>()
                .Select((bit, index) => new { Index = index, Bit = bit })
                .Where(anon => anon.Bit)
                .All(anon => this.bits.Get(anon.Index));
        }
    }
}