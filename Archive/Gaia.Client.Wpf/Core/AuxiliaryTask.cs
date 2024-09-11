// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuxiliaryTask.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 3 June 2015 1:17:15 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    public static class AuxiliaryTask
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> callbackAsync)
        {
            return AuxiliaryTask
                .TaskFactory
                .StartNew(callbackAsync)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        public static void RunSync(Func<Task> callbackAsync)
        {
            AuxiliaryTask
                .TaskFactory
                .StartNew(callbackAsync)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        public static void RunOnUi(Action callback)
        {
            if (!Dispatcher.CurrentDispatcher.CheckAccess())
            {
                Dispatcher.CurrentDispatcher.Invoke(callback);
            }
        }
    }
}