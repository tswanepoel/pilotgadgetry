// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;
    using Windows.Devices.I2c;

    /// <summary>
    /// Provides an inter-integrated curcuit (I2C) interface to PCA9685.
    /// </summary>
    /// <see href="https://www.adafruit.com/datasheets/PCA9685.pdf" />
    public class Pca9685 : IDisposable
    {
        /// <summary>
        /// The default I2C slave address.
        /// </summary>
        public const int DefaultAddress = 0x40/*0b1000000*/;

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
        /// Initializes the I2C interface using the default slave address to PCA9685.
        /// </summary>
        public Pca9685()
            : this(DefaultAddress)
        {
        }

        /// <summary>
        /// Initializes the I2C interface to PCA9685.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address.</param>
        public Pca9685(int slaveAddress)
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
        /// Sets the frequency for all outputs.
        /// </summary>
        /// <param name="frequency">The number of pulses per second (Hz).</param>
        public void SetFrequency(int frequency)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685));
            }
            
            _device.Write(new[] { (byte)0x0/*MODE1*/, (byte)0x10/*SLEEP = 1 (Low Power Mode)*/ });

            int prescale = (int)Math.Round(25000000f / (4096f * frequency), MidpointRounding.AwayFromZero) - 1;
            _device.Write(new[] { (byte)0xfe/*PRE_SCALE*/, (byte)prescale });

            _device.Write(new[] { (byte)0x0/*MODE1*/, (byte)0x0/*SLEEP = 0 (Normal Mode)*/ });

            const int ticksPerMicrosecond = (int)(TimeSpan.TicksPerMillisecond / 1000);

            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            while (sw.ElapsedTicks < 5 * ticksPerMicrosecond) { }
            
            _device.Write(new[] { (byte)0x0/*MODE1*/, (byte)0xa0/*RESTART = 1 (Enabled), AI (auto-increment) = 1 (Enabled)*/ });

            var buffer = new byte[1];
            _device.WriteRead(new[] { (byte)0xfe/*PRE_SCALE*/ }, buffer);
        }

        /// <summary>
        /// Sets the duty cycle for pulses generated at the given pin number.
        /// </summary>
        /// <remarks>
        /// For exampe, given frequency set at 50Hz (20ms cycles), a value of 0 for high and a value of 102 for low would yield duty cycles from approximately 0ms to 0.5ms.
        /// </remarks>
        /// <param name="number">The pin number to set.</param>
        /// <param name="high">The cyclic position at which pulses must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low">The cyclic position at which pulses must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        public void SetValue(int number, int high, int low)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685));
            }

            if (number < 0 || number > 15)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            if (high < 0 || high > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high));
            }

            if (low < 0 || low > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low));
            }

            if (low < high)
            {
                throw new ArgumentException(nameof(low));
            }

            int register = 0x6/*LED0_ON_L*/ + 4 * number;
            _device.Write(new[] { (byte)(register), (byte)(0xff & high), (byte)(0xf & (high >> 8)), (byte)(0xff & low), (byte)(0xf & (low >> 8)) });
        }

        /// <summary>
        /// Sets the duty cycle for pulses generated at the first pin.
        /// </summary>
        /// <remarks>
        /// For exampe, given frequency set at 50Hz (20ms cycles), a value of 0 for high and a value of 102 for low would yield duty cycles from approximately 0ms to 0.5ms.
        /// </remarks>
        /// <param name="high1">The cyclic position at which pulses must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low1">The cyclic position at which pulses must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        public void SetValue(int high1, int low1)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685));
            }

            if (high1 < 0 || high1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high1));
            }

            if (low1 < 0 || low1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low1));
            }

            if (low1 < high1)
            {
                throw new ArgumentException(nameof(low1));
            }
            
            const byte led0_on_l = 0x6;

            _device.Write(new byte[] 
            {
                led0_on_l,

                (byte)(0xff & high1),
                (byte)(0xf & (high1 >> 8)),
                (byte)(0xff & low1),
                (byte)(0xf & (low1 >> 8))
            });
        }

        /// <summary>
        /// Sets the duty cycles for pulses generated at the first and second pins.
        /// </summary>
        /// <remarks>
        /// For exampe, given frequency set at 50Hz (20ms cycles), a value of 0 for high and a value of 102 for low would yield duty cycles from approximately 0ms to 0.5ms.
        /// </remarks>
        /// <param name="high1">The cyclic position at which pulses at the first pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low1">The cyclic position at which pulses at the first pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="high2">The cyclic position at which pulses at the second pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low2">The cyclic position at which pulses at the second pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        public void SetValue(int high1, int low1, int high2, int low2)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685));
            }

            if (high1 < 0 || high1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high1));
            }

            if (low1 < 0 || low1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low1));
            }

            if (low1 < high1)
            {
                throw new ArgumentException(nameof(low1));
            }

            if (high2 < 0 || high2 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high2));
            }

            if (low2 < 0 || low2 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low2));
            }

            if (low2 < high2)
            {
                throw new ArgumentException(nameof(low2));
            }
            
            const byte led0_on_l = 0x6;

            _device.Write(new byte[] 
            {
                led0_on_l,

                (byte)(0xff & high1),
                (byte)(0xf & (high1 >> 8)),
                (byte)(0xff & low1),
                (byte)(0xf & (low1 >> 8)),
                
                (byte)(0xff & high2),
                (byte)(0xf & (high2 >> 8)),
                (byte)(0xff & low2),
                (byte)(0xf & (low2 >> 8)),
            });
        }

        /// <summary>
        /// Sets the duty cycles for pulses generated at the first, second and third pins.
        /// </summary>
        /// <remarks>
        /// For exampe, given frequency set at 50Hz (20ms cycles), a value of 0 for high and a value of 102 for low would yield duty cycles from approximately 0ms to 0.5ms.
        /// </remarks>
        /// <param name="high1">The cyclic position at which pulses at the first pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low1">The cyclic position at which pulses at the first pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="high2">The cyclic position at which pulses at the second pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low2">The cyclic position at which pulses at the second pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="high3">The cyclic position at which pulses at the third pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low3">The cyclic position at which pulses at the third pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        public void SetValue(int high1, int low1, int high2, int low2, int high3, int low3)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685));
            }

            if (high1 < 0 || high1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high1));
            }

            if (low1 < 0 || low1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low1));
            }

            if (low1 < high1)
            {
                throw new ArgumentException(nameof(low1));
            }

            if (high2 < 0 || high2 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high2));
            }

            if (low2 < 0 || low2 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low2));
            }

            if (low2 < high2)
            {
                throw new ArgumentException(nameof(low2));
            }

            if (high3 < 0 || high3 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high3));
            }

            if (low3 < 0 || low3 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low3));
            }

            if (low3 < high3)
            {
                throw new ArgumentException(nameof(low3));
            }
            
            const byte led0_on_l = 0x6;

            _device.Write(new byte[] 
            {
                led0_on_l,

                (byte)(0xff & high1),
                (byte)(0xf & (high1 >> 8)),
                (byte)(0xff & low1),
                (byte)(0xf & (low1 >> 8)),
                
                (byte)(0xff & high2),
                (byte)(0xf & (high2 >> 8)),
                (byte)(0xff & low2),
                (byte)(0xf & (low2 >> 8)),

                (byte)(0xff & high3),
                (byte)(0xf & (high3 >> 8)),
                (byte)(0xff & low3),
                (byte)(0xf & (low3 >> 8))
            });
        }

        /// <summary>
        /// Sets the duty cycles for pulses generated at the first, second, third and fourth pins.
        /// </summary>
        /// <remarks>
        /// For exampe, given frequency set at 50Hz (20ms cycles), a value of 0 for high and a value of 102 for low would yield duty cycles from approximately 0ms to 0.5ms.
        /// </remarks>
        /// <param name="high1">The cyclic position at which pulses at the first pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low1">The cyclic position at which pulses at the first pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="high2">The cyclic position at which pulses at the second pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low2">The cyclic position at which pulses at the second pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="high3">The cyclic position at which pulses at the third pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low3">The cyclic position at which pulses at the third pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="high4">The cyclic position at which pulses at the fourth pin must transition to high, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        /// <param name="low4">The cyclic position at which pulses at the fourth pin must transition to low, specified as a value from 0 (at beginning of cycle) to 4095 (at end of cycle).</param>
        public void SetValue(int high1, int low1, int high2, int low2, int high3, int low3, int high4, int low4)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685));
            }

            if (high1 < 0 || high1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high1));
            }

            if (low1 < 0 || low1 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low1));
            }

            if (low1 < high1)
            {
                throw new ArgumentException(nameof(low1));
            }

            if (high2 < 0 || high2 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high2));
            }

            if (low2 < 0 || low2 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low2));
            }

            if (low2 < high2)
            {
                throw new ArgumentException(nameof(low2));
            }

            if (high3 < 0 || high3 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high3));
            }

            if (low3 < 0 || low3 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low3));
            }

            if (low3 < high3)
            {
                throw new ArgumentException(nameof(low3));
            }

            if (high4 < 0 || high4 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(high4));
            }

            if (low4 < 0 || low4 > 4095)
            {
                throw new ArgumentOutOfRangeException(nameof(low4));
            }

            if (low4 < high4)
            {
                throw new ArgumentException(nameof(low4));
            }

            const byte led0_on_l = 0x6;

            _device.Write(new byte[] 
            {
                led0_on_l,

                (byte)(0xff & high1),
                (byte)(0xf & (high1 >> 8)),
                (byte)(0xff & low1),
                (byte)(0xf & (low1 >> 8)),
                
                (byte)(0xff & high2),
                (byte)(0xf & (high2 >> 8)),
                (byte)(0xff & low2),
                (byte)(0xf & (low2 >> 8)),

                (byte)(0xff & high3),
                (byte)(0xf & (high3 >> 8)),
                (byte)(0xff & low3),
                (byte)(0xf & (low3 >> 8)),

                (byte)(0xff & high4),
                (byte)(0xf & (high4 >> 8)),
                (byte)(0xff & low4),
                (byte)(0xf & (low4 >> 8))
            });
        }

        /// <summary>
        /// Releases resources used by the I2C interface to PCA9685.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the I2C interface to PCA9685.
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
