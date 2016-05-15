// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;
    using Windows.Devices.I2c;

    /// <summary>
    /// Provides an inter-integrated circuit (I2C) interface to LSM303D.
    /// </summary>
    /// <see href="https://www.pololu.com/file/download/LSM303D.pdf?file_id=0J703" />
    public class Lsm303d
    {
        /// <summary>
        /// The default I2C slave address.
        /// </summary>
        public const int HighAddress = 0x1d/*0b0011101*/;

        /// <summary>
        /// The alternate I2C slave address.
        /// </summary>
        public const int LowAddress = 0x1c/*0b0011100*/;

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
        /// Initializes the I2C interface using the default slave address to LSM303D.
        /// </summary>
        public Lsm303d()
            : this(HighAddress)
        {
        }

        /// <summary>
        /// Initializes the I2C interface to LSM303D.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address.</param>
        public Lsm303d(int slaveAddress)
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
        /// <param name="outputDataRate">The output data rate for raw acceleratino data.</param>
        /// <param name="enableZ">The value indicating whether the Z-axis is enabled.</param>
        /// <param name="enableY">The value indicating whether the Y-axis is enabled.</param>
        /// <param name="enableX">The value indicating whether the X-axis is enabled.</param>
        public void WriteCtrlReg1(Lsm303dAccelerationOutputDataRate outputDataRate, bool enableZ = true, bool enableY = true, bool enableX = true)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303d));
            }

            var value =
                ((byte)outputDataRate << 4) |
                (enableZ ? 0x1 << 2 : 0x0) |
                (enableY ? 0x1 << 1 : 0x0) |
                (enableX ? 0x1 : 0x0);

            _device.Write(new[] { (byte)0x20/*CTRL_REG1*/, (byte)value });
        }

        /// <summary>
        /// Writes the value for the CTRL_REG2 register.
        /// </summary>
        /// <param name="bandwidth">The acceleration anti-alias filter bandwidth.</param>
        /// <param name="scale">The full scale for raw acceleration data.</param>
        public void WriteCtrlReg2(Lsm303dAccelerometerAntiAliasFilterBandwidth bandwidth, Lsm303dAccelerometerFullScale scale)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303d));
            }
            
            var value =
                ((byte)bandwidth << 6) |
                ((byte)scale << 3);
            
            _device.Write(new[] { (byte)0x21/*CTRL_REG2*/, (byte)value });
        }

        /// <summary>
        /// Writes the value for the CTRL_REG5 register.
        /// </summary>
        /// <param name="magneticResolution">The magnetic resolution selection.</param>
        /// <param name="outputDataRate">The output data rate for raw magnetic data.</param>
        /// <param name="enableTemperature">The value indicating whether the thermometer is enabled.</param>
        public void WriteCtrlReg5(Lsm303dMagneticResolution magneticResolution, Lsm303dMagneticOutputDataRate outputDataRate, bool enableTemperature = false)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303d));
            }

            var value =
                (enableTemperature ? 0x8 : 0x0) |
                ((byte)magneticResolution << 6) |
                ((byte)outputDataRate << 2);

            _device.Write(new[] { (byte)0x24/*CTRL_REG5*/, (byte)value });
        }

        /// <summary>
        /// Writes the value for the CTRL_REG6 register.
        /// </summary>
        /// <param name="scale">The full scale for raw magnetic data.</param>
        public void WriteCtrlReg6(Lsm303dMagneticFullScale scale)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303d));
            }
            
            _device.Write(new[] { (byte)0x25/*CTRL_REG6*/, (byte)((byte)scale << 5) });
        }

        /// <summary>
        /// Writes the value for the CTRL_REG7 register.
        /// </summary>
        /// <param name="mode">The magnetic sensor mode selection.</param>
        public void WriteCtrlReg7(Lsm303dMagneticSensorMode mode)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303d));
            }
            
            _device.Write(new[] { (byte)0x26/*CTRL_REG7*/, (byte)mode });
        }

        /// <summary>
        /// Returns 3-axis raw acceleration data.
        /// </summary>
        /// <returns>The 3-axis raw acceleration data.</returns>
        public Lsm303dAccelerationData ReadAccelerationData()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303d));
            }
            
            var buffer = new byte[6];
            _device.WriteRead(new[] { (byte)(0x28/*OUT_X_L_A*/ | 0x80/*auto-increment*/) }, buffer);
            
            return new Lsm303dAccelerationData(
                x: (short)(buffer[0] | (buffer[1] << 8)),
                y: (short)(buffer[2] | (buffer[3] << 8)),
                z: (short)(buffer[4] | (buffer[5] << 8)));
        }

        /// <summary>
        /// Returns 3-axis raw magnetic data.
        /// </summary>
        /// <returns>The 3-axis raw magnetic data.</returns>
        public Lsm303dMagneticData ReadMagneticData()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303d));
            }
            
            var buffer = new byte[6];
            _device.WriteRead(new[] { (byte)(0x8/*OUT_X_L_M*/ | 0x80/*auto-increment*/) }, buffer);

            return new Lsm303dMagneticData(
                x: (short)(buffer[0] | (buffer[1] << 8)),
                y: (short)(buffer[2] | (buffer[3] << 8)),
                z: (short)(buffer[4] | (buffer[5] << 8)));
        }

        /// <summary>
        /// Releases resources used by the I2C interface to LSM303D.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the I2C interface to LSM303D.
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
