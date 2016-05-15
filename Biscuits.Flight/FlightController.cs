// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    using Biscuits.Devices;
    using System;
    using System.Reactive.Subjects;
    using System.Reactive.Linq;

    public class FlightController : IFlightController
    {
        private readonly IObservable<double> _thrustUntrimmed;
        private readonly IObservable<double> _pitchUntrimmed;
        private readonly IObservable<double> _rollUntrimmed;
        private readonly IObservable<double> _yawUntrimmed;

        private readonly Subject<double> _thrustTrim = new Subject<double>();
        private readonly Subject<double> _pitchTrim = new Subject<double>();
        private readonly Subject<double> _rollTrim = new Subject<double>();
        private readonly Subject<double> _yawTrim = new Subject<double>();

        private double _thrustTrimLatest = 0d;
        private double _pitchTrimLatest = 0d;
        private double _rollTrimLatest = 0d;
        private double _yawTrimLatest = 0d;

        public IObservable<double> Thrust
        {
            get
            {
                return _thrustUntrimmed
                    .CombineLatest(
                        _thrustTrim, 
                        (thrust, trim) => Math.Min(Math.Max(thrust + trim, -1d), 1d));
            }
        }

        public IObservable<double> Pitch
        {
            get
            {
                return _thrustUntrimmed
                    .CombineLatest(
                        _thrustTrim,
                        (thrust, trim) => Math.Min(Math.Max(thrust + trim, -1d), 1d));
            }
        }

        public IObservable<double> Roll
        {
            get
            {
                return _rollUntrimmed
                    .CombineLatest(
                        _rollTrim,
                        (roll, trim) => Math.Min(Math.Max(roll + trim, -1d), 1d));
            }
        }

        public IObservable<double> Yaw
        {
            get
            {
                return _yawUntrimmed
                    .CombineLatest(
                        _yawTrim,
                        (yaw, trim) => Math.Min(Math.Max(yaw + trim, -1d), 1d));
            }
        }

        public double ThrustTrim
        {
            get { return _thrustTrimLatest; }
            set
            {
                double valueSafe = Math.Min(Math.Max(value, -1d), 1d);

                if (_thrustTrimLatest != valueSafe)
                {
                    _thrustTrim.OnNext(_thrustTrimLatest = valueSafe);
                }
            }
        }

        public double PitchTrim
        {
            get { return _pitchTrimLatest; }
            set
            {
                double valueSafe = Math.Min(Math.Max(value, -1d), 1d);

                if (_pitchTrimLatest != valueSafe)
                {
                    _pitchTrim.OnNext(_pitchTrimLatest = valueSafe);
                }
            }
        }

        public double RollTrim
        {
            get { return _rollTrimLatest; }
            set
            {
                double valueSafe = Math.Min(Math.Max(value, -1d), 1d);

                if (_rollTrimLatest != valueSafe)
                {
                    _rollTrim.OnNext(_rollTrimLatest = valueSafe);
                }
            }
        }

        public double YawTrim
        {
            get { return _yawTrimLatest; }
            set
            {
                double valueSafe = Math.Min(Math.Max(value, -1d), 1d);

                if (_yawTrimLatest != valueSafe)
                {
                    _yawTrim.OnNext(_yawTrimLatest = valueSafe);
                }
            }
        }

        private FlightController(IObservable<double> thrust, IObservable<double> pitch, IObservable<double> roll, IObservable<double> yaw)
        {
            _thrustUntrimmed = thrust;
            _pitchUntrimmed = pitch;
            _rollUntrimmed = roll;
            _yawUntrimmed = yaw;
        }

        public static FlightController FromGamepad(IGamepad gamepad, double trimStep = 0.01d)
        {
            return FromGamepad(gamepad, trimStep, trimStep, trimStep, trimStep);
        }

        public static FlightController FromGamepad(
            IGamepad gamepad, 
            double thrustTrimStep,
            double pitchTrimStep,
            double rollTrimStep,
            double yawTrimStep)
        {
            var thrust = new Subject<double>();
            var pitch = new Subject<double>();
            var roll = new Subject<double>();
            var yaw = new Subject<double>();

            var controller = new FlightController(thrust, pitch, roll, yaw);

            gamepad.ThumbstickChanged += 
                (sender, e) =>
                    {
                        switch (e.Thumbstick)
                        {
                            case GamepadThumbstick.Left:
                                thrust.OnNext(Math.Min(Math.Max(-e.Vertical, -1d), 1d));
                                yaw.OnNext(Math.Min(Math.Max(e.Horizontal, -1d), 1d));
                                break;

                            case GamepadThumbstick.Right:
                                pitch.OnNext(Math.Min(Math.Max(e.Vertical, -1d), 1d));
                                roll.OnNext(Math.Min(Math.Max(e.Horizontal, -1d), 1d));
                                break;
                        }
                    };

            gamepad.ButtonPressed +=
                (sender, e) =>
                    {
                        switch (e.Button)
                        {
                            case GamepadButton.Up:
                                controller.ThrustTrim += thrustTrimStep;
                                break;

                            case GamepadButton.Down:
                                controller.ThrustTrim -= thrustTrimStep;
                                break;

                            case GamepadButton.Left:
                                controller.YawTrim -= yawTrimStep;
                                break;

                            case GamepadButton.Right:
                                controller.YawTrim += yawTrimStep;
                                break;

                            case GamepadButton.Y:
                                controller.PitchTrim -= pitchTrimStep;
                                break;

                            case GamepadButton.A:
                                controller.PitchTrim += pitchTrimStep;
                                break;

                            case GamepadButton.X:
                                controller.RollTrim -= rollTrimStep;
                                break;

                            case GamepadButton.B:
                                controller.RollTrim += rollTrimStep;
                                break;

                            case GamepadButton.Back:
                                controller.ThrustTrim = 0d;
                                controller.PitchTrim = 0d;
                                controller.RollTrim = 0d;
                                controller.YawTrim = 0d;
                                break;
                        }
                    };

            return controller;
        }
    }
}
