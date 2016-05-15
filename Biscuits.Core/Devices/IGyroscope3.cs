// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    public interface IGyroscope3
    {
        double Frequency { get; }
        AngularVelocity3 Read();
    }
}
