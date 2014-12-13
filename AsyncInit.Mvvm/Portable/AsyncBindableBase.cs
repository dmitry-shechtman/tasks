﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Ditto.AsyncInit.Mvvm
{
    /// <summary>
    /// Base class for entities with asynchronous initialization support.
    /// </summary>
    public abstract class AsyncBindableBase : BindableBase
    {
        /// <summary>
        /// Property storage.
        /// </summary>
        private readonly IDictionary<string, object> _storage;

        /// <summary>
        /// Creates a new instance of the entity.
        /// </summary>
        protected AsyncBindableBase()
        {
            _storage = new Dictionary<string, object>();
        }

        /// <summary>
        /// Creates a new instance of the entity.
        /// </summary>
        /// <param name="storageCapacity">Initial capacity of the property storage.</param>
        protected AsyncBindableBase(int storageCapacity)
        {
            _storage = new Dictionary<string, object>(storageCapacity);
        }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="getValue">Function to get the value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The property's value.</returns>
        protected T GetProperty<T>(Func<T> getValue, [CallerMemberName] string propertyName = null)
        {
            if (getValue == null)
                throw new ArgumentNullException("getValue");
            return DoGetProperty(getValue, propertyName);
        }

        /// <summary>
        /// Asynchronously gets the value of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="getValueAsync">Asynchronous function to get the value.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="taskListener">Task listener.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The property's value if the function successfully completed; otherwise, <value>null</value>.</returns>
        protected T GetProperty<T>(Func<Task<T>> getValueAsync, CancellationToken cancellationToken = default(CancellationToken),
            ITaskListener taskListener = null, [CallerMemberName] string propertyName = null)
            where T : class
        {
            if (getValueAsync == null)
                throw new ArgumentNullException("getValueAsync");
            return DoGetProperty(ct => getValueAsync(), cancellationToken, taskListener ?? StubTaskListener.Instance, propertyName);
        }

        /// <summary>
        /// Asynchronously gets the value of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="getValueAsync">Asynchronous function to get the value.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="taskListener">Task listener.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The property's value if the function successfully completed; otherwise, <value>null</value>.</returns>
        protected T? GetProperty<T>(Func<Task<T?>> getValueAsync, CancellationToken cancellationToken = default(CancellationToken),
            ITaskListener taskListener = null, [CallerMemberName] string propertyName = null)
            where T : struct
        {
            if (getValueAsync == null)
                throw new ArgumentNullException("getValueAsync");
            return DoGetProperty(ct => getValueAsync(), cancellationToken, taskListener ?? StubTaskListener.Instance, propertyName);
        }

        /// <summary>
        /// Asynchronously gets the value of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="getValueAsync">Asynchronous function to get the value.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="taskListener">Task listener.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The property's value if the function successfully completed; otherwise, <value>null</value>.</returns>
        protected T GetProperty<T>(Func<CancellationToken, Task<T>> getValueAsync, CancellationToken cancellationToken = default(CancellationToken),
            ITaskListener taskListener = null, [CallerMemberName] string propertyName = null)
            where T : class
        {
            if (getValueAsync == null)
                throw new ArgumentNullException("getValueAsync");
            return DoGetProperty(getValueAsync, cancellationToken, taskListener ?? StubTaskListener.Instance, propertyName);
        }

        /// <summary>
        /// Asynchronously gets the value of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="getValueAsync">Asynchronous function to get the value.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="taskListener">Task listener.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The property's value if the function successfully completed; otherwise, <value>null</value>.</returns>
        protected T? GetProperty<T>(Func<CancellationToken, Task<T?>> getValueAsync, CancellationToken cancellationToken = default(CancellationToken),
            ITaskListener taskListener = null, [CallerMemberName] string propertyName = null)
            where T : struct
        {
            if (getValueAsync == null)
                throw new ArgumentNullException("getValueAsync");
            return DoGetProperty(getValueAsync, cancellationToken, taskListener ?? StubTaskListener.Instance, propertyName);
        }

        /// <summary>
        /// Asynchronously gets the value of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="getValueAsync">Asynchronous function to get the value.</param>
        /// <param name="cancelCommand">Cancel command</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The property's value if the function successfully completed; otherwise, <value>null</value>.</returns>
        protected T GetProperty<T>(Func<CancellationToken, Task<T>> getValueAsync, CancelAsyncCommand cancelCommand,
            [CallerMemberName] string propertyName = null)
            where T : class
        {
            if (getValueAsync == null)
                throw new ArgumentNullException("getValueAsync");
            return DoGetProperty(getValueAsync, cancelCommand, propertyName);
        }

        /// <summary>
        /// Asynchronously gets the value of a property.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="getValueAsync">Asynchronous function to get the value.</param>
        /// <param name="cancelCommand">Cancel command</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>The property's value if the function successfully completed; otherwise, <value>null</value>.</returns>
        protected T? GetProperty<T>(Func<CancellationToken, Task<T?>> getValueAsync, CancelAsyncCommand cancelCommand,
            [CallerMemberName] string propertyName = null)
            where T : struct
        {
            if (getValueAsync == null)
                throw new ArgumentNullException("getValueAsync");
            return DoGetProperty(getValueAsync, cancelCommand, propertyName);
        }

        /// <summary>
        /// Invalidates a specified property or the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to invalidate, or <value>null</value>
        /// or <see cref="String.Empty"/> to invalidate the entire entity.</param>
        protected void Invalidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                ClearProperties();
            else
                RemoveProperty(propertyName);
            OnPropertyChanged(propertyName);
        }

        internal T DoGetProperty<T>(Func<T> getValue, string propertyName)
        {
            T value;
            if (!TryGetProperty(out value, propertyName))
                value = DoDoGetProperty(getValue, propertyName);
            return value;
        }

        private T DoDoGetProperty<T>(Func<T> getValue, string propertyName)
        {
            T value = getValue();
            AddProperty(value, propertyName);
            return value;
        }

        internal T DoGetProperty<T>(Func<CancellationToken, Task<T>> getValueAsync, CancelAsyncCommand cancelCommand, string propertyName)
        {
            if (cancelCommand == null)
                throw new ArgumentNullException("cancelCommand");
            return DoGetProperty(getValueAsync, cancelCommand.CancellationToken, cancelCommand, propertyName);
        }

        internal T DoGetProperty<T>(Func<CancellationToken, Task<T>> getValueAsync, CancellationToken cancellationToken, ITaskListener taskListener, string propertyName)
        {
            T value;
            if (!TryGetProperty(out value, propertyName))
                DoDoGetProperty(getValueAsync, cancellationToken, taskListener, propertyName);
            return value;
        }

        private void DoDoGetProperty<T>(Func<CancellationToken, Task<T>> getValueAsync, CancellationToken cancellationToken, ITaskListener taskListener, string propertyName)
        {
            AddProperty(null, propertyName);
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            TaskEx.Run(() => DoGetPropertyAsync(getValueAsync, cancellationToken, taskListener))
                .ContinueWith(task => OnGetPropertyCompleted(task, taskListener, propertyName),
                    cancellationToken, TaskContinuationOptions.None, taskScheduler);
        }

        private static async Task<T> DoGetPropertyAsync<T>(Func<CancellationToken, Task<T>> getValueAsync,
            CancellationToken cancellationToken, ITaskListener taskListener)
        {
            cancellationToken.ThrowIfCancellationRequested();
            taskListener.NotifyTaskStarting();
            return await getValueAsync(cancellationToken).ConfigureAwait(false);
        }

        private void OnGetPropertyCompleted<T>(Task<T> task, ITaskListener taskListener, string propertyName)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    OnGetPropertyRanToCompletion(task.Result, taskListener, propertyName);
                    break;
                case TaskStatus.Canceled:
                    OnGetPropertyCanceled(taskListener, propertyName);
                    break;
                case TaskStatus.Faulted:
                    OnGetPropertyFaulted(taskListener, propertyName);
                    break;
                default:
                    break;
            }
        }

        private void OnGetPropertyRanToCompletion<T>(T value, ITaskListener taskListener, string propertyName)
        {
            if (!object.Equals(value, null))
            {
                SetProperty(value, propertyName);
                OnPropertyChanged(propertyName);
            }
            taskListener.NotifyTaskCompleted(true);
        }

        private void OnGetPropertyCanceled(ITaskListener taskListener, string propertyName)
        {
            RemoveProperty(propertyName);
            taskListener.NotifyTaskCompleted(null);
        }

        private void OnGetPropertyFaulted(ITaskListener taskListener, string propertyName)
        {
            RemoveProperty(propertyName);
            taskListener.NotifyTaskCompleted(false);
        }

        /// <summary>
        /// Gets a property's value from the storage.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="value">The property's value if found; otherwise, the default value for
        /// <typeparamref name="T"/>.</param>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <returns><value>true</value> if value was found.</returns>
        private bool TryGetProperty<T>(out T value, string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");
            object result;
            if (_storage.TryGetValue(propertyName, out result))
            {
                value = (T)result;
                return true;
            }
            value = default(T);
            return false;
        }

        /// <summary>
        /// Adds a property's value to the storage.
        /// </summary>
        /// <param name="value">The property's value, or <value>null</value> for a placeholder.</param>
        /// <param name="propertyName">The name of the property to add.</param>
        private void AddProperty(object value, string propertyName)
        {
            _storage.Add(propertyName, value);
        }

        /// <summary>
        /// Sets a property's value in the storage.
        /// </summary>
        /// <param name="value">The property's value.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        private void SetProperty(object value, string propertyName)
        {
            _storage[propertyName] = value;
        }

        /// <summary>
        /// Removes a property's value from the storage.
        /// </summary>
        /// <param name="propertyName">The name of the property to remove.</param>
        private void RemoveProperty(string propertyName)
        {
            _storage.Remove(propertyName);
        }

        /// <summary>
        /// Removes all properties' values from the storage.
        /// </summary>
        private void ClearProperties()
        {
            _storage.Clear();
        }
    }
}