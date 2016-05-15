// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    using Biscuits.Extensions;
    using Extensions;
    using System;
    using System.Reactive.Linq;

    public class Ahrs : IAhrs
    {
        private readonly IObservable<AngularVelocity3> _angularVelocity;
        private readonly IObservable<GForce3> _acceleration;
        private readonly IObservable<MagneticFluxDensity3> _magnetism;

        private readonly MagneticFluxDensity3 _magnetismMinCalibration;
        private readonly MagneticFluxDensity3 _magnetismMaxCalibration;

        private AngularVelocity3 _angularVelocityCalibration = new AngularVelocity3(x: 0d, y: 0d, z: 0d);
        private bool _isCalibrateAngularVelocity = true;

        private double _accelerationCalibration = 1d;
        private bool _isCalibrateAcceleration = true;

        private Quaternion _accelerometerOrientationCalibration = Quaternion.FromAxisAngle(new Vector3(0d, 0d, 1d), 0d);
        private bool _isCalibrateAccelerometerOrientation = true;
        
        private double _angularPositionControlYawCalibration = 0d;
        private bool _isCalibrateAngularPositionControlYaw = true;
        
        /// <summary>
        /// Gets the observable sequence of angular velocity measurements, in degrees-per-second (dps).
        /// </summary>
        public IObservable<AngularVelocity3> AngularVelocity
        {
            get { return _angularVelocity; }
        }

        /// <summary>
        /// Gets the observable sequence of angular velocity estimates after calibration, in degrees-per-second (dps).
        /// </summary>
        public IObservable<AngularVelocity3> AngularVelocityCalibrated
        {
            get
            {
                return AngularVelocity
                    .Select(angularVelocity =>
                        {
                            if (_isCalibrateAngularVelocity)
                            {
                                _angularVelocityCalibration = new AngularVelocity3(
                                    x: -angularVelocity.X,
                                    y: -angularVelocity.Y,
                                    z: -angularVelocity.Z);

                                _isCalibrateAngularVelocity = false;
                            }

                            return new AngularVelocity3(
                                x: angularVelocity.X + _angularVelocityCalibration.X,
                                y: angularVelocity.Y + _angularVelocityCalibration.Y,
                                z: angularVelocity.Z + _angularVelocityCalibration.Z);
                        });
            }
        }

        /// <summary>
        /// Gets the observable sequence of rotations, as quaternions.
        /// </summary>
        public IObservable<Quaternion> Rotation
        {
            get
            {
                return AngularVelocityCalibrated
                    .TimeInterval()
                    .Select(angularVelocityTimed =>
                        {
                            AngularVelocity3 angularVelocity = angularVelocityTimed.Value;
                            double dt = angularVelocityTimed.Interval.TotalSeconds;

                            Vector3 w = angularVelocity.Normalize();
                            double theta = AngleConvert.ToRadians(angularVelocity.Magnitude) * dt;

                            return Quaternion.FromAxisAngle(w, -theta);
                        });
            }
        }

        /// <summary>
        /// Gets the observable sequence of acceleration force measurements, in g-force (g).
        /// </summary>
        public IObservable<GForce3> Acceleration
        {
            get { return _acceleration; }
        }

        /// <summary>
        /// Gets the observable sequence of acceleration force estimates after calibration, in g-force (g).
        /// </summary>
        public IObservable<GForce3> AccelerationCalibrated
        {
            get
            {
                return Acceleration
                    .Select(acceleration =>
                        {
                            if (_isCalibrateAcceleration)
                            {
                                _accelerationCalibration = acceleration.Magnitude / 1d/*G*/;
                                _isCalibrateAcceleration = false;
                            }

                            return new GForce3(
                                x: acceleration.X / _accelerationCalibration,
                                y: acceleration.Y / _accelerationCalibration,
                                z: acceleration.Z / _accelerationCalibration);
                        });
            }
        }

        /// <summary>
        /// Gets the observable sequence of orientations relative to the direction of acceleration, as quaternions.
        /// </summary>
        public IObservable<Quaternion> AccelerometerOrientation
        {
            get
            {
                return AccelerationCalibrated
                    .Where(acceleration => acceleration.Magnitude >= 0.2d/*G*/)
                    .Select(acceleration =>
                        {
                            Vector3 direction = acceleration.Normalize();
                            var accelerometerOrientation = new Quaternion(w: 1d, vector: direction).Normalize();

                            if (_isCalibrateAccelerometerOrientation)
                            {
                                EulerAngles angles = EulerAngles.FromVector(accelerometerOrientation.GetAxis());
                                Vector3 v = new Vector3(x: angles.Pitch, y: angles.Roll, z: 0d);

                                double theta = v.Magnitude;
                                Vector3 w = v.Normalize();

                                _accelerometerOrientationCalibration = Quaternion.FromAxisAngle(w, theta);
                                _isCalibrateAccelerometerOrientation = false;
                            }

                            return _accelerometerOrientationCalibration
                                .Multiply(accelerometerOrientation)
                                .Multiply(_accelerometerOrientationCalibration.Conjugate());
                        });
            }
        }

        /// <summary>
        /// Gets the observable sequence of orientations relative to the surface of the ground, as quaternions.
        /// </summary>
        public IObservable<Quaternion> GroundOrientation
        {
            get
            {
                return Observable
                    .Scan(
                        Biscuits.Extensions.ObservableExtensions.CombineLatest(
                            AccelerometerOrientation,
                            Rotation,
                            (accelerometerOrientation, rotation, accelerometerOrientationChanged, rotationChanged) =>
                                new { AccelerometerOrientation = accelerometerOrientation, Rotation = rotation, AccelerometerOrientationChanged = accelerometerOrientationChanged, RotationChanged = rotationChanged }),
                        new { GroundOrientation = default(Quaternion) },
                        (accu, t) =>
                        {
                            Quaternion groundOrientation = accu.GroundOrientation;

                            if (t.RotationChanged)
                            {
                                groundOrientation = t.Rotation
                                    .Multiply(groundOrientation)
                                    .Multiply(t.Rotation.Conjugate())
                                    .SlerpAngle(t.AccelerometerOrientation, 0.00025d/*rad*/);
                            }

                            return new { GroundOrientation = groundOrientation };
                        })
                    .Select(t => t.GroundOrientation);
            }
        }

        /// <summary>
        /// Gets the observable sequence of acceleration force estimates relative to the surface of the ground, in g-force (g). 
        /// </summary>
        public IObservable<GForce3> AccelerationOverGround
        {
            get
            {
                return Observable
                    .CombineLatest(
                        AccelerationCalibrated,
                        AccelerometerOrientation,
                        GroundOrientation,
                        (acceleration, accelerometerOrientation, groundOrientation)
                            => new { Acceleration = acceleration, AccelerometerOrientation = accelerometerOrientation, GroundOrientation = groundOrientation })
                    .Select(t =>
                        {
                            Vector3 accelerationDirection = t.AccelerometerOrientation.GetAxis();
                            Vector3 acceleration = accelerationDirection.Scale(t.Acceleration.Magnitude);

                            Vector3 groundDirection = t.GroundOrientation.GetAxis();
                            EulerAngles groundAngles = EulerAngles.FromVector(groundDirection);

                            double accelerationX = 
                                Math.Cos(groundAngles.Roll) * acceleration.X + Math.Sin(groundAngles.Roll) * acceleration.Z;

                            double accelerationY = 
                                Math.Cos(groundAngles.Pitch) * acceleration.Y - Math.Sin(groundAngles.Pitch) * acceleration.Z;

                            double accelerationZ =
                                Math.Sin(groundAngles.Pitch) * acceleration.Y - Math.Sin(groundAngles.Roll) * acceleration.X +
                                Math.Cos(groundAngles.Roll) * Math.Cos(groundAngles.Pitch) * acceleration.Z - 1d;

                            return new GForce3(x: accelerationX, y: accelerationY, z: accelerationZ);
                        });
            }
        }

        /// <summary>
        /// Gets the observable sequence of magnetic flux density measurements, in Gauss (g).
        /// </summary>
        public IObservable<MagneticFluxDensity3> Magnetism
        {
            get { return _magnetism; }
        }

        /// <summary>
        /// Gets the observable sequence of magnetic north direction estimates after calibration.
        /// </summary>
        public IObservable<Vector3> MagneticNorthDirection
        {
            get { return Magnetism.ToDirection(_magnetismMinCalibration, _magnetismMaxCalibration); }
        }

        /// <summary>
        /// Gets the observable sequence of orientations relative to the direction of magnetic north, as quaternions.
        /// </summary>
        public IObservable<Quaternion> MagneticNorthOrientation
        {
            get
            {
                return Observable
                    .Scan(
                        Biscuits.Extensions.ObservableExtensions.CombineLatest(
                            MagneticNorthDirection,
                            Rotation,
                            (magnetism, rotation, magnetismChanged, rotationChanged) =>
                                new { Magnetism = magnetism, Rotation = rotation, MagnetismChanged = magnetismChanged, RotationChanged = rotationChanged }),
                        new { MagnometerOrientation = default(Quaternion), MagneticNorthOrientation = default(Quaternion) },
                        (accu, t) =>
                            {
                                Quaternion magnometerOrientation = accu.MagnometerOrientation;
                                Quaternion magneticNorthOrientation = accu.MagneticNorthOrientation;

                                if (t.MagnetismChanged)
                                {
                                    Vector3 direction = t.Magnetism.Normalize();
                                    magnometerOrientation = new Quaternion(w: 1d, vector: direction).Normalize();
                                }

                                if (t.RotationChanged)
                                {
                                    magneticNorthOrientation = t.Rotation
                                        .Multiply(magneticNorthOrientation)
                                        .Multiply(t.Rotation.Conjugate())
                                        .SlerpAngle(magnometerOrientation, 0.01d/*rad*/);
                                }

                                return new { MagnometerOrientation = magnometerOrientation, MagneticNorthOrientation = magneticNorthOrientation };
                            })
                    .Select(t => t.MagneticNorthOrientation);
            }
        }

        /// <summary>
        /// Gets the observable sequence of optimal angular position estimates, in radians.
        /// </summary>
        public IObservable<EulerAngles> AngularPosition
        {
            get
            {
                return Observable
                    .CombineLatest(
                        GroundOrientation,
                        MagneticNorthOrientation,
                        (gravityOrientation, magneticNorthOrientation) =>
                        {
                            Vector3 down = gravityOrientation.GetAxis();
                            Vector3 magneticNorth = magneticNorthOrientation.GetAxis();
                            Vector3 east = down.CrossProduct(magneticNorth).Normalize();
                            Vector3 northAlongSurface = east.CrossProduct(down).Normalize();

                            double yaw = EulerAngles.FromVector(new Vector3(northAlongSurface.X, -northAlongSurface.Z, northAlongSurface.Y)).Roll;

                            if (_isCalibrateAngularPositionControlYaw)
                            {
                                _angularPositionControlYawCalibration = yaw;
                                _isCalibrateAngularPositionControlYaw = false;
                            }

                            EulerAngles angularPosition = EulerAngles.FromVector(down);
                            return new EulerAngles(angularPosition.Pitch, angularPosition.Roll, yaw);
                        });
            }
        }

        public Ahrs(
            IObservable<AngularVelocity3> angularVelocity,
            IObservable<GForce3> acceleration,
            IObservable<MagneticFluxDensity3> magnetism)
            : this(angularVelocity, acceleration, magnetism, new MagneticFluxDensity3(-1d, -1d, -1d), new MagneticFluxDensity3(1d, 1d, 1d))
        {
        }

        public Ahrs(
            IObservable<AngularVelocity3> angularVelocity,
            IObservable<GForce3> acceleration,
            IObservable<MagneticFluxDensity3> magnetism,
            MagneticFluxDensity3 magnetismMinCalibration,
            MagneticFluxDensity3 magnetismMaxCalibration)
        {
            _angularVelocity = angularVelocity;
            _acceleration = acceleration;
            _magnetism = magnetism;
            _magnetismMinCalibration = magnetismMinCalibration;
            _magnetismMaxCalibration = magnetismMaxCalibration;
        }
    }
}
