// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    public class Xbox360ControllerThumbstickEventArgs : EventArgs
    {
        private readonly Xbox360ControllerThumbstick _thumbstick;
        private readonly long _horizontal;
        private readonly long _vertical;

        public Xbox360ControllerThumbstick Thumbstick
        {
            get { return _thumbstick; }
        }

        public long Horizontal
        {
            get { return _horizontal; }
        }

        public long Vertical
        {
            get { return _vertical; }
        }

        public Xbox360ControllerThumbstickEventArgs(Xbox360ControllerThumbstick thumbstick, long horizontal, long vertical)
        {
            _thumbstick = thumbstick;
            _horizontal = horizontal;
            _vertical = vertical;
        }
    }
}
