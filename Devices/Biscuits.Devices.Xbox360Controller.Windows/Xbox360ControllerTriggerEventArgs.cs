// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    public class Xbox360ControllerTriggerEventArgs : EventArgs
    {
        private readonly long _value;

        public long Value
        {
            get { return _value; }
        }

        public Xbox360ControllerTriggerEventArgs(long value)
        {
            _value = value;
        }
    }
}
