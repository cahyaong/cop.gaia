// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Guard.cs" company="nGratis">
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
// <creation_timestamp>Wednesday, 27 May 2015 1:11:11 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Core
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using JetBrains.Annotations;

    public static class Guard
    {
        [DebuggerStepThrough]
        public static void AgainstNullArgument<T>([NotNull][InstantHandle] Expression<Func<T>> argumentExpression, Func<string> getReason = null)
            where T : class
        {
            if (argumentExpression.Compile()() == null)
            {
                var argument = argumentExpression.FindName();
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_NullArgument.WithCurrentFormat(argument),
                    !string.IsNullOrEmpty(reason) ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason) : string.Empty);

                Throw.ArgumentNullException(argument, message);
            }
        }

        [DebuggerStepThrough]
        public static void AgainstDefaultArgument<T>([NotNull][InstantHandle] Expression<Func<T>> argumentExpression, Func<string> getReason = null)
        {
            if (argumentExpression.Compile()().Equals(default(T)))
            {
                var argument = argumentExpression.FindName();
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_DefaultArgument.WithCurrentFormat(argument),
                    !string.IsNullOrEmpty(reason) ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason) : string.Empty);

                Throw.ArgumentException(argument, message);
            }
        }

        [DebuggerStepThrough]
        public static void AgainstNullOrWhitespaceArgument([NotNull][InstantHandle] Expression<Func<string>> argumentExpression, Func<string> getReason = null)
        {
            var value = argumentExpression.Compile()();

            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value))
            {
                var argument = argumentExpression.FindName();
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_StringNullOrWhitespace.WithCurrentFormat(argument),
                    !string.IsNullOrEmpty(reason) ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason) : string.Empty);

                Throw.ArgumentException(argument, message);
            }
        }

        [DebuggerStepThrough]
        [ContractAnnotation("isInvalid:true => halt")]
        public static void AgainstInvalidArgument<T>(bool isInvalid, [InstantHandle] Expression<Func<T>> argumentExpression, Func<string> getReason = null)
        {
            if (isInvalid)
            {
                var argument = argumentExpression.FindName();
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_InvalidArgument.WithCurrentFormat(argument),
                    !string.IsNullOrEmpty(reason) ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason) : string.Empty);

                Throw.ArgumentException(argument, message);
            }
        }

        [DebuggerStepThrough]
        [ContractAnnotation(" => halt")]
        public static void AgainstInvalidOperation(Func<string> getReason = null)
        {
            var reason = getReason != null ? getReason() : null;

            var message = "{0}{1}".WithCurrentFormat(
                Messages.Guard_Exception_InvalidOperation,
                !string.IsNullOrEmpty(reason) ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason) : string.Empty);

            Throw.InvalidOperationException(message);
        }

        [DebuggerStepThrough]
        [ContractAnnotation("isInvalid:true => halt")]
        public static void AgainstInvalidOperation(bool isInvalid, Func<string> getReason = null)
        {
            if (isInvalid)
            {
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_InvalidOperation,
                    !string.IsNullOrEmpty(reason) ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason) : string.Empty);

                Throw.InvalidOperationException(message);
            }
        }

        [DebuggerStepThrough]
        [ContractAnnotation("value:null => halt")]
        public static void AgainstUnexpectedNullValue<T>(T value, Func<string> getReason = null)
            where T : class
        {
            if (value == null)
            {
                var reason = getReason != null ? getReason() : null;

                var message = "{0}{1}".WithCurrentFormat(
                    Messages.Guard_Exception_UnexpectedNullValue,
                    !string.IsNullOrEmpty(reason) ? Environment.NewLine + Messages.Guard_Label_Reason.WithCurrentFormat(reason) : string.Empty);

                Throw.InvalidOperationException(message);
            }
        }
    }
}