﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RapidGuard.cs" company="nGratis">
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
// <creation_timestamp>Thursday, 20 August 2015 12:01:08 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System;
    using System.Diagnostics;
    using JetBrains.Annotations;
    using nGratis.Cop.Core.Contract;

    public static class RapidGuard
    {
        private const string UnknownArgument = "Unknown";

        [DebuggerStepThrough]
        [ContractAnnotation("argument:null => halt")]
        public static void AgainstNullArgument<T>(T argument, Func<string> getReason = null)
            where T : class
        {
            if (argument == null)
            {
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_NullArgument.WithCurrentFormat(UnknownArgument),
                    !string.IsNullOrEmpty(reason)
                        ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason)
                        : string.Empty);

                Throw.ArgumentNullException(UnknownArgument, message);
            }
        }

        [DebuggerStepThrough]
        [ContractAnnotation("isInvalid:true => halt")]
        public static void AgainstInvalidArgument(bool isInvalid, Func<string> getReason = null)
        {
            if (isInvalid)
            {
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_InvalidArgument.WithCurrentFormat(UnknownArgument),
                    !string.IsNullOrWhiteSpace(reason)
                        ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason)
                        : string.Empty);

                Throw.ArgumentException(UnknownArgument, message);
            }
        }

        [DebuggerStepThrough]
        public static void AgainstNegativeArgument<T>(T argument, Func<string> getReason = null)
            where T : struct, IConvertible, IComparable<T>
        {
            if (argument.CompareTo(default(T)) < 0)
            {
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_NegativeArgument.WithCurrentFormat(UnknownArgument),
                    !string.IsNullOrEmpty(reason)
                        ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason)
                        : string.Empty);

                Throw.ArgumentException(UnknownArgument, message);
            }
        }
    }
}