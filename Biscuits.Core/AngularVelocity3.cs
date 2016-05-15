// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System.Diagnostics;

    /// <summary>
    /// Represents 3-axis rotation speed in degrees-per-second (dps). 
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}, Z={Z}, Magnitude={Magnitude}")]
    public struct AngularVelocity3
    {
        private Vector3 _value;

        /// <summary>
        /// Gets the speed of rotation around the X-axis, in degrees-per-second (dps).
        /// </summary>
        public double X
        {
            get { return _value.X; }
        }

        /// <summary>
        /// Gets the speed of rotation around the Y-axis, in degrees-per-second (dps).
        /// </summary>
        public double Y
        {
            get { return _value.Y; }
        }

        /// <summary>
        /// Gets the speed of rotation around the Z-axis, in degrees-per-second (dps).
        /// </summary>
        public double Z
        {
            get { return _value.Z; }
        }

        public double Magnitude
        {
            get { return _value.Magnitude; }
        }

        /// <summary>
        /// Initializes the 3-axis rotation speed.
        /// </summary>
        /// <param name="x">The speed of rotation around the X-axis, in degrees-per-second (dps).</param>
        /// <param name="y">The speed of rotation around the Y-axis, in degrees-per-second (dps).</param>
        /// <param name="z">The speed of rotation around the Z-axis, in degrees-per-second (dps).</param>
        public AngularVelocity3(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        private AngularVelocity3(Vector3 value)
        {
            _value = value;
        }

        public static AngularVelocity3 operator +(AngularVelocity3 left, AngularVelocity3 right)
        {
            return new AngularVelocity3(left._value + right._value);
        }

        public static AngularVelocity3 operator -(AngularVelocity3 left, AngularVelocity3 right)
        {
            return new AngularVelocity3(left._value - right._value);
        }

        public static AngularVelocity3 operator *(AngularVelocity3 self, double value)
        {
            return new AngularVelocity3(self._value * value);
        }

        public static AngularVelocity3 operator /(AngularVelocity3 self, double value)
        {
            return new AngularVelocity3(self._value / value);
        }

        /// <summary>
        /// Returns a 3-axis unit vector that represents the direction of rotation.
        /// </summary>
        /// <returns>The 3-axis unit vector.</returns>
        public Vector3 Normalize()
        {
            return _value.Normalize();
        }
    }
}
