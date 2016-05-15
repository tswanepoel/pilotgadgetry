// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the full scales for raw magnetic data from LSM303D in guass (G).
    /// </summary>
    public enum Lsm303dMagneticFullScale
    {
        /// <summary>
        /// Represents a full scale of 2G.
        /// </summary>
        Scale2G = 0x0,

        /// <summary>
        /// Represents a full scale of 4G.
        /// </summary>
        Scale4G = 0x1,

        /// <summary>
        /// Represents a full scale of 8G.
        /// </summary>
        Scale8G = 0x2,

        /// <summary>
        /// Represents a full scale of 12G.
        /// </summary>
        Scale12G = 0x3
    }
}
