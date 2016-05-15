// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides pressure.
    /// </summary>
    public class Lps25hBarometer : IBarometer, IDisposable
    {
        private static Lazy<Lps25hBarometer> _default = new Lazy<Lps25hBarometer>(CreateDefault, true);

        private readonly Lps25h _lps25h;
        private bool _disposed;

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static Lps25hBarometer Default
        {
            get { return _default.Value; }
        }

        /// <summary>
        /// Initializes the barometer.
        /// </summary>
        public Lps25hBarometer()
        {
            _lps25h = new Lps25h();
        }

        /// <summary>
        /// Initializes the barometer.
        /// </summary>
        /// <param name="slaveAddress"></param>
        public Lps25hBarometer(int slaveAddress)
        {
            _lps25h = new Lps25h(slaveAddress);
        }

        private static Lps25hBarometer CreateDefault()
        {
            var barometer = new Lps25hBarometer();
            barometer.Initialize();

            return barometer;
        }

        /// <summary>
        /// Configures the default output data rate of the barometer.
        /// </summary>
        public void Initialize()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25hBarometer));
            }

            _lps25h.WriteCtrlReg1(Lps25hOutputDataRate.Rate25Hz);
            _lps25h.WriteResConf(Lps25hPressureResolution.Average8, Lps25hTemperatureResolution.Average8);
        }
        
        /// <summary>
        /// Returns pressure.
        /// </summary>
        /// <returns>The pressure in Pascal (Pa).</returns>
        public double Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lps25hBarometer));
            }

            int value = _lps25h.ReadPressureData();
            return value / 40.96d;
        }

        /// <summary>
        /// Releases resources used by the barometer.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the barometer.
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
