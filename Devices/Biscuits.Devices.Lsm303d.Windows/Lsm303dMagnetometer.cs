namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Provides 3-axis magnetic flux density.
    /// </summary>
    public class Lsm303dMagnetometer : IMagnetometer3, IDisposable
    {
        private static Lazy<Lsm303dMagnetometer> _default = new Lazy<Lsm303dMagnetometer>(CreateDefault, true);

        private const double _scale = 12d;
        private readonly Lsm303d _lsm303d;
        private bool _disposed;

        /// <summary>
        /// Gets the frequency of the magnetometer.
        /// </summary>
        public double Frequency
        {
            get { return 100d; }
        }

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static Lsm303dMagnetometer Default
        {
            get { return _default.Value; }
        }

        /// <summary>
        /// Initializes the magetometer.
        /// </summary>
        public Lsm303dMagnetometer()
        {
            _lsm303d = new Lsm303d();
        }

        /// <summary>
        /// Initializes the magnetometer.
        /// </summary>
        /// <param name="slaveAddress">The I2C slave address of LSM303D.</param>
        public Lsm303dMagnetometer(int slaveAddress)
        {
            _lsm303d = new Lsm303d(slaveAddress);
        }

        private static Lsm303dMagnetometer CreateDefault()
        {
            var magnometer = new Lsm303dMagnetometer();
            magnometer.Initialize();

            return magnometer;
        }

        /// <summary>
        /// Configures the default magnetic resolution, output data rate, full scale and sensor mode of the magnetometer.
        /// </summary>
        public void Initialize()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303dMagnetometer));
            }

            _lsm303d.WriteCtrlReg5(Lsm303dMagneticResolution.High, Lsm303dMagneticOutputDataRate.Rate100Hz);
            _lsm303d.WriteCtrlReg6(Lsm303dMagneticFullScale.Scale12G);
            _lsm303d.WriteCtrlReg7(Lsm303dMagneticSensorMode.ContinuousConversion);
        }

        /// <summary>
        /// Returns 3-axis magnetic flux density.
        /// </summary>
        /// <returns>The 3-axis magnetic flux density in Guass (G).</returns>
        public MagneticFluxDensity3 Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(Lsm303dMagnetometer));
            }

            Lsm303dMagneticData data = _lsm303d.ReadMagneticData();

            return new MagneticFluxDensity3(
                x: data.X / (double)short.MaxValue * _scale,
                y: data.Y / (double)short.MaxValue * _scale,
                z: data.Z / (double)short.MaxValue * _scale);
        }

        /// <summary>
        /// Releases resources used by the magnetometer.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the magnetometer.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lsm303d.Dispose();
            }

            _disposed = true;
        }
    }
}
