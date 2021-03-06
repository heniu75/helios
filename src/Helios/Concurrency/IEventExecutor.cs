﻿// Copyright (c) Petabridge <https://petabridge.com/>. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.
// See ThirdPartyNotices.txt for references to third party code used inside Helios.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helios.Concurrency
{
    /// <summary>
    ///     An abstraction built on top of an underlying <see cref="IEventExecutor" />
    /// </summary>
    public interface IWrappedEventExecutor : IEventExecutor
    {
    }

    /// <summary>
    ///     Executes events generated by Helios channels and transports
    /// </summary>
    public interface IEventExecutor
    {
        /// <summary>
        ///     Returns <c>true</c> if the current <see cref="Thread" /> belongs to this event loop,
        ///     <c>false</c> otherwise.
        /// </summary>
        /// <remarks>
        ///     Used to determine if we can safely execute an operation or not, or if we need to schedule
        ///     it in the future.
        /// </remarks>
        bool InEventLoop { get; }

        /// <summary>
        ///     Gets a <see cref="Task" /> object that completes once this <see cref="IEventExecutor" /> has been terminated.
        /// </summary>
        Task TerminationTask { get; }

        /// <summary>
        ///     <c>true</c> if this <see cref="IEventExecutor" /> is in the process of being terminated.
        /// </summary>
        bool IsShuttingDown { get; }

        /// <summary>
        ///     <c>true</c> if this <see cref="IEventExecutor" /> has finished shutting down.
        /// </summary>
        bool IsShutDown { get; }

        /// <summary>
        ///     Returns <c>true</c> if all tasks have completed following shut down.
        /// </summary>
        bool IsTerminated { get; }

        /// <summary>
        ///     Checks to see if this <see cref="IEventExecutor" /> is executing inside the given thread
        /// </summary>
        bool IsInEventLoop(Thread thread);

        /// <summary>
        ///     Executes the <see cref="IRunnable" />
        /// </summary>
        void Execute(IRunnable task);

        /// <summary>
        ///     Executes the <see cref="Action" />
        /// </summary>
        /// <param name="action">The delegate that will be invoked by the<see cref="IEventExecutor" /></param>
        /// <param name="state">The state parameter used by the underlying delegate.</param>
        void Execute(Action<object> action, object state);

        /// <summary>
        ///     Executes the <see cref="Action" />.
        /// </summary>
        /// <param name="action">The delegate that will be invoked by the<see cref="IEventExecutor" /></param>
        void Execute(Action action);

        /// <summary>
        ///     Executes the <see cref="Action" />
        /// </summary>
        /// <param name="action">The delegate that will be invoked by the<see cref="IEventExecutor" /></param>
        /// <param name="context">The context used by the underlying delegate.</param>
        /// <param name="state">The state parameter used by the underlying delegate.</param>
        void Execute(Action<object, object> action, object context, object state);

        /// <summary>
        ///     Executes the given <see cref="Func{TResult}" /> and returns <see cref="Task{TResult}" /> indicating when the
        ///     function is completed.
        /// </summary>
        Task<T> SubmitAsync<T>(Func<T> func);

        /// <summary>
        ///     Executes the given <see cref="Func{TResult}" /> and returns <see cref="Task{TResult}" /> indicating when the
        ///     function is completed.
        /// </summary>
        Task<T> SubmitAsync<T>(Func<T> func, CancellationToken cancellationToken);

        /// <summary>
        ///     Executes the given <see cref="Func{TResult}" /> and returns <see cref="Task{TResult}" /> indicating when the
        ///     function is completed.
        /// </summary>
        Task<T> SubmitAsync<T>(Func<object, T> func, object state);

        /// <summary>
        ///     Executes the given <see cref="Func{TResult}" /> and returns <see cref="Task{TResult}" /> indicating when the
        ///     function is completed.
        /// </summary>
        Task<T> SubmitAsync<T>(Func<object, T> func, object state, CancellationToken cancellationToken);

        /// <summary>
        ///     Executes the given <see cref="Func{TResult}" /> and returns <see cref="Task{TResult}" /> indicating when the
        ///     function is completed.
        /// </summary>
        Task<T> SubmitAsync<T>(Func<object, object, T> func, object context, object state);

        /// <summary>
        ///     Executes the given <see cref="Func{TResult}" /> and returns <see cref="Task{TResult}" /> indicating when the
        ///     function is completed.
        /// </summary>
        Task<T> SubmitAsync<T>(Func<object, object, T> func, object context, object state,
            CancellationToken cancellationToken);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        IScheduledTask Schedule(Action action, TimeSpan delay);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <paramref name="state" /> parameter is useful to when repeated execution of an action against
        ///     different objects is needed.
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        IScheduledTask Schedule(Action<object> action, object state, TimeSpan delay);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <paramref name="context" /> and <paramref name="state" /> parameters are useful when repeated execution of
        ///     an action against different objects in different context is needed.
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        IScheduledTask Schedule(Action<object, object> action, object context, object state, TimeSpan delay);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <paramref name="state" /> parameter is useful to when repeated execution of an action against
        ///     different objects is needed.
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        Task ScheduleAsync(Action<object> action, object state, TimeSpan delay, CancellationToken cancellationToken);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <paramref name="state" /> parameter is useful to when repeated execution of an action against
        ///     different objects is needed.
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        Task ScheduleAsync(Action<object> action, object state, TimeSpan delay);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        Task ScheduleAsync(Action action, TimeSpan delay, CancellationToken cancellationToken);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        Task ScheduleAsync(Action action, TimeSpan delay);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <paramref name="context" /> and <paramref name="state" /> parameters are useful when repeated execution of
        ///     an action against different objects in different context is needed.
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        Task ScheduleAsync(Action<object, object> action, object context, object state, TimeSpan delay);

        /// <summary>
        ///     Schedules the given action for execution after the specified delay would pass.
        /// </summary>
        /// <remarks>
        ///     <paramref name="context" /> and <paramref name="state" /> parameters are useful when repeated execution of
        ///     an action against different objects in different context is needed.
        ///     <para>Threading specifics are determined by <c>IEventExecutor</c> implementation.</para>
        /// </remarks>
        Task ScheduleAsync(Action<object, object> action, object context, object state, TimeSpan delay,
            CancellationToken cancellationToken);

        Task GracefulShutdownAsync();

        Task GracefulShutdownAsync(TimeSpan quietPeriod, TimeSpan timeout);

        /// <summary>
        ///     If this executor is a <see cref="IWrappedEventExecutor" />, then this method will return the underlying
        ///     <see cref="IEventExecutor" />.
        ///     Otherwise it will just return itself.
        /// </summary>
        IEventExecutor Unwrap();
    }
}

