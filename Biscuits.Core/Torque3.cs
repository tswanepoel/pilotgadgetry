// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System.Diagnostics;

    /// <summary>
    /// Represents 3-axis torque in newton metres (Nm). 
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}, Z={Z}, Magnitude={Magnitude}")]
    public struct Torque3
    {
        private Vector3 _value;

        /// <summary>
        /// Gets the torque on the X-axis, in newton metres (Nm).
        /// </summary>
        public double X
        {
            get { return _value.X; }
        }

        /// <summary>
        /// Gets the torque on the Y-axis, in newton metres (Nm).
        /// </summary>
        public double Y
        {
            get { return _value.Y; }
        }

        /// <summary>
        /// Gets the torque on the Z-axis, in newton metres (Nm).
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
        /// Initializes the 3-axis torque.
        /// </summary>
        /// <param name="x">The torque on the X-axis, in newton metres (Nm).</param>
        /// <param name="y">The torque on the Y-axis, in newton metres (Nm).</param>
        /// <param name="z">The torque on the Z-axis, in newton metres (Nm).</param>
        public Torque3(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        private Torque3(Vector3 value)
        {
            _value = value;
        }

        public static Torque3 operator +(Torque3 left, Torque3 right)
        {
            return new Torque3(left._value + right._value);
        }

        public static Torque3 operator -(Torque3 left, Torque3 right)
        {
            return new Torque3(left._value - right._value);
        }

        public static Torque3 operator *(Torque3 self, double value)
        {
            return new Torque3(self._value * value);
        }

        public static Torque3 operator /(Torque3 self, double value)
        {
            return new Torque3(self._value / value);
        }
    }
}
