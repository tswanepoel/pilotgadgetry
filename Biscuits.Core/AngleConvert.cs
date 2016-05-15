// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System;

    public static class AngleConvert
    {
        public static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180d;
        }

        public static double ToDegrees(double radians)
        {
            return radians * 180d / Math.PI;
        }
    }
}
