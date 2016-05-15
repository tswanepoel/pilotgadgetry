// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the resolutions for raw temperature data from LPS25H.
    /// </summary>
    public enum Lps25hTemperatureResolution
    {
        /// <summary>
        /// Represents an internal average of 8.
        /// </summary>
        Average8 = 0x0,

        /// <summary>
        /// Represents an internal average of 16.
        /// </summary>
        Average16 = 0x1,

        /// <summary>
        /// Represents an internal average of 32.
        /// </summary>
        Average32 = 0x2,

        /// <summary>
        /// Represents an internal average of 64.
        /// </summary>
        Average64 = 0x3
    }
}
