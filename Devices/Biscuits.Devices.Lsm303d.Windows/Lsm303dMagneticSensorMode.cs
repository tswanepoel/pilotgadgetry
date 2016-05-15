// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Defines magnetic sensor modes for LSM303D.
    /// </summary>
    public enum Lsm303dMagneticSensorMode
    {
        /// <summary>
        /// Represents continuous-conversion mode.
        /// </summary>
        ContinuousConversion = 0x0,

        /// <summary>
        /// Represents single-conversion mode.
        /// </summary>
        SingleConversion = 0x1,

        /// <summary>
        /// Represents power-down mode.
        /// </summary>
        PowerDown1 = 0x2,

        /// <summary>
        /// Represents power-down mode.
        /// </summary>
        PowerDown2 = 0x3
    }
}
