// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;
    using Windows.Devices.I2c;

    /// <summary>
    /// Provides an inter-integrated circuit (I2C) interface to LPS25H.
    /// </summary>
    /// <see href="https://www.pololu.com/file/download/LPS25H.pdf?file_id=0J761" />
    public class Lps25h : IDisposable
    {
        /// <summary>
        /// The default I2C slave address.
        /// </summary>
        public const int HighAddress = 0x5d/*0b1011101*/;

        /// <summary>
        /// The alternate I2C slave address.
        /// </summary>
        public const int LowAddress = 0x5c/*0b1011100*/;

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
        /// Initializes the I2C interface using the default slave address to LPS25H.
        /// </summary>
        public Lps25h()
            : this(HighAddress)
        {
        }

        /// <summary>
        /// Initializes the I2C interface to LPS25H.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address.</param>
        public Lps25h(int slaveAddress)
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
        /// Writes the value for the CRTL_REG1 register.
        /// </summary>
        /// <param name="outputDataRate">The output data rate for raw temperature and pressure data.</param>
        /// <param name="powerDown">The value indicating whether to enter power-down mode.</param>
        public void WriteCtrlReg1(Lps25hOutputDataRate outputDataRate, bool powerDown = false)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25hBarometer));
            }
            
            var value =
                (powerDown ? 0x0 : 0x80) |
                ((byte)outputDataRate << 4);

            _device.Write(new[] { (byte)0x20/*CTRL_REG1*/, (byte)value });
        }

        /// <summary>
        /// Writes the value for the RES_CONF register.
        /// </summary>
        /// <param name="pressureResolution">The resolution for raw pressure data.</param>
        /// <param name="temperatureResolution">The resolution for raw pressure data.</param>
        public void WriteResConf(Lps25hPressureResolution pressureResolution, Lps25hTemperatureResolution temperatureResolution)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25h));
            }

            var value = ((byte)pressureResolution << 2) | ((byte)temperatureResolution << 2);
            _device.Write(new[] { (byte)0x10/*CONF_RES*/, (byte)value });
        }

        /// <summary>
        /// Returns raw pressure data.
        /// </summary>
        /// <returns>Raw pressure data.</returns>
        public int ReadPressureData()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25h));
            }
            
            var buffer = new byte[3];
            _device.WriteRead(new[] { (byte)(0x28/*PRESS_OUT_XL*/ | 0x80/*auto-increment*/) }, buffer);

            return (buffer[0] | (buffer[1] << 8) | (buffer[2] << 16));
        }

        /// <summary>
        /// Returns raw temperature data.
        /// </summary>
        /// <returns>Raw temperature data.</returns>
        public short ReadTemperatureData()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25h));
            }
            
            var buffer = new byte[2];
            _device.WriteRead(new[] { (byte)(0x2b/*TEMP_OUT_L*/ | 0x80/*auto-increment*/) }, buffer);

            return (short)(buffer[0] | (buffer[1] << 8));
        }

        /// <summary>
        /// Releases resources used by the I2C interface to LPS25H.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the I2C interface to LPS25H.
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
