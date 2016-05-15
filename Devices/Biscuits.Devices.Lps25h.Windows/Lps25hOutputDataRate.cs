// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the output data rates for raw temperature and pressure data from LPS25H.
    /// </summary>
    public enum Lps25hOutputDataRate
    {
        /// <summary>
        /// Represents once-off output.
        /// </summary>
        OneShot = 0x0,

        /// <summary>
        /// Represents an output data rate of 1Hz.
        /// </summary>
        Rate1Hz = 0x1,

        /// <summary>
        /// Represents an output data rate of 7Hz.
        /// </summary>
        Rate7Hz = 0x2,

        /// <summary>
        /// Represents an output data rate of 12.5Hz.
        /// </summary>
        Rate12Hz = 0x3,

        /// <summary>
        /// Represents an output data rate of 25Hz.
        /// </summary>
        Rate25Hz = 0x4
    }
}
