// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices.Pam7q
{
    using System;
    using Windows.Devices.I2c;

    /// <summary>
    /// Provides an inter-integrated circuit (I2C) interface to u-blox PAM-7Q.
    /// </summary>
    public class Pam7q : IDisposable
    {
        /// <summary>
        /// The default I2C slave address.
        /// </summary>
        public const int DefaultAddress = 0x42/*0b100001*/;

        private readonly int _address;
        private I2cDevice _device;
        private bool _disposed;

        /// <summary>
        /// Gets the I2C slave address.
        /// </summary>
        public int Address
        {
            get { return _address; }
        }

        /// <summary>
        /// Initializes the I2C interface using the default slave address to PAM-7Q.
        /// </summary>
        public Pam7q()
            : this(DefaultAddress)
        {
        }

        /// <summary>
        /// Initializes the I2C interface to PAM-7Q.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address.</param>
        public Pam7q(int slaveAddress)
        {
            _address = slaveAddress;
            Initialize();
        }
        
        private void Initialize()
        {
            var connection = new I2cConnectionSettings(_address)
                                 {
                                     BusSpeed = I2cBusSpeed.StandardMode,
                                     SharingMode = I2cSharingMode.Shared
                                 };
            
            I2cController controller = I2cController.GetDefaultAsync().AsTask().Result;
            _device = controller.GetDevice(connection);
        }
        
        /// <summary>
        /// Returns the underlying data stream.
        /// </summary>
        /// <returns>A <c>Pam7qStream</c> that provides raw data from PAM-7Q.</returns>
        public Pam7qStream GetStream()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pam7q));
            }

            return Pam7qStream.Create(this);
        }

        /// <summary>
        /// Reads a sequence of bytes.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset
        /// and (offset + count - 1) replaced by the bytes read from the current source.
        /// </param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested.</returns>
        public virtual int Read(byte[] buffer, int offset, int count)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pam7q));
            }

            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0 || offset >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int length;
            byte[] data;

            do
            {
                while ((length = ReadLengthCore()) == 0) { }
                data = ReadDataCore(Math.Min(length, count));
            }
            while (data.Length == 0);

            Array.Copy(data, 0, buffer, offset, data.Length);
            return data.Length;
        }

        private int ReadLengthCore()
        {
            var buffer = new byte[2];
            _device.WriteRead(new[] { (byte)0xfd/*length*/ }, buffer);

            return (ushort)((buffer[0] << 8) | buffer[1]);
        }

        private byte[] ReadDataCore(int length)
        {
            var buffer = new byte[length];
            _device.WriteRead(new[] { (byte)0xff/*data*/ }, buffer);

            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0xff/*padding*/)
                {
                    var bufferTruncated = new byte[i];
                    Array.Copy(buffer, bufferTruncated, i);

                    return bufferTruncated;
                }
            }

            return buffer;
        }

        /// <summary>
        /// Releases resources used by the I2C interface to PAM-7Q.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the I2C interface to PAM-7Q.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _device.Dispose();
            }

            _disposed = true;
        }
    }
}
