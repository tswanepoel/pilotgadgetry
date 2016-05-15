// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides 3-axis rotation speed.
    /// </summary>
    public class L3gd20hGyroscope : IGyroscope3, IDisposable
    {
        private static Lazy<L3gd20hGyroscope> _default = new Lazy<L3gd20hGyroscope>(CreateDefault, true);

        private const double _scale = 2000d;
        private readonly L3gd20h _l3gd20h;
        private bool _disposed;

        /// <summary>
        /// Gets the frequency of the gyroscope.
        /// </summary>
        public double Frequency
        {
            get { return 760d; }
        }

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static L3gd20hGyroscope Default
        {
            get { return _default.Value; }
        }
        
        /// <summary>
        /// Initializes the gyroscope.
        /// </summary>
        public L3gd20hGyroscope()
        {
            _l3gd20h = new L3gd20h();
        }

        /// <summary>
        /// Initializes the gyroscope.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address of L3GD20H.</param>
        public L3gd20hGyroscope(int slaveAddress)
        {
            _l3gd20h = new L3gd20h(slaveAddress);
        }

        /// <summary>
        /// Configures the output data rate and full scale of the gyroscope.
        /// </summary>
        public virtual void Initialize()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(L3gd20hGyroscope));
            }

            _l3gd20h.WriteCtrlReg1(L3gd20hOutputDataRate.Rate760Hz);
            _l3gd20h.WriteCtrlReg4(L3gd20hFullScale.Scale2000dps);
        }

        /// <summary>
        /// Returns 3-axis rotation speed.
        /// </summary>
        /// <returns>The 3-axis rotation speed in degrees-per-second (dps).</returns>
        public AngularVelocity3 Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(L3gd20hGyroscope));
            }

            L3gd20hAngularRateData data = _l3gd20h.Read();

            return new AngularVelocity3(
                x: (double)data.X / short.MaxValue * _scale,
                y: (double)data.Y / short.MaxValue * _scale,
                z: (double)data.Z / short.MaxValue * _scale);
        }

        private static L3gd20hGyroscope CreateDefault()
        {
            var gyroscope = new L3gd20hGyroscope();
            gyroscope.Initialize();

            return gyroscope;
        }

        /// <summary>
        /// Releases resources used by the gyroscope.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the gyroscope.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _l3gd20h.Dispose();
            }

            _disposed = true;
        }
    }
}
