// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RapidGuard.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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