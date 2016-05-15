// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;
    using Windows.Devices.I2c;

    /// <summary>
    /// Provides an inter-integrated circuit (I2C) interface to L3GD20H.
    /// </summary>
    /// <see href="https://www.pololu.com/file/download/L3GD20.pdf?file_id=0J563" />
    public class L3gd20h: IDisposable
    {
        /// <summary>
        /// The default I2C slave address.
        /// </summary>
        public const int HighAddress = 0x6b/*0b1101011*/;

        /// <summary>
        /// The alternate I2C slave address.
        /// </summary>
        public const int LowAddress = 0x6a/*0b1101010*/;
        
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
        /// Initializes the I2C interface using the default slave address to L3GD20H.
        /// </summary>
        public L3gd20h()
            : this(HighAddress)
        {
        }
        
        /// <summary>
        /// Initializes the I2C interface to L3GD20H.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address.</param>
        public L3gd20h(int slaveAddress)
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
        /// Writes the value for the CTRL_REG1 register.
        /// </summary>
        /// <param name="outputDataRate">The output data rate for raw anglar rate data.</param>
        /// <param name="enableZ">The value indicating whether the Z-axis is enabled.</param>
        /// <param name="enableY">The value indicating whether the Y-axis is enabled.</param>
        /// <param name="enableX">The value indicating whether the X-axis is enabled.</param>
        public void WriteCtrlReg1(L3gd20hOutputDataRate outputDataRate, bool enableZ = true, bool enableY = true, bool enableX = true)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(L3gd20h));
            }
            
            var value =
                ((byte)outputDataRate << 6) |
                (0x2 << 4)/*Bandwidth (BW)*/ |
                (0x1 << 3)/*Power-down (PD) = Normal*/ |
                (enableZ ? 0x1 << 2 : 0x0) |
                (enableY ? 0x1 << 1 : 0x0) |
                (enableX ? 0x1 : 0x0);

            _device.Write(new[] { (byte)0x20/*CTRL_REG1*/, (byte)value });
        }

        /// <summary>
        /// Writes the value for the CTRL_REG4 register.
        /// </summary>
        /// <param name="scale">The full scale for raw angular rate data.</param>
        public void WriteCtrlReg4(L3gd20hFullScale scale)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(L3gd20h));
            }

            _device.Write(new[] { (byte)0x23/*CTRL_REG4*/, (byte)((byte)scale << 4) });
        }

        /// <summary>
        /// Returns 3-axis raw angular rate data.
        /// </summary>
        /// <returns>The 3-axis raw angular rate data.</returns>
        public L3gd20hAngularRateData Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(L3gd20h));
            }
            
            var buffer = new byte[6];
            _device.WriteRead(new[] { (byte)(0x28/*OUT_X_L*/ | 0x80/*auto-increment*/) }, buffer);

            return new L3gd20hAngularRateData(
                x: (short)(buffer[0] | (buffer[1] << 8)),
                y: (short)(buffer[2] | (buffer[3] << 8)),
                z: (short)(buffer[4] | (buffer[5] << 8)));
        }

        /// <summary>
        /// Releases resources used by the I2C interface to L3GD20H.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the I2C interface to L3GD20H.
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
