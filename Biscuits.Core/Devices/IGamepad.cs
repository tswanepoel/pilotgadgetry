// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    public interface IGamepad
    {
        event EventHandler<GamepadThumbstickEventArgs> ThumbstickChanged;
        event EventHandler<GamepadButtonEventArgs> ButtonPressed;
    }
}
