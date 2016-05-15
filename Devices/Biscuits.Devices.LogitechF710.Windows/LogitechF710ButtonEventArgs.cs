// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides button event data for Logitech F710.
    /// </summary>
    public class LogitechF710ButtonEventArgs : EventArgs
    {
        private readonly LogitechF710Button _button;
        
        /// <summary>
        /// Gets the button.
        /// </summary>
        public LogitechF710Button Button
        {
            get { return _button; }
        }

        /// <summary>
        /// Initializes the button event data.
        /// </summary>
        /// <param name="button">The button.</param>
        public LogitechF710ButtonEventArgs(LogitechF710Button button)
        {
            _button = button;
        }
    }
}
