// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the output data rates for raw acceleration data from LSM303D.
    /// </summary>
    public enum Lsm303dAccelerationOutputDataRate
    {
        /// <summary>
        /// Represents power-down mode.
        /// </summary>
        PowerDown = 0x0,

        /// <summary>
        /// Represents an output data rate of 3.125Hz.
        /// </summary>
        Rate3Hz = 0x1,

        /// <summary>
        /// Represents an output data rate of 6.25Hz.
        /// </summary>
        Rate6Hz = 0x2,

        /// <summary>
        /// Represents an output data rate of 12.5Hz.
        /// </summary>
        Rate12Hz = 0x3,

        /// <summary>
        /// Represents an output data rate of 25Hz.
        /// </summary>
        Rate25Hz = 0x4,

        /// <summary>
        /// Represents an output data rate of 50Hz.
        /// </summary>
        Rate50Hz = 0x5,

        /// <summary>
        /// Represents an output data rate of 100Hz.
        /// </summary>
        Rate100Hz = 0x6,

        /// <summary>
        /// Represents an output data rate of 200Hz.
        /// </summary>
        Rate200Hz = 0x7,

        /// <summary>
        /// Represents an output data rate of 400Hz.
        /// </summary>
        Rate400Hz = 0x8,

        /// <summary>
        /// Represents an output data rate of 800Hz.
        /// </summary>
        Rate800Hz = 0x9,

        /// <summary>
        /// Represents an output data rate of 1600Hz.
        /// </summary>
        Rate1600Hz = 0xa
    }
}
