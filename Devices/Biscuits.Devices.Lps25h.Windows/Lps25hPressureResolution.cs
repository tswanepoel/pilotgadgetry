// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the resolutions for raw pressure data from LPS25H.
    /// </summary>
    public enum Lps25hPressureResolution
    {
        /// <summary>
        /// Represents an internal average of of 8.
        /// </summary>
        Average8 = 0x0,

        /// <summary>
        /// Represents and internal average of 32.
        /// </summary>
        Average32 = 0x1,

        /// <summary>
        /// Represents an internal average of 128.
        /// </summary>
        Average128 = 0x2,

        /// <summary>
        /// Represents and internal average of 512.
        /// </summary>
        Average512 = 0x3
    }
}
