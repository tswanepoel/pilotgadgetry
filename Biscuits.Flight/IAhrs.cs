// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    using System;

    public interface IAhrs
    {
        IObservable<EulerAngles> AngularPosition { get; }
    }
}
