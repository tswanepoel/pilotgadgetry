// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    public class GamepadThumbstickEventArgs : EventArgs
    {
        private readonly GamepadThumbstick _thumbstick;
        private readonly double _horizontal;
        private readonly double _vertical;

        public GamepadThumbstick Thumbstick
        {
            get { return _thumbstick; }
        }

        public double Horizontal
        {
            get { return _horizontal; }
        }

        public double Vertical
        {
            get { return _vertical; }
        }

        public GamepadThumbstickEventArgs(GamepadThumbstick thumbstick, double horizontal, double vertical)
        {
            _thumbstick = thumbstick;
            _horizontal = horizontal;
            _vertical = vertical;
        }
    }
}
