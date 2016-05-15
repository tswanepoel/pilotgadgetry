// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides 3-axis acceleration force.
    /// </summary>
    public class Lsm303dAccelerometer : IAccelerometer3, IDisposable
    {
        private static Lazy<Lsm303dAccelerometer> _default = new Lazy<Lsm303dAccelerometer>(CreateDefault, true);

        private const double _scale = 4d;
        private readonly Lsm303d _lsm303d;
        private bool _disposed;

        /// <summary>
        /// Gets the frequency of the accelerometer.
        /// </summary>
        public double Frequency
        {
            get { return 400d; }
        }

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static Lsm303dAccelerometer Default
        {
            get { return _default.Value; }
        }

        /// <summary>
        /// Initializes the accelerometer.
        /// </summary>
        public Lsm303dAccelerometer()
        {
            _lsm303d = new Lsm303d();
        }

        /// <summary>
        /// Initializes the accelerometer.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address of LSM303D.</param>
        public Lsm303dAccelerometer(int slaveAddress)
        {
            _lsm303d = new Lsm303d(slaveAddress);
        }

        private static Lsm303dAccelerometer CreateDefault()
        {
            var accelerometer = new Lsm303dAccelerometer();
            accelerometer.Initialize();

            return accelerometer;
        }

        /// <summary>
        /// Configures the default output data rate, anti-alias filter bandwidth and full scale of the accelerometer.
        /// </summary>
        public virtual void Initialize()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303dAccelerometer));
            }

            _lsm303d.WriteCtrlReg1(Lsm303dAccelerationOutputDataRate.Rate400Hz);
            _lsm303d.WriteCtrlReg2(Lsm303dAccelerometerAntiAliasFilterBandwidth.Bandwidth194Hz, Lsm303dAccelerometerFullScale.Scale4g);
        }

        /// <summary>
        /// Returns 3-axis acceleration force.
        /// </summary>
        /// <returns>The 3-axis acceleration force in G-Force (g).</returns>
        public virtual GForce3 Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303dAccelerometer));
            }

            Lsm303dAccelerationData data = _lsm303d.ReadAccelerationData();

            return new GForce3(
                x: data.X / (double)short.MaxValue * _scale,
                y: data.Y / (double)short.MaxValue * _scale,
                z: data.Z / (double)short.MaxValue * _scale);
        }

        /// <summary>
        /// Releases resources used by the accelerometer.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the accelerometer.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lsm303d.Dispose();
            }

            _disposed = true;
        }
    }
}
