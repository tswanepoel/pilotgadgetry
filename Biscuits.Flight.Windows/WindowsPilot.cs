// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    using Extensions;
    using Devices;
    using Scheduling;
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using Windows.System.Threading;

    public sealed class WindowsPilot : Pilot
    {
        private const double OutputFrequency = 190/*Hz*/;
        private const double YawFrequency = 100/*Hz*/;

        private WindowsPilot(
            IAircraft aircraft, 
            IObservable<double> thrust,
            IObservable<EulerAngles> angularPositionSetpoint,
            IObservable<AngularVelocity3> angularVelocity, 
            IObservable<EulerAngles> angularPosition)
            : base(aircraft, thrust, angularPositionSetpoint, angularVelocity, angularPosition)
        {
        }

        public static WindowsPilot Create(
            IAircraft aircraft, 
            IGyroscope3 gyroscope, 
            IAccelerometer3 accelerometer, 
            IMagnetometer3 magnetometer, 
            IFlightController controller,
            double thrustMax,
            double angularPositionPitchMax,
            double angularPositionRollMax,
            double angularVelocityYawMax)
        {
            var angularVelocityNoisy = new Subject<AngularVelocity3>();
            var accelerationNoisy = new Subject<GForce3>();
            var magnetismNoisy = new Subject<MagneticFluxDensity3>();

            IObservable<AngularVelocity3> angularVelocityOptimal =
                angularVelocityNoisy
                    .Filter(
                        estimatedMeasurementNoiseCovariance: 0.0000025d,
                        estimatedProcessNoiseCovariance: 0d,
                        controlWeight: 0.5d * OutputFrequency / gyroscope.Frequency,
                        initialEstimate: 0d,
                        initialErrorCovariance: 1d)
                    .Multiply(1.08d/*calibration*/);

            IObservable<GForce3> accelerationOptimal =
                accelerationNoisy
                    .Filter(
                        estimatedMeasurementNoiseCovariance: 0.025d,
                        estimatedProcessNoiseCovariance: 0d,
                        controlWeight: 0.5d * OutputFrequency / accelerometer.Frequency,
                        initialEstimate: 0d,
                        initialErrorCovariance: 1d);

            IObservable<MagneticFluxDensity3> magnetismOptimal =
                magnetismNoisy
                    .Filter(
                        estimatedMeasurementNoiseCovariance: 0.05d,
                        estimatedProcessNoiseCovariance: 0d,
                        controlWeight: 0.5d * OutputFrequency / magnetometer.Frequency,
                        initialEstimate: 0d,
                        initialErrorCovariance: 1d);

            var magnetismMinCalibration = new MagneticFluxDensity3(
                x: -0.273980716005263d,
                y: -0.245318477916743d,
                z: -0.237557110710227d);

            var magnetismMaxCalibration = new MagneticFluxDensity3(
                x: 0.148042531751317d,
                y: 0.177504483195312d,
                z: 0.155796981828076d);

            var ahrs = new Ahrs(angularVelocityOptimal, accelerationOptimal, magnetismOptimal, magnetismMinCalibration, magnetismMaxCalibration);

            var angularVelocityInterval = TimeSpan.FromTicks(Convert.ToInt64(TimeSpan.TicksPerSecond / gyroscope.Frequency));
            var accelerationInterval = TimeSpan.FromTicks(Convert.ToInt64(TimeSpan.TicksPerSecond / accelerometer.Frequency));
            var magnetismInterval = TimeSpan.FromTicks(Convert.ToInt64(TimeSpan.TicksPerSecond / magnetometer.Frequency));

            var yawTimer = new Subject<object>();
            var yawInterval = TimeSpan.FromTicks(Convert.ToInt64(TimeSpan.TicksPerSecond / YawFrequency));

            var outputTimer = new Subject<object>();
            var outputInterval = TimeSpan.FromTicks(Convert.ToInt64(TimeSpan.TicksPerSecond / OutputFrequency));
            
            SyncLoop.ProduceAsync(
                angularVelocityInterval, a => gyroscope.Read(), angularVelocityNoisy,
                accelerationInterval, a => accelerometer.Read(), accelerationNoisy,
                magnetismInterval, a => magnetometer.Read(), magnetismNoisy,
                yawInterval, a => null, yawTimer,
                outputInterval, a => null, outputTimer,
                WorkItemPriority.High).AsTask();

            IObservable<double> thrust =
                controller.Thrust.Select(value => value * thrustMax);

            IObservable<double> angularPositionPitchSetpoint =
                controller.Pitch.Select(value => value * angularPositionPitchMax);
            
            IObservable<double> angularPositionRollSetpoint = 
                controller.Roll.Select(value => value * angularPositionRollMax);

            IObservable<double> angularPositionYawSetpoint =
                Extensions.ObservableExtensions
                    .CombineLatest(
                        controller.Yaw,
                        yawTimer.TimeInterval(),
                        (yaw, timer, yawChanged, timerChanged) =>
                            new { Yaw = yaw, YawChanged = yawChanged, TimerElapsed = timerChanged, TimerInterval = timer.Interval })
                    .Scan(
                        default(double),
                        (accu, t) =>
                            {
                                double yaw = accu;

                                if (t.TimerElapsed)
                                {
                                    double dt = t.TimerInterval.TotalSeconds;
                                    yaw += t.Yaw * angularVelocityYawMax * dt;

                                    if (yaw > AngleConvert.ToRadians(180d))
                                    {
                                        yaw -= AngleConvert.ToRadians(360d);
                                    }
                                    else if (yaw <= AngleConvert.ToRadians(-180d))
                                    {
                                        yaw += AngleConvert.ToRadians(360d);
                                    }
                                }

                                return yaw;
                            });

            IObservable<EulerAngles> angularPositionSetpoint =
                Observable.CombineLatest(
                    angularPositionPitchSetpoint,
                    angularPositionRollSetpoint,
                    angularPositionYawSetpoint,
                    (pitch, roll, yaw) =>
                        new EulerAngles(pitch, roll, yaw));
            
            var pilot = new WindowsPilot(
                aircraft,
                thrust,
                angularPositionSetpoint,
                ahrs.AngularVelocityCalibrated,
                ahrs.AngularPosition);

            outputTimer.Subscribe(state => pilot.WriteOutput());
            return pilot;
        }
    }
}
