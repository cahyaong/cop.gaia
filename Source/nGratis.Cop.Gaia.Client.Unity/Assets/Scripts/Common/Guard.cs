// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Throw.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 - 2016 Cahya Ong
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
// <creation_timestamp>Saturday, 9 April 2016 5:40:34 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System;
    using System.Diagnostics;

    public static class Guard
    {
        public static class Argument
        {
            [DebuggerStepThrough]
            public static void IsNull(object value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
            }

            [DebuggerStepThrough]
            public static void IsZeroOrNegative(int value)
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Value cannot be zero or negative.");
                }
            }

            [DebuggerStepThrough]
            public static void IsZeroOrNegative(float value)
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Value cannot be zero or negative.");
                }
            }

            [DebuggerStepThrough]
            public static void IsNegative(int value)
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value cannot be negative.");
                }
            }

            [DebuggerStepThrough]
            public static void IsNegative(float value)
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value cannot be negative.");
                }
            }
        }

        public static class Operation
        {
            [DebuggerStepThrough]
            public static void IsUnexpectedNull(object value)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Value cannot be null");
                }
            }

            [DebuggerStepThrough]
            public static void IsInvalidState(bool isInvalid)
            {
                if (isInvalid)
                {
                    throw new InvalidOperationException("Operation state is not valid");
                }
            }
        }
    }
}