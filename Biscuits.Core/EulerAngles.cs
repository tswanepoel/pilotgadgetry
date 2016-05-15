// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Pitch={Pitch}, Roll={Roll}, Yaw={Yaw}")]
    public struct EulerAngles
    {
        private readonly double _pitch;
        private readonly double _roll;
        private readonly double _yaw;

        public double Pitch
        {
            get { return _pitch; }
        }

        public double Roll
        {
            get { return _roll; }
        }

        public double Yaw
        {
            get { return _yaw; }
        }

        public EulerAngles(double pitch, double roll, double yaw)
        {
            _pitch = pitch;
            _roll = roll;
            _yaw = yaw;
        }

        public static EulerAngles FromVector(Vector3 value)
        {
            return FromVector(value, 0d);
        }

        // http://www.freescale.com/files/sensors/doc/app_note/AN3461.pdf
        public static EulerAngles FromVector(Vector3 value, double miu)
        {
            double sign = value.Z < 0d ? -1d : 1d;
            double pitch = Math.Atan2(value.Y, Math.Sqrt(value.X * value.X + value.Z * value.Z));
            double roll = Math.Atan2(-value.X, sign * Math.Sqrt(value.Z * value.Z + miu * value.X * value.X));

            return new EulerAngles(pitch, roll, 0d);
        }

        public Vector3 ToVector()
        {
            double x = Math.Cos(_pitch) * -Math.Sin(_roll);
            double y = Math.Sin(_pitch);
            double z = Math.Sqrt(1d - x * x - y * y);

            return new Vector3(x, y, z);
        }
    }
}
