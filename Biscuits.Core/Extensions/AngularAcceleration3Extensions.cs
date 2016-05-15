// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Extensions
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;

    public static class AngularAcceleration3Extensions
    {
        public static IObservable<Torque3> ToTorque(this IObservable<AngularAcceleration3> source, double mass, double radius)
        {
            double momentOfInertia = mass * Math.Pow(radius, 2d);
            return ToTorque(source, momentOfInertia);
        }

        public static IObservable<Torque3> ToTorque(this IObservable<AngularAcceleration3> source, double momentOfInertia)
        {
            return source
                .Select(angularAcceleration =>
                    {
                        return new Torque3(
                            x: momentOfInertia * angularAcceleration.X,
                            y: momentOfInertia * angularAcceleration.Y,
                            z: momentOfInertia * angularAcceleration.Z);
                    });
        }
    }
}
