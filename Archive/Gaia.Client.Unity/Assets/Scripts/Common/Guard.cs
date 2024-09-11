// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Throw.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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