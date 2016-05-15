// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Represents 3-axis raw acceleration data from LSM303D.
    /// </summary>
    public struct Lsm303dAccelerationData
    {
        private readonly short _x;
        private readonly short _y;
        private readonly short _z;

        /// <summary>
        /// Gets the raw acceleration data for the X-axis.
        /// </summary>
        public short X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the raw acceleratino data for the Y-axis.
        /// </summary>
        public short Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the raw acceleration data for the Z-axis.
        /// </summary>
        public short Z
        {
            get { return _z; }
        }

        /// <summary>
        /// Initializes the 3-axis acceleratin data.
        /// </summary>
        /// <param name="x">The raw acceleration data for the X-axis.</param>
        /// <param name="y">The raw acceleration data for the Y-axis.</param>
        /// <param name="z">The raw acceleration data for the Z-axis.</param>
        public Lsm303dAccelerationData(short x, short y, short z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
