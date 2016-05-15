// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    using System;

    public interface IFlightController
    {
        IObservable<double> Thrust { get; }
        IObservable<double> Pitch { get; }
        IObservable<double> Roll { get; }
        IObservable<double> Yaw { get; }
    }
}
