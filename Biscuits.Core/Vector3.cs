// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("X={X}, Y={Y}, Z={Z}, Magnitude={Magnitude}")]
    public struct Vector3
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        private readonly double _magnitude;

        public double X
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
        }

        public double Z
        {
            get { return _z; }
        }

        public double Magnitude
        {
            get { return _magnitude; }
        }

        public Vector3(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
            _magnitude = Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }

        public Vector3 Normalize()
        {
            if (_magnitude == 0d)
            {
                return new Vector3(_x, _y, _z);
            }

            return new Vector3(
                x: _x / _magnitude,
                y: _y / _magnitude,
                z: _z / _magnitude);
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(
                x: left._x + right._x,
                y: left._y + right._y,
                z: left._z + right._z);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(
                x: left._x - right._x,
                y: left._y - right._y,
                z: left._z - right._z);
        }

        public static Vector3 operator *(Vector3 self, double value)
        {
            return new Vector3(
                x: self._x * value,
                y: self._y * value,
                z: self._z * value);
        }

        public static Vector3 operator /(Vector3 self, double value)
        {
            return new Vector3(
                x: self._x / value,
                y: self._y / value,
                z: self._z / value);
        }

        public double DotProduct(Vector3 value)
        {
            return _x * value._x + _y * value._y + _z * value._z;
        }

        public Vector3 CrossProduct(Vector3 value)
        {
            return new Vector3(
                x: _y * value._z - _z * value._y,
                y: _z * value._x - _x * value._z,
                z: _x * value._y - _y * value._x);
        }

        public Vector3 Scale(double scale)
        {
            return new Vector3(
                x: _x / _magnitude * scale,
                y: _y / _magnitude * scale,
                z: _z / _magnitude * scale);
        }
    }
}
