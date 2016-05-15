// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Represents 3-axis raw magnetic data from LSM303D.
    /// </summary>
    public struct Lsm303dMagneticData
    {
        private readonly short _x;
        private readonly short _y;
        private readonly short _z;

        /// <summary>
        /// Gets raw magnetic data for the X-axis.
        /// </summary>
        public short X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets raw magnetic data for the Y-axis.
        /// </summary>
        public short Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets raw magnetic data for the Z-axis.
        /// </summary>
        public short Z
        {
            get { return _z; }
        }

        /// <summary>
        /// Initializes the 3-axis raw magnetic data.
        /// </summary>
        /// <param name="x">The raw magnetic data for the X-axis.</param>
        /// <param name="y">The raw magnetic data for the Y-axis.</param>
        /// <param name="z">The raw magnetic data for the Z-axis.</param>
        public Lsm303dMagneticData(short x, short y, short z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
