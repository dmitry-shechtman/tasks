﻿using System;
using System.Collections.Generic;

namespace Ditto.AsyncInit.Mvvm
{
    /// <summary>
    /// Task listener to propagate notifications to multiple listeners.
    /// </summary>
    public class AggregateTaskListener : ITaskListener
    {
        private readonly IEnumerable<ITaskListener> _listeners;

        /// <summary>
        /// Creates a new instance of the listener.
        /// </summary>
        /// <param name="listeners">Task listeners.</param>
        public AggregateTaskListener(IEnumerable<ITaskListener> listeners)
        {
            if (listeners == null)
                throw new ArgumentNullException("listeners");
            this._listeners = listeners;
        }

        /// <summary>
        /// Creates a new instance of the listener.
        /// </summary>
        /// <param name="listeners">Task listeners.</param>
        public AggregateTaskListener(params ITaskListener[] listeners)
        {
            if (listeners == null)
                throw new ArgumentNullException("listeners");
            this._listeners = listeners;
        }

        void ITaskListener.NotifyTaskStarting()
        {
            foreach (var listener in _listeners)
                listener.NotifyTaskStarting();
        }

        void ITaskListener.NotifyTaskCompleted(bool? isSuccess)
        {
            foreach (var listener in _listeners)
                listener.NotifyTaskCompleted(isSuccess);
        }
    }
}
