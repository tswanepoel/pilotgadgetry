// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    public class Xbox360ControllerInputEventArgs : EventArgs
    {
        private readonly Xbox360ControllerInputData _data;

        public Xbox360ControllerInputData Data
        {
            get { return _data; }
        }

        public Xbox360ControllerInputEventArgs(Xbox360ControllerInputData data)
        {
            _data = data;
        }
    }
}
