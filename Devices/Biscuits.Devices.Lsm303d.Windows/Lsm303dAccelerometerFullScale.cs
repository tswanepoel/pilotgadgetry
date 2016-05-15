// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the full scales for raw accelation data from LSM303D in G-Force (g).
    /// </summary>
    public enum Lsm303dAccelerometerFullScale
    {
        /// <summary>
        /// Represents a full scale of 2g.
        /// </summary>
        Scale2g = 0x0,

        /// <summary>
        /// Represents a full scale of 4g.
        /// </summary>
        Scale4g = 0x1,

        /// <summary>
        /// Represents a full scale of 8g.
        /// </summary>
        Scale6g = 0x2,

        /// <summary>
        /// Represents a full scale of 8g.
        /// </summary>
        Scale8g = 0x3,

        /// <summary>
        /// Represents a full scale of 16g.
        /// </summary>
        Scale16g = 0x4
    }
}
