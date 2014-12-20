﻿using Ditto.AsyncMvvm.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Ditto.AsyncMvvm
{
    /// <summary>
    /// Base class for asynchronous property helpers.
    /// </summary>
    public abstract class AsyncPropertyHelperBase : IAsyncPropertyHelper
    {
        private readonly Action<string> _onPropertyChanged;

        /// <summary>
        /// Creates a new property helper.
        /// </summary>
        /// <param name="onPropertyChanged">Property change notification delegate.</param>
        protected AsyncPropertyHelperBase(Action<string> onPropertyChanged)
        {
            if (onPropertyChanged == null)
                throw new ArgumentNullException("onPropertyChanged");
            this._onPropertyChanged = onPropertyChanged;
        }

        /// <summary>
        /// Gets the value of a lazy property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValue">The delegate used to calculate the property value.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        /// <param name="propertyName">The name of the property.</param>
        public T Get<T>(Func<T> getValue, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
        {
            return DoGet(getValue, comparer, propertyName);
        }

        /// <summary>
        /// Gets the value of an asynchronous property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValueAsync">The delegate used to calculate the property value.</param>
        /// <param name="token">The optional cancellation token.</param>
        /// <param name="listener">The optional task listener.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        /// <param name="propertyName">The name of the property.</param>
        public T Get<T>(Func<Task<T>> getValueAsync, CancellationToken token = default(CancellationToken),
            ITaskListener listener = null, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
        {
            if (getValueAsync == null)
                throw new ArgumentNullException("getValueAsync");
            return DoGet(ct => getValueAsync(), token, listener, comparer, propertyName);
        }

        /// <summary>
        /// Gets the value of an asynchronous property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValueAsync">The delegate used to calculate the property value.</param>
        /// <param name="token">The optional cancellation token.</param>
        /// <param name="listener">The optional task listener.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        /// <param name="propertyName">The name of the property.</param>
        public T Get<T>(Func<CancellationToken, Task<T>> getValueAsync, CancellationToken token = default(CancellationToken),
            ITaskListener listener = null, IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
        {
            return DoGet(getValueAsync, token, listener, comparer, propertyName);
        }

        /// <summary>
        /// Invalidates a specified property or the entire entity.
        /// </summary>
        /// <param name="propertyName">The name of the property to invalidate, or <value>null</value>
        /// or <see cref="String.Empty"/> to invalidate the entire entity.</param>
        public void Invalidate([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                InvalidateEntity();
            else
            {
                var property = GetProperty(propertyName);
                if (property != null)
                    property.Invalidate(true, propertyName);
            }
        }

        /// <summary>
        /// Retrieves the specified lazy property if it exists; otherwise, returns <value>null</value>.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        public ILazyProperty<T> GetLazyProperty<T>([CallerMemberName] string propertyName = null)
        {
            return GetProperty(propertyName) as ILazyProperty<T>;
        }

        /// <summary>
        /// Retrieves the specified asynchronous property if it exists; otherwise, returns <value>null</value>.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="propertyName">The name of the property.</param>
        public IAsyncProperty<T> GetAsyncProperty<T>([CallerMemberName] string propertyName = null)
        {
            return GetProperty(propertyName) as IAsyncProperty<T>;
        }

        /// <summary>
        /// Retrieves the specified lazy property if it exists; otherwise, creates the lazy property and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValue">The delegate used to calculate the property value.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        /// <param name="propertyName">The name of the property.</param>
        public ILazyProperty<T> GetOrAddLazyProperty<T>(Func<T> getValue, IEqualityComparer<T> comparer = null,
            [CallerMemberName] string propertyName = null)
        {
            return GetOrAddProperty(() => CreateLazyProperty(getValue, comparer), propertyName);
        }

        /// <summary>
        /// Retrieves the specified asynchronous property if it exists; otherwise, creates the asynchronous property and returns it.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValueAsync">The delegate used to calculate the property value.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        /// <param name="propertyName">The name of the property.</param>
        public IAsyncProperty<T> GetOrAddAsyncProperty<T>(Func<CancellationToken, Task<T>> getValueAsync,
            IEqualityComparer<T> comparer = null, [CallerMemberName] string propertyName = null)
        {
            return GetOrAddProperty(() => CreateAsyncProperty(getValueAsync, comparer), propertyName);
        }

        /// <summary>
        /// Creates a lazy property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValue">The delegate used to calculate the property value.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        protected virtual ILazyProperty<T> CreateLazyProperty<T>(Func<T> getValue, IEqualityComparer<T> comparer)
        {
            return new LazyProperty<T>(NotifyValueChanged, getValue, comparer);
        }

        /// <summary>
        /// Creates an asynchronous property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValueAsync">The delegate used to calculate the property value.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        protected virtual IAsyncProperty<T> CreateAsyncProperty<T>(Func<CancellationToken, Task<T>> getValueAsync, IEqualityComparer<T> comparer)
        {
            return new AsyncProperty<T>(NotifyValueChanged, getValueAsync, comparer);
        }

        /// <summary>
        /// Retrieves the specified property if it exists; otherwise, returns <value>null</value>.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected abstract IProperty GetProperty(string propertyName);

        /// <summary>
        /// Retrieves the specified property if it exists; otherwise, creates the property and returns it.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="createProperty">The delegate used to create the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected abstract TProperty GetOrAddProperty<TProperty>(Func<TProperty> createProperty, string propertyName)
            where TProperty : IProperty;

        /// <summary>
        /// Invalidates the entire entity.
        /// </summary>
        protected abstract void InvalidateEntity();

        /// <summary>
        /// Notifies on change in property value.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="value">The new value of the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected virtual void NotifyValueChanged<T>(T value, string propertyName)
        {
            _onPropertyChanged(propertyName);
        }

        /// <summary>
        /// Invokes the property change notification delegate.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            _onPropertyChanged(propertyName);
        }

        /// <summary>
        /// Gets the value of a lazy property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValue">The delegate used to calculate the property value.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        /// <param name="propertyName">The name of the property.</param>
        private T DoGet<T>(Func<T> getValue, IEqualityComparer<T> comparer, string propertyName)
        {
            return GetOrAddLazyProperty(getValue, comparer, propertyName).GetValue(propertyName);
        }

        /// <summary>
        /// Gets the value of an asynchronous property.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="getValueAsync">The delegate used to calculate the property value.</param>
        /// <param name="token">The optional cancellation token.</param>
        /// <param name="listener">The optional task listener.</param>
        /// <param name="comparer">The optional equality comparer.</param>
        /// <param name="propertyName">The name of the property.</param>
        private T DoGet<T>(Func<CancellationToken, Task<T>> getValueAsync, CancellationToken token, ITaskListener listener, IEqualityComparer<T> comparer, string propertyName)
        {
            return GetOrAddAsyncProperty(getValueAsync, comparer, propertyName).GetValue(token, listener, propertyName);
        }
    }
}