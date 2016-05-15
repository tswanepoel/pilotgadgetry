// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines the magnetic resolution selections for LSM303D.
    /// </summary>
    public enum Lsm303dMagneticResolution
    {
        /// <summary>
        /// Represents low resolution.
        /// </summary>
        Low = 0x0,

        /// <summary>
        /// Represents high resolution.
        /// </summary>
        High = 0x3
    }
}
