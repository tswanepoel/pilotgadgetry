// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides input event data for Logitech F710.
    /// </summary>
    public class LogitechF710InputEventArgs : EventArgs
    {
        private readonly LogitechF710InputData _data;

        /// <summary>
        /// Gets the input data.
        /// </summary>
        public LogitechF710InputData Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Initializes the input event data.
        /// </summary>
        /// <param name="data">The data.</param>
        public LogitechF710InputEventArgs(LogitechF710InputData data)
        {
            _data = data;
        }
    }
}
