// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the output data rates for raw angular rate data from L3GD20H.
    /// </summary>
    public enum L3gd20hOutputDataRate
    {
        /// <summary>
        /// Represents an output data rate of 95Hz.
        /// </summary>
        Rate95Hz = 0x0,

        /// <summary>
        /// Represents an output data rate of 190Hz.
        /// </summary>
        Rate190Hz = 0x1,

        /// <summary>
        /// Represents an output data rate of 380Hz.
        /// </summary>
        Rate380Hz = 0x2,

        /// <summary>
        /// Represents an output data rate of 760Hz.
        /// </summary>
        Rate760Hz = 0x3
    }
}
