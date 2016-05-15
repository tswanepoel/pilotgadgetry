// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides proximity.
    /// </summary>
    public class Srf02ProximitySensor : IProximitySensor, IDisposable
    {
        private static Lazy<Srf02ProximitySensor> _default = new Lazy<Srf02ProximitySensor>(true);

        private readonly Srf02 _srf02;
        private bool _disposed;

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static Srf02ProximitySensor Default
        {
            get { return _default.Value; }
        }

        /// <summary>
        /// Initializes the proximity sensor.
        /// </summary>
        public Srf02ProximitySensor()
        {
            _srf02 = new Srf02();
        }

        /// <summary>
        /// Initializes the proximity sensor.
        /// </summary>
        /// <param name="slaveAddress"></param>
        public Srf02ProximitySensor(int slaveAddress)
        {
            _srf02 = new Srf02(slaveAddress);
        }

        /// <summary>
        /// Returns proximity.
        /// </summary>
        /// <returns>The distance, in meters.</returns>
        public double Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02ProximitySensor));
            }

            BeginRead();
            return EndRead();
        }

        /// <summary>
        /// Starts reading proximity data.
        /// </summary>
        public void BeginRead()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02ProximitySensor));
            }

            _srf02.BeginRead();
        }

        /// <summary>
        /// Completes reading proximity data.
        /// </summary>
        /// <returns>The distance, in meters.</returns>
        public double EndRead()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02ProximitySensor));
            }

            ushort value = _srf02.EndRead();
            double distance = value / 1000000d / 2d * 340.29d/*speed of sound at sea level*/;

            return distance < 0.16d ? 0d : distance;
        }

        /// <summary>
        /// Releases resources used by the proximity sensor.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// Releases resources used by the proximity sensor.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _srf02.Dispose();
            }

            _disposed = true;
        }
    }
}
