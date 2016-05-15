// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    using Extensions;
    using System;
    using System.Reactive.Linq;

    public class Pilot : IPilot
    {
        private readonly IAircraft _aircraft;
        private readonly IObservable<double> _thrust;
        private readonly IObservable<EulerAngles> _angularPositionSetpoint;
        private readonly IObservable<AngularVelocity3> _angularVelocity;
        private readonly IObservable<EulerAngles> _angularPosition;
        private double _latestThrust = 0d;
        private double _latestPitch = 0d;
        private double _latestRoll = 0d;
        private double _latestYaw = 0d;

        public IObservable<double> Thrust
        {
            get { return _thrust; }
        }
        
        public IObservable<EulerAngles> AngularPosition
        {
            get { return _angularPosition; }
        }

        public IObservable<EulerAngles> AngularPositionSetpoint
        {
            get { return _angularPositionSetpoint; }
        }

        public IObservable<EulerAngles> AngularPositionControl
        {
            get
            {
                return AngularPosition.Manipulate(
                    setpoint: AngularPositionSetpoint,
                    proportionalGain: 1d,
                    integralGain: 0d,
                    derivativeGain: 0d,
                    maxErrorCumulative: 0d/*rad*/);
            }
        }

        public IObservable<AngularVelocity3> AngularVelocitySetpoint
        {
            get
            {
                var scale = new AngularVelocity3(
                    x: 10d/*dps*/,
                    y: 10d/*dps*/,
                    z: 5d/*dps*/);

                return AngularPositionControl
                    .Select(value =>
                        {
                            return new AngularVelocity3(
                                x: AngleConvert.ToDegrees(value.Pitch) * scale.X,
                                y: AngleConvert.ToDegrees(value.Roll) * scale.Y,
                                z: AngleConvert.ToDegrees(value.Yaw) * scale.Z);
                        });
            }
        }

        public IObservable<AngularVelocity3> AngularVelocity
        {
            get { return _angularVelocity; }
        }

        public IObservable<AngularVelocity3> AngularVelocityControl
        {
            get
            {
                return AngularVelocity.Manipulate(
                    setpoint: AngularVelocitySetpoint,
                    proportionalGain: 1d,
                    integralGain: 0.25d,
                    derivativeGain: 0d,
                    maxErrorCumulative: 45/*dps*/);
            }
        }

        protected Pilot(
            IAircraft aircraft, 
            IObservable<double> thrust,
            IObservable<EulerAngles> angularPositionSetpoint,
            IObservable<AngularVelocity3> angularVelocity, 
            IObservable<EulerAngles> angularPosition)
        {
            _aircraft = aircraft;
            _thrust = thrust;
            _angularPositionSetpoint = angularPositionSetpoint;
            _angularVelocity = angularVelocity;
            _angularPosition = angularPosition;
        }
        
        private void Initialize()
        {
            Thrust.Subscribe(value => _latestThrust = value);

            AngularVelocityControl.Subscribe(value =>
                {
                    var scale = new AngularVelocity3(
                        x: 720d/*dps*/,
                        y: 720d/*dps*/,
                        z: 720d/*dps*/);

                    _latestPitch = Math.Min(Math.Max(value.X / scale.X, -1d), 1d);
                    _latestRoll = Math.Min(Math.Max(value.Y / scale.Y, -1d), 1d);
                    _latestYaw = Math.Min(Math.Max(value.Z / scale.Z, -1d), 1d);
                });
        }

        protected virtual void WriteOutput()
        {
            _aircraft.SetControl(_latestThrust, _latestPitch, _latestRoll, _latestYaw);
        }
    }
}
