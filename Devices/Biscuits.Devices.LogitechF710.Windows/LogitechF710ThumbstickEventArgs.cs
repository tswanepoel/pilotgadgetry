// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides thumbstick event data for Logitech F710.
    /// </summary>
    public class LogitechF710ThumbstickEventArgs : EventArgs
    {
        private readonly LogitechF710Thumbstick _thumbstick;
        private readonly long _horizontal;
        private readonly long _vertical;

        /// <summary>
        /// Gets the thumbstick.
        /// </summary>
        public LogitechF710Thumbstick Thumbstick
        {
            get { return _thumbstick; }
        }

        /// <summary>
        /// Gets the position along the horizontal-axis.
        /// </summary>
        public long Horizontal
        {
            get { return _horizontal; }
        }

        /// <summary>
        /// Gets the position along the vertical-axis.
        /// </summary>
        public long Vertical
        {
            get { return _vertical; }
        }

        /// <summary>
        /// Initializes the thumbstick event data.
        /// </summary>
        /// <param name="thumbstick">The thumbstick.</param>
        /// <param name="horizontal">The position along the horizontal-axis.</param>
        /// <param name="vertical">The position along the vertical-axis.</param>
        public LogitechF710ThumbstickEventArgs(LogitechF710Thumbstick thumbstick, long horizontal, long vertical)
        {
            _thumbstick = thumbstick;
            _horizontal = horizontal;
            _vertical = vertical;
        }
    }
}
