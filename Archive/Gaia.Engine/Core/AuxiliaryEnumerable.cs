// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuxiliaryEnumerable.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 27 June 2015 12:59:51 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System.Collections.Generic;
    using nGratis.Cop.Core.Contract;

    public static class AuxiliaryEnumerable
    {
        public static IEnumerable<int> Step(int start, int end, int size)
        {
            Guard.AgainstInvalidArgument(start < 0, () => start);
            Guard.AgainstInvalidArgument(start >= end, () => start);
            Guard.AgainstInvalidArgument(size <= 0, () => size);
            Guard.AgainstInvalidOperation(end + size >= int.MaxValue);

            for (var index = start; index < end; index += size)
            {
                yield return index;
            }
        }

        public static IEnumerable<uint> Range(uint start, uint count)
        {
            Guard.AgainstInvalidOperation(start + count >= uint.MaxValue);

            for (var index = 0U; index < count; index++)
            {
                yield return start + index;
            }
        }

        public static IEnumerable<uint> Step(uint start, uint end, uint size)
        {
            Guard.AgainstInvalidArgument(start >= end, () => start);
            Guard.AgainstInvalidArgument(size == 0, () => size);
            Guard.AgainstInvalidOperation(end + size >= uint.MaxValue);

            for (var index = start; index < end; index += size)
            {
                yield return index;
            }
        }
    }
}