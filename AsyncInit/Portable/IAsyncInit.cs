﻿using System.Threading.Tasks;

namespace Ditto.AsyncInit
{
    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync();
    }

    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
    /// <typeparam name="TArg">The type of the initialization argument.</typeparam>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit<TArg>
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <param name="arg">The initialization argument.</param>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync(TArg arg);
    }

    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first initialization argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second initialization argument.</typeparam>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit<TArg1, TArg2>
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <param name="arg1">The first initialization argument.</param>
        /// <param name="arg2">The second initialization argument.</param>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync(TArg1 arg1, TArg2 arg2);
    }

    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first initialization argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second initialization argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third initialization argument.</typeparam>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit<TArg1, TArg2, TArg3>
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <param name="arg1">The first initialization argument.</param>
        /// <param name="arg2">The second initialization argument.</param>
        /// <param name="arg3">The third initialization argument.</param>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync(TArg1 arg1, TArg2 arg2, TArg3 arg3);
    }

    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first initialization argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second initialization argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third initialization argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth initialization argument.</typeparam>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit<TArg1, TArg2, TArg3, TArg4>
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <param name="arg1">The first initialization argument.</param>
        /// <param name="arg2">The second initialization argument.</param>
        /// <param name="arg3">The third initialization argument.</param>
        /// <param name="arg4">The fourth initialization argument.</param>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    }

    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first initialization argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second initialization argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third initialization argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth initialization argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth initialization argument.</typeparam>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit<TArg1, TArg2, TArg3, TArg4, TArg5>
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <param name="arg1">The first initialization argument.</param>
        /// <param name="arg2">The second initialization argument.</param>
        /// <param name="arg3">The third initialization argument.</param>
        /// <param name="arg4">The fourth initialization argument.</param>
        /// <param name="arg5">The fifth initialization argument.</param>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5);
    }

    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first initialization argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second initialization argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third initialization argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth initialization argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth initialization argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth initialization argument.</typeparam>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <param name="arg1">The first initialization argument.</param>
        /// <param name="arg2">The second initialization argument.</param>
        /// <param name="arg3">The third initialization argument.</param>
        /// <param name="arg4">The fourth initialization argument.</param>
        /// <param name="arg5">The fifth initialization argument.</param>
        /// <param name="arg6">The sixth initialization argument.</param>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6);
    }

    /// <summary>
    /// Interface for asynchronously initialized types.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first initialization argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second initialization argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third initialization argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth initialization argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth initialization argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth initialization argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh initialization argument.</typeparam>
	/// <conceptualLink target="c731bb1a-010a-40c6-856b-421ebbd05a26" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public interface IAsyncInit<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>
    {
        /// <summary>
        /// Asynchronously initializes an instance.
        /// </summary>
        /// <param name="arg1">The first initialization argument.</param>
        /// <param name="arg2">The second initialization argument.</param>
        /// <param name="arg3">The third initialization argument.</param>
        /// <param name="arg4">The fourth initialization argument.</param>
        /// <param name="arg5">The fifth initialization argument.</param>
        /// <param name="arg6">The sixth initialization argument.</param>
        /// <param name="arg7">The seventh initialization argument.</param>
        /// <returns>A <see cref="Task"/> capturing the initialization.</returns>
        Task InitAsync(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7);
    }

}
