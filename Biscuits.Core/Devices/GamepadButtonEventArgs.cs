// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    public class GamepadButtonEventArgs : EventArgs
    {
        private readonly GamepadButton _button;

        public GamepadButton Button
        {
            get { return _button; }
        }

        public GamepadButtonEventArgs(GamepadButton button)
        {
            _button = button;
        }
    }
}
