// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the anti-alias filter bandwidths for raw acceleration data from LSM303D.
    /// </summary>
    public enum Lsm303dAccelerometerAntiAliasFilterBandwidth
    {
        /// <summary>
        /// Represents an anti-alias filter bandwidth of 773Hz.
        /// </summary>
        Bandwidth773Hz = 0x0,

        /// <summary>
        /// Represents an anti-alias filter bandwidth of 194Hz.
        /// </summary>
        Bandwidth194Hz = 0x1,

        /// <summary>
        /// Represents an anti-alias filter bandwidth of 362Hz.
        /// </summary>
        Bandwidth362Hz = 0x2,

        /// <summary>
        /// Represents an anti-alias filter bandwidth of 50Hz.
        /// </summary>
        Bandwidth50Hz = 0x3
    }
}
