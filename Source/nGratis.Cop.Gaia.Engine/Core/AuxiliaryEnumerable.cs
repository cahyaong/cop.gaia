// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="AuxiliaryEnumerable.cs" company="nGratis">
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
// <creation_timestamp>Saturday, 27 June 2015 12:59:51 AM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System.Collections.Generic;

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