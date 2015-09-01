// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentKind.cs" company="nGratis">
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
        Statistic = 1,
        Constitution = 3,
        Trait = 7,
        Placement = 8
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
                .Where(annon => annon.Bit)
                .All(annon => this.bits.Get(annon.Index));
        }
    }
}