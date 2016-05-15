// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System.Diagnostics;

    /// <summary>
    /// Represents 3-axis acceleration force in g-force (g). 
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}, Z={Z}, Magnitude={Magnitude}")]
    public struct GForce3
    {
        private Vector3 _value;

        /// <summary>
        /// Gets the acceleration force along the X-axis, in g-force (g).
        /// </summary>
        public double X
        {
            get { return _value.X; }
        }

        /// <summary>
        /// Gets the acceleration force along the Y-axis, in g-force (g).
        /// </summary>
        public double Y
        {
            get { return _value.Y; }
        }

        /// <summary>
        /// Gets the acceleration force along the Z-axis, in g-force (g).
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
        /// Initializes the 3-axis acceleration force.
        /// </summary>
        /// <param name="x">The acceleration force along the X-axis, in g-force (g).</param>
        /// <param name="y">The acceleration force along the Y-axis, in g-force (g).</param>
        /// <param name="z">The acceleration force along the Z-axis, in g-force (g).</param>
        public GForce3(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        /// <summary>
        /// Initialzes the 3-axis acceleration force.
        /// </summary>
        /// <param name="direction">The direction of the acceleration force.</param>
        /// <param name="magnitude">The magnitude of the acceleration force, in g-force (g).</param>
        public GForce3(Vector3 direction, double magnitude)
        {
            _value = direction.Scale(magnitude);
        }

        private GForce3(Vector3 value)
        {
            _value = value;
        }

        public GForce3 Scale(double magnitude)
        {
            return new GForce3(_value.Scale(magnitude));
        }

        public static GForce3 operator +(GForce3 left, GForce3 right)
        {
            return new GForce3(left._value + right._value);
        }

        public static GForce3 operator -(GForce3 left, GForce3 right)
        {
            return new GForce3(left._value - right._value);
        }

        public static GForce3 operator *(GForce3 self, double value)
        {
            return new GForce3(self._value * value);
        }

        public static GForce3 operator /(GForce3 self, double value)
        {
            return new GForce3(self._value / value);
        }

        public Vector3 Normalize()
        {
            return _value.Normalize();
        }
    }
}
