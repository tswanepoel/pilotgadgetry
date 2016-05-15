// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System.Diagnostics;

    /// <summary>
    /// Represents 3-axis rate of change of angular velocity in radians-per-second squared. (rps<sup>2</sup>). 
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}, Z={Z}, Magnitude={Magnitude}")]
    public struct AngularAcceleration3
    {
        private Vector3 _value;

        /// <summary>
        /// Gets the rate of change of angular velocity around the X-axis, in radians-per-second squared (rps<sup>2</sup>).
        /// </summary>
        public double X
        {
            get { return _value.X; }
        }

        /// <summary>
        /// Gets the rate of change of angular velocity around the Y-axis, in radians-per-second squared (rps<sup>2</sup>).
        /// </summary>
        public double Y
        {
            get { return _value.Y; }
        }

        /// <summary>
        /// Gets the rate of change of angular velocity around the Z-axis, in radians-per-second squared (rps<sup>2</sup>).
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
        /// Initializes the 3-axis rate of change of angular velocity.
        /// </summary>
        /// <param name="x">The rate of change of angular velocity around the X-axis, in radians-per-second squared (rps<sup>2</sup>).</param>
        /// <param name="y">The rate of change of angular velocity around the Y-axis, in radians-per-second squared (rps<sup>2</sup>).</param>
        /// <param name="z">The rate of change of angular velocity around the Z-axis, in radians-per-second squared (rps<sup>2</sup>).</param>
        public AngularAcceleration3(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        private AngularAcceleration3(Vector3 value)
        {
            _value = value;
        }

        public static AngularAcceleration3 operator +(AngularAcceleration3 left, AngularAcceleration3 right)
        {
            return new AngularAcceleration3(left._value + right._value);
        }

        public static AngularAcceleration3 operator -(AngularAcceleration3 left, AngularAcceleration3 right)
        {
            return new AngularAcceleration3(left._value - right._value);
        }

        public static AngularAcceleration3 operator *(AngularAcceleration3 self, double value)
        {
            return new AngularAcceleration3(self._value * value);
        }

        public static AngularAcceleration3 operator /(AngularAcceleration3 self, double value)
        {
            return new AngularAcceleration3(self._value / value);
        }
    }
}
