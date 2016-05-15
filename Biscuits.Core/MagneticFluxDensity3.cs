// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System.Diagnostics;

    /// <summary>
    /// Represents 3-axis magnetic flux density in Gauss (g).
    /// </summary>
    [DebuggerDisplay("X={X}, Y={Y}, Z={Z}, Magnitude={Magnitude}")]
    public struct MagneticFluxDensity3
    {
        private readonly Vector3 _value;

        /// <summary>
        /// Gets the magnetic flux density along the X-axis, in Gauss (g).
        /// </summary>
        public double X
        {
            get { return _value.X; }
        }

        /// <summary>
        /// Gets the magnetic flux density along the Y-axis, in Gauss (g).
        /// </summary>
        public double Y
        {
            get { return _value.Y; }
        }

        /// <summary>
        /// Gets the magnetic flux density along the Z-axis, in Gauss (g).
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
        /// Initializes the 3-axis magnetic flux density.
        /// </summary>
        /// <param name="x">The magnetic flux density along the X-axis, in Gauss (g).</param>
        /// <param name="y">The magnetic flux density along the Y-axis, in Gauss (g).</param>
        /// <param name="z">The magnetic flux density along the Z-axis, in Gauss (g).</param>
        public MagneticFluxDensity3(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        public MagneticFluxDensity3(Vector3 direction, double magnitude)
        {
            _value = direction.Scale(magnitude);
        }

        private MagneticFluxDensity3(Vector3 value)
        {
            _value = value;
        }

        public static MagneticFluxDensity3 operator +(MagneticFluxDensity3 left, MagneticFluxDensity3 right)
        {
            return new MagneticFluxDensity3(left._value + right._value);
        }

        public static MagneticFluxDensity3 operator -(MagneticFluxDensity3 left, MagneticFluxDensity3 right)
        {
            return new MagneticFluxDensity3(left._value - right._value);
        }

        public static MagneticFluxDensity3 operator *(MagneticFluxDensity3 self, double value)
        {
            return new MagneticFluxDensity3(self._value * value);
        }

        public static MagneticFluxDensity3 operator /(MagneticFluxDensity3 self, double value)
        {
            return new MagneticFluxDensity3(self._value / value);
        }

        public Vector3 Normalize()
        {
            return _value.Normalize();
        }
    }
}
