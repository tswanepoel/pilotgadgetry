// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the full scales for raw anguar rate data from L3GD20H in degrees-per-second (dps).
    /// </summary>
    public enum L3gd20hFullScale
    {
        /// <summary>
        /// Represents a full scale of 250 degrees-per-second (dps).
        /// </summary>
        Scale250dps = 0x0,

        /// <summary>
        /// Represents a full scale of 500 degrees-per-second (dps).
        /// </summary>
        Scale500dps = 0x1,

        /// <summary>
        /// Represents a full scale of 1000 degrees-per-second (dps).
        /// </summary>
        Scale1000dps = 0x2,

        /// <summary>
        /// Represents a full scale of 2000 degrees-per-second (dps).
        /// </summary>
        Scale2000dps = 0x3
    }
}
