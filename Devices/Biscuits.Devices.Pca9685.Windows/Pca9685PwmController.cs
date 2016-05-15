// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Represents the PCA9685 pulse-width modulation (PWM) controller.
    /// </summary>
    public class Pca9685PwmController : IPwmController
    {
        private static Lazy<Pca9685PwmController> _default = new Lazy<Pca9685PwmController>(CreateDefault, true);
        
        private const int _scale = 4095;
        private readonly Pca9685 _pca9685;
        private readonly int _frequency;
        private bool _disposed;

        /// <summary>
        /// Gets the frequency of the PWM controller.
        /// </summary>
        public double Frequency
        {
            get { return 400d; }
        }

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static Pca9685PwmController Default
        {
            get { return _default.Value; }
        }
        
        /// <summary>
        /// Initializes the PWM controller.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        public Pca9685PwmController(int frequency)
        {
            _pca9685 = new Pca9685();
            _frequency = frequency;
        }

        /// <summary>
        /// Initializes the PWM controller.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="slaveAddress">The I2C slave address of PCA9685.</param>
        public Pca9685PwmController(int frequency, int slaveAddress)
        {
            _pca9685 = new Pca9685(slaveAddress);
            _frequency = frequency;
        }

        private static Pca9685PwmController CreateDefault()
        {
            var controller = new Pca9685PwmController(400);
            controller.Initialize();

            return controller;
        }

        /// <summary>
        /// Configures the frequency of the PWM controller.
        /// </summary>
        public void Initialize()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685PwmController));
            }

            _pca9685.SetFrequency(_frequency);
        }

        /// <summary>
        /// Sets the duty cycle for pulses generated at the given pin number.
        /// </summary>
        /// <param name="number">The pin number to set.</param>
        /// <param name="high">The duration of time for which pulses remain high.</param>
        public void SetValue(int number, double high)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685PwmController));
            }
            
            double wavelength = 1000d / _frequency;
            int low = Math.Min(Math.Max(Convert.ToInt32(high / wavelength * _scale), 0), _scale);
            
            _pca9685.SetValue(number, 0, low);
        }

        /// <summary>
        /// Sets the duty cycle for pulses generated at the first pin.
        /// </summary>
        /// <param name="high1">The duration of time for which pulses remain high at the first pin.</param>
        public void SetValue(double high1)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685PwmController));
            }

            double wavelength = 1000d / _frequency;
            int low1 = Math.Min(Math.Max(Convert.ToInt32(high1 / wavelength * _scale), 0), _scale);

            _pca9685.SetValue(0, low1);
        }

        /// <summary>
        /// Sets the duty cycles for pulses generated at the first and second pins.
        /// </summary>
        /// <param name="high1">The duration of time for which pulses remain high at the first pin.</param>
        /// <param name="high2">The duration of time for which pulses remain high at the second pin.</param>
        public void SetValue(double high1, double high2)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685PwmController));
            }

            double wavelength = 1000d / _frequency;
            int low1 = Math.Min(Math.Max(Convert.ToInt32(high1 / wavelength * _scale), 0), _scale);
            int low2 = Math.Min(Math.Max(Convert.ToInt32(high2 / wavelength * _scale), 0), _scale);

            _pca9685.SetValue(0, low1, 0, low2);
        }

        /// <summary>
        /// Sets the duty cycles for pulses generated at the first, second and third pins.
        /// </summary>
        /// <param name="high1">The duration of time for which pulses remain high at the first pin.</param>
        /// <param name="high2">The duration of time for which pulses remain high at the second pin.</param>
        /// <param name="high3">The duration of time for which pulses remain high at the third pin.</param>
        public void SetValue(double high1, double high2, double high3)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685PwmController));
            }

            double wavelength = 1000d / _frequency;
            int low1 = Math.Min(Math.Max(Convert.ToInt32(high1 / wavelength * _scale), 0), _scale);
            int low2 = Math.Min(Math.Max(Convert.ToInt32(high2 / wavelength * _scale), 0), _scale);
            int low3 = Math.Min(Math.Max(Convert.ToInt32(high3 / wavelength * _scale), 0), _scale);

            _pca9685.SetValue(0, low1, 0, low2, 0, low3);
        }

        /// <summary>
        /// Sets the duty cycles for pulses generated at the first, second, third and fourth pins.
        /// </summary>
        /// <param name="high1">The duration of time for which pulses remain high at the first pin.</param>
        /// <param name="high2">The duration of time for which pulses remain high at the second pin.</param>
        /// <param name="high3">The duration of time for which pulses remain high at the third pin.</param>
        /// <param name="high4">The duration of time for which pulses remain high at the fourth pin.</param>
        public void SetValue(double high1, double high2, double high3, double high4)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Pca9685PwmController));
            }

            double wavelength = 1000d / _frequency;
            int low1 = Math.Min(Math.Max(Convert.ToInt32(high1 / wavelength * _scale), 0), _scale);
            int low2 = Math.Min(Math.Max(Convert.ToInt32(high2 / wavelength * _scale), 0), _scale);
            int low3 = Math.Min(Math.Max(Convert.ToInt32(high3 / wavelength * _scale), 0), _scale);
            int low4 = Math.Min(Math.Max(Convert.ToInt32(high4 / wavelength * _scale), 0), _scale);

            _pca9685.SetValue(0, low1, 0, low2, 0, low3, 0, low4);
        }

        /// <summary>
        /// Releases resources used by the PWM controller.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the PWM controller.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pca9685.Dispose();
            }

            _disposed = true;
        }
    }
}
