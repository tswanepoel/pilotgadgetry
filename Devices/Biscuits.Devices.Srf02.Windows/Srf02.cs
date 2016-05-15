// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;
    using System.Diagnostics;
    using Windows.Devices.I2c;

    /// <summary>
    /// Provides an I2C interface to SRF02.
    /// </summary>
    public class Srf02 : IDisposable
    {
        /// <summary>
        /// The default I2C slave address.
        /// </summary>
        public const int DefaultAddress = 0x70/*0b1110000*/;

        private readonly int _address;
        private I2cDevice _device;
        private Stopwatch _stopwatch;
        private bool _disposed;

        /// <summary>
        /// Gets the I2C slave address.
        /// </summary>
        public int Address
        {
            get { return _address; }
        }

        /// <summary>
        /// Initializes the I2C interface using the default slave address to SRF02.
        /// </summary>
        public Srf02()
            : this(DefaultAddress)
        {
        }

        /// <summary>
        /// Initializes the I2C interface to SRF02.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address.</param>
        public Srf02(int slaveAddress)
        {
            _address = slaveAddress;
            Initialize();
        }

        private void Initialize()
        {
            var connection = new I2cConnectionSettings(_address)
                                 {
                                     BusSpeed = I2cBusSpeed.FastMode,
                                     SharingMode = I2cSharingMode.Shared
                                 };

            I2cController controller = I2cController.GetDefaultAsync().AsTask().Result;
            _device = controller.GetDevice(connection);
        }

        /// <summary>
        /// Returns raw proximity data.
        /// </summary>
        /// <returns>Raw proximity data.</returns>
        public ushort Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02));
            }

            BeginRead();
            return EndRead();
        }

        public void BeginRead()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02));
            }

            if (_stopwatch != null)
            {
                throw new InvalidOperationException();
            }

            _device.Write(new[] { (byte)0x0/*Command Register*/, (byte)(0x52/*Real Ranging Mode in microseconds*/) });
            _stopwatch = Stopwatch.StartNew();
        }

        public ushort EndRead()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Srf02));
            }
            
            if (_stopwatch == null)
            {
                throw new InvalidOperationException();
            }

            while (_stopwatch.ElapsedTicks < TimeSpan.TicksPerSecond * 0.07m) { }
            
            var buffer = new byte[2];
            _device.WriteRead(new[] { (byte)0x2/*Range High Byte*/ }, buffer);

            _stopwatch = null;
            return (ushort)((buffer[0] << 8) | buffer[1]);
        }

        /// <summary>
        /// Disposes the I2C interface.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposes the I2C interface.
        /// </summary>
        /// <param name="disposing">The value indicating whether the I2C interface is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _device.Dispose();
                _disposed = true;
            }
        }
    }
}
