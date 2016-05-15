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
        private readonly Srf02 _srf02;
        private bool _disposed;
        private static Lazy<Srf02ProximitySensor> _default = new Lazy<Srf02ProximitySensor>(true);

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
        /// <returns>The distance in meters.</returns>
        public double Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02ProximitySensor));
            }

            BeginRead();
            return EndRead();
        }

        public void BeginRead()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02ProximitySensor));
            }

            _srf02.BeginRead();
        }

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
        /// Disposes the proximity sensor.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes the proximity sensor.
        /// </summary>
        /// <param name="disposing">The value indicating whether the proximity sensor is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _srf02.Dispose();
                _disposed = true;
            }
        }
    }
}
