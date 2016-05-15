// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    public class Xbox360ControllerButtonEventArgs : EventArgs
    {
        private readonly Xbox360ControllerButton _button;
        
        public Xbox360ControllerButton Button
        {
            get { return _button; }
        }

        public Xbox360ControllerButtonEventArgs(Xbox360ControllerButton button)
        {
            _button = button;
        }
    }
}
