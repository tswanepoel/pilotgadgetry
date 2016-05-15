// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides temperature.
    /// </summary>
    public class Lps25hThermometer : IDisposable
    {
        private static Lazy<Lps25hThermometer> _default = new Lazy<Lps25hThermometer>(CreateDefault, true);

        private readonly Lps25h _lps25h;
        private bool _disposed;

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static Lps25hThermometer Default
        {
            get { return _default.Value; }
        }

        /// <summary>
        /// Initializes the thermometer.
        /// </summary>
        public Lps25hThermometer()
        {
            _lps25h = new Lps25h();
        }

        /// <summary>
        /// Initializes the thermometer.
        /// </summary>
        /// <param name="slaveAddress"></param>
        public Lps25hThermometer(int slaveAddress)
        {
            _lps25h = new Lps25h(slaveAddress);
        }

        private static Lps25hThermometer CreateDefault()
        {
            var thermometer = new Lps25hThermometer();
            thermometer.Initialize();

            return thermometer;
        }

        /// <summary>
        /// Configures the default output data rate of the thermometer.
        /// </summary>
        public void Initialize()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25hThermometer));
            }

            _lps25h.WriteCtrlReg1(Lps25hOutputDataRate.Rate25Hz);
        }
        
        /// <summary>
        /// Returns temperature.
        /// </summary>
        /// <returns>The temperature in Celcius (&#176;C)</returns>
        public double Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25hThermometer));
            }

            int value = _lps25h.ReadTemperatureData();
            return 42.5d + value / 480d;
        }

        /// <summary>
        /// Releases resources used by the thermometer.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the thermometer.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lps25h.Dispose();
            }

            _disposed = true;
        }
    }
}
