// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    public interface IAccelerometer3
    {
        double Frequency { get; }
        GForce3 Read();
    }
}
